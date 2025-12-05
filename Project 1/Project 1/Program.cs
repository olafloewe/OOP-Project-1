using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_1;

namespace Project_1 {
    internal class Program {
        static void Main(string[] args) {

            /* TODO LIST:
             
            Add users
            login system
            serialize data
            GUI for:
                employee lookup
                schedule lookup
                add employee
                remove employee
                edit employee
                add schedule
                remove schedule
                edit schedule 
             admin privelage
             
             */

            Hospital hospital = new Hospital();
            GUI.StartMenu();

            Console.ReadLine();
        }
    }
}