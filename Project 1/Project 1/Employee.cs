using System;
using Project_1;

namespace Project_1 {
    [Serializable]
    public abstract class Employee {

        string name;
        string surName;
        long pessel;
        string username;
        string password;

        public Employee(string name, string surname, long pesel, string username, string password) {
            this.setName(name);
            this.setSurName(surname);
            this.setPessel(pesel);
            this.setUsername(username);
            this.setPassword(password);
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
        
        public virtual bool Login(string username, string password) {
            Console.WriteLine($"Attempting login with username: {username} and password: {password}");
            if ( username == this.username && password == this.password) return true;
            return false;
        }
    }
}
