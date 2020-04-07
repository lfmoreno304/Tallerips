using Entity;

namespace ipsdotnet.Models {
    public class PerosnaModels {
        public string Nombre { get; set; }
        public string Identificacion { get; set; }
        public double Salario { get; set; }
        public double Servico { get; set; }
        public double Copago {get; set;}
    }
    public class PersonaViewModel : PerosnaModels{
        public PersonaViewModel (){
        }
        public PersonaViewModel (Persona persona)
        {
            Identificacion = persona.Identificacion;
            Nombre = persona.Nombre;
            Salario = persona.Salario;
            Servico = persona.Servicio;
            Copago = persona.Copago;
        }
    }
}