using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1 {
    internal class GUI {

        public GUI() {  
        
        }
         
        public static void StartMenu() {
            SelectionPage(new string[] { "argument 1", "argument 2", "argument 3", "argument 4" });
            Dictionary<String, Action> link = new Dictionary<String, Action>;
            link.Add("argument 1", DoSomething());
            
            SelectionPage(link); 

        }
         
        private delegate void DoSomething() { 
            
        };

        // builds a selection page
        private static void SelectionPage(string[] args) {

            // print options
            for (int i = 0; i < args.Length; i++) { 
                Console.WriteLine($"{i+1}. {args[i]}");
            }

            // ask for input
            int selection = ReadInput(args.Length);

            // display input
            Console.WriteLine($"\nselection made: {selection}");
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
