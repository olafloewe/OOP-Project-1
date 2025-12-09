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

        private static Hospital hospital;

        static void Main(string[] args) {

            /* TODO LIST:
             
            Add users
            login system
            serialize data
            implement GUIs
            admin privelage
             
             */

            
            BinaryFormatter formatter = new BinaryFormatter();
            
            // load data
            try{
                using (FileStream fs = new FileStream("data.txt", FileMode.Open, FileAccess.Read)){
                    hospital = (Hospital)formatter.Deserialize(fs);
                }
            }
            catch (FileNotFoundException){
                hospital = Hospital.GetHospital();
            }
            
            // hospital.AddEmployee(new Administrator("admin", "admin", 1, "admin", "admin"));
            // hospital.GetEmployees().ForEach(emp => Console.WriteLine(emp.getName()));

            Console.WriteLine($"emps in MAIN: {hospital.GetEmployees().Count()}");
            GUI.StartMenu();



            // save data
            using (FileStream fs = new FileStream("data.txt", FileMode.Create, FileAccess.Write)){
                formatter.Serialize(fs, hospital);
            }

            // HOLD CONSOLE OPEN
            Console.ReadLine();
        }
    }
}