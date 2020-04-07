using System;
using System.Collections.Generic;
using Datos;
using Entity;
namespace Logica {
    public class PersonaService {
        private readonly ConnectionManager _connection;
        private readonly PersonaRepository _personaRepository;
        public PersonaService (string connectionString) {
            this._connection = new ConnectionManager (connectionString);
            this._personaRepository = new PersonaRepository (this._connection);
        }
        public GuardarPersonaResponse Guardar (Persona persona) {
            try {
                persona.CalcularCopago ();
                this._connection.Open ();
                this._personaRepository.Guardar (persona);
                this._connection.Close ();
                return new GuardarPersonaResponse (persona);
            } catch (Exception e) {
                return new GuardarPersonaResponse ($"Error de la Aplicacion: {e.Message}");
            } finally { this._connection.Close (); }
        }
        public List<Persona> ConsultarTodos ()
        {
            this._connection.Open ();
            List<Persona> personas = this._personaRepository.ConsultarTodos ();
            this._connection.Close ();
            return personas;
        }

        public Persona ConsultarPorIdentificacion(string identificacion){
            this._connection.Open();
            Persona persona=new Persona();
            return persona=this._personaRepository.BuscarPorIdentificacion(identificacion);
        }
    }
    public class GuardarPersonaResponse {
        public GuardarPersonaResponse (Persona persona) {
            Error = false;
            Persona = persona;
        }
        public GuardarPersonaResponse (string mensaje) {
            Error = true;
            Mensaje = mensaje;
        }
        public bool Error { get; set; }
        public string Mensaje { get; set; }
        public Persona Persona { get; set; }
    }
}