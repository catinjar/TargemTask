using System;

namespace MyList {
    internal class MyListProgram {
        private static void Main(string[] args) {
            // Some MyList testing

            var list = new MyList<string>() {
                "This",
                "Is",
                "My",
                "List"
            };

            void PrintList() {
                foreach (var str in list) {
                    Console.WriteLine(str);
                }
                Console.WriteLine("-----");
            }

            PrintList();

            Console.WriteLine($"{list[0]} {list[-1]}");
            Console.WriteLine(list.IndexOf("My"));
            Console.WriteLine(list.IndexOf("My1"));
            Console.WriteLine("-----");

            list.Insert(0, "Added");
            list.Insert(5, "Added");

            list.RemoveAt(5);
            list.Remove("Added");

            PrintList();

            list.Clear();

            PrintList();

            Console.ReadKey();
        }
    }
}
