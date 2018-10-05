using System;
using System.Collections.Generic;

namespace Calculator {
    public static class OperatorHelper {
        private static readonly Dictionary<TokenType, Token> operators = new Dictionary<TokenType, Token>() {
            [TokenType.Add       ] = new TokenBinaryArithmetic((a, b) => a + b)           ,
            [TokenType.Substract ] = new TokenBinaryArithmetic((a, b) => a - b)           ,
            [TokenType.Multiply  ] = new TokenBinaryArithmetic((a, b) => a * b)           ,
            [TokenType.Divide    ] = new TokenBinaryArithmetic((a, b) => a / CheckZero(b)),
            [TokenType.Modulus   ] = new TokenBinaryArithmetic((a, b) => a % CheckZero(b)),
            [TokenType.Pow       ] = new TokenBinaryArithmetic((a, b) => Math.Pow(a, b))  ,

            [TokenType.BitwiseAnd] = new TokenBinaryBitwise((a, b) => a & b),
            [TokenType.BitwiseOr ] = new TokenBinaryBitwise((a, b) => a | b),
            [TokenType.BitwiseXor] = new TokenBinaryBitwise((a, b) => a ^ b),

            [TokenType.Positive  ] = new TokenUnary(a =>  a),
            [TokenType.Negate    ] = new TokenUnary(a => -a)
        };


        public static Token GetOperator(TokenType tokenType)
            => operators[tokenType];

        public static bool HasOperator(TokenType tokenType)
            => operators.ContainsKey(tokenType);

        public static bool HasBinary(TokenType tokenType)
            => operators.ContainsKey(tokenType) && operators[tokenType] is TokenBinary;

        public static bool HasUnary(TokenType tokenType)
            => operators.ContainsKey(tokenType) && operators[tokenType] is TokenUnary;


        private static double CheckZero(double d)
            => d != 0 ? d : throw new DivideByZeroException("Divide by zero"); // Without check you get infinity
    }
}
