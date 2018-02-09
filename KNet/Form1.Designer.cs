namespace KNet
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartGraphic = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnNextIteration = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chartGraphic)).BeginInit();
            this.SuspendLayout();
            // 
            // chartGraphic
            // 
            this.chartGraphic.BackColor = System.Drawing.Color.Black;
            chartArea1.Name = "ChartArea1";
            this.chartGraphic.ChartAreas.Add(chartArea1);
            this.chartGraphic.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartGraphic.Legends.Add(legend1);
            this.chartGraphic.Location = new System.Drawing.Point(0, 0);
            this.chartGraphic.Name = "chartGraphic";
            this.chartGraphic.RightToLeft = System.Windows.Forms.RightToLeft.No;
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Weights";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Input";
            this.chartGraphic.Series.Add(series1);
            this.chartGraphic.Series.Add(series2);
            this.chartGraphic.Size = new System.Drawing.Size(511, 459);
            this.chartGraphic.TabIndex = 0;
            this.chartGraphic.Text = "chartGraphic";
            // 
            // btnNextIteration
            // 
            this.btnNextIteration.Location = new System.Drawing.Point(391, 397);
            this.btnNextIteration.Name = "btnNextIteration";
            this.btnNextIteration.Size = new System.Drawing.Size(108, 29);
            this.btnNextIteration.TabIndex = 1;
            this.btnNextIteration.Text = "Next Iteration";
            this.btnNextIteration.UseVisualStyleBackColor = true;
            this.btnNextIteration.Click += new System.EventHandler(this.btnNextIteration_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 459);
            this.Controls.Add(this.btnNextIteration);
            this.Controls.Add(this.chartGraphic);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.chartGraphic)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartGraphic;
        private System.Windows.Forms.Button btnNextIteration;
    }
}

