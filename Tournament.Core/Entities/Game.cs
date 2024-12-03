using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentProject.Core.Entities
{
    public class Game
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string Title { get; set; }
        public DateTime Time { get; set; }
        
        public int TournamentId { get; set; }   //required foreign key property
        //public Tournament Tournament{ get; set; } //navigational property

    }
}
