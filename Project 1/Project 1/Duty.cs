using System;
using Project_1;

namespace Project_1 {
    public class Duty {

        DateTime date;
        Employee staff;

        public Duty(DateTime date, Employee staff) {
            this.date = date;
            this.staff = staff;
        }
    }
}