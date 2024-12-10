using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentProject.Core.Dto
{
    public class PagingGameDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Time { get; set; }
        public int TournamentId { get; set; }

        public int totalItems = 0;
        public int totalPages = 0;
        public int pageSize = 0;
        public int currentPage = 0;
    }
}