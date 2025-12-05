using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.File;
using Project_1;

namespace Project_1 {
    internal class Program {

        // string JSONString = DataManagement.LoadData("hospitalData.json");

        static void Main(string[] args) {

            /* TODO LIST:
             
            Add users
            login system
            serialize data
            implement GUIs
            admin privelage
             
             */

            Hospital hospital = new Hospital();
            GUI.StartMenu();

            string fileName = "hospital.json";
            string jsonString = JsonSerializer.Serialize(hospital);
            // File.WriteAllText(fileName, jsonString);


            Console.ReadLine();
        }
    }
}