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
            ActivitiesHistories = new HashSet<ActivitiesHistoryDTO>();
            Animals = new HashSet<AnimalDTO>();
        }

        [Key]
        [Column("HealthStatusID")]
        public int HealthStatusId { get; set; }
        [Required]
        [StringLength(20)]
        public string StatusName { get; set; }

        [InverseProperty(nameof(ActivitiesHistoryDTO.AnimalHealthStatus))]
        public virtual ICollection<ActivitiesHistoryDTO> ActivitiesHistories { get; set; }
        [InverseProperty(nameof(AnimalDTO.HealthStatus))]
        public virtual ICollection<AnimalDTO> Animals { get; set; }
    }
}
