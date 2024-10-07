using WebAPI.Model;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;


namespace WebAPI.Package
{
    public class PKG_EMP : PKG_BASE
    {
        public void save_employee(Employee employee)
        {
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = ConnStr;
            conn.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "olerning.PKG_GIORGITSK_EMP.save_employee";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("p_first_name", OracleDbType.Varchar2).Value = employee.First_Name;
            cmd.Parameters.Add("p_last_name", OracleDbType.Varchar2).Value = employee.Last_Name;
            cmd.Parameters.Add("position", OracleDbType.Varchar2).Value = employee.Position;

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public List<Employee> get_employees()
        {
            List<Employee> employees = new List<Employee>();

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = ConnStr;

            conn.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "olerning.PKG_GIORGITSK_EMP.get_employees";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("p_result", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            OracleDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Employee employee = new Employee();

                employee.ID = int.Parse(reader["ID"].ToString());
                employee.First_Name = reader["first_name"].ToString();
                employee.Last_Name = reader["last_name"].ToString();
                employee.Position = reader["position"].ToString();


                employees.Add(employee);
            }


            conn.Clone();

            return employees;
        }



        public void delete_employee(Employee employee)
        {
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = ConnStr;

            conn.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "olerning.PKG_GIORGITSK_EMP.delete_employee";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("p_id", OracleDbType.Int32).Value = employee.ID;

            cmd.ExecuteNonQuery();

            conn.Close();
        }


        //public void update_employee(Employee employee)
        //{
        //    //OracleConnection conn = new OracleConnection();
        //    //conn.ConnectionString = ConnStr;
        //    //conn.Open();

        //    //OracleCommand cmd = new OracleCommand();
        //    //cmd.Connection = conn;
        //    //cmd.CommandText = "olerning.PKG_GIORGITSK_EMP.update_employee";
        //    //cmd.CommandType = CommandType.StoredProcedure;


        //    //cmd.Parameters.Add("p_id", OracleDbType.Int32).Value = employee.ID;
        //    //cmd.Parameters.Add("p_first_name", OracleDbType.Int32).Value = employee.First_Name;
        //    //cmd.Parameters.Add("p_last_name", OracleDbType.Int32).Value = employee.Last_Name;
        //    //cmd.Parameters.Add("p_position", OracleDbType.Int32).Value = employee.Position;


        //    //cmd.ExecuteNonQuery();

        //    //conn.Close();

        //    try
        //    {
        //        using (OracleConnection conn = new OracleConnection(ConnStr))
        //        {
        //            conn.Open();

        //            using (OracleCommand cmd = new OracleCommand())
        //            {
        //                cmd.Connection = conn;
        //                cmd.CommandText = "olerning.PKG_GIORGITSK_EMP.update_employee";
        //                cmd.CommandType = CommandType.StoredProcedure;

        //                cmd.Parameters.Add("p_id", OracleDbType.Int32).Value = employee.ID;
        //                cmd.Parameters.Add("p_first_name", OracleDbType.Varchar2).Value = employee.First_Name;
        //                cmd.Parameters.Add("p_last_name", OracleDbType.Varchar2).Value = employee.Last_Name;
        //                cmd.Parameters.Add("p_position", OracleDbType.Varchar2).Value = employee.Position;

        //                cmd.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception (consider using a logging framework)
        //        Console.WriteLine("Error updating employee: " + ex.Message);
        //    }

        //}


        public void update_employee(Employee employee)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConnStr))
                {
                    conn.Open();

                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "olerning.PKG_GIORGITSK_EMP.update_employee";
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Adding parameters for the stored procedure
                        cmd.Parameters.Add("p_id", OracleDbType.Int32).Value = employee.ID;
                        cmd.Parameters.Add("p_first_name", OracleDbType.Varchar2).Value = employee.First_Name;
                        cmd.Parameters.Add("p_last_name", OracleDbType.Varchar2).Value = employee.Last_Name;
                        cmd.Parameters.Add("p_position", OracleDbType.Varchar2).Value = employee.Position;

                        // Execute the command
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception (you may want to log it in a file or use a logging framework)
                Console.WriteLine("Error updating employee: " + ex.Message);
            }
        }



        public Employee get_emp_by_id(Employee employee)
        {
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = ConnStr;

            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "olerning.PKG_GIORGITSK_EMP.get_emp_by_id";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("p_id", OracleDbType.Int32).Value = employee.ID;
            cmd.Parameters.Add("p_result", OracleDbType.RefCursor).Direction = ParameterDirection.Output;


            OracleDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                //employee.ID = int.Parse(reader["ID"].ToString());
                employee.First_Name = reader["first_name"].ToString();
                employee.Last_Name = reader["last_name"].ToString();
                employee.Position = reader["position"].ToString();
            }

            conn.Close();
            return employee;


        }
    }
}
