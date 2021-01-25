using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TamagotchiUI.Models
{
    [Table("LifeCycleStatus")]
    public partial class LifeCycleStatus
    {
        public LifeCycleStatus()
        {
            ActivitiesHistories = new HashSet<ActivitiesHistory>();
            Animals = new HashSet<Animal>();
        }

        [Key]
        [Column("LifeCycleStatusID")]
        public int LifeCycleStatusId { get; set; }
        [Required]
        [StringLength(20)]
        public string StatusName { get; set; }
        public int CycleTime { get; set; }

        [InverseProperty(nameof(ActivitiesHistory.AnimalCycleStatus))]
        public virtual ICollection<ActivitiesHistory> ActivitiesHistories { get; set; }
        [InverseProperty(nameof(Animal.LifeCycle))]
        public virtual ICollection<Animal> Animals { get; set; }
    }
}
