namespace ProductManagement.Data
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedUserId { get; set; }
        public DateTime LastUpdatedDate { get; set;}
        public int LastUpdatedUserId { get; set;}
        public User CreatedUser { get; set; }
        public User LastUpdatedUser { get; set; }

    }
}
