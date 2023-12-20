using AjudaAiAPI.Contracts.Repository;
using AjudaAiAPI.DTO;
using AjudaAiAPI.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AjudaAiAPI.Controllers
{
    [ApiController]
    [Route("ngo")]
    public class NgoController : ControllerBase
    {
        private readonly INgoRepository _ngoRepository;

        public NgoController(INgoRepository ngoRepository)
        {
            _ngoRepository = ngoRepository;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _ngoRepository.Get());
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _ngoRepository.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(NgoDTO ngo)
        {
            await _ngoRepository.Add(ngo);
            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDTO ngo)
        {
            try
            {
                return Ok(await _ngoRepository.Login(ngo));
            } catch (Exception ex)
            {
                return Unauthorized("Email ou senha inválidos.");
            }
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update(NgoEntity ngo)
        {
            await _ngoRepository.Update(ngo);
            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _ngoRepository.Delete(id);
            return Ok();
        }
    }
}
