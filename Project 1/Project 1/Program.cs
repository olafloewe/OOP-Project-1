using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using Project_1;

namespace Project_1 {
    [Serializable]
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



            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream("data.txt", FileMode.Create)){
                formatter.Serialize(fs, hospital);
            }



            Console.ReadLine();
        }
    }
}