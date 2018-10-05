using Calculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests {
    [TestClass]
    public class CalculatorTests {
        [TestMethod]
        public void TestAdditive() {
            Assert.AreEqual("50 + 40".Eval(), 90);

            Assert.AreEqual("10 + 20 - 40 + 100".Eval(), 90);
        }

        [TestMethod]
        public void TestUnary() {
            Assert.AreEqual("-100".Eval(), -100);

            Assert.AreEqual("+100".Eval(), 100);

            Assert.AreEqual("---+--++--++-+100".Eval(), 100);

            Assert.AreEqual("-(--(+--+(((+--++-+100)))))".Eval(), 100);

            Assert.AreEqual("10 - -10".Eval(), 20);
        }

        [TestMethod]
        public void TestMultiplicative() {
            Assert.AreEqual("10 * 20".Eval(), 200);

            Assert.AreEqual("10 * -20".Eval(), -200);

            Assert.AreEqual("-10 * -20".Eval(), 200);

            Assert.AreEqual("10 / 20".Eval(), 0.5);

            Assert.AreEqual("10 * 20 / 50".Eval(), 4);
        }

        [TestMethod]
        public void TestOrder() {
            Assert.AreEqual("-((10 + 20) * 5) * 30".Eval(), -4500);

            Assert.AreEqual("2 ^ 10 + 2 ^ 2".Eval(), 1028);

            Assert.AreEqual("(2 ^ 10 + 2) ^ 2".Eval(), 1052676);
        }

        [TestMethod]
        public void TestBitwise() {
            Assert.AreEqual("12 & 25".Eval(), 8);

            Assert.AreEqual("12 | 25".Eval(), 29);

            Assert.AreEqual("12 # 25".Eval(), 21); // XOR
        }
    }
}
