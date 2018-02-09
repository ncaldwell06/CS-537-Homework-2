using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections; 

namespace KNet
{
    public partial class Form1 : Form
    {
        int iterationCounter = 0; 
        KNetLogic localKLogic; 
        public Form1(KNetLogic kLogic)
        {
            InitializeComponent();
            localKLogic = kLogic;
            localKLogic.chartForm = this; 
            //set props for charting area.
            chartGraphic.ChartAreas[0].AxisY.ScaleView.Zoom(-0.6, 0.6);
            chartGraphic.ChartAreas[0].AxisX.ScaleView.Zoom(-0.6, 0.6);
            chartGraphic.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Gray;
            chartGraphic.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Gray;
            chartGraphic.ChartAreas[0].BackColor = Color.Black;
            chartGraphic.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.GhostWhite;
            chartGraphic.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.GhostWhite;
            chartGraphic.Titles.Add("Initial Values");
            chartGraphic.Titles[0].ForeColor = Color.GhostWhite;
            chartGraphic.Titles[0].Font  =new Font("Arial", 10, FontStyle.Bold); 
             


            //set props for series 0. This is for weights
            chartGraphic.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chartGraphic.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chartGraphic.Series[0].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            chartGraphic.Series[0].MarkerColor = Color.OrangeRed;
            chartGraphic.Series[0].Color = Color.DarkCyan;
            chartGraphic.Series[0].BorderWidth = 1;
            chartGraphic.Series[0].MarkerSize = 5; 

            //set props for series 1. this is for input points. 
            chartGraphic.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            chartGraphic.Series[1].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            chartGraphic.Series[1].MarkerSize = 5;
            chartGraphic.Series[1].MarkerColor = Color.PaleGoldenrod;


            StartDataProcessing(); 

        }

        public void StartDataProcessing()
        {
            if (localKLogic.CheckForFile())
            {
                //Random values already exist, so we don't need to create them or the file. 
                localKLogic.ReadWeightFile();
                localKLogic.GenerateTrainingSet();
            }
            else
            {
                //random values do not exist so we need to create them and the file. 
                localKLogic.CreateRandomValues();
                localKLogic.CreateTheWeightFile();
                localKLogic.ReadWeightFile();
                localKLogic.GenerateTrainingSet();
                // chartForm.PlotPoints()
                //Form1.PlotPoints()
                // PlotPoints(); 

            }
        }


        public void PlotPoints(string type, ArrayList pointCollection)
        {//plot the points taht are in the collection
            foreach (double[] d in pointCollection)
            {
                if (type == "Points")
                {//just plot the points. this should be used only for the input values, x.

                    chartGraphic.Series[1].Points.AddXY(d[0], d[1]);

                }

                else if (type == "Lines")
                {//plot the line of the points in the colleciton. This should be the weights. 
                    chartGraphic.Series[0].Points.AddXY(d[0], d[1]);
                }
            }
        }

        private void btnNextIteration_Click(object sender, EventArgs e)
        {
            iterationCounter += 10;
            chartGraphic.Titles[0].Text = ("Iteration " + iterationCounter);

            chartGraphic.Series[0].Points.Clear();  //clear the points because they will be reevaluated and regraphed. 
        }

       
    }
}
