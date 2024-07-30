using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROGETTO_S3.Models
{
    public class Product
    {
        //Caratteristiche della tabella
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProduct { get; set; }

        [Required]
        [StringLength(50)]
        public required string Name { get; set; }

        [Range(0,100)]
        public decimal Price { get; set; }

        public byte[]? Photo { get; set; }

        [Range(0,60)]
        public int DeliveryTimeInMinutes { get; set; }

        //Foreign Key (Riferimento alla tabella ingredients)
        public List<Ingridient> Ingridients { get; set; } = new List<Ingridient>();

        
    }
}
