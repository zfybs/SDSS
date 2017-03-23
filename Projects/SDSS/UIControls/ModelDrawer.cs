using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SDSS.Models;
using SDSS.Utility;
using SDSS.Structures;

namespace SDSS.UIControls
{
    internal class ModelDrawer : PictureBox
    {
        /// <summary> 模型的缩放比例，大于1表示放大 </summary>
        private float modelScale;
        //画图
        public void DrawSoilFrameModel(SoilFrameGeometry sfg, Graphics g = null)
        {

            g = g ?? CreateGraphics();
            g.Clear(BackColor);
            //
            float soilHeight = 0;
            double rx, ry, r;
            soilHeight = sfg.SoilHeight.Sum();
            if (this.Width != 0 && this.Height != 0 && sfg.SoilWidth > 0)
            {
                rx = (float)this.Width / sfg.SoilWidth;
                ry = (float)this.Height / soilHeight;
                r = Math.Min(rx, ry); // r 值越小，则模型缩得越多
            }
            else
            {
                r = 10;
            }

            modelScale = (float)(r * 0.8);

            float translatex = (this.Width - sfg.SoilWidth * modelScale) / 2;
            float translatey = (this.Height - soilHeight * modelScale) / 2;

            //图形平移
            g.TranslateTransform(translatex, translatey);
            //图形缩放
            g.ScaleTransform(modelScale, modelScale);

            //结构的整体宽度和高度
            float structureWidth = 0;
            foreach (float i in sfg.StationSegments)
            {
                structureWidth += i;
            }
            float structureHeight = 0;
            foreach (float i in sfg.StationFloors)
            {
                structureHeight += i;
            }
            //结构左上角点的横坐标
            float sLeftUp = sfg.SoilWidth / 2 - structureWidth / 2;

            #region---    画土层
            Pen soilPen = new Pen(Color.Black, 1);
            var colors = sdUtils.ClassicalColorsExpand(sfg.SoilHeight.Length);
            for (int i = 0; i < sfg.SoilHeight.Length; i++)
            {
                float[] SoilHeighti = new float[i];
                Array.Copy(sfg.SoilHeight, 0, SoilHeighti, 0, i);
                float soilHeighti = 0;
                foreach (float f in SoilHeighti)
                {
                    soilHeighti += f;
                }
                //g.DrawLine(soilPen, 0, soilHeighti, sfg.SoilWidth, soilHeighti);
                try
                {
                    // SolidBrush soil = new SolidBrush(Color.FromArgb(25 * (int)i + 50, 225 - 25 * (int)i, 25 * (int)i, 225 - 25 * (int)i));
                    SolidBrush soil = new SolidBrush(colors[i]);
                    g.FillRectangle(soil, 0, soilHeighti, sfg.SoilWidth, sfg.SoilHeight[i]);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            #endregion

            #region---    画结构
            Pen structurePen1 = new Pen(Color.Black, 2f / modelScale);
            Pen structurePen2 = new Pen(Color.Black, 1f / modelScale);
            SolidBrush structure = new SolidBrush(Color.White);
            g.FillRectangle(structure, sLeftUp, sfg.OverlyingSoilHeight, structureWidth, structureHeight);

            RectangleF recStructure = new RectangleF(sLeftUp, sfg.OverlyingSoilHeight, sLeftUp + structureWidth, sfg.OverlyingSoilHeight + structureHeight);
            g.DrawRectangle(structurePen1, sLeftUp, sfg.OverlyingSoilHeight, structureWidth, structureHeight);

            for (int i = 0; i < sfg.StationSegments.Length; i++)
            {
                float[] StationSegmenti = new float[i];
                Array.Copy(sfg.StationSegments, 0, StationSegmenti, 0, i);
                float stationSegmenti = 0;
                foreach (float f in StationSegmenti)
                {
                    stationSegmenti += f;
                }
                g.DrawLine(structurePen2, sLeftUp + stationSegmenti, sfg.OverlyingSoilHeight,
                    sLeftUp + stationSegmenti, sfg.OverlyingSoilHeight + structureHeight);
            }
            for (int i = 0; i < sfg.StationFloors.Length; i++)
            {
                float[] StationFloori = new float[i];
                Array.Copy(sfg.StationFloors, 0, StationFloori, 0, i);
                float stationFloori = 0;
                foreach (float f in StationFloori)
                {
                    stationFloori += f;
                }
                g.DrawLine(structurePen2, sLeftUp, sfg.OverlyingSoilHeight + stationFloori,
                    sLeftUp + structureWidth, sfg.OverlyingSoilHeight + stationFloori);
            }
            #endregion
        }

        public void DrawSoilTunnelModel(SoilTunnelGeometry stg, Graphics g = null)
        {
            g = g ?? CreateGraphics();
            g.Clear(BackColor);


            //构造图形平移和缩放需要的参数
            float soilHeight = stg.SoilHeight.Sum(); ;
            double rx, ry, r;

            if (this.Width != 0 && this.Height != 0 && stg.SoilWidth > 0)
            {
                rx = (float)this.Width / stg.SoilWidth;
                ry = (float)this.Height / soilHeight;
                r = Math.Min(rx, ry); // r 值越小，则模型缩得越多
            }
            else { r = 10; }

            modelScale = (float)(r * 0.8);

            float translatex = (this.Width - stg.SoilWidth * modelScale) / 2;
            float translatey = (this.Height - soilHeight * modelScale) / 2;

            //图形平移
            g.TranslateTransform(translatex, translatey);
            //图形缩放
            g.ScaleTransform(modelScale, modelScale);

            //由 stg 得到后面画圆形隧道需要的参数， tunnelWidth，tunnelHeight，segmentNum，radius
            int segmentNum = stg.TunnelSegmentNum;
            float radius = stg.TunnelRadius;
            float tunnelWidth = 2 * radius;
            float tunnelHeight = 2 * radius;

            #region---    画土层
            Pen soilPen = new Pen(Color.Black, 1);
            var colors = sdUtils.ClassicalColorsExpand(stg.SoilHeight.Length);
            for (int i = 0; i < stg.SoilHeight.Length; i++)
            {
                float[] SoilHeighti = new float[i];
                Array.Copy(stg.SoilHeight, 0, SoilHeighti, 0, i);
                float soilHeighti = SoilHeighti.Sum();

                try
                {
                    SolidBrush soil = new SolidBrush(colors[i]);
                    g.FillRectangle(soil, 0, soilHeighti, stg.SoilWidth, stg.SoilHeight[i]);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            #endregion

            #region---    画结构
            Pen structurePen1 = new Pen(Color.Black, 8f / modelScale);
            Pen structurePen2 = new Pen(Color.White, 4f / modelScale);
            SolidBrush structure = new SolidBrush(Color.White);
            g.FillEllipse(structure, stg.SoilWidth / 2 - radius , stg.OverlyingSoilHeight, tunnelWidth , tunnelHeight);
            //SolidBrush structure1 = new SolidBrush(Color.Black);
            //g.FillEllipse(structure1, stg.SoilWidth / 2 - radius - 2f / modelScale, stg.OverlyingSoilHeight - 2f / modelScale, tunnelWidth + 4f / modelScale, tunnelHeight + 4f / modelScale);
            //g.FillEllipse(structure, stg.SoilWidth / 2 - radius + 2f / modelScale, stg.OverlyingSoilHeight + 2f / modelScale, tunnelWidth - 4f / modelScale, tunnelHeight - 4f / modelScale);

            double[] Angles = new double[segmentNum + 1];
            if (segmentNum != 0)
            {
                double _angle = 360 / segmentNum;
                for (int i = 0; i < segmentNum + 1; i++)
                {
                    double _angleI = _angle * (i + 0.5);
                    Angles[i] = _angleI;
                }
                PointF point1 = new PointF(stg.SoilWidth / 2, (stg.OverlyingSoilHeight + radius));
                for (int i = 0; i < segmentNum; i++)
                {
                    g.DrawArc(structurePen1, stg.SoilWidth / 2 - radius, stg.OverlyingSoilHeight, tunnelWidth, tunnelHeight, (float)Angles[i], (float)_angle);
                    float a = (float)(stg.SoilWidth / 2 + (radius+ 4f / modelScale) * Math.Cos(Angles[i] * Math.PI / 180));
                    float b = (float)((stg.OverlyingSoilHeight + radius) + (radius + 4f / modelScale) * Math.Sin(Angles[i] * Math.PI / 180));
                    PointF point2 = new PointF(a, b);
                    g.DrawLine(structurePen2, point1, point2);
                }
            }

            #endregion
        }

    }
}
