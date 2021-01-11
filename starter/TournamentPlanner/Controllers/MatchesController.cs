using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TournamentPlanner.Data;

namespace TournamentPlanner.Controllers
{
    [ApiController]
    [Route("api/matches")]
    public class MatchesController : ControllerBase
    {
        private readonly TournamentPlannerDbContext context;

        public MatchesController(TournamentPlannerDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("open")]
        public async Task<IList<Match>> GetMatches()
            => await this.context.GetIncompleteMatches();

        [HttpPost]
        [Route("generate")]
        public async Task GenerateMatches()
            => await this.context.GenerateMatchesForNextRound();
    }
}
