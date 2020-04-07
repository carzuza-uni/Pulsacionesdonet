using System;
using System.Collections.Generic;
using Datos;
using Entity;

namespace Logica
{
    public class PersonaService
    {
        private readonly ConnectionManager _conexion;
        private readonly PersonaRepository personaRepository;

        public PersonaService(string connectionString){
            _conexion = new ConnectionManager(connectionString);
            personaRepository = new PersonaRepository(_conexion);
        }

        public GuardarPersonaResponse Guardar(Persona persona)
        {
            try
            {
                persona.CalcularPulsaciones();
                _conexion.Open();
                personaRepository.Guardar(persona);
                _conexion.Close();
                return new GuardarPersonaResponse(persona);
            }
            catch (Exception e)
            {
                return new GuardarPersonaResponse($"Error de la Aplicacion: {e.Message}");
            }
            finally { _conexion.Close(); }
        }

        public List<Persona> ConsultarTodos()
        {
            _conexion.Open();
            List<Persona> personas = personaRepository.ConsultarTodos();
            _conexion.Close();
            return personas;
        }

        public string Eliminar(string identificacion)
        {
            try
            {
                _conexion.Open();
                var persona = personaRepository.BuscarPorIdentificacion(identificacion);
                if (persona != null)
                {
                    personaRepository.Eliminar(persona);
                    _conexion.Close();
                    return ($"El registro {persona.Nombre} se ha eliminado satisfactoriamente.");
                }
                else
                {
                    return ($"Lo sentimos, {identificacion} no se encuentra registrada.");
                }
            }
            catch (Exception e)
            {

                return $"Error de la Aplicación: {e.Message}";
            }
            finally { _conexion.Close(); }

        }

        public Persona BuscarxIdentificacion(string identificacion)
        {
            _conexion.Open();
            Persona persona = personaRepository.BuscarPorIdentificacion(identificacion);
            _conexion.Close();
            return persona;
        }
    }

    public class GuardarPersonaResponse 
    {
        public GuardarPersonaResponse(Persona persona)
        {
            Error = false;
            Persona = persona;
        }
        public GuardarPersonaResponse(string mensaje)
        {
            Error = true;
            Mensaje = mensaje;
        }
        public bool Error { get; set; }
        public string Mensaje { get; set; }
        public Persona Persona { get; set; }
    }
}
