using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_1 {
    [Serializable]
    public class Doctor : Employee {

        readonly string[] specialties = new string[] { "cardiologist", "urologist", "neurologist", "laryngologist" };
        List<Duty> duties = new List<Duty>();
        int PWZ;
        string specialty;

        public Doctor(string name, string surname, long pesel, string username, string password, string specialty, int PWZ) : base(name, surname, pesel, username, password) {
            if (!specialties.Contains(specialty.ToLower())) throw new Exception("Invalid specialty");

            this.PWZ = PWZ;
            this.specialty = specialty;
        }

        public override string ToString() {
            return $"Doctor: {GetName()} {GetSurName()}, specialty: {specialty.ToLower()}, PWZ: {PWZ}";
        }

        public void AddDuty(Duty duty) {
            // TODO
            // check if duty date is not overlapping with existing duties by a day
            // check if duty date is not in the past
            // check if no same specialty doctor is on duty that day
            // check if no more than 10 duties assigned in a month
            duties.Add(duty);
        }

        public List<Duty> DutyList() {
            return duties;
        }
    }
}