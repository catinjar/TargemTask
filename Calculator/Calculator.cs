using System.Collections.Generic;

namespace Calculator {
    public class Calculator {
        public static double Eval(string expression) {
            var parser = new Parser(expression);
            var tokens = parser.Parse();

            var operands = new Stack<double>();

            while (tokens.Count != 0) {
                var token = tokens.Dequeue();
                token.Eval(operands);
            }

            return operands.Peek();
        }
    }
}
