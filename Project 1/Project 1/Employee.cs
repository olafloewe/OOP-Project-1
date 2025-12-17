using System;
using System.Collections.Generic;

namespace Project_1 {
    [Serializable]
    public abstract class Employee {

        string name;
        string surName;
        long pesel;
        string username;
        string password;

        public Employee(string name, string surname, long pesel, string username, string password) {
            this.SetName(name);
            this.SetSurName(surname);
            this.SetPesel(pesel);
            this.SetUsername(username);
            this.SetPassword(password);
        }

        public void SetName(string name) {
            this.name = name;
        }

        public string GetName() {
            return name;
        }

        public void SetSurName(string surName) {
            this.surName = surName;
        }

        public string GetSurName() {
            return surName;
        }

        public void SetPesel(long pesel) {
            this.pesel = pesel;
        }

        public long GetPesel() {
            return pesel;
        }

        public void SetUsername(string username) {
            this.username = username;
        }

        public string GetUsername() {
            return username;
        }

        public void SetPassword(string password) {
            this.password = password;
        }

        public string GetPassword() {
            return password;
        }

        public bool Login(string username, string password) {
            if (username.ToLower() == this.username.ToLower() && password == this.password) return true;
            return false;
        }
        public virtual void AddDuty(Duty duty) {
        }

        public virtual List<Duty> GetDutyList() {
            return null;
        }
    }
}
