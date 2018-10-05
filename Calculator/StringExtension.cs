namespace Calculator {
    public static class StringExtension {
        public static double Eval(this string expression)
            => Calculator.Eval(expression);
    }
}
