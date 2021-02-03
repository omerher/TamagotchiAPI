using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TamagotchiUI.Models
{
    [Table("HealthStatus")]
    public partial class HealthStatusDTO
    {
        public HealthStatusDTO()
        {
            
        }

        [Key]
        [Column("HealthStatusID")]
        public int HealthStatusId { get; set; }
        [Required]
        [StringLength(20)]
        public string StatusName { get; set; }

    }
}
