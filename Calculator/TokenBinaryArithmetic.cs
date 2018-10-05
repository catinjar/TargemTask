using System;
using System.Collections.Generic;

namespace Calculator {
    class TokenBinaryArithmetic : TokenBinary {
        Func<double, double, double> op;

        public TokenBinaryArithmetic(Func<double, double, double> op)
            => this.op = op;

        public override void Eval(Stack<double> operands) {
            double right = operands.Pop();
            double left  = operands.Pop();
            operands.Push(op(left, right));
        }
    }
}
