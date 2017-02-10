using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SDSS.Definitions;

namespace SDSS.UIControls
{
    /// <summary> 参数信息的添加或者对已有参数文件的编辑 </summary>
    /// <typeparam name="T"></typeparam>
    public partial class EditDefinition : Form
    {
        #region ---   Property

        public object Instance { get { return propertyGrid1.SelectedObject; } }

        #endregion

        /// <summary> 编辑定义 </summary>
        /// <param name="instance">要进行绑定和参数设置的那个对象的实例</param>
        public EditDefinition(object instance)
        {
            InitializeComponent();
            //
            if (instance == null)
            {
                throw new NullReferenceException("进行属性编辑的对象不能为空");
            }
            //
            propertyGrid1.SelectedObject = instance;
        }


        private void buttonOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        #region ---   属性值发生变化
        /// <summary>
        /// 当绑定的对象的属性值发生变化时
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (e.ChangedItem.Label == "Type")
            {
                var v = e.ChangedItem.Value;
                if (v is ProfileType)
                {
                    object o = propertyGrid1.SelectedObject;
                    Profile m = NewProfile(o as Profile, (ProfileType)v);
                    propertyGrid1.SelectedObject = m;
                }
                else if (v is MaterialType)
                {
                    object o = propertyGrid1.SelectedObject;
                    Material m = NewMaterial(o as Material, (MaterialType)v);
                    propertyGrid1.SelectedObject = m;
                }
            }
        }

        private Material NewMaterial(Material oriMaterial, MaterialType newMaterialType)
        {
            Material newM;
            switch (newMaterialType)
            {
                case MaterialType.MohrCoulomb:
                    newM = new MohrCoulomb(oriMaterial.Name, oriMaterial.Density, oriMaterial.Elasticity, oriMaterial.PoissonRatio,
                        cohesion: 1000, frictionAngle: 30);
                    break;
                default:
                    newM = new Material(oriMaterial.Name, oriMaterial.Density, oriMaterial.Elasticity, oriMaterial.PoissonRatio, newMaterialType);
                    break;
            }
            return newM;
        }

        private Profile NewProfile(Profile oriProfile, ProfileType newProfileType)
        {
            Profile newP;
            switch (newProfileType)
            {
                case ProfileType.T:
                    newP = new T(oriProfile.Name,width:1,height:0.8,generalThickness:0.2);
                    break;
                default:  // 
                    newP = new Rectangular(oriProfile.Name, width: 1, height: 0.8);
                    break;
            }
            return newP;
        }

        #endregion
    }
}
