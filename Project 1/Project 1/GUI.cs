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

            // adds delegates and arguments to dictionary
            link.Add("Login", login);
            link.Add("Employee Lookup", employeeLookUp);
            link.Add("Schedule Lookup", scheduleLookUp);

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

        // TODO implement GUI pages

        // TO BE CALLED BY DELEGATES ========================================================================================================================================== 

        // ADMIN COMMANDS ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

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

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        // logs user into system, given the right data
        private static void Login(){
            string userInput;
            string passInput;

            // dont accept empty input
            do{
                Console.Clear();
                Console.WriteLine("Login Page");

                // request data
                Console.Write("Username: ");
                userInput = Console.ReadLine();
                Console.Write("Password: ");
                passInput = Console.ReadLine();
            } while (userInput == "" || passInput == "");

            // verify login data
            hospital.GetEmployees().ForEach(emp => {
                if (emp.Login(userInput, passInput)){
                    Console.WriteLine($"Login as ({emp}) Successful!");
                }
            });
        }

        // looks up employee data based on username
        private static void EmployeeLookUp(){
            string userInput;
            bool found = false;

            // dont accept empty input
            do
            {
                Console.Clear();
                Console.WriteLine("Employee Lookup Page");

                // request data
                Console.Write("Please enter a username: ");
                userInput = Console.ReadLine();
            } while (userInput == "");

            // fetch employee data
            hospital.GetEmployees().ForEach(emp => {
                if (emp.getUsername().ToLower() == userInput.ToLower()){
                    Console.WriteLine($"\nFound employee: {emp}");
                    found = true;
                }
            });

            if(!found) Console.WriteLine("\nEmployee not found.");
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
