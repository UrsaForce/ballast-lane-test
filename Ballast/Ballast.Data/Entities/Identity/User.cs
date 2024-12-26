namespace Ballast.Data.Entities.Identity
{
    public class User
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public byte[] Password { get; set; }
        public byte[] SecurityStamp { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string RegistrationIp { get; set; }
    }
}
