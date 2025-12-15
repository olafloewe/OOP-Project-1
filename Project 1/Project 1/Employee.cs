using System;
using Project_1;

namespace Project_1 {
    [Serializable]
    public abstract class Employee {

        string name;
        string surName;
        long pesel;
        string username;
        string password;

        public Employee(string name, string surname, long pesel, string username, string password) {
            this.setName(name);
            this.setSurName(surname);
            this.setPesel(pesel);
            this.setUsername(username);
            this.setPassword(password);
        }

        protected void setName(string name) {
            this.name = name;
        }

        public string getName() {
            return name;
        }

        protected void setSurName(string surName) {
            this.surName = surName;
        }

        public string getSurName() {
            return surName;
        }

        protected void setPesel(long pesel) {
            this.pesel = pesel;
        }

        public long getPesel() {
            return pesel;
        }

        protected void setUsername(string username) {
            this.username = username;
        }

        public string getUsername() {
            return username;
        }

        protected void setPassword(string password) {
            this.password = password;
        }

        public string getPassword() {
            return password;
        }

        public bool Login(string username, string password) {
            if (username.ToLower() == this.username.ToLower() && password == this.password) return true;
            return false;
        }
    }
}
