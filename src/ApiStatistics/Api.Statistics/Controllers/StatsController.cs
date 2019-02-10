using Api.Statistics.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Api.Statistics.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public StatsController(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        // api/v1/stats/player/6/games/total
        [HttpGet("player/{id}/games/total")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetGamesCount(string id)
        {
            var games = await _statisticsRepository.GetUserGames(id);
            if (games == null) return NotFound();

            return Ok(new { totalGames = games.Count() });
        }

        // api/v1/stats/player/6/games
        [HttpGet("player/{id}/games")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetPlayerGames(string id)
        {
            var games = await _statisticsRepository.GetUserGames(id);
            if (games == null) return NotFound();

            return Ok(games);
        }

        // TODO add more fancy endpoints like total deadths, some averages like score/game or deaths/game
    }
}