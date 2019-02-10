using Api.Statistics.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Api.Statistics.Domain.DTOs;
using Api.Statistics.Domain.Entity;
using AutoMapper;
using System.Collections.Generic;

namespace Api.Statistics.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly IStatisticsRepository _statisticsRepository;
        private readonly IMapper _mapper;

        public StatsController(IStatisticsRepository statisticsRepository, IMapper mapper)
        {
            _statisticsRepository = statisticsRepository;
            _mapper = mapper;
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
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PlayerGamesDto))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetPlayerGames(string id)
        {
            var games = await _statisticsRepository.GetUserGames(id);
            if (games == null) return NotFound();

            var mapped = _mapper.Map<IEnumerable<UserGamesEntity>, IEnumerable<PlayerGamesDto>>(games);
           
            return Ok(mapped);
        }

        // TODO add more fancy endpoints like total deadths, some averages like score/game or deaths/game
    }
}