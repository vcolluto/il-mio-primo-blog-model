namespace NetCore_01.Models
{
    public class Tag
    {
        public int Id { get; set; } 
        public string Title { get; set; }

        public List<Post> Posts { get; set; }

        public Tag() 
        {
        }
    }
}
