using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace KNet
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            KNetLogic kLogic = new KNetLogic();
            Application.Run(new Form1(kLogic));

           
            //if (kLogic.CheckForFile())
            //{
            //    //Random values already exist, so we don't need to create them or the file. 
            //    kLogic.ReadWeightFile();
            //    kLogic.GenerateTrainingSet(); 
            //}
            //else
            //{
            //    //random values do not exist so we need to create them and the file. 
            //    kLogic.CreateRandomValues();
            //    kLogic.CreateTheWeightFile();
            //    kLogic.ReadWeightFile();
            //    kLogic.GenerateTrainingSet(); 
            //   // chartForm.PlotPoints()
            //    //Form1.PlotPoints()
            //   // PlotPoints(); 
                
            //}
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
          
           
           
        }
    }
}
