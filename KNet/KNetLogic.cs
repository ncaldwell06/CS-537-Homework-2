using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO; 

namespace KNet
{
    public class KNetLogic
    {
        string weightFilePath = @"C:\Users\Nathan\Desktop\Classes\CS 537\Homework 2\WeightValues.txt"; 
        int clusterCount = 50;
        float minWeight = -.5f; //weights are cluster weights
        float maxWeight = .5f;
        float trainingMin = -0.5f;
        float trainingMax = 0.5f; 

        float learningRate = 0.5f;
        float learningMinRate = .001f;

        int R = 2; 

        public Form1 chartForm; 

        ArrayList clusterWeights = new ArrayList();
        ArrayList trainingSets = new ArrayList();
        ArrayList distances = new ArrayList(); //keep track of the distances as we iterate over the input nodes. 


        public bool CheckForFile()
        {
            bool t = false; 
            //string curFile = @"C:\Users\Nathan\Desktop\Classes\CS 537\Homework 2\WeightValues.txt";
            Console.WriteLine(File.Exists(weightFilePath) ? "File exists." : "File does not exist.");
            t = File.Exists(weightFilePath) ? true : false;
            return t; 

        }

        public void CreateRandomValues()
        {//Create the random weights and store them in a list. 
            Random dRand = new Random();
            for (int i = 0; i < clusterCount; i++)
            {
                double[] weightValus = new double[2]; 
                for(int w = 0; w <2; w++)
                {
                    weightValus[w] = GenerateRandomFloatValue(dRand, minWeight, maxWeight);//Math.Round(dRand.NextDouble() * (maxWeight - minWeight) + minWeight, 2); 
                }
                clusterWeights.Add(weightValus);
                Console.WriteLine("Weights for " + i + " is" + "[" + weightValus[0] + " , " + weightValus[1] + "]"); 
            }
        }


        public double GenerateRandomFloatValue(Random dRand, float min, float max)
        {
            
            return Math.Round(dRand.NextDouble() * (max - min) + min, 2); 
        }

        public void CreateTheWeightFile()
        {//Function to create the file that will hold the weight values.
            StreamWriter sw = new StreamWriter(weightFilePath); 
            foreach (double[] d in clusterWeights)
            {
                string combined = String.Join(",", d.Select(p => p.ToString()).ToArray());//combine the double values and save them in a string. 
                sw.WriteLine(combined);
            }

            sw.Close(); //Always be sure to close the stream writer. 
          
        }

        public void ReadWeightFile()
        {//Read the file containing all the randomly generated weights. 
            if(clusterWeights.Count > 0)
            {
                clusterWeights.Clear(); //we can go ahead and clear out the collection. 
            }

            string weightValues = ""; 
            StreamReader sr = new StreamReader(weightFilePath);
            while ((weightValues = sr.ReadLine()) != null)
            {
                //now we have the value from the line in a file, now we need to parse it and store it as a double[]. 
                SplitAndSaveWeightValue(weightValues); 

            }

            sr.Close();  //Close the stream. 

            //////\nTest!!!/////
            Console.WriteLine("Testing the parsing values");
            int i = 0; 
            foreach(double[] d in clusterWeights)
            {
                Console.WriteLine("Weights for " + i + " is" + "[" + d[0] + " , " + d[1] + "]");
                i++; 
            }
        }

        public void SplitAndSaveWeightValue(string stringWeight)
        {//Split the string, convert to a double, store in the collection .
            
            double[] doubleWeights = new double[2]; 
            string[] parsedWeights = stringWeight.Split(','); 
            int localIndex = 0; 
            foreach(string weight in parsedWeights)
            {
                doubleWeights[localIndex] = Convert.ToDouble(weight);
                localIndex++; 
            }

            clusterWeights.Add(doubleWeights); 
        }

        public void GenerateTrainingSet()
        {//This will be where we generate the 100 training points. 
            //The values will be x1 + x2 < .25
            //the end result will be a float[2] with 2 values that are between trainingMin and trainingMax. 
            //each array will be stored in a collection of other data points. 

            int setCount = 0;  //count the number of sets we have added to the training set collection. 
            Random trainingRand = new Random();
            Console.WriteLine("Find Training Sets and print the sums"); 
            while (setCount < 100)
            {
                double x1 = GenerateRandomFloatValue(trainingRand, trainingMin, trainingMax);
                double x2 = GenerateRandomFloatValue(trainingRand, trainingMin, trainingMax); 
                if(x1 + x2 < 0.25)
                {
                    double[] trainingPoint = new double[2];
                    trainingPoint[0] = x1;
                    trainingPoint[1] = x2;
                    double sum = x1 + x2; 
                    trainingSets.Add(trainingPoint);
                    Console.WriteLine("Sum of " + x1 + " " + x2 + " = " + sum ); 
                    setCount++; 
                }
            }

            Console.WriteLine("\n**********************\n");

            Console.WriteLine("Testing Training sets\n");
            int i = 0;
            foreach (double[] t in trainingSets)
            {
                Console.WriteLine("Training Set " + i + " is" + "[" + t[0] + " , " + t[1] + "]");
                i++;
            }

            //plot initial points. 
            chartForm.PlotPoints("Points", trainingSets);
            chartForm.PlotPoints("Lines", clusterWeights);
            BeginTraining(); 
        }

        public void BeginTraining()
        {
            int iterationCount = 0;
            int localMin = 0; 
            
            while(learningRate > .001f && R >= 0)
            {
                if(iterationCount %10 != 1 )
                {
                    iterationCount++;
                    //for each training pattern
                    foreach (double[] d in trainingSets)
                    {//foreach value in d 
                        FindMinDistanceForSingleTrainingSet(d);
                    }
                    if (R == 0)
                    {
                        //do not update neightbors of J. 
                    }

                    ReduceLearningRate();
                }
               
            }

           
            R--; 
        }

        public void FindMinDistanceForSingleTrainingSet(double[] inputValue)
        {
            //for each of the clusters
            //we want to compute the eculdian (sp) distance that is closest to the training set

            double distance = 0;
            double shortestDistance = 10; //just set an arbitary shortest distance to start with
            int indexOfShortestWeight = 0; 
            int indexCounter = 0; 
            double[] winningSet = new double[2]; 
            //for each cluster, we need to get the weight arrays and use the vaules 
            foreach (double[] wValues in clusterWeights)
            {
                //for each node in the input vector, we need to do something like the following
                // (w1 - x1)^2 + (w2 - x2)^2
                double w1 = wValues[0];
                double w2 = wValues[1]; //i have the weight values now. 

                double x1 = inputValue[0];
                double x2 = inputValue[1];

                distance = Math.Pow(w1 - x1, 2) + Math.Pow(w2 - x2, 2); 
                if(distance < shortestDistance)
                {
                    shortestDistance = distance;
                    indexOfShortestWeight = indexCounter; //index of the weight vector that is closest to the input. 
                    winningSet = wValues; 
                }
                    indexCounter++; 
               
            }

            Console.WriteLine("Shortest weight set for [" + inputValue[0] + "," + inputValue[1] + "]" + " is [" + winningSet[0] + "," + winningSet[1] + "] at index " + indexOfShortestWeight); 
            //now that we have the index of the weight vector that is closest to the input vector x, we need to adjust the weights for w. 
        }


        public void ReduceLearningRate()
        {
            //reduces the .05 to .001 over 100 times
            learningRate -= .00499f; 
        }

    }
}
