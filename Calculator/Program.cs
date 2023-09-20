using CalculatorLibrary;

namespace CalculatorProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            int counter = 0;
            // Display title as the C# console calculator app.
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            Calculator calculator = new Calculator();

            while (!endApp)
            {
                // Declare variables and set to empty.
                double result = 0;

                // Ask the user to type the numbers.
                Console.Write("Type a number, and then press Enter: ");
                double cleanNum1 = calculator.ReturnANumber();

                Console.Write("Type another number, and then press Enter: ");
                double cleanNum2 = calculator.ReturnANumber();


                // Ask the user to choose an operator.
                string op = calculator.ReturnAnOperation();

                try
                {
                    result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else
                    {
                        Console.WriteLine("Your result: {0:0.##}\n", result);
                        counter++;
                        calculator.AddToList(cleanNum1, cleanNum2, op, result);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }

                Console.WriteLine("------------------------\n");

                // Wait for the user to respond before closing.
                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n")
                    endApp = true;

                Console.WriteLine(); // Friendly linespacing.
            }

            if (counter == 1)
                Console.WriteLine($"The calcualtor was used {counter} time.");
            else
                Console.WriteLine($"The calcualtor was used {counter} times.");

            // If the calculator was used.
            if (counter > 0)
            {
                bool endLoop = false;
                calculator.ShowLatestCalculations();

                Console.WriteLine("\nDo you wish to delete the list?");
                Console.Write("Press 'y' and Enter if you wish to do so, or press any other key and Enter if you wish to keep the list: ");

                if (Console.ReadLine().Trim().ToLower() == "y")
                    calculator.DeleteLatestCalculations();
                else
                {
                    while (!endLoop)
                    {
                        Console.WriteLine("\nDo you wish to use a previous result and continue a calculation?");
                        Console.Write("Press 'y' and Enter if you wish to do so, or press any other key and Enter if you don't: ");

                        if (Console.ReadLine().Trim().ToLower() == "y")
                        {
                            // Take the calculation number from the user and store it in a value;
                            Console.Write("\nType the number of the calculation that you want, and then press Enter: ");
                            double calculationNumber = calculator.ReturnANumber();

                            // Get the result number
                            double firstNumber = calculator.ReturnACalculationResult(Convert.ToInt32(calculationNumber));

                            if (double.IsNaN(firstNumber))
                            {
                                Console.WriteLine("The number of the calculation was incorrect.");
                                break;
                            }

                            Console.WriteLine($"\nYou have chosen operation number {calculationNumber} and the first number is {firstNumber}.");

                            // Ask the user for a second number
                            Console.Write("Type the second number, and then press Enter: ");
                            double secondNumber = calculator.ReturnANumber();

                            // Ask the user to choose an operation
                            string op = calculator.ReturnAnOperation();

                            double result;

                            try
                            {
                                result = calculator.DoOperation(firstNumber, secondNumber, op);
                                if (double.IsNaN(result))
                                {
                                    Console.WriteLine("This operation will result in a mathematical error.\n");
                                }
                                else
                                {
                                    Console.WriteLine("Your result: {0:0.##}\n", result);
                                    counter++;
                                    calculator.AddToList(firstNumber, secondNumber, op, result);
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                            }

                            calculator.ShowLatestCalculations();
                        }
                        else
                            endLoop = true;
                    }
                }
            }

            // Add call to close the JSON writer before return.
            calculator.Finish();
            return;
        }
    }
}