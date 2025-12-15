using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_1;

namespace Project_1 {
    [Serializable]
    public class Nurse : Employee {
        List<Duty> duties = new List<Duty>();

        public Nurse(string name, string surname, long pesel, string username, string password) : base(name, surname, pesel, username, password) {

        }

        public override string ToString() {
            return $"Nurse: {getName()} {getSurName()}";
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