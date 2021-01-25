using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TamagotchiUI.Models
{
    [Table("HealthStatus")]
    public partial class HealthStatus
    {
        public HealthStatus()
        {
            ActivitiesHistories = new HashSet<ActivitiesHistory>();
            Animals = new HashSet<Animal>();
        }

        [Key]
        [Column("HealthStatusID")]
        public int HealthStatusId { get; set; }
        [Required]
        [StringLength(20)]
        public string StatusName { get; set; }

        [InverseProperty(nameof(ActivitiesHistory.AnimalHealthStatus))]
        public virtual ICollection<ActivitiesHistory> ActivitiesHistories { get; set; }
        [InverseProperty(nameof(Animal.HealthStatus))]
        public virtual ICollection<Animal> Animals { get; set; }
    }
}
