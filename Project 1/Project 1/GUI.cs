using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

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
            Console.WriteLine($"Welcome {emp.GetName()} {emp.GetSurName()}({emp.GetType().Name})!\n"); // this looks better here than ToString() override

            // create delegates for each page
            Dictionary<String, Delegate> link = new Dictionary<String, Delegate>();

            // adds delegates and arguments to dictionary

            // nurse and doctor commands
            if (emp.GetType().Name.ToString().ToLower() == "nurse" || emp.GetType().Name.ToString().ToLower() == "doctor") {
                link.Add("This months schedule", new Delegate(ThisMonthsSchedule));
            }

            // commands for all employees
            link.Add("Employee List", new Delegate(EmployeeList));
            link.Add("Employee Lookup", new Delegate(EmployeeLookUp));
            link.Add("Schedule Lookup", new Delegate(ScheduleLookUp));

            // admin commands
            if (emp.GetType().Name.ToString().ToLower() == "administrator") {
                link.Add("Employee Add", new Delegate(EmployeeAdd));
                link.Add("Employee Remove", new Delegate(EmployeeRemove));
                link.Add("Employee Edit", new Delegate(EmployeeEdit));
                link.Add("Schedule Edit", new Delegate(ScheduleEdit));
            }

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
            if (!int.TryParse(key.ToString(), out int result)) throw new Exception("Oops, something went wrong with parsing your int.");

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
            int i = 1;

            // fetch and display employee data
            hospital.GetEmployees().ForEach(emp => {
                if (emp.GetType().Name.ToString().ToLower() != "administrator") Console.WriteLine($"{i++}. {emp}");
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
                    if (emp.GetUsername().ToLower() == usernameInput.ToLower()) {
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
                    if (emp.GetUsername().ToLower() == userInput.ToLower()) {
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
            // SendKeys.SendWait("hello");
            string searchInput;
            bool usernameTaken;
            string nameInput;
            string surnameInput;
            long peselInput;
            string usernameInput;
            string passwordInput;
            int empTypeSelection;
            int specialtySelection;
            int pwzInput;
            Employee employee = null;
            Employee newEmployee;
            string[] employeeType = new string[] { "Administrator", "Doctor", "Nurse" };
            string[] specialties = new string[] { "cardiologist", "urologist", "neurologist", "laryngologist" };

            // repeat until employee found or user exits
            do {
                // dont accept empty input
                do {
                    // GUI element
                    Console.Clear();
                    Console.WriteLine("Employee edit Page\n");

                    // request data
                    Console.Write("Please enter the username of the user to be edited: ");
                    searchInput = Console.ReadLine();
                } while (searchInput == "");

                // fetch employee data
                hospital.GetEmployees().ForEach(emp => {
                    if (emp.GetUsername().ToLower() == searchInput.ToLower()) {
                        Console.WriteLine($"\nFound employee: {emp}");
                        employee = emp;
                    }
                });

            } while (employee == null);

            Console.Clear();
            Console.WriteLine($"Employee edit Page (editing: {employee})\n");

            // request data
            Console.Write("Please enter an employee name: ");
            SendKeys.SendWait($"{employee.GetName()}");
            nameInput = Console.ReadLine();

            Console.Write("Please enter an employee Surname: ");
            SendKeys.SendWait($"{employee.GetSurName()}");
            surnameInput = Console.ReadLine();

            string input;
            do {
                Console.Write("Please enter an employee pesel: ");
                SendKeys.SendWait($"{employee.GetPesel()}");
                input = Console.ReadLine();
            } while (!long.TryParse(input, out peselInput));

            // repeat until username is unique
            do {
                usernameTaken = false;
                Console.Write("Please enter an employee username: ");
                SendKeys.SendWait($"{employee.GetUsername()}");
                usernameInput = Console.ReadLine();

                // fetch employee data
                hospital.GetEmployees().ForEach(emp => {
                    if (emp.GetUsername().ToLower() == usernameInput.ToLower() && (emp.GetUsername() != employee.GetUsername())) {
                            Console.WriteLine($"\nUsername allready taken");
                        usernameTaken = true;
                    }
                });
            } while (usernameTaken);


            Console.Write("Please enter an employee password: ");
            SendKeys.SendWait($"{employee.GetPassword()}");
            passwordInput = Console.ReadLine();

            // use ReadInput() to choose employee type and specialty
            Console.WriteLine($"Please select employee type for {employee}:\n");
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
                    Console.WriteLine($"\nPlease select a specialty:\n");
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

                    if (employee.GetType() == newEmployee.GetType()) {
                        Console.WriteLine("\nNurse unchanged, transferring duties to edited doctor.");
                    }

                    break;
                default:
                    throw new Exception("Invalid employee type selection");
            }

            if (employee.GetType().Name == newEmployee.GetType().Name && employee.GetType().Name.ToString().ToLower() == "doctor") {
                Doctor oldDoc = (Doctor)employee;
                Doctor newDoc = (Doctor)newEmployee;
                // profession unchanged
                if (oldDoc.GetSpecialty() == newDoc.GetSpecialty()) {
                    oldDoc.SetName(nameInput);
                    oldDoc.SetSurName(surnameInput);
                    oldDoc.SetPesel(peselInput);
                    oldDoc.SetUsername(usernameInput);
                    oldDoc.SetPassword(passwordInput);
                    oldDoc.SetPWZ(newDoc.GetPWZ());

                    Console.Clear();
                    Console.WriteLine($"Employee edit Page\n");
                    Console.WriteLine($"Employee ({employee}) edited successfully,\n({newEmployee}) will be added to the database");
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                }
                // specialty changed
                if (oldDoc.GetSpecialty() != newDoc.GetSpecialty()) {
                    hospital.RemoveEmployee(employee);
                    hospital.AddEmployee(newEmployee);

                    Console.Clear();
                    Console.WriteLine($"Employee edit Page\n");
                    Console.WriteLine($"Employee ({employee}) edited and changed specialty successfully,\n({newEmployee}) will be added to the database");
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                }
            }

            // changed profession
            if (employee.GetType().Name != newEmployee.GetType().Name) {
                hospital.RemoveEmployee(employee);
                hospital.AddEmployee(newEmployee);
                Console.Clear();
                Console.WriteLine($"Employee edit Page\n");
                Console.WriteLine($"Employee ({employee}) edited and changed profession successfully,\n({newEmployee}) will be added to the database");
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
            }

            // return to start menu
            StartMenu(currentLogin);
        }

        private static void ScheduleEdit() {
            string userInput;
            Employee employee = null;

            // GUI
            Console.Clear();
            Console.WriteLine("Schedule Edit Page\n");

            // repeat until employee found or user exits
            do {
                userInput = ReadStringInput("Please enter a username to edit schedule for: ");
                employee = FetchEmployeeByUsername(userInput);

                // failed search
                if (employee == null) Console.WriteLine("\nEmployee not found.");
                // check if employee is Nurse or Doctor
                else if ((employee.GetType().Name.ToString().ToLower() == "administrator" || employee.GetType().Name.ToString().ToLower() == "nurse")) {
                    Console.WriteLine("\nEmployee is not a nurse or a doctor.");
                    employee = null;
                }
            } while (employee == null);

            // GUI
            Console.Clear();
            Console.WriteLine("Schedule Edit Page");

            Console.WriteLine($"\nSelect an action for: {employee}\n");
            
            Console.WriteLine("1. Add Duty\n2. Remove Duty");
            ConsoleKey input = Console.ReadKey().Key;
            
            if(input == ConsoleKey.D1) DutyAdd(employee);
            if(input == ConsoleKey.D2) DutyRemove(employee);

            // return to start menu
            StartMenu(currentLogin);
        }

        private static void DutyAdd(Employee emp) {
            string dayInput;
            string monthInput;
            string yearInput;
            DateTime dutyDate;
            bool validDate = false;

            // cast employee to correct type
            if (emp.GetType().Name.ToLower() == "doctor") emp = (Doctor)emp;
            if (emp.GetType().Name.ToLower() == "nurse") emp = (Nurse)emp;

            // GUI
            Console.Clear();
            Console.WriteLine("Duty add Page\n");

            // repeat until valid date input
            do {
                // data input loop
                do { dayInput = ReadStringInput("Please enter day: "); } while (dayInput == "");
                do { monthInput = ReadStringInput("Please enter month: "); } while (monthInput == "");
                do { yearInput = ReadStringInput("Please enter year: "); } while (yearInput == "");

                // try to add date, catch if something goes wrong
                try {
                    // parse date and validate
                    DateTime.TryParse($"{dayInput}/{monthInput}/{yearInput} 00:00:00 AM", out dutyDate);
                    if (DateTime.Now.CompareTo(dutyDate) != -1) throw new Exception("Date is not in the future");
                    validDate = true;

                    // check for 10 duties per month
                    int dutyCount = 0;
                    emp.GetDutyList().ForEach(duty => {
                        // same month and year
                        if (duty.GetDate().Month == dutyDate.Month && duty.GetDate().Year == dutyDate.Year) {
                            dutyCount++;
                            if(dutyCount >= 10) throw new Exception("Employee already has 10 duties this month and can not be assigned to any more duties.");
                        }
                    });

                    // check for duties the day before and after
                    emp.GetDutyList().ForEach(duty => {
                        if (dutyDate.ToFileTimeUtc() == duty.GetDate().ToFileTimeUtc()) throw new Exception("Employee already has a duty on the same day.");
                        if (dutyDate.ToFileTimeUtc() == duty.GetDate().AddDays(1).ToFileTimeUtc()) throw new Exception("Employee already has a duty on the previous day.");
                        if (dutyDate.ToFileTimeUtc() == duty.GetDate().AddDays(-1).ToFileTimeUtc()) throw new Exception("Employee already has a duty on the next day.");
                    });

                    // check for other employees of same type having duty on same day
                    hospital.GetEmployees().ForEach(otherEmp => {
                        // check other employees of same type
                        if (emp.GetType().Name.ToLower() == otherEmp.GetType().Name.ToLower() && emp.GetType().Name.ToLower() == "doctor") {
                            Doctor otherDoc = (Doctor)otherEmp;
                            Doctor doc = (Doctor)emp;
                            otherDoc.GetDutyList().ForEach(duty => {
                                if (dutyDate.ToFileTimeUtc() == duty.GetDate().ToFileTimeUtc() && doc.GetSpecialty() == otherDoc.GetSpecialty()) throw new Exception($"Another {otherDoc.GetSpecialty()} already has a duty on the same day.");
                            });
                        }
                    });

                    // successfully passed all other checks
                    emp.AddDuty(new Duty(dutyDate, emp));
                    Console.WriteLine($"\nAdd duty executed sucessfully");
                } catch (Exception e) {
                    Console.Clear();
                    Console.WriteLine("Duty add Page\n");

                    Console.WriteLine(e.Message);
                }
            } while (!validDate);

            // further action
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();

            // return to start menu
            StartMenu(currentLogin);
        }

        private static void DutyRemove(Employee emp) {
            int input;
            int page = 0;

            // cast employee to correct type
            if (emp.GetType().Name.ToLower() == "doctor") emp = (Doctor)emp;
            if (emp.GetType().Name.ToLower() == "nurse") emp = (Nurse)emp;

            do {
                // GUI
                Console.Clear();
                Console.WriteLine("Duty remove Page\n");
                Console.WriteLine("Select a duty to remove:\n");

                // display duties on pages
                Console.WriteLine("1. Previous Page");
                for (int i = 0; i < 7; i++) {
                    // + (page * 7) in index
                    if ((page * 7) + i >= emp.GetDutyList().Count()) 
                        Console.WriteLine($"{(i + 2)}. ");
                    else 
                        Console.WriteLine($"{(i + 2)}. {emp.GetDutyList()[(page * 7) + i]}");
                }
                Console.WriteLine("9. Next Page");

                input = ReadInput(9);

                // increment pages
                if (input == 1 && page > 0) page--;
                if (input == 9 && page < Math.Floor((decimal) emp.GetDutyList().Count() / 7)) page++;
            } while (input == 1 || input == 9);

            // remove selected duty
            // -2 to offset for UI and 0 index
            // page * 7 to offset
            emp.GetDutyList().RemoveAt((page * 7) + input - 2);
            Console.WriteLine($"\nDuty removed successfully");

            // further action
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();

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
                // GUI element
                Console.Clear();
                Console.WriteLine("Employee Lookup Page\n");

                userInput = ReadStringInput("Please enter a username to edit schedule for: ");
                employee = FetchEmployeeByUsername(userInput);

                // failed search
                if (employee == null) Console.WriteLine("\nEmployee not found.");

                Console.ReadKey();
            } while (employee == null);

            // return to start menu
            StartMenu(currentLogin);
        }

        private static void ScheduleLookUp() {
            Doctor doctor;
            Nurse nurse;
            String userInput;

            Console.Clear();
            Console.WriteLine("Schedule Lookup Page\n");

            // dont accept empty input
            do {
                // GUI element
                Console.Clear();
                Console.WriteLine("Schedule Lookup Page\n");

                // request data
                Console.Write("Please enter the username of the user to display the schedule for: ");
                userInput = Console.ReadLine();
            } while (userInput == "");

            // fetch employee data
            hospital.GetEmployees().ForEach(emp => {
                if (emp.GetUsername().ToLower() == userInput.ToLower()) {
                    // check if employee is Nurse or Doctor
                    if (emp.GetType().Name.ToString().ToLower() == "doctor") {
                        doctor = (Doctor)emp;

                        Console.WriteLine($"\nSchedule for {doctor}:\n");
                        // fetch and display employee data
                        doctor.GetDutyList().ForEach(duty => {
                            Console.WriteLine($"{doctor.GetDutyList().IndexOf(duty) + 1}. {duty}");
                        });
                    }
                    if (emp.GetType().Name.ToString().ToLower() == "nurse") {
                        nurse = (Nurse)emp;

                        Console.WriteLine($"\nSchedule for {nurse}:\n");
                        // fetch and display employee data
                        nurse.GetDutyList().ForEach(duty => {
                            Console.WriteLine($"{nurse.GetDutyList().IndexOf(duty) + 1}. {duty}");
                        });
                    }
                }
            });

            // further action
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();

            // return to start menu
            StartMenu(currentLogin);
        }

        private static void ThisMonthsSchedule() {
            Console.Clear();
            Console.WriteLine("This Months Schedule Page\n");

            Console.WriteLine($"\nSchedule for {currentLogin}:\n");
            // fetch and display employee data
            currentLogin.GetDutyList().ForEach(duty => {
                Console.WriteLine($"{currentLogin.GetDutyList().IndexOf(duty) + 1}. {duty}");
            });

            // further action
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();

            // return to start menu
            StartMenu(currentLogin);
        }

        private static Employee FetchEmployeeByUsername(string username) {
            Employee employee = null;

            // fetch employee data
            hospital.GetEmployees().ForEach(emp => {
                if (emp.GetUsername().ToLower() == username.ToLower()) {
                    Console.WriteLine($"\nFound employee: {emp}");
                    employee = emp;
                }
            });

            return employee;
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
