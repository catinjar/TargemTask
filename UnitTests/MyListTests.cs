using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyListLib;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests {
    [TestClass]
    public class MyListTests {
        [TestMethod]
        public void Test() {
            var list1 = new MyList<string>() {
                "This",
                "Is",
                "My",
                "List"
            };

            var list2 = new List<string>() {
                "This",
                "Is",
                "My",
                "List"
            };

            CollectionAssert.AreEquivalent(list1.ToList(), list2);

            Assert.AreEqual($"{list1[0]} {list1[-1]}", $"{list2[0]} {list2[list2.Count - 1]}"); // "This List"

            Assert.AreEqual(list1.IndexOf("My"), list1.IndexOf("My"));

            Assert.AreEqual(list1.IndexOf("My1"), list1.IndexOf("My1"));

            list1.Insert(0, "Added");
            list2.Insert(0, "Added");
            CollectionAssert.AreEquivalent(list1.ToList(), list2);

            list1.Insert(5, "Added");
            list2.Insert(5, "Added");
            CollectionAssert.AreEquivalent(list1.ToList(), list2);

            list1.RemoveAt(5);
            list2.RemoveAt(5);
            CollectionAssert.AreEquivalent(list1.ToList(), list2);

            list1.Remove("Added");
            list2.Remove("Added");
            CollectionAssert.AreEquivalent(list1.ToList(), list2);

            list1.Clear();
            list2.Clear();
            CollectionAssert.AreEquivalent(list1.ToList(), list2);
        }
    }
}
