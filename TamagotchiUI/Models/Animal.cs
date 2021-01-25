using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TamagotchiUI.Models
{
    [Table("Animal")]
    [Index(nameof(CreationDate), Name = "idx_animalcreationdate")]
    public partial class Animal
    {
        public Animal()
        {
            ActivitiesHistories = new HashSet<ActivitiesHistory>();
        }

        [Key]
        [Column("AnimalID")]
        public int AnimalId { get; set; }
        [Column("PlayerID")]
        public int? PlayerId { get; set; }
        [Required]
        [StringLength(20)]
        public string AnimalName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreationDate { get; set; }
        [Column("AWeight")]
        public int Aweight { get; set; }
        [Column("AHunger")]
        public int Ahunger { get; set; }
        [Column("ACleanliness")]
        public int Acleanliness { get; set; }
        [Column("AHappiness")]
        public int Ahappiness { get; set; }
        [Column("HealthStatusID")]
        public int? HealthStatusId { get; set; }
        [Column("LifeCycleID")]
        public int? LifeCycleId { get; set; }

        [ForeignKey(nameof(HealthStatusId))]
        [InverseProperty("Animals")]
        public virtual HealthStatus HealthStatus { get; set; }
        [ForeignKey(nameof(LifeCycleId))]
        [InverseProperty(nameof(LifeCycleStatus.Animals))]
        public virtual LifeCycleStatus LifeCycle { get; set; }
        [ForeignKey(nameof(PlayerId))]
        [InverseProperty("Animals")]
        public virtual Player Player { get; set; }
        [InverseProperty("ActiveAnimal")]
        public virtual Player PlayerNavigation { get; set; }
        [InverseProperty(nameof(ActivitiesHistory.Animal))]
        public virtual ICollection<ActivitiesHistory> ActivitiesHistories { get; set; }
    }
}
