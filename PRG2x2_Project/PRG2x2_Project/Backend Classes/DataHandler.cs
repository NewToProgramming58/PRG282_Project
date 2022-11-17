using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRG2x2_Project
{
    enum Tables
    {
        Student,
        Module,
        StudentModules,
        Resource,
        StudentModuleDetails
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
                if (GetOperator(op) == "LIKE")
                {
                    return $"WHERE [{field}] {GetOperator(op)} '%{rhs}%'";
                }
                return $"WHERE [{field}] {GetOperator(op)} '{rhs}'";
            } 
            else
            {
                if (GetOperator(op) == "LIKE")
                {
                    return $"WHERE [{field}] {GetOperator(op)} '%{rhs}%'";
                }
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
                    MessageBox.Show(ex.Message);
                }
                finally 
                {
                    masterConnection.Close();
                    this.Con.Close();
                }
            }        
        }
        public DataTable GetData(Tables table, string condition = "", dynamic tableObject = null)
        {
            string tableName = ((Tables)table).ToString();
            string qry;
            if (table == Tables.StudentModuleDetails && tableObject != null) 
            {
                qry = tableObject.Join();
            }
            else
            {
                qry = $"SELECT * FROM {tableName}\n{condition}";
            }
            SqlDataAdapter dataAdapter = new SqlDataAdapter(qry, this.Con);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            return dt; 
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
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                HandleExeption(ex);
            }
            finally
            {
                Con.Close();
            }
        }

        public void Delete(Tables table, string condition, dynamic tableObject = null)
        {
            string tableName = ((Tables)table).ToString();
            string qry;
            SqlCommand command;
            qry = $"DELETE FROM {tableName}\n{condition}";
            switch (table) 
            { 
                case Tables.Student:
                    qry = $"DELETE FROM StudentModules {condition}\n";
                    command = new SqlCommand(qry, this.Con);
                    Con.Open();
                    command.ExecuteNonQuery();
                    Con.Close();
                    break;

                case Tables.Module:
                    qry = $"DELETE FROM [Resource] {condition}\n";
                    command = new SqlCommand(qry, this.Con);
                    Con.Open();
                    command.ExecuteNonQuery();
                    Con.Close();
                    break;
                case Tables.Resource:
                    qry = tableObject.Delete();                   
                    break;
            }     
            SqlCommand cmd = new SqlCommand(qry, this.Con);
            Con.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                HandleExeption(ex);
            }
            finally 
            {
                Con.Close();
            }            
        }

        public void Insert(dynamic tableObject)
        {
            string qry = tableObject.Insert();
            SqlCommand cmd = new SqlCommand(qry, Con);
            Con.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                HandleExeption(ex);
            }
            finally 
            {
                Con.Close();
            }            
        }
        private void HandleExeption(SqlException e) 
        {
            switch (e.Number) 
            {
                case 2627:
                    MessageBox.Show($"This record already exits, try updating it", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    break;
            }
        }
    }   //rtbModuleResource.Text = dgvModuleOutput.SelectedRows[0].Cells[1].Value.ToString();
}
