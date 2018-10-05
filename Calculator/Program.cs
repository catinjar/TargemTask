using System;

namespace Calculator {
    internal class CalculatorProgram {
        private static void Main() {
            Console.WriteLine("Enter the expression:");
            Console.Write(">>> ");

            string line;

            while ((line = Console.ReadLine()) != "quit") {
                if (line.Trim().Length > 0) {
                    try {
                        Console.WriteLine(line.Eval());
                    }
                    catch (Exception e) {
                        Console.WriteLine($"Error: {e.Message}");
                    }
                }

                Console.Write(">>> ");
            }
        }
    }
}
