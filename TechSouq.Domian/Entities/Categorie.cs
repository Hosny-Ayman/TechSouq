namespace TechSouq.Domain.Entities
{
    public class Categorie
    {

        public int Id { get; set; }
        public string Name { get; set; }


        public ICollection<Product> Products { get; set; }
    }
}
