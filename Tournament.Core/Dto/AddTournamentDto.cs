﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentProject.Core.Dto
{
    public class AddTournamentDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public DateTime StartDate { get; set; }
    }
}
