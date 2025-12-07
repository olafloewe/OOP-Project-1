using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Project_1 {
    [Serializable]
    public class Hospital {

        private static Hospital instance;

        public List<Employee> employees = new List<Employee>();

        private Hospital() { }

        public static Hospital GetHospital() {
            if (instance == null) {
                instance = new Hospital();
            }
            return instance;
        }

        public void AddEmployee(Employee emp) {
            employees.Add(emp);
        }
    }
}