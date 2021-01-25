using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TamagotchiUI.Models
{
    [Table("Activity")]
    public partial class ActivityDTO
    {
        public ActivityDTO()
        {
            ActivitiesHistories = new HashSet<ActivitiesHistoryDTO>();
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
        [InverseProperty(nameof(ActivitiesCategoryDTO.Activities))]
        public virtual ActivitiesCategoryDTO ActivityCategory { get; set; }
        [InverseProperty(nameof(ActivitiesHistoryDTO.Activity))]
        public virtual ICollection<ActivitiesHistoryDTO> ActivitiesHistories { get; set; }
    }
}
