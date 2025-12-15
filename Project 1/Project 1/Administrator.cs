using System;
using System.Runtime.CompilerServices;
using Project_1;

namespace Project_1 {
    [Serializable]
    public class Administrator : Employee {

        public Administrator(string name, string surname, long pesel, string username, string password) : base(name, surname, pesel, username, password) {
        }



        public override string ToString() {
            return $"Admin: {getName()} {getSurName()}";
        }
    }
}