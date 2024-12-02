using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentProject.Core.Entities
{
    public class Tournament
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        ICollection<Game> Games { get; set; }
    }
}
