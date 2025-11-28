using System;
using Project_1;

namespace Project_1 {
    public abstract class Employee {

        abstract string name;
        abstract string surName;
        abstract string pessel;
        abstract string username;
        abstract string password;

        public Employee() {

        }

        private virtual void setName(string name) {
            this.name = name;
        }

        private virtual string getName() {
            return name;
        }

        private virtual void setSurName(string surName) {
            this.surName = surName;
        }

        private virtual string getSurName() {
            return surName;
        }

        private virtual void setPessel(string pessel) {
            this.pessel = pessel;
        }

        private virtual string getPessel() {
            return pessel;
        }

        private virtual void setUsername(string username) {
            this.username = username;
        }

        private virtual string getUsername() {
            return username;
        }

        private virtual void setPassword(string password) {
            this.password = password;
        }

        private virtual string getPassword() {
            return password;
        }

        private virtual void Login(string username, string password) {
            // TODO add login here
        }
    }
}
