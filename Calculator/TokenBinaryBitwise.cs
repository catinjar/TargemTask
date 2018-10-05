using System;
using System.Collections.Generic;

namespace Calculator {
    public class TokenBinaryBitwise : TokenBinary {
        Func<int, int, int> op;

        public TokenBinaryBitwise(Func<int, int, int> op)
            => this.op = op;

        public override void Eval(Stack<double> operands) {
            double right = operands.Pop();
            double left  = operands.Pop();

            bool IsInteger(double d)
                => Math.Abs(d % 1) <= Double.Epsilon * 100;

            // Bitwise operations make sense only for integer numbers so we need to check
            if (IsInteger(left) && IsInteger(right)) {
                operands.Push(op((int)left, (int)right));
            }
            else {
                throw new SyntaxException($"Bitwise operations work only with integers: {left}, {right}");
            }
        }
    }
}
