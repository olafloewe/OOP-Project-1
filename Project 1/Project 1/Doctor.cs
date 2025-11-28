using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_1;

namespace Project_1 {
    public class Doctor : Employee {

        string[] specialities = new string[] { "cardiologist", "urologist", "neurologist", "laryngologist" };
        List<Duty> duties = new List<Duty>();
        int PWZ;
        string speciality;

        public Doctor(string name, string surname, long pesel, string username, string password, string speciality, int PWZ) {
            if (!specialities.Contains(speciality.ToLower())) throw new Exception("Invalid speciality");

            this.setName(name);
            this.setSurName(surname);
            this.setPessel(pesel);
            this.setUsername(username);
            this.setPassword(password);
            this.PWZ = PWZ;
            this.speciality = speciality;
        }

        public override string ToString() {
            return $"Doctor: {getName()} {getSurName()}, Speciality: {speciality.ToLower()}, PWZ: {PWZ}";
        }

        public void AddDuty(Duty duty) {
            // TODO
            // check if duty date is not overlapping with existing duties by a day
            // check if duty date is not in the past
            // check if no same speciality doctor is on duty that day
            // check if no more than 10 duties assigned in a month
            duties.Add(duty);
        }

        public List<Duty> DutyList() { 
            return duties;
        }
    }
}