namespace TechSouq.Application.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public string ShippingStreet { get; set; }
        public string ShippingCity { get; set; }

        public int UserId { get; set; }
        

        public int DeliveryMethodId { get; set; }
       

        public int AddressId { get; set; }


    }

}
