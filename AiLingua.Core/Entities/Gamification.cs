using AiLingua.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiLingua.Core.Entities
{
    public class Gamification
    {
        [Key] // Marks the property as the primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Makes it an identity column

        [Column("GamificationId")]

        public int Id { get; set; }
        public string UserId { get; set; }
        public  User User { get; set; } // FK to User
        public int Points { get; set; }
        public string Rewards { get; set; } // Comma-separated rewards
    }
}
