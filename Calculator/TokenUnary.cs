using System;
using System.Collections.Generic;

namespace Calculator {
    public class TokenUnary : Token {
        Func<double, double> op;

        public TokenUnary(Func<double, double> op)
            => this.op = op;

        public override void Eval(Stack<double> operands) {
            double value = operands.Pop();
            operands.Push(op(value));
        }
    }
}
