using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SDSS.UIControls
{

    public partial class BoundaryParamForm : Form
    {
        public float Kx { get; private set; }

        public float Ky { get; private set; }
        public float SoilWidth { get; private set; }

        public BoundaryParamForm(float kx, float ky)
        {
            InitializeComponent();
            Kx = kx;
            Ky = ky;
            //
            RefreshTextboxes();
        }

        private void RefreshTextboxes()
        {
            textBox_Kx.Text = Kx.ToString();
            textBox_Ky.Text = Ky.ToString();
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            if (Convert.ToSingle(textBox_Kx.Text) == 0
                || Convert.ToSingle(textBox_Ky.Text) == 0)
            {
                MessageBox.Show("请输入相应的参数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                string tx = textBox_Kx.Text;
                Kx = Convert.ToSingle(tx);
                string ty = textBox_Ky.Text;
                Ky = Convert.ToSingle(ty);
                Close();
            }
        }

        private void textBox_Kx_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar) && e.KeyChar != 46)
            {
                e.Handled = true;
            }
        }

    }
}
