using System;

namespace Calculator {
    public class SyntaxException : Exception {
        public SyntaxException() {}

        public SyntaxException(string message)
            : base(message) {}
    }
}
