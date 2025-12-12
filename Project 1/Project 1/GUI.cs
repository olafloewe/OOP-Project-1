using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace Project_1 {
    [Serializable]
    internal class GUI {

        private delegate void Delegate();
        private static Hospital hospital = Hospital.GetHospital();
        private static Employee currentLogin;

        public GUI() { }

        // logs user into system, given the right data
        public static void Login() {
            string userInput;
            string passInput;
            Employee employee = null;

            // repeat until successful login
            do {
                // dont accept empty input
                do {
                    Console.Clear();
                    Console.WriteLine("Login Page\n");

                    // request data
                    Console.Write("Username: ");
                    userInput = Console.ReadLine();
                    Console.Write("Password: ");
                    passInput = Console.ReadLine();
                } while (userInput == "" || passInput == "");

                // verify login data
                hospital.GetEmployees().ForEach(emp => {
                    if (emp.Login(userInput, passInput)) {
                        employee = emp;
                    }
                });
            } while (employee == null);

            currentLogin = employee;
            StartMenu(employee);
        }

        // start menu after successful login to select options to proceed
        private static void StartMenu(Employee emp) {
            // welcome user
            Console.Clear();
            Console.WriteLine($"Welcome {emp.getName()} {emp.getSurName()}({emp.GetType().Name})!\n"); // this looks better here than ToString() override

            // create delegates for each page
            Dictionary<String, Delegate> link = new Dictionary<String, Delegate>();

            // adds delegates and arguments to dictionary
            // admin commands
            if (emp.GetType().Name.ToString().ToLower() == "administrator") {
                link.Add("Employee List", new Delegate(EmployeeList));
                link.Add("Employee Add", new Delegate(EmployeeAdd));
                link.Add("Employee Remove", new Delegate(EmployeeRemove));
                link.Add("Employee Edit", new Delegate(EmployeeEdit));
                link.Add("Schedule Edit", new Delegate(ScheduleEdit));
            }
            // nurse and doctor commands
            link.Add("This months schedule", new Delegate(ThisMonthsSchedule));
            link.Add("Employee Lookup", new Delegate(EmployeeLookUp));
            link.Add("Schedule Lookup", new Delegate(ScheduleLookUp));
            link.Add("Log out", new Delegate(LogOut));

            // asks user to select a page
            SelectionPage(link);
        }

        // builds a selection page and calls delegate
        private static void SelectionPage(Dictionary<String, Delegate> dict) {
            for (int i = 0; i < dict.Count(); i++) Console.WriteLine($"{i + 1}. {dict.Keys.ToArray()[i]}"); // display options
            int selection = ReadInput(dict.Count(), true); // ask for input
            dict.Values.ToArray()[selection - 1].Invoke(); // execute selected option
        }

        // read and return a key input stroke from the console to be used in user input for GUI
        private static int ReadInput(int inputRange, bool keyInput = false) {
            // only want selection from 1-9
            if (keyInput && inputRange > 9 || keyInput && inputRange < 1) throw new Exception("Invalid selection options");
            char[] inputChars = new char[inputRange + 1];

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
            if (!int.TryParse(key.ToString(), out result)) throw new Exception("Oops, something went wrong with parsing your int.");

            return result;
        }

        // read and return a string input from the console until input is not empty
        private static string ReadStringInput(string prompt) {
            string input;
            do {
                Console.Write(prompt);
                input = Console.ReadLine();
            } while (input == "");
            return input;
        }

        // ADMIN COMMANDS ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private static void EmployeeList() {
            Console.Clear();
            Console.WriteLine("Employee List Page\n");

            // fetch and display employee data
            hospital.GetEmployees().ForEach(emp => {
                // TODO incremental index display instead of index in list
                if (emp.GetType().Name.ToString().ToLower() != "administrator") Console.WriteLine($"{hospital.GetEmployees().IndexOf(emp) + 1}. {emp}");
            });

            // confirmation before returning
            Console.WriteLine("\nPress any button to continue!");
            Console.ReadKey();

            // return to start menu
            StartMenu(currentLogin);
        }

        // asks for employee data and adds them to the hospital system
        private static void EmployeeAdd() {
            Console.Clear();
            Console.WriteLine("Employee Add Page\n");

            bool usernameTaken;
            string nameInput;
            string surnameInput;
            long peselInput;
            string usernameInput;
            string passwordInput;
            int empTypeSelection;
            int specialtySelection;
            int pwzInput;
            Employee newEmployee;

            string[] employeeType = new string[] { "Administrator", "Doctor", "Nurse" };
            string[] specialties = new string[] { "cardiologist", "urologist", "neurologist", "laryngologist" };


            // request data
            nameInput = ReadStringInput("Please enter a employee name: ");
            surnameInput = ReadStringInput("Please enter a employee Surname: ");
            while (!long.TryParse(ReadStringInput("Please enter a employee pesel: "), out peselInput)) ;

            // repeat until username is unique
            do {
                usernameTaken = false;
                usernameInput = ReadStringInput("Please enter a employee username: ");

                // fetch employee data
                hospital.GetEmployees().ForEach(emp => {
                    if (emp.getUsername().ToLower() == usernameInput.ToLower()) {
                        Console.WriteLine($"\nUsername allready taken");
                        usernameTaken = true;
                    }
                });
            } while (usernameTaken);

            passwordInput = ReadStringInput("Please enter a employee password: ");

            // use ReadInput() to choose employee type and specialty
            Console.WriteLine("Please select employee type:\n");
            for (int i = 0; i < employeeType.Length; i++) {
                Console.WriteLine($"{i + 1}. {employeeType[i]}");
            }
            empTypeSelection = ReadInput(employeeType.Length, true);

            switch (empTypeSelection) {
                case 1:
                    newEmployee = new Administrator(nameInput, surnameInput, peselInput, usernameInput, passwordInput);
                    break;
                case 2:
                    // ask doctor specific data
                    Console.WriteLine("\nPlease select a specialty:\n");
                    for (int i = 0; i < specialties.Length; i++) {
                        Console.WriteLine($"{i + 1}. {specialties[i]}");
                    }
                    specialtySelection = ReadInput(specialties.Length, true);

                    // 7 didgit PWZ number
                    while (!int.TryParse(ReadStringInput("\nPlease enter a PWZ number: "), out pwzInput) || pwzInput < 0 || pwzInput > 9999999) ;

                    newEmployee = new Doctor(nameInput, surnameInput, peselInput, usernameInput, passwordInput, specialties[specialtySelection - 1], pwzInput);
                    break;
                case 3:
                    newEmployee = new Nurse(nameInput, surnameInput, peselInput, usernameInput, passwordInput);
                    break;
                default:
                    throw new Exception("Invalid employee type selection");
            }

            hospital.AddEmployee(newEmployee);

            // confirmation before returning
            Console.WriteLine($"Employee ({newEmployee}) added successfully, press any button to continue!");
            Console.ReadKey();

            // return to start menu
            StartMenu(currentLogin);
        }

        // remove employee by username
        private static void EmployeeRemove() {
            Console.Clear();
            Console.WriteLine("Employee Remove Page\n");

            string userInput;
            ConsoleKey key;
            Employee employee = null;
            bool confirmDelete = false;

            // repeat until employee found or user exits
            do {
                // dont accept empty input
                do {
                    // GUI element
                    Console.Clear();
                    Console.WriteLine("Employee Remove Page\n");

                    // request data
                    Console.Write("Please enter the username of the user to be removed: ");
                    userInput = Console.ReadLine();
                } while (userInput == "");

                // fetch employee data
                hospital.GetEmployees().ForEach(emp => {
                    if (emp.getUsername().ToLower() == userInput.ToLower()) {
                        Console.WriteLine($"\nFound employee: {emp}");
                        employee = emp;
                    }
                });

                // failed search
                if (employee == null) Console.WriteLine("\nEmployee not found.");

                // further action
                Console.WriteLine("\nEmployee deleted successfully, press any key to quit.");
                key = Console.ReadKey().Key;
                // delete employee here
                hospital.RemoveEmployee(employee);
                confirmDelete = true;

            } while (!confirmDelete);

            // return to start menu
            if (employee == currentLogin) LogOut();
            else
                StartMenu(currentLogin);
        }

        private static void EmployeeEdit() {
            Console.Clear();
            Console.WriteLine("Employee Edit Page\n");

            // return to start menu
            StartMenu(currentLogin);
        }

        private static void ScheduleEdit() {
            string userInput;
            Employee employee = null;

            // repeat until employee found or user exits
            do {
                // dont accept empty input
                do {
                    Console.Clear();
                    Console.WriteLine("Schedule Edit Page\n");

                    // request data
                    Console.Write("Please enter a username: ");
                    userInput = Console.ReadLine();
                } while (userInput == "");

                // fetch employee data
                hospital.GetEmployees().ForEach(emp => {
                    if (emp.getUsername().ToLower() == userInput.ToLower()) {
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


            // return to start menu
            StartMenu(currentLogin);
        }

        private static void DutyAdd(Employee emp) {
            Console.Clear();
            Console.WriteLine("Duty add Page\n");

            // return to start menu
            StartMenu(currentLogin);
        }

        private static void DutyRemove(Employee emp) {
            Console.Clear();
            Console.WriteLine("Duty remove Page\n");

            // return to start menu
            StartMenu(currentLogin);
        }

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        // looks up employee data based on username
        private static void EmployeeLookUp() {
            string userInput;
            ConsoleKey key;
            Employee employee = null;

            // repeat until employee found or user exits
            do {
                // dont accept empty input
                do {
                    // GUI element
                    Console.Clear();
                    Console.WriteLine("Employee Lookup Page\n");

                    // request data
                    Console.Write("Please enter a username: ");
                    userInput = Console.ReadLine();
                } while (userInput == "");

                // fetch employee data
                hospital.GetEmployees().ForEach(emp => {
                    if (emp.getUsername().ToLower() == userInput.ToLower()) {
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

        private static void ScheduleLookUp() {
            Console.Clear();
            Console.WriteLine("Schedule Lookup Page\n");

            // return to start menu
            StartMenu(currentLogin);
        }

        private static void ThisMonthsSchedule() {
            Console.Clear();
            Console.WriteLine("This Months Schedule Page\n");

            // return to start menu
            StartMenu(currentLogin);
        }

        // return to login page
        private static void LogOut() {
            BinaryFormatter formatter = new BinaryFormatter();

            // save data after logout
            using (FileStream fs = new FileStream("data.txt", FileMode.Create, FileAccess.Write)) {
                formatter.Serialize(fs, hospital);
            }

            // return to login page
            Login();
        }
    }
}
