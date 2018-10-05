using System;
using System.Collections.Generic;

namespace Calculator {
    /// <summary>
    /// Converts infix expression into Reverse Polish Notation using Dijkstra's Shunting-yard algorithm
    /// </summary>
    public class Parser {
        private static readonly Dictionary<TokenType, int> precedence = new Dictionary<TokenType, int>() {
            [TokenType.OpenParenthesis] =  -1,
            [TokenType.BitwiseOr     ]  =   1,
            [TokenType.BitwiseXor    ]  =   2,
            [TokenType.BitwiseAnd    ]  =   3,
            [TokenType.Add           ]  =   4, [TokenType.Substract] = 4,
            [TokenType.Multiply      ]  =   5, [TokenType.Divide]    = 5, [TokenType.Modulus] = 5,
            [TokenType.Pow           ]  =   6,
        };

        private Tokenizer tokenizer;
        private Queue<Token> output;
        private Stack<TokenType> operators;

        public Parser(string infixExpression) {
            tokenizer = new Tokenizer(infixExpression);
            output    = new Queue<Token>();
            operators = new Stack<TokenType>();
        }

        public Queue<Token> Parse() {
            while (tokenizer.Token != TokenType.End) {
                switch (tokenizer.Token) {
                    case TokenType.Number:
                        ParseNumber();
                        break;
                    case TokenType.OpenParenthesis:
                        ParseOpenParenthesis();
                        break;
                    case TokenType.CloseParenthesis:
                        ParseCloseParenthesis();
                        break;
                    default:
                        ParseUnary();
                        ParseBinary();
                        break;
                }

                tokenizer.NextToken();
            }

            // We can't enqueue '(' so we get exception if it doesn't have a pair
            try {
                while (operators.Count != 0) {
                    EnqueueOperator();
                }
            }
            catch (KeyNotFoundException) {
                throw new SyntaxException("Missing close parenthesis");
            }

            return output;
        }

        private void ParseNumber() {
            output.Enqueue(new TokenNumber(tokenizer.Number));

            // Need to enqueue unary operators after a number
            while (operators.Count != 0 && OperatorHelper.HasUnary(operators.Peek())) {
                EnqueueOperator();
            }
        }

        private void ParseOpenParenthesis() {
            operators.Push(tokenizer.Token);
        }

        private void ParseCloseParenthesis() {
            // If there is no '(' on the stack we get an exception
            try {
                while (operators.Peek() != TokenType.OpenParenthesis) {
                    EnqueueOperator();
                }
            }
            catch (InvalidOperationException) {
                throw new SyntaxException("Missing open parenthesis");
            }

            operators.Pop(); // Pop '('

            // Need to enqueue unary operators after a pair of parenthesis
            while (operators.Count != 0 && OperatorHelper.HasUnary(operators.Peek())) {
                EnqueueOperator();
            }
        }

        private void ParseBinary() {
            if (OperatorHelper.HasBinary(tokenizer.Token)) {
                int currentPrecedence = precedence[tokenizer.Token];

                while (operators.Count != 0 && currentPrecedence <= precedence[operators.Peek()]) {
                    EnqueueOperator();
                }

                operators.Push(tokenizer.Token);
            }
        }

        private void ParseUnary() {
            // We only have '+' and '-' as unary operators so we don't need to check precedence and pop from stack
            if (OperatorHelper.HasUnary(tokenizer.Token)) {
                operators.Push(tokenizer.Token);
            }
        }

        private void EnqueueOperator()
            => output.Enqueue(OperatorHelper.GetOperator(operators.Pop()));
    }
}
