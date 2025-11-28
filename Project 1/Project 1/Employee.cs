using System;
using Project_1;

namespace Project_1 {
    public abstract class Employee {

        string name;
        string surName;
        long pessel;
        string username;
        string password;

        public Employee() {

        }

        public virtual void setName(string name) {
            this.name = name;
        }

        public virtual string getName() {
            return name;
        }

        public virtual void setSurName(string surName) {
            this.surName = surName;
        }

        public virtual string getSurName() {
            return surName;
        }

        public virtual void setPessel(long pessel) {
            this.pessel = pessel;
        }

        public virtual long getPessel() {
            return pessel;
        }

        public virtual void setUsername(string username) {
            this.username = username;
        }

        public virtual string getUsername() {
            return username;
        }

        public virtual void setPassword(string password) {
            this.password = password;
        }

        public virtual string getPassword() {
            return password;
        }

        public virtual void Login(string username, string password) {
            // TODO add login here
        }
    }
}
