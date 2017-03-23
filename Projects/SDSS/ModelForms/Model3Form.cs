using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SDSS.Models;
using SDSS.Structures;

namespace SDSS.ModelForms
{
    internal partial class Model3Form : MainForm
    {
        private readonly Model3 _model3;

        #region ---   构造函数
        public Model3Form(Model3 tunnelModel) : base(tunnelModel)
        {
            InitializeComponent();
            //
            _model3 = tunnelModel;
            tunnelConstructor1.TunnelPointorChanged += TunnelConstructor1_TunnelPointorChanged;
        }

        private void TunnelConstructor1_TunnelPointorChanged(Tunnel obj)
        {
            _model3.Tunnel = obj;
        }

        private void TunnelConstructor1OnTunnelPointorChanged(Tunnel newTunnel)
        {
            _model3.Tunnel = newTunnel;
            RefreshUI_PictureBox(_model3, null);
        }

        #endregion

        #region ---   界面的刷新 Refresh_NewModel

        /// <summary> 当窗口所对应的整个 TunnelModel 发生改变时，刷新整个界面 </summary>
        /// <param name="tunnelModel"></param>
        public override void Refresh_NewModel(ModelBase tunnelModel)
        {
            base.Refresh_NewModel(tunnelModel);
            //
            var tm = tunnelModel as Model3;
            //
            tunnelConstructor1.ImportTunnelOrDefinitions(tm.Tunnel, tm.Definitions);
            //
            OnSdMaterialDefinitionChanged();
            OnSdProfileDefinitionChanged();
            //
        }

        public override void OnSdMaterialDefinitionChanged()
        {
            base.OnSdMaterialDefinitionChanged();
            tunnelConstructor1.MaterialDefinitionChanged();
            //
        }

        public override void OnSdProfileDefinitionChanged()
        {
            base.OnSdProfileDefinitionChanged();
            tunnelConstructor1.ProfileDefinitionChanged();
            //
        }

        #endregion
    }
}
