namespace Models
{
    public class User : BaseModel
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Adress { get; set; }
        public string PhoneNumber { get; set; }

        public User()
        {

        }

    }
}