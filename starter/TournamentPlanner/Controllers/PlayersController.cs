using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TournamentPlanner.Data;

namespace TournamentPlanner.Controllers
{
    [ApiController]
    [Route("api/players")]
    public class PlayersController : ControllerBase
    {
        private readonly TournamentPlannerDbContext context;

        public PlayersController(TournamentPlannerDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IList<Player>> GetPlayers([FromQuery] string name = null)
        {
            return await this.context.GetFilteredPlayers(name);
        }

        [HttpPost]
        public async Task<Player> AddPlayer([FromBody] Player player)
        {
            this.context.Players.Add(player);
            await this.context.SaveChangesAsync();
            return player;
        }
    }
}
