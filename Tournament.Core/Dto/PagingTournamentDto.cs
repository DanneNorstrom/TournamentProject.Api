using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentProject.Core.Entities;

namespace TournamentProject.Core.Dto
{
    public class PagingTournamentDto
    { 
        public int Id { get; set; }

        public string Title { get; set; }

        private DateTime startdate;
        private DateTime enddate;

        public DateTime StartDate
        {
            get { return startdate; }
            set { startdate = value; enddate = startdate.AddMonths(3); }
        }

        public DateTime EndDate
        {
            get { return enddate; }
        }

        public ICollection<Game> Games { get; set; }

        public int totalItems = 0;
        public int totalPages = 0;
        public int pageSize = 0;
        public int currentPage = 0;
    }
}
    

