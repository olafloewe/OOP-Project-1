using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Project_1 {
    [Serializable]
    public class Hospital {
        private static Hospital hospital;
        private List<Employee> employees = new List<Employee>();

        private Hospital() { }

        public static Hospital GetHospital() {
            if (hospital == null) {
                hospital = new Hospital();
            }
            return hospital;
        }

        public List<Employee> GetEmployees() {
            return employees;
        }

        public void AddEmployee(Employee emp) {
            employees.Add(emp);
        }

        public override string ToString()
        {
            return "TESTING";
        }
    }
}