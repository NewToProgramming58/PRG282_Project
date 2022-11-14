using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG282_Project
{
    class Resource
    {
        string code;
        string coderesource;  

        public string Code { get => code; set => code = value; }
        public string Coderesource { get => coderesource; set => coderesource = value; }

        public Resource(string coderesource, string code)
        {
            Coderesource = coderesource;
            Code = code;
        }
        public string Update(string resource)
        {
            return $"UPDATE Resource\n" +
                $"SET [Module Code] = '{Code}', [Resource] = '{Coderesource}''\n" +
                $"WHERE [Resource] = '{resource}]";
        }
        public string Insert()
        {
            return $"INSERT INTO Resource\n" +
                $"VALUES ('{Code}', '{coderesource}')'";
        }
    }
}
