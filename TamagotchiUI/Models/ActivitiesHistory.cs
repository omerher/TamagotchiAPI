using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TamagotchiUI.Models
{
    [Table("ActivitiesHistory")]
    public partial class ActivitiesHistory
    {
        [Key]
        [Column("HistoryID")]
        public int HistoryId { get; set; }
        [Column("AnimalID")]
        public int? AnimalId { get; set; }
        [Column("ActivityID")]
        public int? ActivityId { get; set; }
        public int AnimalAge { get; set; }
        [Column("AWeight")]
        public int Aweight { get; set; }
        [Column("AHunger")]
        public int Ahunger { get; set; }
        [Column("AHappiness")]
        public int Ahappiness { get; set; }
        [Column("ACleanliness")]
        public int Acleanliness { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ActivityDate { get; set; }
        [Column("AnimalCycleStatusID")]
        public int? AnimalCycleStatusId { get; set; }
        [Column("AnimalHealthStatusID")]
        public int? AnimalHealthStatusId { get; set; }

        [ForeignKey(nameof(ActivityId))]
        [InverseProperty("ActivitiesHistories")]
        public virtual Activity Activity { get; set; }
        [ForeignKey(nameof(AnimalId))]
        [InverseProperty("ActivitiesHistories")]
        public virtual Animal Animal { get; set; }
        [ForeignKey(nameof(AnimalCycleStatusId))]
        [InverseProperty(nameof(LifeCycleStatus.ActivitiesHistories))]
        public virtual LifeCycleStatus AnimalCycleStatus { get; set; }
        [ForeignKey(nameof(AnimalHealthStatusId))]
        [InverseProperty(nameof(HealthStatus.ActivitiesHistories))]
        public virtual HealthStatus AnimalHealthStatus { get; set; }
    }
}
