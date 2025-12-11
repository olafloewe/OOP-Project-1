using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
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

            // hospital.AddEmployee(new Nurse("Maja", "Mini", 123, "MINI", "mini"));

            GUI.Login();



            // save data
            using (FileStream fs = new FileStream("data.txt", FileMode.Create, FileAccess.Write)){
                formatter.Serialize(fs, hospital);
            }

            // HOLD CONSOLE OPEN
            Console.ReadLine();
        }
    }
}