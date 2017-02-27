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
    public partial class FrameConstructor : Form
    {
        private ushort _layerCount;
        private ushort _spanCount;

        /// <summary> 从下往上每一层的层高，单位为m。 </summary>
        public float[] LayerHeights { get; private set; }
        /// <summary> 从左往右每一跨的宽度，单位为m。 </summary>
        public float[] SpanWidths { get; private set; }

        #region ---   构造函数

        private static FrameConstructor _uiniqueInstance;

        public static FrameConstructor GetUniqueInstance(ushort layerCount, ushort spanCount)
        {
            var ins = _uiniqueInstance ?? new FrameConstructor();
            //
            ins.SetDataGridView(layerCount, spanCount);
            //
            _uiniqueInstance = ins;
            return ins;
        }

        public static FrameConstructor GetUniqueInstance(float[] layerHeights, float[] spanWidths)
        {
            var ins = _uiniqueInstance ?? new FrameConstructor();
            //
            ins.SetDataGridView(layerHeights, spanWidths);
            //
            _uiniqueInstance = ins;
            return ins;
        }

        /// <summary> 构造函数 </summary>
        private FrameConstructor()
        {
            InitializeComponent();
            //
            KeyPreview = true;
            //
            dgv_Layers.KeyDelete = true;
            dgv_Layers.ShowRowNumber = true;
            dgv_Layers.SupportPaste = true;
            //
            dgv_Spans.KeyDelete = true;
            dgv_Spans.ShowRowNumber = true;
            dgv_Spans.SupportPaste = true;
            //
            ColumnLayerHeight.ValueType = typeof(double);
            ColumnSpanWidth.ValueType = typeof(double);
        }

        #endregion

        #region ---   窗口的打开与关闭

        private void FrameConstructor_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            // e.Cancel = true;
        }
        private void FrameConstructor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region ---   DataGridView

        private void SetDataGridView(float[] layerHeights, float[] spanWidths)
        {
            //
            var layerCount = (ushort)layerHeights.Length;
            var spanCount = (ushort)spanWidths.Length;
            SetDataGridView(layerCount, spanCount);
            //
            LayerHeights = layerHeights;
            SpanWidths = spanWidths;
            //
            SetTableValues();
        }

        private void SetDataGridView(ushort layerCount, ushort spanCount)
        {
            //
            if (layerCount != _layerCount)
            {
                ResetRows(dgv_Layers, layerCount);
                _layerCount = layerCount;
            }
            if (spanCount != _spanCount)
            {
                ResetRows(dgv_Spans, spanCount);
                _spanCount = spanCount;
            }
            //
            LayerHeights = new float[layerCount];
            SpanWidths = new float[spanCount];
            //
            SetTableValues();
        }

        private void ResetRows(DataGridView dgv, int newRowsCount)
        {
            int originalCount = dgv.RowCount;
            if (newRowsCount > originalCount)
            {
                for (int i = 0; i < newRowsCount - originalCount; i++)
                {
                    dgv.Rows.Add();
                }
            }
            else if (newRowsCount < originalCount)
            {
                for (int i = originalCount - 1; i > newRowsCount - 1; i--)
                {
                    dgv.Rows.RemoveAt(i);
                }
            }
        }

        private void SetTableValues()
        {
            for (int i = 0; i < _layerCount; i++)
            {
                var r = dgv_Layers.Rows[i];
                r.Cells[0].Value = LayerHeights[i];
            }

            for (int i = 0; i < _spanCount; i++)
            {
                var r = dgv_Spans.Rows[i];
                r.Cells[0].Value = SpanWidths[i];
            }
        }

        private void dgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
        #endregion

        private void buttonOk_Click(object sender, EventArgs e)
        {
            // 构造层高与跨度数据
            try
            {
                LayerHeights = new float[_layerCount];
                for (int i = 0; i < _layerCount; i++)
                {
                    var obj = dgv_Layers.Rows[i].Cells[0].Value;
                    if (obj == null || string.IsNullOrEmpty(obj.ToString()) || (double)obj <= 0) throw new NullReferenceException($"倒数第{i + 1}层的层高值必须大于0");
                    LayerHeights[i] = (float)obj;
                }
                SpanWidths = new float[_spanCount];
                for (int i = 0; i < _spanCount; i++)
                {
                    var obj = dgv_Spans.Rows[i].Cells[0].Value;
                    if (obj == null || string.IsNullOrEmpty(obj.ToString()) || (double)obj <= 0) throw new NullReferenceException($"左数第{i + 1}跨的跨度值必须大于0");
                    SpanWidths[i] = (float)obj;
                }
                // close the form
                Close();
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
