using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2x2_Project.Properties
{
    class Student
    {
        int number;
        string name;
        string surname;
        string gender;
        DateTime dob;
        string phone;
        string address;
        string image;       

        public int Number { get => number; set => number = value; }
        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
        public DateTime Dob { get => dob; set => dob = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Address { get => address; set => address = value; }
        public string Image { get => image; set => image = value; }
        public string Gender { get => gender; set => gender = value; }

        public Student(int number)
        {
            Number = number;
        }

        public Student(int number, string name, string surname, DateTime dob, string gender, string phone, string address, string image)
        {
            Number = number;
            Name = name;
            Surname = surname;
            Dob = dob;
            Gender = gender;
            Phone = phone;
            Address = address;
            Image = image;
        }
        public Student(string name, string surname, DateTime dob, string gender, string phone, string address, string image)
        {           
            Name = name;
            Surname = surname;
            Dob = dob;
            Gender = gender;
            Phone = phone;
            Address = address;
            Image = image;
        }
        public string Update()
        {
            return $"UPDATE Student\n" +
                $"SET [Name] = '{Name}', [Surname] = '{Surname}', [DOB] = '{dob}', [Gender] = '{Gender}', [Phone] = '{Phone}', [Address] = '{Address}', [Student Image] = '{Image}'\n" +
                $"WHERE [Student Number] = {Number}";
        }

        public string Insert()
        {
            return $"INSERT INTO Student\n" +
                $"VALUES ('{Name}', '{Surname}', '{dob.ToString("yyyy/MM/dd")}', '{Gender}', '{Phone}', '{Address}', '{Image}')";               
        }

        public string Join()
        {
            return
                "SELECT M.[Module Code], M.[Module Name], M.[Module Description], SM.[Status]\n" +
                "FROM Module M\n" +
                "INNER JOIN StudentModules SM\n" +
                "ON M.[Module Code] = SM.[Module Code]\n" +
                $"WHERE [Student Number] = {Number}";

        }
    }
}
