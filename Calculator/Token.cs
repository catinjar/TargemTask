using System.Collections.Generic;

namespace Calculator {
    public abstract class Token {
        public abstract void Eval(Stack<double> operands);
    }
}
