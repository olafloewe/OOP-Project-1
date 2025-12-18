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

        public string GetName() => name;
        public string GetSurName() => surName;
        public long GetPesel() => pesel;
        public string GetUsername() => username;
        public string GetPassword() => password;

        private void SetName(string name) {
            this.name = name;
        }

        private void SetSurName(string surName) {
            this.surName = surName;
        }

        private void SetPesel(long pesel) {
            this.pesel = pesel;
        }

        private void SetUsername(string username) {
            this.username = username;
        }

        private void SetPassword(string password) {
            this.password = password;
        }

        public bool Login(string username, string password) {
            if (username.ToLower() == this.username.ToLower() && password == this.password) return true;
            return false;
        }
        public virtual void AddDuty(Duty duty) { }

        public virtual List<Duty> GetDutyList() {
            return null;
        }

        public void EditData(string name, string surname, long pesel, string username, string password) {
            this.SetName(name);
            this.SetSurName(surname);
            this.SetPesel(pesel);
            this.SetUsername(username);
            this.SetPassword(password);
        }
    }
}
