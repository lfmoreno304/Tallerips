using System.Collections.Generic;
using System.Linq;
using Entity;
using ipsdotnet.Models;
using Logica;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;

namespace ipsdotnet.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase {
        private readonly PersonaService _personaService;
        public IConfiguration Configuration { get; }

        public PersonaController (IConfiguration configuration) {
            this.Configuration = configuration;
            string connectionString = this.Configuration["ConnectionStrings:DefaultConnection"];
            this._personaService = new PersonaService (connectionString);
        }

        [HttpGet]
        public IEnumerable<PersonaViewModel> Gets () {
            var personas = this._personaService.ConsultarTodos ().Select (p => new PersonaViewModel (p));
            return personas;
        }

        [HttpGet("{identificacion}")]
        public ActionResult<PersonaViewModel> Get (string identificacion)
        {
            var persona = _personaService.ConsultarPorIdentificacion (identificacion);
            if (persona == null) return NotFound ();
            var personaViewModel = new PersonaViewModel (persona);
            return personaViewModel;

        }

        [HttpPost]
        public ActionResult<PersonaViewModel> Post (PerosnaModels personaInput) {
            Persona persona = MapearPersona (personaInput);
            var response = _personaService.Guardar (persona);
            if (response.Error) {
                return BadRequest (response.Mensaje);
            }
            return Ok (response.Persona);

        }
        private Persona MapearPersona (PerosnaModels personaInput) {
            var persona = new Persona {
                Identificacion = personaInput.Identificacion,
                Nombre = personaInput.Nombre,
                Servicio = personaInput.Servico,
                Salario = personaInput.Salario,
                Copago = personaInput.Copago
            };
            return persona;
        }
    }
}