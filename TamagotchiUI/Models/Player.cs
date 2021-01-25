using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TamagotchiUI.Models
{
    [Table("Player")]
    [Index(nameof(PlayerEmail), Name = "UQ__Player__1796E06C35D748EF", IsUnique = true)]
    [Index(nameof(Username), Name = "UQ__Player__536C85E4EA44DCAA", IsUnique = true)]
    [Index(nameof(PlayerEmail), Name = "idx_playeremail")]
    [Index(nameof(Username), Name = "idx_playerusername")]
    public partial class Player
    {
        public Player()
        {
            Animals = new HashSet<Animal>();
        }

        [Key]
        [Column("PlayerID")]
        public int PlayerId { get; set; }
        [Required]
        [StringLength(20)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(20)]
        public string LastName { get; set; }
        [Required]
        [StringLength(40)]
        public string PlayerEmail { get; set; }
        [StringLength(15)]
        public string Gender { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? BirthDate { get; set; }
        [Required]
        [StringLength(30)]
        public string Username { get; set; }
        [Required]
        [StringLength(30)]
        public string Password { get; set; }
        [Column("ActiveAnimalID")]
        public int? ActiveAnimalId { get; set; }

        [ForeignKey(nameof(ActiveAnimalId))]
        [InverseProperty(nameof(Animal.PlayerNavigation))]
        public virtual Animal ActiveAnimal { get; set; }
        [InverseProperty(nameof(Animal.Player))]
        public virtual ICollection<Animal> Animals { get; set; }
    }
}
