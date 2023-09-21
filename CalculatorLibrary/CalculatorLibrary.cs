using Microsoft.VisualBasic;
using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        JsonWriter writer;
        List<string> latestCalculations = new List<string>();
        public Calculator() 
        {
            StreamWriter logFile = File.CreateText("calculatorlog.json");
            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();
        }

        public double DoOperation(double num1, double num2, string op)
        {
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
            writer.WriteStartObject();
            writer.WritePropertyName("Operand 1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operand 2");
            writer.WriteValue(num2);
            writer.WritePropertyName("Operation");

            // Use a switch statement to do the math.
            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    writer.WriteValue("Add");
                    break;
                case "s":
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
                    break;
                case "m":
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    break;
                case "d":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                        writer.WriteValue("Divide");
                    }
                    break;
                case "p":
                    result = Math.Pow(num1, num2);
                    writer.WriteValue("Power");
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            return result;
        }

        public double DoOperation(double num1, string op)
        {
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
            writer.WriteStartObject();
            writer.WritePropertyName("Operand");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operation");

            // Use a switch statement to do the math.
            switch (op)
            {
                case "sqrt":
                    result = Math.Sqrt(num1);
                    writer.WriteValue("Square Root");
                    break;
                case "sin":
                    result = Math.Sin(num1);
                    writer.WriteValue("Sin");
                    break;
                case "cos":
                    result = Math.Cos(num1);
                    writer.WriteValue("Cos");
                    break;
                case "tan":
                    result = Math.Tan(num1);
                    writer.WriteValue("Tangent");
                    break;
                case "cot":
                    result = 1 / Math.Tan(num1);
                    writer.WriteValue("Cotangent");
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            return result;
        }


        public void AddToList(double num1, double num2, string op, double result)
        {
            string calculation;
            string operatorr = string.Empty;

            if (op == "a") operatorr = "+";
            else if (op == "s") operatorr = "-";
            else if (op == "m") operatorr = "*";
            else if (op == "d") operatorr = "/";
            else if (op == "p") operatorr = "^";

            calculation = num1 + " " + operatorr + " " + num2 + " = " + Math.Round(result,2);
            latestCalculations.Add(calculation);
        }

        public void AddToList(double num1, string op, double result)
        {
            string calculation;
            string operatorr = string.Empty;

            if (op == "sqrt") operatorr = "Square Root of";
            else if (op == "sin") operatorr = "Sine of";
            else if (op == "cos") operatorr = "Cosine of";
            else if (op == "tan") operatorr = "Tangent of";
            else if (op == "cot") operatorr = "Cotangent of";

            calculation = operatorr + " " + num1 + " = " + Math.Round(result, 2);
            latestCalculations.Add(calculation);
        }

        public void ShowLatestCalculations()
        {
            int counter = 0;

            Console.WriteLine();
            foreach (string calculation in latestCalculations)
                Console.WriteLine($"Calculation #{++counter}: {calculation}");
        }

        public void DeleteLatestCalculations()
        {
            latestCalculations.Clear();
            Console.WriteLine("\nAll the calculations have been deleted. The list is now empty.");
        }

        public double ReturnACalculationResult(int operationNumber)
        {
            double result = 0;

            if (operationNumber > latestCalculations.Count || operationNumber <= 0)
                return double.NaN;

            for (int i = 0; i < latestCalculations.Count; i++)
            {
                if (i + 1 == operationNumber)
                {
                    result = Convert.ToDouble(latestCalculations[i].Substring(latestCalculations[i].IndexOf("=") + 1));
                }
            }

            return result;
        }

        public double ReturnANumber()
        {
            string numInput1 = "";

            numInput1 = Console.ReadLine();

            double cleanNum1 = 0;
            while (!double.TryParse(numInput1, out cleanNum1))
            {
                Console.Write("This is not valid input. Please enter an integer value: ");
                numInput1 = Console.ReadLine();
            }

            return cleanNum1;
        }

        public string ReturnAnOperation()
        {
            Console.WriteLine("\nChoose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\tsqrt - Square Root");
            Console.WriteLine("\tp - Power");
            Console.WriteLine("\tsin - Sine");
            Console.WriteLine("\tcos - Cosine");
            Console.WriteLine("\ttan - Tangent");
            Console.WriteLine("\tcot - Cotangent");
            Console.Write("Your option? ");

            string op = Console.ReadLine();

            return op;
        }

        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }
    }
}