using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GradDesc
{
    class Program
    {        
        static List<double> Xs = new List<double>();
        static List<double> Ys = new List<double>();
        static double theta0 = 0, theta1 = 0, theta2 = 0, alpha = 0.01, errorVal=0;
        static int numIterations = 5000;
        static void Main(string[] args)
        {
            Setup();
        }
        static void Setup()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray; // ~ a e s t h e t i c s ~
            Console.WriteLine("Write the file path to your data. Each line is considered an input/output pair.\nX and Y values must be separated by either a comma, space, OR tab (cannot be more than one).");
            Console.Write("\nExample Path: C:\\Users\\roddur.dasgupta\\Desktop\\data.txt\n\tPath: ");
            string path = Console.ReadLine();
            try
            {
                foreach (string line in File.ReadLines(path))
                {
                    char[] del = { ',', ' ', '\t' };
                    string[] vals = line.Split(del);
                    Xs.Add(Convert.ToDouble(vals[0].Trim()));
                    Ys.Add(Convert.ToDouble(vals[1].Trim()));
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("File not found. Please check your file path. Error details:");
                Console.WriteLine(e.Message);
                Console.WriteLine("Press enter to try again.");
                Console.ReadLine();
                Console.Clear();
                Setup();
            }
            catch (FormatException fe)
            {
                Console.WriteLine("Oops! Looks like your data may not be formatted correctly.\nX and Y values must be separated by either a comma, space, OR tab (cannot be more than one).");
                Console.WriteLine(fe.Message);
                Console.WriteLine("Press enter when you're ready to try again.");
                Console.ReadLine();
                Console.Clear();
                Setup();
            }
            Console.Write("Data successfully parsed.\nChoose a learning rate [You probably shouldn't exceed 0.01]: ");
            string response = Console.ReadLine();
            while (!double.TryParse(response, out alpha))
            {
                Console.Write("Input was not a double. Try again, choose a learning rate: ");
                response = Console.ReadLine();
            }
            Console.Write("Learning rate is {0}. Choose a # of iterations [For best results, a minimum of 5000 is recommended]: ", alpha);
            response = Console.ReadLine();
            while (!int.TryParse(response, out numIterations))
            {
                Console.Write("Input was not an integer. Try again, choose a number of iterations: ");
                response = Console.ReadLine();
            }
            Regression();       
        }
        static void Regression()
        {
            Console.WriteLine("Press enter to start gradient descent algorithm.");
            Console.ReadLine();

            double temp0 = 0, temp1 = 0, temp2 = 0;
            for (int i = 0; i < numIterations; i++)
            {
                errorVal = 0;
                for (int j = 0; j < Xs.Count; j++)
                {
                    temp0 = theta0 - alpha * error(Xs[j], Ys[j]);
                    temp1 = theta1 - alpha * error(Xs[j], Ys[j]) * Xs[j] / Xs.Max();
                    temp2 = theta2 - alpha * error(Xs[j], Ys[j]) * Xs[j] * Xs[j] / Math.Pow(Xs.Max(), 2);                    
                    theta0 = temp0;
                    theta1 = temp1;
                    theta2 = temp2;
                    errorVal += error(Xs[j], Ys[j]);
                }                
                Console.WriteLine("theta0: {0}\ttheta1: {1}\ntheta2: {2}\terror: {3}", theta0, theta1, theta2, errorVal);              
            }
            string bestmodel = "Best model: ";           
            bestmodel += (Math.Round(theta2) != 0) ? Math.Round(theta2) + "x^2 + " : "";
            bestmodel += (Math.Round(theta1) != 0) ? Math.Round(theta1) + "x + " : "";
            bestmodel += (Math.Round(theta0) != 0) ? Math.Round(theta0) + "" : "0";
            Console.WriteLine(bestmodel);
            Console.WriteLine("Average error rate: " + errorVal / Xs.Count);
            Console.WriteLine("Press enter if you want to apply linear regression to another data file!");
            Console.ReadLine();
            Setup();
        }

        static double error(double x, double y)
        { return hypothesis(x) - y; }
        
        static double hypothesis(double x)
        {return theta2 * Math.Pow(x, 2) + theta1 * x + theta0;}

    }
}
