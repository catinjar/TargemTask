using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Calculator {
    public class Tokenizer {
        private readonly Dictionary<(char, bool unary), TokenType> tokenTypes = new Dictionary<(char, bool unary), TokenType>() {
             [('\0', false)] = TokenType.End              ,
             [('(',  false)] = TokenType.OpenParenthesis  ,
             [(')',  false)] = TokenType.CloseParenthesis ,
             [('+',  false)] = TokenType.Add              ,
             [('-',  false)] = TokenType.Substract        ,
             [('*',  false)] = TokenType.Multiply         ,
             [('/',  false)] = TokenType.Divide           ,
             [('^',  false)] = TokenType.Pow              ,
             [('%',  false)] = TokenType.Modulus          ,
             [('&',  false)] = TokenType.BitwiseAnd       ,
             [('|',  false)] = TokenType.BitwiseOr        ,
             [('#',  false)] = TokenType.BitwiseXor       , // '^' is more convenient for xor, but it's used for pow
             [('+',  true) ] = TokenType.Positive         ,
             [('-',  true) ] = TokenType.Negate           ,
             [('(',  true) ] = TokenType.OpenParenthesis    // For '((' cases
        };

        public TokenType PreviousToken { get; private set; }
        public TokenType Token         { get; private set; }
        public double    Number        { get; private set; }

        private StringReader reader;
        private char         currentChar;

        public Tokenizer(string expression) {
            reader = new StringReader(expression);

            NextChar();
            NextToken();
        }

        private void NextChar()
            => currentChar = reader.Peek() < 0 ? '\0' : (char)reader.Read();

        public void NextToken() {
            PreviousToken = Token;

            SkipWhitespaces();

            if (FindOperator()) return;
            if (FindNumber())   return;

            throw new SyntaxException($"Unexpected character: {currentChar}");
        }

        private void SkipWhitespaces() {
            while (char.IsWhiteSpace(currentChar)) {
                NextChar();
            }
        }

        private bool FindOperator() {
            bool unary =
                OperatorHelper.HasOperator(PreviousToken) ||
                PreviousToken == TokenType.OpenParenthesis ||
                PreviousToken == TokenType.None;

            var key = (currentChar, unary);

            if (tokenTypes.ContainsKey(key)) {
                Token = tokenTypes[key];
                NextChar();
                return true;
            }

            return false;
        }

        private bool FindNumber() {
            if (char.IsDigit(currentChar) || currentChar == '.') {
                var sb = new StringBuilder();
                bool hasDecimalPoint = false;

                while (char.IsDigit(currentChar) || (!hasDecimalPoint && currentChar == '.')) {
                    sb.Append(currentChar);
                    hasDecimalPoint = currentChar == '.';
                    NextChar();
                }

                Token = TokenType.Number;

                try {
                    Number = double.Parse(sb.ToString());
                }
                catch (FormatException) {
                    throw new FormatException($"Wrong number format: {sb.ToString()}");
                }

                return true;
            }

            return false;
        }
    }
}
