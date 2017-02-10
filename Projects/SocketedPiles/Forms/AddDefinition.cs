using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SocketedShafts.Forms
{
    public partial class AddDefinition<T> : Form where T : ICloneable, new()
    {
        #region ---   Property

        public T Instance { get; }

        #endregion

        /// <summary> 编辑定义 </summary>
        /// <param name="instance">要进行绑定和参数设置的那个对象的实例</param>
        public AddDefinition(T instance)
        {
            InitializeComponent();
            //
            if (instance == null)
            {
                throw new NullReferenceException("进行属性编辑的对象不能为空");
            }
            Instance = instance;
            //
            propertyGrid1.SelectedObject = Instance;
        }

        /// <summary> 添加定义 </summary>
        public AddDefinition() : this(new T())
        {
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

    }
}
