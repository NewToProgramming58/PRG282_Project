﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRG282_Project
{
    enum Tables
    {
        Student,
        Module,
        StudentModule,
        Resource
    }
    enum Operator 
    {
        Equals,
        Like, 
        NotLike,
        NotEquals
    }
    class DataHandler
    {
        private SqlConnection con;
        public string GetOperator(Operator @operator) 
        {
            switch (@operator)
            {
                case Operator.Equals:
                    return "=";
                case Operator.Like:
                    return "LIKE";
                case Operator.NotEquals:
                    return "!=";
                case Operator.NotLike:
                    return "NOT LIKE";
                default:
                    return "=";
            }
        }
        public string addCondition(string field, Operator op, dynamic rhs) 
        {       
            if (rhs.GetType() == typeof(string) || rhs.GetType() == typeof(DateTime)) 
            {
                return $"WHERE [{field}] {GetOperator(op)} '{rhs}'";
            } 
            else
            {
                return $"WHERE [{field}] {GetOperator(op)} {rhs}";
            }
        }

        public SqlConnection Con { get => con; set => con = value; }

        public DataHandler()
        {            
            bool isExist = false;
            string dbName = "BelgiumCampusStudents";
            string existQuery = $"SELECT * FROM master.dbo.sysdatabases WHERE name ='{dbName}'";

            using (SqlConnection con = new SqlConnection(@"Server=localhost\SQLExpress;Trusted_Connection=True;Integrated security=True;database=master"))
            {
                // Open connection ,run creation query and close.
                con.Open();
                using (SqlCommand command = new SqlCommand(existQuery, con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Checks to see if database has rows. If not the HasRows = null.
                        isExist = reader.HasRows;
                    }
                }
                con.Close();
            }
            // When database doesnt exist, it is created programmatically.
            if (isExist)
            {
                // When DB is 100% then simply open a connection
                this.Con = new SqlConnection($@"Server=localhost\SQLExpress;Integrated Security=True;Trusted_Connection=True;Database={dbName}");
            }
            else
            {
                string creationQuery = $"IF EXISTS(SELECT* FROM SYSDATABASES WHERE NAME= '{dbName}')\n" +
                    $"DROP DATABASE {dbName}\n" +
                    "DECLARE @InstanceDefaultDataPath VARCHAR(MAX)\n" +
                    "SET @InstanceDefaultDataPath = CONVERT(VARCHAR(MAX), (SELECT SERVERPROPERTY('InstanceDefaultDataPath')))\n" +
                    "DECLARE @InstanceDefaultLogPath VARCHAR(MAX)\n" +
                    "SET @InstanceDefaultLogPath = CONVERT(VARCHAR(MAX), (SELECT SERVERPROPERTY('InstanceDefaultLogPath')))\n" +
                    $"EXECUTE(N'CREATE DATABASE {dbName}\n" +
                    $"ON PRIMARY(NAME = N''{dbName}'', FILENAME = N''' + @InstanceDefaultDataPath + N'{dbName}.mdf'')\n" +
                    $"LOG ON(NAME = N''{dbName}_log'', FILENAME = N''' + @InstanceDefaultLogPath + N'{dbName}.ldf'')\n" +
                    $"BACKUP DATABASE {dbName}\n" +
                    $"TO DISK = '''+@InstanceDefaultLogPath+'{dbName}.bak''');\n";

                // USE a Master connection to build DB.
                SqlConnection masterConnection = new SqlConnection(@"Server=localhost\SQLExpress;Trusted_Connection=True;Integrated security=True;database=master");

                SqlCommand cmd = new SqlCommand(creationQuery, masterConnection);
                try
                {
                    // Open connection, run creation query and close.
                    masterConnection.Open();
                    cmd.ExecuteNonQuery();
                    masterConnection.Close();

                    this.Con = new SqlConnection($@"Server=localhost\SQLExpress;Integrated Security=True;Trusted_Connection=True;Database={dbName}");

                    string tablesQuery = File.ReadAllText(@"Queries\CreateTables.txt");

                    string mockDataQuery = File.ReadAllText(@"Queries\InsertData.txt");

                    //Create new connection to newly created database and create each table.
                    this.Con.Open();

                    using (SqlCommand _ = new SqlCommand(tablesQuery, this.Con))
                    {
                        _.ExecuteNonQuery();
                    }

                    using (SqlCommand _ = new SqlCommand(mockDataQuery, this.Con))
                    {
                        _.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(creationQuery+"\n\n"+ex.ToString());
                }
            }        
        }
        public DataTable GetData(Tables table, string condition = "")
        {
            string tableName = ((Tables)table).ToString();
            SqlDataAdapter dataAdapter = new SqlDataAdapter($"SELECT * FROM {tableName}\n{condition}", this.Con);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            return ds.Tables[0];
        }

        public void Update(dynamic tableObject, dynamic id = null) 
        {
            string qry;
            if (id != null)
            {
                qry = tableObject.Update(id);
            }
            else
            {
                qry = tableObject.Update();
            }

            SqlCommand cmd = new SqlCommand(qry, Con);
            Con.Open();
            cmd.ExecuteNonQuery();
            Con.Close();
        }

        public void Delete(Tables table, string condition)
        {
            string tableName = ((Tables)table).ToString();
            SqlCommand cmd = new SqlCommand($"DELETE FROM {tableName}\n{condition}", this.Con);
            Con.Open();
            cmd.ExecuteNonQuery();
            Con.Close();
        }

        public void Insert(dynamic tableObject)
        {
            string qry = tableObject.Update();          

            SqlCommand cmd = new SqlCommand(qry, Con);
            Con.Open();
            cmd.ExecuteNonQuery();
            Con.Close();
        }
    }
}
