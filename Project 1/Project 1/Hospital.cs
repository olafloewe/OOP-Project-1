using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Project_1 {
    [Serializable]
    public class Hospital {
        private static Hospital hospital;
        private List<Employee> employees = new List<Employee>();

        private Hospital() { }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext streamingContext) {
            hospital = this;
        }

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
            // check wether username is taken allready
            try {
                hospital.GetEmployees().ForEach(e => {
                    if (e.GetUsername().ToLower() == emp.GetUsername().ToLower()) throw new Exception("Username allready taken");
                });
                // no exception thrown, add employee
                employees.Add(emp);
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        public void RemoveEmployee(Employee emp) {
            // check wether username is taken allready
            try {
                hospital.GetEmployees().ForEach(e => {
                    if (e.GetUsername().ToLower() == emp.GetUsername().ToLower()) {
                        employees.Remove(e);
                    }
                });
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        public override string ToString() {
            return "TESTING";
        }
    }
}