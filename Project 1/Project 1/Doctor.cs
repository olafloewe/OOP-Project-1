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
        public int GetPWZ() => PWZ;
        public override List<Duty> GetDutyList() => duties;
        public string GetSpecialty() => specialty;

        public override void AddDuty(Duty duty) {
            duties.Add(duty);
        }

        public void EditData(string name, string surname, long pesel, string username, string password, string specialty, int PWZ){
            this.specialty = specialty;
            this.PWZ = PWZ;
            base.EditData(name, surname, pesel, username, password);
        }

        public override string ToString() {
            return $"Doctor: {GetName()} {GetSurName()}, specialty: {specialty.ToLower()}, PWZ: {PWZ}";
        }
    }
}