using Oracle.ManagedDataAccess.Client;
using System.Data;
using WebAPI.Model;

namespace WebAPI.Package
{
    public class PKG_USER : PKG_BASE
    {
        public void saveUser(User user)
        {
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = ConnStr;
            conn.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "olerning.PKG_GIORGITSK_USERS.save_user";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("p_email", OracleDbType.Varchar2).Value = user.Email;
            cmd.Parameters.Add("p_username", OracleDbType.Varchar2).Value = user.Username;
            cmd.Parameters.Add("p_password", OracleDbType.Varchar2).Value = user.Password;

            cmd.ExecuteNonQuery();

            conn.Close();
        }


        public User getUserByUsername(string username) 
        {

            User user = null;
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = ConnStr;

            conn.Open();

            OracleCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            cmd.CommandText = "olerning.PKG_GIORGITSK_USERS.get_user_by_username";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("v_username", OracleDbType.Varchar2).Value = username;
            cmd.Parameters.Add("v_result", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            OracleDataReader reader = cmd.ExecuteReader();
            reader.Read();
            if (reader.HasRows)
            {
                user = new User()
                {

                    Email = reader["email"].ToString(),
                    Username = reader["username"].ToString(),
                    Password = reader["password"].ToString()
                };
            }

            conn.Close();

            return user;

        }




        //public List<User> get_users()
        //{
        //    List<User> users = new List<User>();

        //    OracleConnection conn = new OracleConnection();
        //    conn.ConnectionString = ConnStr;

        //    conn.Open();

        //    OracleCommand cmd = new OracleCommand();
        //    cmd.Connection = conn;
        //    cmd.CommandText = "olerning.PKG_GIORGITSK_USERS.get_users";
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    cmd.Parameters.Add("p_result", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

        //    OracleDataReader reader = cmd.ExecuteReader();

        //    while (reader.Read())
        //    {
        //        User user = new User();

        //        //user.ID = int.Parse(reader["ID"].ToString());
        //        user.Email = reader["email"].ToString();
        //        user.Username = reader["username"].ToString();
        //        user.Password = reader["password"].ToString();


        //        users.Add(user);
        //    }


        //    conn.Clone();

        //    return users;
        //}



        //public User get_user_by_id(User user)
        //{
        //    OracleConnection conn = new OracleConnection();
        //    conn.ConnectionString = ConnStr;

        //    conn.Open();
        //    OracleCommand cmd = new OracleCommand();
        //    cmd.Connection = conn;
        //    cmd.CommandText = "olerning.PKG_GIORGITSK_USERS.get_user_by_id";
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    cmd.Parameters.Add("p_id", OracleDbType.Int32).Value = user.ID;
        //    cmd.Parameters.Add("p_result", OracleDbType.RefCursor).Direction = ParameterDirection.Output;


        //    OracleDataReader reader = cmd.ExecuteReader();
        //    if (reader.Read())
        //    {
        //        //user.ID = int.Parse(reader["ID"].ToString());
        //        user.Email = reader["email"].ToString();
        //        user.Username = reader["username"].ToString();
        //        user.Password = reader["password"].ToString();
        //    }

        //    conn.Close();
        //    return user;


        //}

    }
}
