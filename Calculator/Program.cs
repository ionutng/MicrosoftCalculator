using CalculatorLibrary;

namespace CalculatorProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            int counter = 0;
            double cleanNum2;

            // Display title as the C# console calculator app.
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------");

            Calculator calculator = new Calculator();

            while (!endApp)
            {
                cleanNum2 = double.NaN;

                // Declare variables and set to empty.
                double result;

                // Ask the user to choose an operator.
                string op = calculator.ReturnAnOperation();

                // Ask the user to type the numbers.
                Console.Write("Type a number, and then press Enter: ");
                double cleanNum1 = calculator.ReturnANumber();

                if (op.Trim().ToLower() == "a" || 
                    op.Trim().ToLower() == "s" || 
                    op.Trim().ToLower() == "m" || 
                    op.Trim().ToLower() == "d" || 
                    op.Trim().ToLower() == "p")
                {
                    Console.Write("Type another number, and then press Enter: ");
                    cleanNum2 = calculator.ReturnANumber();
                }

                try
                {
                    if (double.IsNaN(cleanNum2))
                        result = calculator.DoOperation(cleanNum1, op);
                    else
                        result = calculator.DoOperation(cleanNum1, cleanNum2, op);

                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else
                    {
                        Console.WriteLine("Your result: {0:0.##}\n", result);
                        counter++;
                        if (double.IsNaN(cleanNum2))
                            calculator.AddToList(cleanNum1, op, result);
                        else
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
                bool endLoop1 = false;
                calculator.ShowLatestCalculations();

                // Keep running until the list is deleted or the user chooses to stop the calculator.
                while (!endLoop1)
                {
                    bool endLoop2 = false;
                    Console.WriteLine("\nDo you wish to delete the list?");
                    Console.Write("Press 'y' and Enter if you wish to do so, or press any other key and Enter if you wish to keep the list: ");

                    if (Console.ReadLine().Trim().ToLower() == "y")
                    {
                        calculator.DeleteLatestCalculations();
                        endLoop1 = true;
                    }
                    else
                    {
                        while (!endLoop2)
                        {
                            Console.WriteLine("\nDo you wish to use a previous result and continue a calculation?");
                            Console.Write("Press 'y' and Enter if you wish to do so, or press any other key and Enter if you don't: ");

                            if (Console.ReadLine().Trim().ToLower() == "y")
                            {
                                double secondNumber = double.NaN;
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

                                // Ask the user to choose an operation
                                string op = calculator.ReturnAnOperation();

                                if (op.Trim().ToLower() == "a" ||
                                    op.Trim().ToLower() == "s" ||
                                    op.Trim().ToLower() == "m" ||
                                    op.Trim().ToLower() == "d" ||
                                    op.Trim().ToLower() == "p")
                                {
                                    // Ask the user for a second number
                                    Console.Write("Type the second number, and then press Enter: ");
                                    secondNumber = calculator.ReturnANumber();
                                }

                                double result;

                                try
                                {
                                    if (double.IsNaN(secondNumber))
                                        result = calculator.DoOperation(firstNumber, op);
                                    else
                                        result = calculator.DoOperation(firstNumber, secondNumber, op);

                                    if (double.IsNaN(result))
                                    {
                                        Console.WriteLine("This operation will result in a mathematical error.\n");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Your result: {0:0.##}\n", result);
                                        counter++;
                                        if (double.IsNaN(secondNumber))
                                            calculator.AddToList(firstNumber, op, result);
                                        else
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
                                endLoop2 = true;
                        }
                    }

                    Console.WriteLine("\nDo you wish to stop the calculator?");
                    Console.Write("Press 'y' and Enter if you wish to do so, or press any other key and Enter if you don't: ");

                    // Ending the program.
                    if (Console.ReadLine().Trim().ToLower() == "y")
                        endLoop1 = true;
                }
            }

            // Add call to close the JSON writer before return.
            calculator.Finish();
            return;
        }
    }
}