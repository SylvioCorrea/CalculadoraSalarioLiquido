using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using CalculadoraSalarioLiquido.Utils;
using CalculadoraSalarioLiquido.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CalculadoraSalarioLiquido.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculadoraController : ControllerBase
    {

        private Calculadora _calculadora;

        public CalculadoraController()
        {
            _calculadora = new Calculadora();
        }

        // GET: api/<CalculadoraController>
        [HttpGet]
        public IActionResult Get([FromQuery]double salario, int dependentes)
        {
            if (salario < 0 || dependentes < 0) return BadRequest("Entrada inválida. Salário e dependentes não podem ser números negativos.");

            ResultadoCalculadora res = _calculadora.CalculaSalarioLiquido(salario, dependentes);
            return Ok(res);
        }
    }
}
