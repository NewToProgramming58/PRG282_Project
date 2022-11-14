using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG282_Project
{
    class Module
    {
        string code;
        string name;
        string description;
   
        public string Code { get => code; set => code = value; }
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }

        public Module(string code, string name, string description)
        {
            Code = code;
            Name = name;
            Description = description;
        }
        public string Update()
        {
            return $"UPDATE Module\n" +
                $"SET [Module Name] = '{name}', [Module Description] = '{Description}'\n" +
                $"WHERE [Module Code] = '{Code}]";
        }
        public string Insert()
        {
            return $"INSERT INTO Module\n" +
                $"VALUES ('{Code}', '{Name}', '{Description}')'";
        }
    }
}
