using PFSSITE.Models;
using System.ComponentModel.DataAnnotations;

namespace PFSSITE.ViewModel
{
    public class ClassVM
    {
        public int ClassId { get; set; }
        [Display(Name = "Session")]
        [Required]
        public int SessionId { get; set; }
        [Display(Name = "Class Name")]
        [Required]
        public string ClassName { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
