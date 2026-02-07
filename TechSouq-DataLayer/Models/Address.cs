namespace TechSouq_DataLayer.Models
{
    public class Address
    {

        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
