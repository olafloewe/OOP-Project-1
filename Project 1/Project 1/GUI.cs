using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Project_1 {
    [Serializable]
    internal class GUI {

        private delegate void Delegate();
        private static Hospital hospital = Hospital.GetHospital();

        public GUI() { }
         
        public static void StartMenu(){
            Dictionary<String, Delegate> link = new Dictionary<String, Delegate>();
            // create delegates for each page
            Delegate login = Login;
            Delegate employeeLookUp = EmployeeLookUp;
            Delegate scheduleLookUp = ScheduleLookUp;


            Console.WriteLine($"emps in GUI: {hospital.GetEmployees().Count()}");

            // adds delegates and arguments to dictionary
            link.Add("Login", login);
            link.Add("Employee Lookup", employeeLookUp);
            link.Add("Schedule Loopup", scheduleLookUp);

            // asks user to select a page
            SelectionPage(link);
        }

        // builds a selection page and calls delegate
        private static void SelectionPage(Dictionary<String, Delegate> dict) {
            for (int i = 0; i < dict.Count() ; i++) Console.WriteLine($"{i+1}. {dict.Keys.ToArray()[i]}"); // display options
            int selection = ReadInput(dict.Count()); // ask for input
            dict.Values.ToArray()[selection-1].Invoke(); // execute selected option
        }

        // read and return a key input stroke from the console to be used in user input for GUI
        private static int ReadInput(int inputRange) {
            // only want selection from 1-9
            if (inputRange > 9 || inputRange < 1) throw new Exception("Invalid selection options");
            char[] inputChars = new char[inputRange+1];
            
            // generate inputChars
            for (int i = 1; i < inputChars.Length; i++) {
                inputChars[i] = i.ToString()[0];
            }

            // read input
            char key;
            do {
                key = Console.ReadKey().KeyChar;
            } while (!inputChars.Contains(key));

            // parse key to string
            int result;
            if(!int.TryParse(key.ToString(), out result)) throw new Exception("Oops, something went wrong with parsing your int.");

            return result;
        }

        // TO BE CALLED BY DELEGATES ========================================================================================================================================== 

        // TODO implement GUI pages

        private static void Login(){
            Console.Clear();
            Console.WriteLine("Login Page");
            
            // request data
            Console.Write("Username: ");
            string userInput = Console.ReadLine();
            Console.Write("Password: ");
            string passInput = Console.ReadLine();

            Console.WriteLine($"Input: {userInput} {passInput}");

            // verify data
            hospital.GetEmployees().ForEach(emp => Console.WriteLine(emp.getName()));

            Hospital.GetHospital().GetEmployees().ForEach(emp => {
                Console.WriteLine(emp.ToString());
                if (emp.Login(userInput, passInput)){
                    Console.WriteLine("Login Successful");
                }
            });
        }

        private static void EmployeeAdd(){
            Console.Clear();
            Console.WriteLine("Employee Add Page");
        }

        private static void EmployeeRemove(){
            Console.Clear();
            Console.WriteLine("Employee Remove Page");
        }

        private static void EmployeeEdit(){
            Console.Clear();
            Console.WriteLine("Employee Edit Page");
        }

        private static void EmployeeLookUp(){
            Console.Clear();
            Console.WriteLine("Employee Lookup Page");
        }

        private static void ScheduleEdit(){
            Console.Clear();
            Console.WriteLine("Schedule Edit Page");
            // DUTY ADD 
            // DUTY REMOVE
        }

        private static void ScheduleLookUp(){
            Console.Clear();
            Console.WriteLine("Schedule Lookup Page");
        }


        // TO BE CALLED BY DELEGATES ==========================================================================================================================================
    }
}
