using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FamsGames.Repositories;
using FamsGames.Model;

namespace FamsGames.Controllers
{
    /// <summary>
    /// Controlador para Users
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FamsGamesController : ControllerBase
    {
        private readonly FamsGamesRepository famsGamesRepository;
        public FamsGamesController(FamsGamesRepository famsGamesRepository)
        {
            this.famsGamesRepository = famsGamesRepository;
        }
        [HttpGet("Users")]
        public async Task<IActionResult> SelectAllUsers()
        {
            return Ok(await famsGamesRepository.SelectAllUsers());
        }

        [HttpGet("Scores")]
        public async Task<IActionResult> SelectAllScores()
        {
            return Ok(await famsGamesRepository.SelectAllScores());
        }

        [HttpGet("Locations")]
        public async Task<IActionResult> SelectLocations()
        {
            return Ok(await famsGamesRepository.SelectLocations());
        }
        [HttpGet("Preguntas")]
        public async Task<IActionResult> SelectQuestions()
        {
            return Ok(await famsGamesRepository.SelectQuestions());
        }

        [HttpGet("User/{nickname},{password}")]
        public async Task<IActionResult> GetUser(string nickname, string password)
        {

            return Ok(await famsGamesRepository.GetUser(nickname, password));
        }

        [HttpPost("User")]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created = await famsGamesRepository.CreateUser(user);
            return Created("Creado!", created);
        }
        [HttpPost("Score")]
        public async Task<IActionResult> CreateScore([FromBody] Score score)
        {
            if (score == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created = await famsGamesRepository.CreateScore(score);
            return Created("Creado!", created);
        }

        [HttpPut("User/{id}")]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            if (user.IdUser == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updated = await famsGamesRepository.UpdateUser(user);
            return Created("Actualizado!", updated);
        }
        /// ===================================================
        [HttpPut("Score/{id}")]
        public async Task<IActionResult> UpdateScore([FromBody] Score score)
        {
            if (score.IdGame == null || score.IdUser == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updated = await famsGamesRepository.UpdateScore(score);
            return Created("Actualizado!", updated);
        }
        /// ==================================================
        [HttpDelete("User {id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deleted = await famsGamesRepository.DeleteUser(id);
            return Created("Eliminado!", deleted);
        }
        [HttpDelete("Score {id}")]
        public async Task<IActionResult> DeleteScore(int id)
        {
            var deleted = await famsGamesRepository.DeleteScore(id);
            return Created("Eliminado!", deleted);
        }
    }
}
