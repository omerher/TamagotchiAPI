﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TamagotchiUI.Models
{
    [Table("Animal")]
    [Index(nameof(CreationDate), Name = "idx_animalcreationdate")]
    public partial class AnimalDTO
    {
        public AnimalDTO()
        {

        }

        public AnimalDTO(Animal a)
        {
            AnimalId = a.AnimalId;
            AnimalName = a.AnimalName;
            PlayerId = a.PlayerId;
            Aweight = a.Aweight;
            CreationDate = a.CreationDate;
            Ahunger = a.Ahunger;
            Acleanliness = a.Acleanliness;
            Ahappiness = a.Ahappiness;
            HealthStatusId = a.HealthStatusId;
            LifeCycleId = a.LifeCycleId;
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
    }
}
