using SDSS.Models;
using SDSS.Structures;

namespace SDSS.ModelForms
{
    internal partial class Model2Form : MainForm
    {
        #region ---   构造函数

        public Model2Form(Model2 stationModel) : base(stationModel)
        {
            InitializeComponent();
            //
        }

        private void FrameConstructor1OnFramePointorChanged(Frame newFrame)
        {
            var md = Model as Model2;
            md.Frame = newFrame;
        }

        #endregion

        #region ---   界面的刷新 Refresh_NewModel

        /// <summary> 当窗口所对应的整个 StationModel 发生改变时，刷新整个界面 </summary>
        /// <param name="stationModel"></param>
        public override void Refresh_NewModel(ModelBase stationModel)
        {
            base.Refresh_NewModel(stationModel);
            //
            var sm = stationModel as Model2;
            //
            frameConstructor1.ImportFrameOrDefinitions(sm.Frame, sm.Definitions);
            //
            OnSdMaterialDefinitionChanged();
            OnSdProfileDefinitionChanged();
            //
        }

        public override void OnSdMaterialDefinitionChanged()
        {
            base.OnSdMaterialDefinitionChanged();
            frameConstructor1.MaterialDefinitionChanged();
            //
        }

        public override void OnSdProfileDefinitionChanged()
        {
            base.OnSdProfileDefinitionChanged();
            frameConstructor1.ProfileDefinitionChanged();
            //
        }

        #endregion
    }
}