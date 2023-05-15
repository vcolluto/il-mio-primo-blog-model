using Microsoft.AspNetCore.Mvc.Rendering;

namespace NetCore_01.Models
{
    public class PostFormModel
    {
        public Post? Post { get; set; }
        public List<Category>? Categories { get; set; }

        public List<Tag>? Tags { get; set; }        //tutti i tag dai quali posso scegliere
        public List<string>? SelectedTags { get; set; }        //gli id dei tag effettivamente scelti (associati al post)
    }
}
