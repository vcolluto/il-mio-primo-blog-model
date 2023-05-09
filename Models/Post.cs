using NetCore_01.Validations;
using System.ComponentModel.DataAnnotations;

namespace NetCore_01.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [FirstLetterIsUppercaseValidation]
        public string Title { get; set; }


        [Required(ErrorMessage ="Il campo descrizione non può essere vuoto")]
        [StringLength(50,MinimumLength =20,ErrorMessage = "Il campo descrizione deve avere almeno 20 caratteri e non più di 50")]
      
        public string Description { get; set; } 

        //public string? Category { get; set; }
        public string Image { get; set; }


        public Post()
        {

        } 

        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        public Post(string title, string description, string image)
        {            
            Title = title;
            Description = description;
            Image = image;
        }
    }
}
