using System;

namespace Project_1 {
    [Serializable]
    public class Duty {

        DateTime date;
        Employee staff;

        public Duty(DateTime date, Employee staff) {
            this.date = date;
            this.staff = staff;
        }

        public override string ToString() {
            return $"{staff} | {date.ToShortDateString()}";
        }

        public DateTime GetDate() {
            return date;
        }
        public void SetDate(DateTime date) {
            this.date = date;
        }
        public Employee GetEmployee() {
            return staff;
        }
        public void SetEmployee(Employee staff) {
            this.staff = staff;
        }
    }
}