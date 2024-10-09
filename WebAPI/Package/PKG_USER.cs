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
            //cmd.Parameters.Add("p_role", OracleDbType.Int32).Value = user.Role;
            cmd.Parameters.Add("p_role", OracleDbType.Int32).Value = (int)user.Role;

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
                    Password = reader["password"].ToString(),
                    Role = (Role)int.Parse(reader["role"].ToString())

                };
            }

            conn.Close();

            return user;

        }



        //public List<User> get_users()
        //{
        //    List<User> users = new List<User>();

        //    using (OracleConnection conn = new OracleConnection(ConnStr))
        //    {
        //        conn.Open();

        //        using (OracleCommand cmd = new OracleCommand("olerning.PKG_GIORGITSK_USERS.get_users", conn))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            cmd.Parameters.Add("p_result", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

        //            using (OracleDataReader reader = cmd.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    User user = new User()
        //                    {
        //                        ID = int.Parse(reader["ID"].ToString()),
        //                        Email = reader["email"].ToString(),
        //                        Username = reader["username"].ToString(),
        //                        Password = reader["password"].ToString(),
        //                        Role = (Role)int.Parse(reader["role"].ToString())  // Convert role to enum
        //                    };

        //                    users.Add(user);
        //                }
        //            }
        //        }
        //    }

        //    return users;
        //}

        public List<User> get_users()
        {
            List<User> users = new List<User>(); 

            using (OracleConnection conn = new OracleConnection(ConnStr))
            {
                conn.Open();

                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "olerning.PKG_GIORGITSK_USERS.get_users";  // Stored procedure to get all users
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("p_result", OracleDbType.RefCursor).Direction = ParameterDirection.Output;  // Output parameter to receive cursor

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User user = new User()
                            {
                                ID = Convert.ToInt32(reader["id"]),
                                Email = reader["email"].ToString(),
                                Username = reader["username"].ToString(),
                                Password = reader["password"].ToString(),
                                Role = (Role)Convert.ToInt32(reader["role"])  // Convert role (0 or 1) to Role enum
                            };

                            users.Add(user); 
                        }
                    }
                }
            }

            return users; 
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


        //public void delete_user(User user)
        //{
        //    OracleConnection conn = new OracleConnection();
        //    conn.ConnectionString = ConnStr;

        //    conn.Open();

        //    OracleCommand cmd = new OracleCommand();
        //    cmd.Connection = conn;
        //    cmd.CommandText = "olerning.PKG_GIORGITSK_USERS.delete_user";
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    cmd.Parameters.Add("p_id", OracleDbType.Int32).Value = user.ID;

        //    cmd.ExecuteNonQuery();

        //    conn.Close();
        //}


        public void delete_user(int id)
        {
            using (OracleConnection conn = new OracleConnection(ConnStr))
            {
                conn.Open();

                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "olerning.PKG_GIORGITSK_USERS.delete_user";  
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("p_id", OracleDbType.Int32).Value = id;

                    cmd.ExecuteNonQuery(); 
                }
            }
        }


    }
}
