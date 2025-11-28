using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_1;

namespace Project_1 {
    internal class Program {
        static void Main(string[] args) {

            Employee doc = new Doctor("Maja","Tiny", 28052611120, "MiniMaja", "hehe", "cardiologist", 1);

            Console.WriteLine(doc);


            Console.ReadLine();
        }
    }
}