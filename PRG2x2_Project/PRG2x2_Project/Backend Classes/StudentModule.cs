using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2x2_Project
{
    class StudentModule
    {
        int studentNumber;
        string moduleCode;
        string status;

        public int StudentNumber { get => studentNumber; set => studentNumber = value; }
        public string ModuleCode { get => moduleCode; set => moduleCode = value; }
        public string Status { get => status; set => status = value; }

        public StudentModule(int studentNumber, string moduleCode, string status)
        {
            StudentNumber = studentNumber;
            ModuleCode = moduleCode;
            Status = status;
        }

        public string Update()
        {
            return $"UPDATE StudentModules\n" +
                $"SET [Status] = '{Status}'\n" +
                $"WHERE [Student Number = {StudentNumber} AND [Module Code] = '{ModuleCode}]";
        }
        public string Insert()
        {
            return $"INSERT INTO StudentModules\n" +
                $"VALUES ({StudentNumber}, '{ModuleCode}', '{Status}')'";
        }
    }
}
