using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1 {
    internal class Hospital {

        public List<Employee> employees = new List<Employee>();

        public Hospital() {
               
        }

        public void AddEmployee(Employee emp) {
            employees.Add(emp);
        }
    }
}