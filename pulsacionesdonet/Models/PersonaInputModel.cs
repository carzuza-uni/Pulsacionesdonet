using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pulsacionesdonet.Models
{
    public class PersonaInputModel
    {
        public string Nombre { get; set; }
        public string Identificacion { get; set; }
        public int Edad { get; set; }
        public string Sexo { get; set; }
    }
}