using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TamagotchiUI.Models
{
    [Table("Activity")]
    public partial class Activity
    {
        public Activity()
        {
            ActivitiesHistories = new HashSet<ActivitiesHistory>();
        }

        [Key]
        [Column("ActivityID")]
        public int ActivityId { get; set; }
        [Column("ActivityCategoryID")]
        public int? ActivityCategoryId { get; set; }
        [Required]
        [StringLength(30)]
        public string ActivityName { get; set; }
        public int ImprovementRate { get; set; }

        [ForeignKey(nameof(ActivityCategoryId))]
        [InverseProperty(nameof(ActivitiesCategory.Activities))]
        public virtual ActivitiesCategory ActivityCategory { get; set; }
        [InverseProperty(nameof(ActivitiesHistory.Activity))]
        public virtual ICollection<ActivitiesHistory> ActivitiesHistories { get; set; }
    }
}
