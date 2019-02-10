using Api.Statistics.Domain.DTOs;
using Api.Statistics.Domain.Entity;
using Api.Statistics.Infrastructure.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Api.Statistics.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
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
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PlayerTotalsDto))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetGamesCount(string id)
        {
            var games = await _statisticsRepository.GetUserGames(id);
            if (games == null) return NotFound();
            

            // TODO this
            return Ok(new PlayerTotalsDto() { TotalGames = games.Count()});
        }

        // api/v1/stats/player/6/games
        [HttpGet("player/{id}/games")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<PlayerGamesDto>))]
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