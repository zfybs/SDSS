using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;
using eZstd.Drawing;
using SocketedShafts.Entities;

namespace SocketedShafts
{
    internal class SssDrawing
    {
        private PictureBox _pictrueBox;
        private Metafile _metafile;

        #region ---  PictureBox 绘图区域

        /// <summary> 在绘图控件中用来绘图的区域，此区域以外为留白区域 </summary>
        private readonly RectangleF _daAll;

        /// <summary> 在绘图控件中用来绘制桩的区域 </summary>
        private RectangleF _daShaft;

        /// <summary> 在绘图控件中用来绘制土层的区域 </summary>
        private RectangleF _daSoilLayers;

        /// <summary> 在绘图控件中用来绘制文字、标高等标记的区域 </summary>
        private RectangleF _daNotations;

        #endregion

        #region ---  物理模型区域

        #endregion

        public SssDrawing(PictureBox control)
        {
            _pictrueBox = control;

            // 指定各种绘图区域
            byte margin = 15; // 留白区域
            _daAll = new RectangleF(x: margin, y: margin,
                width: control.Size.Width - 2*margin, height: control.Size.Height - 2*margin);
            // 桩
            float shaftAxis = _daAll.Left + _daAll.Width*2/3;
            _daShaft = new RectangleF(shaftAxis - 5, _daAll.Top, 10, _daAll.Height);
            // 注释区
            _daNotations = new RectangleF(_daAll.Left, _daAll.Top, _daAll.Width/3, _daAll.Height);
            // 土层区
            _daSoilLayers = new RectangleF(_daAll.Left + _daAll.Width/3, _daAll.Top, _daAll.Width*2/3, _daAll.Height);
        }

        #region ---   绘图

        public void Draw(SocketedShaftSystem sss)
        {
            DrawOnImage(sss);
        }

        private void DrawOnImage(SocketedShaftSystem sss)
        {
            Bitmap im = new Bitmap(_pictrueBox.Size.Width, _pictrueBox.Size.Height);
            Graphics gr;
            gr = Graphics.FromImage(im);
            //
            gr.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
            DrawSss(gr, sss);

            // 刷新并覆盖原有图形
            _pictrueBox.Image = im;
            gr.Dispose();

            // 将最后的绘制结果保存下来
            // _pictrueBox.Image.Save("4.jpg");
        }

        private void DrawOnControl(SocketedShaftSystem sss)
        {
            Graphics gr;
            gr = _pictrueBox.CreateGraphics();
            // 清除原有图像
            gr.Clear(_pictrueBox.BackColor);

            DrawSss(gr, sss);
            gr.Dispose();
        }


        /// <summary> 开始进行全局绘制 </summary>
        /// <param name="g"></param>
        private void DrawSss(Graphics g, SocketedShaftSystem sss)
        {
            // 首先要确保有至少一个土层和桩截面
            if (sss.SocketedShaft.Sections.Count <= 0 || sss.SoilLayers.Count <= 0)
            {
                // 不需要进行绘制
                return;
            }

            SortedSet<float> elevations; // 所有可能的标高线，其第一个元素为最小值
            GetVerticalBounds(sss, out elevations);

            //
            g.ResetTransform();
            DrawSoilLayer(g, elevations, sss);
            g.ResetTransform();
            DrawShaft(g, elevations, sss);
            g.ResetTransform();
            DrawNotes(g, elevations, sss);
        }


        private void DrawShaft(Graphics gr, SortedSet<float> elevations, SocketedShaftSystem sss)
        {
            // 转换坐标系：对 Y 方向进行变换
            float sp;
            float ratio;
            TransformUtils.GetAxisTransform(
                s1: _daShaft.Top, s2: _daShaft.Top + _daShaft.Height,
                m1: elevations.Max, m2: elevations.Min,
                mp: 0f, sp: out sp, ratio: out ratio);

            // 将得到的结果应用到仿射矩阵中
            Matrix mY = new Matrix();
            mY.Translate(_daShaft.Left, sp);
            mY.Scale(1, ratio);
            gr.Transform = mY; // 将变换应用到绘图面板中

            // 在模型坐标系下绘图
            var colors = ColorUtils.ColorExpand(new Color[3]
            {
                Color.FromArgb(255, 255, 0, 0),
                Color.FromArgb(255, 255, 192, 0),
                Color.FromArgb(255, 255, 255, 0)
            }, sss.SocketedShaft.Sections.Count);

            for (int i = 0; i < sss.SocketedShaft.Sections.Count; i++)
            {
                var s = sss.SocketedShaft.Sections[i];
                gr.FillRectangle(new SolidBrush(colors[i]),
                    new RectangleF(0, s.Bottom, _daShaft.Width, s.Top - s.Bottom));
            }
        }

        private void DrawSoilLayer(Graphics g, SortedSet<float> elevations, SocketedShaftSystem sss)
        {
            // 转换坐标系
            Matrix m = new Matrix();
            float sp;
            float ratio;
            TransformUtils.GetAxisTransform(_daSoilLayers.Top, _daSoilLayers.Top + _daSoilLayers.Height,
                elevations.Max, elevations.Min, 0f, out sp, out ratio);
            m.Translate(_daSoilLayers.Left, sp);
            m.Scale(1, ratio);
            g.Transform = m;

            // 在模型坐标系下绘图
            var colors = ColorUtils.ColorExpand(new Color[3]
            {
                Color.FromArgb(255, 0, 176, 80),
                Color.FromArgb(255, 0, 112, 192),
                Color.FromArgb(255, 0, 32, 96)
            }, sss.SoilLayers.Count);

            // colors = ColorUtils.ClassicalExpand(sss.SoilLayers.Count);
            for (int i = 0; i < sss.SoilLayers.Count; i++)
            {
                var s = sss.SoilLayers[i];
                g.FillRectangle(new SolidBrush(colors[i]),
                    new RectangleF(0, s.Bottom, _daSoilLayers.Width, s.Top - s.Bottom));
            }
        }

        /// <summary> 绘制标高与注释 </summary>
        /// <param name="g"></param>
        /// <param name="elevations"></param>
        /// <param name="sss"></param>
        private void DrawNotes(Graphics g, SortedSet<float> elevations, SocketedShaftSystem sss)
        {
            // 转换坐标系
            Matrix m = new Matrix();
            float sp;
            float ratio;
            TransformUtils.GetAxisTransform(_daNotations.Top, _daNotations.Top + _daNotations.Height,
                elevations.Max, elevations.Min,
                0f, out sp, out ratio);
            m.Translate(_daNotations.Left, sp);
            m.Scale(1, ratio);
            // g.Transform = m;

            // 在模型坐标系下绘图
            var c = Color.Black;
            var modelPoints = elevations.Select(r => new PointF(0, r)).ToArray();
            m.TransformPoints(modelPoints);
            // 增加一定高度以放置字符
            float stringHight = 15f;
            for (int j = 0; j < modelPoints.Length; j++)
            {
                modelPoints[j].Y -= stringHight;
            }
            // 写字
            Pen blackPen = new Pen(Color.Black);
            Font ft = new Font("Times New Romans", 10, FontStyle.Regular); // "Arial"
            int i = 0;
            foreach (var ele in elevations)
            {
                g.DrawLine(blackPen, new PointF(0, modelPoints[i].Y + stringHight),
                    new PointF(_daNotations.Width, modelPoints[i].Y + stringHight));
                g.DrawString(ele.ToString("0.0"), ft, new SolidBrush(c), modelPoints[i]);
                i += 1;
            }
        }

        #endregion

        #region ---   基本坐标系定位

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sss"></param>
        /// <param name="elevations">所有可能的标高线，其第一个元素为最小值</param>
        private void GetVerticalBounds(SocketedShaftSystem sss, out SortedSet<float> elevations)
        {
            elevations = new SortedSet<float>();

            // 提取土层信息
            foreach (var l in sss.SoilLayers)
            {
                elevations.Add(l.Top);
                elevations.Add(l.Bottom);
            }

            // 提取桩截面信息
            foreach (var s in sss.SocketedShaft.Sections)
            {
                elevations.Add(s.Top);
                elevations.Add(s.Bottom);
            }
        }

        #endregion

        #region ---   保存为矢量图

        public void Save(string filePath, SocketedShaftSystem sss)
        {
            // 创建一张空图片 并 获取其画板 Graphics 对象
            Bitmap bmp = new Bitmap(220, 220); // 这里的220*220并不能决定最后矢量图的大小
            Graphics gr = Graphics.FromImage(bmp);

            // 由位图的面板生成对应的空白矢量图 并 获取 矢量图的画板 grVector 对象
            Metafile _metafile = new Metafile(filePath, gr.GetHdc());
            Graphics grVector = Graphics.FromImage(_metafile);
            // 绘图
            DrawSss(grVector, sss);
            // 保存
            grVector.Save();
            //
            grVector.Dispose();
            _metafile.Dispose();
        }

        #endregion

        #region ---   代码测试

        /// <summary> 创建一个矢量图形，并导出为 Emf 或 Wmf 文件 </summary>
        /// <param name="filePathToSave">要保存到的文件的绝对路径，推荐的后缀名为 .mwf 或 .emf </param>
        public static void CreateMetafileAndExport(string filePathToSave)
        {
            // 创建一张空图片 并 获取其画板 Graphics 对象
            Bitmap bmp = new Bitmap(220, 220); // 这里的220*220并不能决定最后矢量图的大小
            Graphics gr = Graphics.FromImage(bmp);

            // 由位图的面板生成对应的空白矢量图 并 获取 矢量图的画板 grVector 对象
            Metafile mf = new Metafile(filePathToSave, gr.GetHdc());
            Graphics grVector = Graphics.FromImage(mf);
            // 在矢量图画板中绘制矢量图形
            DrawShapes(grVector);

            // 将绘制好的矢量图形保存到外部文件 filePathToSave 中
            grVector.Save();
            //
            grVector.Dispose();
            mf.Dispose();
        }

        /// <summary> 绘制图形 </summary>
        /// <param name="g">用于绘图的Graphics对象</param>
        private static void DrawShapes(Graphics g)
        {
            HatchBrush hb = new HatchBrush(HatchStyle.LightUpwardDiagonal, Color.Black, Color.White);
            g.FillEllipse(Brushes.Gray, 10f, 10f, 200, 200);
            g.DrawEllipse(new Pen(Color.Black, 1f), 10f, 10f, 200, 200);
            g.FillEllipse(hb, 30f, 95f, 30, 30);
            g.DrawEllipse(new Pen(Color.Black, 1f), 30f, 95f, 30, 30);
            g.FillEllipse(hb, 160f, 95f, 30, 30);
            g.DrawEllipse(new Pen(Color.Black, 1f), 160f, 95f, 30, 30);
            g.FillEllipse(hb, 95f, 30f, 30, 30);
            g.DrawEllipse(new Pen(Color.Black, 1f), 95f, 30f, 30, 30);
            g.FillEllipse(hb, 95f, 160f, 30, 30);
            g.DrawEllipse(new Pen(Color.Black, 1f), 95f, 160f, 30, 30);
            g.FillEllipse(Brushes.Blue, 60f, 60f, 100, 100);
            g.DrawEllipse(new Pen(Color.Black, 1f), 60f, 60f, 100, 100);
            g.FillEllipse(Brushes.BlanchedAlmond, 95f, 95f, 30, 30);
            g.DrawEllipse(new Pen(Color.Black, 1f), 95f, 95f, 30, 30);
            g.DrawRectangle(new Pen(Brushes.Blue, 0.1f), 6, 6, 208, 208);
            g.DrawLine(new Pen(Color.Black, 0.1f), 110f, 110f, 220f, 25f);
            g.DrawString("剖面图", new Font("宋体", 9f), Brushes.Green, 220f, 20f);
        }

        #endregion
    }
}