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
    internal class GUI {

        private delegate void Delegate();
        private static Hospital hospital = Hospital.GetHospital();
        private static Employee currentLogin;

        public GUI() { }

        // logs user into system, given the right data
        public static void Login(){
            string userInput;
            string passInput;
            Employee employee = null;

            // repeat until successful login
            do{
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
                        employee = emp;
                    }
                });
            } while (employee == null);

            currentLogin = employee;
            StartMenu(employee);
        }

        // start menu after successful login to select options to proceed
        private static void StartMenu(Employee emp){
            // welcome user
            Console.Clear();
            Console.WriteLine($"Welcome {emp.getName()} {emp.getSurName()}({emp.GetType().Name})!\n"); // this looks better here than ToString() override

            // create delegates for each page
            Dictionary<String, Delegate> link = new Dictionary<String, Delegate>();

            // adds delegates and arguments to dictionary
            // admin commands
            if (emp.GetType().Name.ToString().ToLower() == "administrator") {
                link.Add("Employee Add", new Delegate(EmployeeAdd));
                link.Add("Employee Remove", new Delegate(EmployeeRemove));
                link.Add("Employee Edit", new Delegate(EmployeeEdit));
                link.Add("Schedule Edit", new Delegate(ScheduleEdit));
            }
            // nurse and doctor commands
            link.Add("Employee Lookup", new Delegate(EmployeeLookUp));
            link.Add("Schedule Lookup", new Delegate(ScheduleLookUp));
            link.Add("Log out", new Delegate(LogOut));

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

        private static void ScheduleEdit(){
            string userInput;
            Employee employee = null;

            // repeat until employee found or user exits
            do{
                // dont accept empty input
                do{
                    Console.Clear();
                    Console.WriteLine("Schedule Edit Page");

                    // request data
                    Console.Write("Please enter a username: ");
                    userInput = Console.ReadLine();
                } while (userInput == "");

                // fetch employee data
                hospital.GetEmployees().ForEach(emp => {
                    if (emp.getUsername().ToLower() == userInput.ToLower()){
                        Console.WriteLine($"\nFound employee: {emp}");
                        employee = emp;
                    }
                });

                // failed search
                if (employee == null) Console.WriteLine("\nEmployee not found.");
            } while (employee == null);

            // TODO
            // DUTY ADD 
            // DUTY REMOVE 
        }


        private static void DutyAdd(Employee emp){
            Console.Clear();
            Console.WriteLine("Duty add Page");
        }

        private static void DutyRemove(Employee emp){
            Console.Clear();
            Console.WriteLine("Duty remove Page");
        }

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        // looks up employee data based on username
        private static void EmployeeLookUp(){
            string userInput;
            ConsoleKey key;
            Employee employee = null;

            // repeat until employee found or user exits
            do{
                // dont accept empty input
                do{
                    // GUI element
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
                        employee = emp;
                    }
                });

                // failed search
                if (employee == null) Console.WriteLine("\nEmployee not found.");

                // further action
                Console.WriteLine("\nPress 1. to search again or any other key to quit.");
                key = Console.ReadKey().Key;

            } while (key == ConsoleKey.D1);

            // return to start menu
            StartMenu(currentLogin);
        }

        private static void ScheduleLookUp(){
            Console.Clear();
            Console.WriteLine("Schedule Lookup Page");
        }
 
        // return to login page
        private static void LogOut(){

            BinaryFormatter formatter = new BinaryFormatter();

            // save data
            using (FileStream fs = new FileStream("data.txt", FileMode.Create, FileAccess.Write)){
                formatter.Serialize(fs, hospital);
            }

            Login();
        }
    }
}
