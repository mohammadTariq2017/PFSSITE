using System.ComponentModel.DataAnnotations;

namespace PFSSITE.Models
{
    public class Session : BaseEntity
    {
        [Key]
        public int SessionId { get; set; }

        public string SessionName { get; set; }

        public string Description { get; set; }
        public ICollection<Class> Class { get; set; }
    }
}
