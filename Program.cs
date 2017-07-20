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
                    if (!line.Substring(0, 1).Equals("*")) //for comments
                    {
                        char[] del = { ',', ' ', '\t' };
                        string[] vals = line.Split(del);
                        Xs.Add(Convert.ToDouble(vals[0].Trim()));
                        Ys.Add(Convert.ToDouble(vals[1].Trim()));
                    }
                }
            }
            catch (FileNotFoundException fnfe)
            {
                Console.WriteLine("File not found. Please check your file path. Error details:");
                Console.WriteLine(fnfe.Message);
                Console.WriteLine("Press enter to try again.");
                Console.ReadLine();
                Console.Clear();
                Setup();
            }
            catch (FormatException fe)
            {
                Console.WriteLine("Oops! Looks like your data may not be formatted correctly.\nX and Y values must be separated by either a comma, space, OR tab (cannot be more than one).\nFull error details:");
                Console.WriteLine(fe.Message);
                Console.WriteLine("Press enter when you're ready to try again.");
                Console.ReadLine();
                Console.Clear();
                Setup();
            }
            catch (DirectoryNotFoundException dnfe)
            {
                Console.WriteLine("Oops! The directory was not found. Perhaps there is a typo in your file path?\nHere is the full error message: ");
                Console.WriteLine(dnfe.Message);
                Console.WriteLine("Press enter when you're ready to try again.");
                Console.ReadLine();
                Console.Clear();
                Setup();
            }
            catch (Exception e)
            {
                Console.WriteLine("Okay I'm usually good with exceptions but I have no idea what you messed up.\nHere, Microsoft will tell you what you did wrong:");
                Console.WriteLine(e.Message);
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
            if (alpha > 0.3)
            {
                Console.WriteLine("Ok, a little big, don't you think? Let's just stick with 0.01.");
                alpha = 0.01;
            }
            if (alpha < 0.0000001)
            {
                Console.WriteLine("Ok, a little small, don't you think? Let's just stick with 0.000001.");
                alpha = 0.000001;
            }
            Console.Write("\nLearning rate is {0}. Choose a # of iterations [For best results, a minimum of 5000 is recommended]: ", alpha);
            response = Console.ReadLine();
            while (!int.TryParse(response, out numIterations))
            {
                Console.Write("Input was not an integer. Try again, choose a number of iterations: ");
                response = Console.ReadLine();
            }
            if (numIterations < 500)
            {
                Console.WriteLine(numIterations + " is a pretty small number of iterations. Let's stick with 1000.");
                numIterations = 1000;                
            }
            if (numIterations > 1000000)
            {
                Console.WriteLine("Capping number of iterations at a million.");
                numIterations = 1000000;
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
            theta2 = Math.Round(theta2);
            theta1 = Math.Round(theta1);
            theta0 = Math.Round(theta0);
            if (theta2 != 1) //for the aesthetics of x^2 instead of 1x^2
                bestmodel += (theta2 != 0) ? theta2 + "x^2 + " : ""; //exclude if not applicable
            else
                bestmodel += "x^2 + ";
            if (theta2 != 1)
                bestmodel += (theta1 != 0) ? theta1 + "x + " : "";
            else
                bestmodel += "x + ";            
            bestmodel += (theta0 != 0) ? theta0 + "" : "0";
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
