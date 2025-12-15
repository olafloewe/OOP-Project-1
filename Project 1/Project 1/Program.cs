using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Project_1 {
    [Serializable]
    internal class Program {

        private static Hospital hospital;

        static void Main(string[] args) {

            /* TODO LIST:
             
            Add users
            implement GUIs
             
             */

            BinaryFormatter formatter = new BinaryFormatter();

            // load data
            try {
                using (FileStream fs = new FileStream("data.txt", FileMode.Open, FileAccess.Read)) {
                    hospital = (Hospital)formatter.Deserialize(fs);
                }
            } catch (FileNotFoundException) {
                Console.WriteLine("No data file found, starting new hospital");
                hospital = Hospital.GetHospital();
                hospital.AddEmployee(new Administrator("admin", "admin", 0, "admin", "admin"));
            }

            GUI.Login();

            // HOLD CONSOLE OPEN
            Console.ReadLine();
        }
    }
}