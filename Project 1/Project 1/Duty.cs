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
    }
}