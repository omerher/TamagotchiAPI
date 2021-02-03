using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TamagotchiUI.Models
{
    public partial class ActivitiesCategoryDTO
    {
        public ActivitiesCategoryDTO()
        {
            
        }

        [Key]
        [Column("AcitivtyCategoryID")]
        public int AcitivtyCategoryId { get; set; }
        [Required]
        [StringLength(20)]
        public string CategoryName { get; set; }

    }
}
