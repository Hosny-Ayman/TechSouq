namespace TechSouq.Application.Dtos
{
    public class AddressDto
    {
        public int Id { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string Phone { get; set; }

        public int? UserId { get; set; }
    }

}
