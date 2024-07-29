using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROGETTO_S3.Models
{
    public class Role
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdRole { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        //Riferimento EF
        public List<User> Users { get; set; } = new List<User>();
    }
}