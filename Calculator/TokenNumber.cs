using System.Collections.Generic;

namespace Calculator {
    public class TokenNumber : Token {
        private double number;

        public TokenNumber(double number)
            => this.number = number;

        public override void Eval(Stack<double> operands)
            => operands.Push(number);
    }
}
