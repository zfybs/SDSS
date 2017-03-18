using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SDSS.Definitions;
using SDSS.PostProcess;
using SDSS.Project;
using SDSS.Solver;
using SDSS.Models;
using SDSS.Structures;
using SDSS.UIControls;
using SDSS.Utility;
using Timer = System.Timers.Timer;

namespace SDSS.ModelForms
{
    internal partial class MainForm : Form
    {
        #region ---   Fields

        /// <summary> 前处理：参数输入 </summary>
        public virtual ModelBase Model { get; private set; }

        /// <summary> 中间求解器 </summary>
        protected AbaqusSolver _Solver;

        /// <summary> 后处理：提取结果、撰写报告 </summary>
        protected PostProcessor _PostProcessor;

        public AbqWorkingDir WorkingDir { get; private set; }

        public void SetAbqWorkingDir(string workingDir)
        {
            WorkingDir = new AbqWorkingDir(workingDir);
        }

        #endregion

        #region ---   窗口的打开与关闭

        /// <summary> 在其派生窗口类也构造完成后，再次进行界面的设置 </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary> 构造函数 </summary>
        public MainForm(ModelBase sm) : this()
        {
            //
            Model = sm;
            //
        }


        /// <summary> 在其派生窗口类也构造完成后，再次进行界面的设置 </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            // 确保子窗口的菜单栏被隐藏
            foreach (var c in Controls)
            {
                if (c is MenuStrip)
                {
                    (c as MenuStrip).Visible = false;
                }
            }
            //
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_Solver != null && _Solver.State == SolverState.Calculating)
            {
                var res = MessageBox.Show(@"Abaqus正在计算中，是否将其终止？", @"提示", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);
                if (res == DialogResult.Yes)
                {
                    ForceSolverShutDown(null, null);
                }
            }
            //
        }

        /// <summary> 窗口关闭时将窗口所对应的模型数据也关闭 </summary>
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Model = null;
            //
            if (_Solver != null)
            {
                _Solver.Dispose();
                _Solver = null;
            }
            //
            _PostProcessor = null;
        }

        #endregion

        #region ---   模型文件的 保存 与 另存为

        /// <summary> 模型数据所对应的xml文件，如果此<see cref="ModelBase"/>对象不是通过外部文件导入进来的，则此字段的值为 null。 </summary>
        public string ModelFilePath { get; private set; }

        /// <summary> 设置模型数据对应哪一个 xml 文档 </summary>
        /// <param name="mdi">用来限定此方法的调用对象</param>
        /// <param name="modelFilePath"></param>
        public void SetModelFilePath(MainFormMdi mdi, string modelFilePath)
        {
            ModelFilePath = modelFilePath;
        }

        /// <summary> 将模型数据保存到xml文档中 </summary>
        public void SaveModel()
        {
            if (Model != null && !string.IsNullOrEmpty(ModelFilePath))
            {
                StringBuilder sb = new StringBuilder();
                bool succ = sdUtils.ExportToXmlFile(xmlFilePath: ModelFilePath, src: Model,
                    errorMessage: ref sb);
                if (succ)
                {
                    MessageBox.Show(@"模型导出成功", @"提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("模型导出失败\r\n" + sb, @"提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        #endregion

        #region ---   界面的刷新 Refresh_NewModel

        /// <summary> 当窗口所对应的整个 StationModel 发生改变时，刷新整个界面 </summary>
        /// <param name="stationModel"></param>
        public virtual void Refresh_NewModel(ModelBase stationModel)
        {
            // :warning: 将整个模型
            Model = stationModel;
            // 图片框
            RefreshUI_PictureBox(stationModel, null);
        }

        #endregion

        #region ---   PictureBox 绘图操作

        private void modelDrawer1_Resize(object sender, EventArgs e)
        {
            RefreshUI_PictureBox(Model, null);
        }

        private void modelDrawer1_Paint(object sender, PaintEventArgs e)
        {
            RefreshUI_PictureBox(Model, e.Graphics);
        }

        /// <param name="g">其值可赋为 null</param>
        protected void RefreshUI_PictureBox(ModelBase sm, Graphics g)
        {
            // 将新模型进行重新绘制
            if (sm != null)
            {
                SoilStructureGeometry ssg = sm.GetStationGeometry() as SoilStructureGeometry;
                if (ssg != null)
                {
                    modelDrawer1.DrawSoilStructureModel(ssg, g);
                }
            }
        }

        #endregion

        #region ---   Definition的添加与界面处理

        private void button_Profiles_Click(object sender, EventArgs e)
        {
            DefinitionManager<Profile> dm = new DefinitionManager<Profile>(Model.Definitions.Profiles);
            dm.ShowDialog();
            //
            OnSdProfileDefinitionChanged();
        }

        private void button_Materials_Click(object sender, EventArgs e)
        {
            DefinitionManager<Material> dm = new DefinitionManager<Material>(Model.Definitions.Materials);
            dm.ShowDialog();
            //
            OnSdMaterialDefinitionChanged();
        }

        /// <summary> 整个系统的材料定义发生变化，比如增加，减少等 </summary>
        public virtual void OnSdMaterialDefinitionChanged()
        {
        }

        /// <summary> 整个系统的截面定义发生变化，比如增加，减少等 </summary>
        public virtual void OnSdProfileDefinitionChanged()
        {
        }

        #endregion

        #region ---   Abaqus 求解 以及 前后处理

        #region ---   求解计算

        private BgwParameters _bcParameters;

        /// <summary> 作为 BackgroundWorker.RunWorkerAsync() 的参数 </summary>
        private class BgwParameters
        {
            public AbaqusSolver Solver;
            public string ErrorMessage;
        }

        private void buttonSolve_Click(object sender, EventArgs e)
        {
            StringBuilder errorMessage = new StringBuilder();

            // 对模型进行检查，看是否可以进行计算
            if (Model.Validate(ref errorMessage))
            {
                _Solver = new AbaqusSolver(
                    workingDir: WorkingDir,
                    solverGui: Options.SolverGUI
                    );

                // 检查计算环境，文件配置
                if (_Solver.CheckEnvironment(Model, ref errorMessage))
                {
                    // 求解计算
                    _bcParameters = new BgwParameters()
                    {
                        Solver = _Solver,
                        ErrorMessage = errorMessage.ToString(),
                    };
                    //
                    progressBar1.Visible = true;
                    progressBar1.Style = ProgressBarStyle.Marquee;

                    if (_bgw_Solver.IsBusy != true)
                    {
                        // 
                        OnSdCalculationStart();
                        // Start the asynchronous operation.
                        _bgw_Solver.RunWorkerAsync(argument: _bcParameters);
                    }
                }
            }
        }

        /// <summary> 开始计算 </summary>
        private void _bgw_Solver_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                BgwParameters para = e.Argument as BgwParameters;
                //
                _Solver.CalculationTimerElapsed += SolverOnCalculationTimerElapsed;
                //
                SolverState state = para.Solver.Execute(
                    waitingSeconds: Options.WaitingSeconds,
                    errorMessage: out para.ErrorMessage
                    );
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                e.Result = ex;
                // ignored
            }
        }

        /// <summary> 强行终止 Abaqus 的计算 </summary>
        private void ForceSolverShutDown(object sender, EventArgs e)
        {
            if (_Solver != null && _Solver.State == SolverState.Calculating)
            {
                _Solver.TerminateAbqCalculation();
            }
        }

        #endregion

        #region ---   计算过程中的操作

        private void SolverOnCalculationTimerElapsed(Timer timer, TimeSpan timeSpan)
        {
            if (_bgw_Solver != null)
            {
                _bgw_Solver.ReportProgress((int)timeSpan.TotalSeconds, timeSpan);
            }
        }

        private void _bgw_Solve_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var ts = (TimeSpan)e.UserState;
            label_elapsedTime.Text = @"Elapsed time : " + ts.ToString(@"hh\:mm\:ss");
        }

        #endregion

        #region ---   后处理操作

        /// <summary> 计算完成，开始后处理 </summary>
        private void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            OnSdCalculationFinished();
            AbaqusSolver solver = _bcParameters.Solver;
            //
            if (e.Cancelled) // 判断是否是手动退出线程
            {
                // 说明
                // solver.State = SolverState.FailedInCs;
            }

            // 后处理
            StringBuilder sb = new StringBuilder();
            _PostProcessor = new PostProcessor(Model, solver);
            if (_PostProcessor.CheckFinishState(errorMessage: ref sb))
            {
                SolverState ss = _PostProcessor.CheckResultFiles(errorMessage: ref sb);
                if (ss == SolverState.Succeeded)
                {
                    if (Options.DirectlyReport)
                    {
                        ReadAndShowResults();
                    }
                    else
                    {
                        var res = MessageBox.Show(@"计算结束且成功，是否直接生成报告？", @"Congratulations", MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Asterisk);
                        if (res == DialogResult.OK)
                        {
                            ReadAndShowResults();
                        }
                    }
                }
                else if (ss == SolverState.FailedWithError)
                {
                    var res = MessageBox.Show("Abaqus 计算过程中出错而导致计算终止！\r\n" + sb.ToString(), @"提示", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else
            {
                var res = MessageBox.Show("Abaqus 计算过程未正常结束！\r\n" + sb.ToString(), @"提示", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 将 Abaqus 的计算结果进行读取，并显示出来
        /// </summary>
        /// <param name="pp"></param>
        public void ReadAndShowResults()
        {
            if (_PostProcessor != null)
            {
                try
                {
                    if (_PostProcessor.Results == null)
                    {
                        _PostProcessor.ReadResultFile(resultFilePath: WorkingDir.F_AbqResult);
                    }
                    _PostProcessor.ShowResultsList();
                }
                catch (Exception ex)
                {
                    // ignored
                    var res = MessageBox.Show("后处理过程出现异常！\r\n", @"提示", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else
            {
                var res = MessageBox.Show(@"未找到有效的计算结果！", @"提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        /// <summary> 为 Abaqus 的开始计算设置对应的 UI 界面 </summary>
        protected virtual void OnSdCalculationStart()
        {
            button_Terminate.Visible = true;
            button_Terminate.Focus();
            buttonSolve.Enabled = false;
            label_elapsedTime.Text = "";
            label_elapsedTime.Visible = true;
        }

        /// <summary> 为 Abaqus 的结束计算设置对应的 UI 界面 </summary>
        protected virtual void OnSdCalculationFinished()
        {
            progressBar1.Visible = false;
            button_Terminate.Visible = false;
            buttonSolve.Enabled = true;
            label_elapsedTime.Visible = false;
        }

        #endregion

        #region ---   作为基类提供的一些通用方法

        #endregion
    }
}