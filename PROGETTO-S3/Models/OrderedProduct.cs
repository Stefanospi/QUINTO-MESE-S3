﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PROGETTO_S3.Models
{
    public class OrderedProduct
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdOrderedProduct { get; set; }

        [Required]
        public int Quantity { get; set; }

        // Riferimenti EF
        [Required]
        public int IdOrder { get; set; }

        [ForeignKey("IdOrder")]
        public Order Order { get; set; }

        [Required]
        public int IdProduct { get; set; }

        [ForeignKey("IdProduct")]
        public Product Product { get; set; }
    }
}