using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1 {
    internal class GUI {

        private delegate void Delegate();

        public GUI() {  
        
        }
         
        public static void StartMenu(){
            Dictionary<String, Delegate> link = new Dictionary<String, Delegate>();
            // create delegates for each page
            Delegate login = new Delegate(Login);
            Delegate employeeLookUp = EmployeeLookUp;
            Delegate scheduleLookUp = ScheduleLookUp;

            // adds delegates and arguments to dictionary
            link.Add("Login", login);
            link.Add("Employee Lookup", employeeLookUp);
            link.Add("Schedule Loopup", scheduleLookUp);

            // asks user to select a page
            SelectionPage(link);
        }

        private static void Login(){
            Console.WriteLine("Login Page");
        }

        private static void EmployeeLookUp(){
            Console.WriteLine("Employee Lookup Page");
        }

        private static void ScheduleLookUp(){
            Console.WriteLine("Schedule Lookup Page");
        }

        // builds a selection page
        private static void SelectionPage(Dictionary<String, Delegate> dict) {
            
            // print options
            for (int i = 0; i < dict.Count() ; i++) Console.WriteLine($"{i+1}. {dict.Keys.ToArray()[i]}");

            // ask for input
            int selection = ReadInput(dict.Count());

            // execute selected option
            dict.Values.ToArray()[selection].Invoke();
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
    }
}
