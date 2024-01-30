namespace WebApplication3.Model
{
    public class UserDetailModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string NRIC { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Resume { get; set; }
        public string WhoAmI { get; set; }

        // Add any other properties you need

        // Constructor to set default values
        public UserDetailModel()
        {
            DateOfBirth = new DateTime(1980, 1, 1);
        }
    }
}
