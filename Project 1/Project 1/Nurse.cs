using System;
using System.Collections.Generic;

namespace Project_1 {
    [Serializable]
    public class Nurse : Employee {
        List<Duty> duties = new List<Duty>();

        public Nurse(string name, string surname, long pesel, string username, string password) : base(name, surname, pesel, username, password) {

        }

        public override List<Duty> GetDutyList() => duties;

        public override string ToString() {
            return $"Nurse: {GetName()} {GetSurName()}";
        }

        public override void AddDuty(Duty duty) {
            duties.Add(duty);
        }
    }
}