using TechSouq.Domain.Enums;


namespace TechSouq.Application.Dtos
{
    public class CartDto
    {

        public int Id { get; set; }

        public int UserId { get; set; }
       
        public CartStatus Status { get; set; }
      
    }

}
