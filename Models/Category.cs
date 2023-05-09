using System.ComponentModel.DataAnnotations;
namespace NetCore_01.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Post> Posts { get; set; }
    }
}
