namespace RecipeApp.Models
{
    public class RecipeDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Method { get; set; }
        public DateTimeOffset LastModified { get; set; }
        public IEnumerable<Item> Items { get; set; }
        public class Item 
        { 
            public string Name { get; set; }
            public string Quantity { get; set; }
        }

    }
}
