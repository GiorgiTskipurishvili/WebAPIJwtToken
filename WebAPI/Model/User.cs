namespace WebAPI.Model
{
    public class User
    {
        public int ID { get; set; }
        public string Email { get; set; }

        public string Username {  get; set; }
        public string Password { get; set; }    
        //public int Role {  get; set; }

        public Role Role { get; set; }
     
    }

    public enum Role
    {
        Admin = 1,
        User = 0

        //Admin,
        //User
    }
}
