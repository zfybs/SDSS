using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using eZstd.Miscellaneous;
using SDSS.Definitions;
using SDSS.Project;
using SDSS.Models;
using SDSS.UIControls;
using SDSS.Utility;

namespace SDSS.ModelForms
{
    /// <summary> <see cref="MainForm"/>的容器 </summary>
    /// <remarks>此类是为了承载不同模型窗口的菜单项而设计的（因为<see cref="MenuStrip"/>菜单控件在派生窗口中无法修改）。
    /// 它作为<see cref="MainForm"/>的容器，与 <see cref="MainForm"/> 窗口组合起来构造了整个程序中不同计算模型的界面的基类。
    /// 一方面处理与整个程序相关的操作（与具体项目无关），比如帮助文档、默认计算文件夹等；
    /// 另一方面与 <see cref="MainForm"/> 窗口组合起来处理 <see cref="ModelBase"/> 类的一些通用性代码。 </remarks>
    internal partial class MainFormMdi : Form
    {
        #region ---   Fields

        /// <summary> 主界面中的<see cref="ModelBase"/>对象必须要通过此字段来获取，
        /// 因为<see cref="MainForm"/>窗口中绑定的<see cref="ModelBase"/>可能在模型导入过程中发生改变。 </summary>
        public MainForm ModelForm { get; private set; }


        /// <summary>
        /// 主界面中的<see cref="ModelBase"/>对象必须要通过此字段来获取，
        /// 因为<see cref="MainForm"/>窗口中绑定的<see cref="ModelBase"/>可能在模型导入过程中发生改变。 
        /// </summary>
        private ModelBase _model
        {
            get { return ModelForm?.Model; }
        }

        #endregion

        #region ---   窗口打开与关闭

        /// <summary> 构造函数 </summary>
        public MainFormMdi(ModelType type, CalculationMethod method, string modelName)
        {
            InitializeComponent();
            //
            ModelForm = CreateModelForm(type, method);
            ModelForm.Model.ModelName = modelName;
            //
        }

        private void MainFormMdi_Load(object sender, EventArgs e)
        {
            SetAndShowChildForm(ModelForm);
            RefreshUI_Model(ModelForm, _model);
        }

        /// <summary> 窗口关闭 </summary>
        private void TSM_Exit_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        #endregion

        #region ---   !!! 模型文件的打开保存

        private void tsm_Open_Click(object sender, EventArgs e)
        {
            string modelFile = null;
            ModelBase newModel = ImportFromXml(out modelFile);
            if (newModel != null)
            {
                var mainF = GetModelForm(newModel);

                // 将最新导入的模型刷新到整个界面中
                SetAndShowChildForm(mainF);
                //
                RefreshUI_Model(mainF, newModel);

                //
                mainF.SetModelFilePath(this, modelFile);
            }
        }

        private void tsm_Save_Click(object sender, EventArgs e)
        {
            ModelForm.SaveModel();
        }

        /// <summary> 将整个模型导出到 xml 文档 </summary>
        private void tsm_SaveAs_Click(object sender, EventArgs e)
        {
            StringBuilder errorMessage = new StringBuilder();

            if (_model.Validate(ref errorMessage))
            {
                string filePath = sdUtils.ChooseSaveStationModel("导出车站模型");
                if (filePath != null)
                {
                    ModelForm.SetModelFilePath(this, filePath);
                    ModelForm.SaveModel();
                }
            }
            else
            {
                MessageBox.Show("当前模型不符合导出规范 \n\r" + errorMessage, "提示", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary> 从 xml 文档中导入<see cref="ModelBase"/>对象 </summary>
        public ModelBase ImportFromXml(out string openedFile)
        {
            openedFile = sdUtils.ChooseOpenStationModel("导入车站模型");
            if (openedFile != null)
            {
                StringBuilder errorMessage = new StringBuilder();
                try
                {
                    bool succeeded;
                    var tp = sdUtils.GetXmlRootType(openedFile, typeof(ModelBase), false);
                    var sm = sdUtils.ImportFromXml(openedFile, tp,
                        out succeeded, ref errorMessage) as ModelBase;

                    if (succeeded && sm != null)
                    {
                        return sm;
                    }
                }
                catch (Exception ex)
                {
                    DebugUtils.ShowDebugCatch(ex, "指定的文件不能正常解析为车站模型。\r\n" + errorMessage + "\r\n请检查文件中的内容，或者重新指定模型文件。");
                    return null;
                }
            }
            return null;
        }

        #endregion

        #region ---   !!! 子窗口（模型界面）的选择、创建、与关闭

        /// <summary> 从打开的窗口中匹配对应的子窗口，或者创建一个新的模型子窗口 </summary>
        private MainForm GetModelForm(ModelBase model)
        {
            MainForm matchedForm = null;
            foreach (var cf in MdiChildren)
            {
                var childForm = cf as MainForm;
                if (childForm != null && childForm.Model.Equals(model))
                {
                    matchedForm = childForm;
                    break;
                }
            }
            if (matchedForm == null)
            {
                matchedForm = CreateModelForm(model.ModelType, model.CalculationMethod, model);
            }
            return matchedForm;
        }

        /// <summary> 创建一个新的模型子窗口 </summary>
        /// <param name="model">如果其值为 null，则会新构造一个<see cref="ModelBase"/>对象，
        /// 如果其值不为 null，则必须确保其<see cref="ModelBase.ModelType"/>属性与<see cref="ModelBase.CalculationMethod"/>属性 必须与前两个参数相同 </param>
        private MainForm CreateModelForm(ModelType type, CalculationMethod method, ModelBase model = null)
        {
            MainForm mf = null;
            //
            const byte typeBit = 10;
            var mIndex = (byte)type * typeBit + (byte)method;
            switch (mIndex)
            {
                // 矩形框架
                case (byte)ModelType.Frame * typeBit + (byte)CalculationMethod.InertialForce:
                    {
                        Model1 sm = null;
                        if (model == null)
                        {
                            sm = new Model1();
                            Program.ConstructStationModel(sm);
                        }
                        else
                        {
                            sm = model as Model1;
                        }
                        mf = new Model1Form(sm);
                        break;
                    }
                case (byte)ModelType.Frame * typeBit + (byte)CalculationMethod.FanYingWeiYi:
                    {
                        Model2 sm = null;
                        if (model == null)
                        {
                            sm = new Model2();
                            // Program.ConstructStationModel(sm);
                        }
                        else
                        {
                            sm = model as Model2;
                        }
                        mf = new Model2Form(sm);
                        break;
                    }
                case (byte)ModelType.Frame * typeBit + (byte)CalculationMethod.Method3:
                    {
                        break;
                    }
                case (byte)ModelType.Frame * typeBit + (byte)CalculationMethod.Method4:
                    {
                        break;
                    }
                // 圆形隧道
                case (byte)ModelType.Tunnel * typeBit + (byte)CalculationMethod.InertialForce:
                    {
                        Model3 tm = null;
                        if (model == null)
                        {
                            tm = new Model3();
                            //Program.ConstructStationModel(sm);
                        }
                        else
                        {
                            tm = model as Model3;
                        }
                        mf = new Model3Form(tm);
                        break;
                    }
                case (byte)ModelType.Tunnel * typeBit + (byte)CalculationMethod.FanYingWeiYi:
                    {
                        break;
                    }
                case (byte)ModelType.Tunnel * typeBit + (byte)CalculationMethod.Method3:
                    {
                        break;
                    }
                case (byte)ModelType.Tunnel * typeBit + (byte)CalculationMethod.Method4:
                    {
                        break;
                    }
                // 矿山法
                case (byte)ModelType.Model2 * typeBit + (byte)CalculationMethod.InertialForce:
                    {
                        break;
                    }
                case (byte)ModelType.Model2 * typeBit + (byte)CalculationMethod.FanYingWeiYi:
                    {
                        break;
                    }
                case (byte)ModelType.Model2 * typeBit + (byte)CalculationMethod.Method3:
                    {
                        break;
                    }
                case (byte)ModelType.Model2 * typeBit + (byte)CalculationMethod.Method4:
                    {
                        break;
                    }
            }
            // 为新创建的子窗口进行相关初始设置
            if (mf != null)
            {
                mf.FormClosed += MfOnFormClosed;
                mf.Model.ModelNameChanged += ModelOnModelNameChanged;
                mf.SetAbqWorkingDir(Options.DefaultAbqWorkingDir);
                mf.Text = mf.Model.DescriptionName + @" - " + mf.Model.ModelName;
            }
            return mf;
        }

        /// <summary> 强制关闭当前活动的子窗口 </summary>
        private void button_CloseChildForm_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }
        }

        /// <summary> 子窗口关闭的事件 </summary>
        private void MfOnFormClosed(object sender, FormClosedEventArgs formClosedEventArgs)
        {
            var mf = sender as MainForm;
            if (mf == ModelForm)
            {
                ModelForm = null;
            }
        }

        /// <summary> 在其派生窗口类也构造完成后，再次进行界面的设置 </summary>
        private void ModelOnModelNameChanged(ModelBase modelBase, string s)
        {
            ModelForm.Text = $"{modelBase.DescriptionName} - {s}";
            Text = $"{Constants.ProjectConsts.ProjectTitle} - {ModelForm.Text}";
        }

        /// <summary> 子窗口激活的事件 </summary>
        private void MainFormMdi_MdiChildActivate(object sender, EventArgs e)
        {
            var form = ActiveMdiChild as MainForm;
            if (form != null)
            {
                ModelForm = form;
                SetToolStripTxtboxText(tst_abqWorkingDir, form.WorkingDir.WorkingDirectory);
                Text = $"{Constants.ProjectConsts.ProjectTitle} - {form.Model.DescriptionName} - {form.Model.ModelName}";
            }
            //
        }

        #endregion

        #region ---   界面 与 子窗口 的刷新

        /// <summary> 创建并打开 子窗口 </summary>
        private void SetAndShowChildForm(Form childForm)
        {
            //
            childForm.MdiParent = this;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.MinimizeBox = false;
            childForm.MaximizeBox = false;
            childForm.Dock = DockStyle.Fill;
            childForm.WindowState = FormWindowState.Normal; //  如果要显示关闭按钮，则将其设置为 Maximized
            //
            MinimumSize = new Size(childForm.MinimumSize.Width + 10, childForm.MinimumSize.Height + 20);
            Size = new Size(childForm.Size.Width + 10, childForm.Size.Height + 20);
            //
            //childForm.Select();
            childForm.Show();
            // 下面的操作看上去没用，但是可以解决在创建新模型窗口后，显示出来被缩放的问题。
            // childForm.WindowState = FormWindowState.Maximized;
        }

        /// <summary> 当窗口所对应的整个 StationModel 发生改变时，刷新整个界面 </summary>
        private void RefreshUI_Model(MainForm form, ModelBase newModel)
        {
            ModelForm = form;

            // MDI 界面中的刷新
            _model.ModelName = _model.ModelName;
            tsm_Save.Enabled = false;
            // 然后将新的模型刷新到新的子窗口中
            try
            {
                form.Refresh_NewModel(newModel);
            }
            catch (Exception ex)
            {
                DebugUtils.ShowDebugCatch(ex, @"将新导入的模型信息刷新到界面时出错。");
            }
        }

        #endregion

        #region ---   设置软件环境

        private OptionsForm _opForm;

        private void TSM_Option_Click(object sender, EventArgs e)
        {
            if (_opForm == null)
            {
                _opForm = new OptionsForm();
            }
            _opForm.ShowDialog();
        }

        private static void SetToolStripTxtboxText(ToolStripTextBox tstb, string text)
        {
            tstb.Text = text;
            var width = tstb.TextBox.CreateGraphics().MeasureString(text, tstb.Font).Width;
            var size = new Size((int)width, tstb.Height);
            tstb.TextBox.Size = size;
            // tstb.Size = size;
        }

        #endregion

        #region ---   辅助 MainForm 窗口中的菜单操作（因为 MainForm 中无法设置菜单栏控件）

        private void tsm_ShowResult_Click(object sender, EventArgs e)
        {
            ModelForm.ReadAndShowResults();
        }

        //设置当前项目的计算工作路径（项目>工作文件夹）

        private void tsm_ModelInfos_Click(object sender, EventArgs e)
        {
            var modelOptions = new ModelOptions(ModelForm);
            var res = modelOptions.ShowDialog();
            if (res == DialogResult.OK)
            {
                SetToolStripTxtboxText(tst_abqWorkingDir, ModelForm.WorkingDir.WorkingDirectory);
            }
        }

        #region ---   整个系统的材料与截面定义

        private void tsm_Materials_Click(object sender, EventArgs e)
        {
            var dm = new DefinitionManager<Material>(_model.Definitions.Materials);
            dm.ShowDialog();
            //
            ModelForm.OnSdMaterialDefinitionChanged();
        }

        private void tsm_Profiles_Click(object sender, EventArgs e)
        {
            var dm = new DefinitionManager<Profile>(_model.Definitions.Profiles);
            dm.ShowDialog();
            //
            ModelForm.OnSdProfileDefinitionChanged();
        }

        #endregion

        #endregion

        #region ---   菜单项的显示与禁用

        private void tsm_Files_DropDownOpening(object sender, EventArgs e)
        {
            tsm_Save.Enabled = (ModelForm != null) && File.Exists(ModelForm.ModelFilePath);
            tsm_SaveAs.Enabled = (ModelForm != null) && (ModelForm.Model != null);
        }

        #endregion
    }
}