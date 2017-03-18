﻿using System;
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
        public void DrawSoilStructureModel(SoilStructureGeometry ssg, Graphics g = null)
        {

            g = g ?? CreateGraphics();
            g.Clear(BackColor);
            //
            float soilHeight = 0;
            double rx, ry, r;
            soilHeight = ssg.SoilHeight.Sum();
            if (this.Width != 0 && this.Height != 0 && ssg.SoilWidth > 0)
            {
                rx = (float)this.Width / ssg.SoilWidth;
                ry = (float)this.Height / soilHeight;
                r = Math.Min(rx, ry); // r 值越小，则模型缩得越多
            }
            else
            {
                r = 10;
            }

            modelScale = (float)(r * 0.8);

            float translatex = (this.Width - ssg.SoilWidth * modelScale) / 2;
            float translatey = (this.Height - soilHeight * modelScale) / 2;

            //图形平移
            g.TranslateTransform(translatex, translatey);
            //图形缩放
            g.ScaleTransform(modelScale, modelScale);

            //结构的整体宽度和高度
            float structureWidth = 0;
            foreach (float i in ssg.StationSegments)
            {
                structureWidth += i;
            }
            float structureHeight = 0;
            foreach (float i in ssg.StationFloors)
            {
                structureHeight += i;
            }
            //结构左上角点的横坐标
            float sLeftUp = ssg.SoilWidth / 2 - structureWidth / 2;


            //画土层
            Pen soilPen = new Pen(Color.Black, 1);
            var colors = sdUtils.ClassicalColorsExpand(ssg.SoilHeight.Length);
            for (int i = 0; i < ssg.SoilHeight.Length; i++)
            {
                float[] SoilHeighti = new float[i];
                Array.Copy(ssg.SoilHeight, 0, SoilHeighti, 0, i);
                float soilHeighti = 0;
                foreach (float f in SoilHeighti)
                {
                    soilHeighti += f;
                }
                //g.DrawLine(soilPen, 0, soilHeighti, ssg.SoilWidth, soilHeighti);
                try
                {
                    // SolidBrush soil = new SolidBrush(Color.FromArgb(25 * (int)i + 50, 225 - 25 * (int)i, 25 * (int)i, 225 - 25 * (int)i));
                    SolidBrush soil = new SolidBrush(colors[i]);
                    g.FillRectangle(soil, 0, soilHeighti, ssg.SoilWidth, ssg.SoilHeight[i]);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            #region--列举法画土层
            //SolidBrush soil1 = new SolidBrush(Color.FromArgb(0, 0, 225));
            //SolidBrush soil2 = new SolidBrush(Color.FromArgb(0,225 , 0));
            //SolidBrush soil3 = new SolidBrush(Color.FromArgb(225, 0, 0));
            //SolidBrush soil4 = new SolidBrush(Color.FromArgb(125, 125, 125));
            //SolidBrush soil5 = new SolidBrush(Color.FromArgb(0, 0, 100));
            //SolidBrush soil6 = new SolidBrush(Color.FromArgb(0, 0, 100));
            //g.DrawLine(soilPen, 0, 0, ssg.SoilWidth, 0);
            //g.DrawLine(soilPen, 0, ssg.SoilHeight[0], ssg.SoilWidth, ssg.SoilHeight[0]);
            //g.DrawLine(soilPen, 0, ssg.SoilHeight[0] + ssg.SoilHeight[1], ssg.SoilWidth, ssg.SoilHeight[0] + ssg.SoilHeight[1]);
            //g.DrawLine(soilPen, 0, ssg.SoilHeight[0] + ssg.SoilHeight[1] + ssg.SoilHeight[2], ssg.SoilWidth, ssg.SoilHeight[0] + ssg.SoilHeight[1] + ssg.SoilHeight[2]);
            //g.DrawLine(soilPen, 0, ssg.SoilHeight[0] + ssg.SoilHeight[1] + ssg.SoilHeight[2] + ssg.SoilHeight[3], ssg.SoilWidth, ssg.SoilHeight[0] + ssg.SoilHeight[1] + ssg.SoilHeight[2] + ssg.SoilHeight[3]);
            //g.DrawLine(soilPen, 0, ssg.SoilHeight[0] + ssg.SoilHeight[1] + ssg.SoilHeight[2] + ssg.SoilHeight[3] + ssg.SoilHeight[4], ssg.SoilWidth, ssg.SoilHeight[0] + ssg.SoilHeight[1] + ssg.SoilHeight[2] + ssg.SoilHeight[3] + ssg.SoilHeight[4]);
            //g.DrawLine(soilPen, 0, ssg.SoilHeight[0] + ssg.SoilHeight[1] + ssg.SoilHeight[2] + ssg.SoilHeight[3] + ssg.SoilHeight[4] + ssg.SoilHeight[5], ssg.SoilWidth, ssg.SoilHeight[0] + ssg.SoilHeight[1] + ssg.SoilHeight[2] + ssg.SoilHeight[3] + ssg.SoilHeight[4] + ssg.SoilHeight[5]);
            //g.FillRectangle(soil1,  0, 0,ssg.SoilWidth,ssg.SoilHeight[0]);
            //g.FillRectangle(soil2,  0, ssg.SoilHeight[0],ssg.SoilWidth,ssg.SoilHeight[1]);
            //g.FillRectangle(soil3,  0, ssg.SoilHeight[0]+ssg.SoilHeight[1],ssg.SoilWidth,ssg.SoilHeight[2]);
            //g.FillRectangle(soil4,  0, ssg.SoilHeight[0]+ssg.SoilHeight[1]+ssg.SoilHeight[2],ssg.SoilWidth,ssg.SoilHeight[3]);
            //g.FillRectangle(soil5,  0, ssg.SoilHeight[0]+ssg.SoilHeight[1]+ssg.SoilHeight[2]+ssg.SoilHeight[3],ssg.SoilWidth,ssg.SoilHeight[4]);
            //g.FillRectangle(soil6,  0, ssg.SoilHeight[0]+ssg.SoilHeight[1]+ssg.SoilHeight[2]+ssg.SoilHeight[3]+ssg.SoilHeight[4],ssg.SoilWidth,ssg.SoilHeight[5]);
            #endregion

            //画结构
            Pen structurePen1 = new Pen(Color.Black, 2f / modelScale);
            Pen structurePen2 = new Pen(Color.Black, 1f / modelScale);
            SolidBrush structure = new SolidBrush(Color.White);
            g.FillRectangle(structure, sLeftUp, ssg.OverlyingSoilHeight, structureWidth, structureHeight);

            RectangleF recStructure = new RectangleF(sLeftUp, ssg.OverlyingSoilHeight, sLeftUp + structureWidth, ssg.OverlyingSoilHeight + structureHeight);
            g.DrawRectangle(structurePen1, sLeftUp, ssg.OverlyingSoilHeight, structureWidth, structureHeight);

            #region--列举法画结构
            //g.DrawLine(structurePen1, sLeftUp, ssg.OverlyingSoilHeight, sLeftUp + structureWidth, ssg.OverlyingSoilHeight);
            //g.DrawLine(structurePen1, sLeftUp + structureWidth, ssg.OverlyingSoilHeight, sLeftUp + structureWidth, ssg.OverlyingSoilHeight + structureHeight);
            //g.DrawLine(structurePen1, sLeftUp + structureWidth, ssg.OverlyingSoilHeight + structureHeight, sLeftUp, ssg.OverlyingSoilHeight + structureHeight);
            //g.DrawLine(structurePen1, sLeftUp, ssg.OverlyingSoilHeight + structureHeight, sLeftUp, ssg.OverlyingSoilHeight);
            //g.DrawLine(structurePen2, sLeftUp, ssg.OverlyingSoilHeight, sLeftUp + structureWidth, ssg.OverlyingSoilHeight);
            #endregion
            for (int i = 0; i < ssg.StationSegments.Length; i++)
            {
                float[] StationSegmenti = new float[i];
                Array.Copy(ssg.StationSegments, 0, StationSegmenti, 0, i);
                float stationSegmenti = 0;
                foreach (float f in StationSegmenti)
                {
                    stationSegmenti += f;
                }
                g.DrawLine(structurePen2, sLeftUp + stationSegmenti, ssg.OverlyingSoilHeight,
                    sLeftUp + stationSegmenti, ssg.OverlyingSoilHeight + structureHeight);
            }
            for (int i = 0; i < ssg.StationFloors.Length; i++)
            {
                float[] StationFloori = new float[i];
                Array.Copy(ssg.StationFloors, 0, StationFloori, 0, i);
                float stationFloori = 0;
                foreach (float f in StationFloori)
                {
                    stationFloori += f;
                }
                g.DrawLine(structurePen2, sLeftUp, ssg.OverlyingSoilHeight + stationFloori,
                    sLeftUp + structureWidth, ssg.OverlyingSoilHeight + stationFloori);
            }
        }

    }
}
