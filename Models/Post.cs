using System.ComponentModel.DataAnnotations;

namespace NetCore_01.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; } 
        public string Title { get; set; }   
        public string Description { get; set; } 
        public string Image { get; set; }


        public Post()
        {

        } 

        public Post(string title, string description, string image)
        {            
            Title = title;
            Description = description;
            Image = image;
        }
    }
}
