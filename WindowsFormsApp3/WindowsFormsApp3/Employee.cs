using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3
{
    [Serializable]
    public class Employee
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public RoleType Role { get; set; }

        public Employee(int id, string name, string username, string password, RoleType role)
        {
            Id = id;
            Name = name;
            Username = username;
            Password = password;
            Role = role;
        }

        override
        public string ToString()
        {
            return "Name: " + this.Name + "  Access control: " + this.Role;
        }

        public enum RoleType
        {
            Employee,
            Admin
        }
    }
}
