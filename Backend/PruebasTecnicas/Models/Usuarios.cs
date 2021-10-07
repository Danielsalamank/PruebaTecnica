using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Models
{
    public class UsuariosModel
    {
        public int CODIGO { get; set; }
        public string NOMBRES { get; set; }
        public string APELLIDO { get; set; }
        public DateTime? FECHA_NACIMIENTO { get; set; }
        public string FOTO_USUARIO { get; set; }
        public string ESTADO_CIVIL { get; set; }
        public bool TIENE_HERMANOS { get; set; }
    }

    public class UsuariosRegistro
    {
        public string NOMBRES { get; set; }
        public string APELLIDO { get; set; }
        public DateTime? FECHA_NACIMIENTO { get; set; }
        public string FOTO_USUARIO { get; set; }
        public string ESTADO_CIVIL { get; set; }
        public bool TIENE_HERMANOS { get; set; }
    }

    public class EstadoCivilModel
    {
        public int? CODIGO { get; set; }
        public string NOMBRE { get; set; }

    }

    public class Respuesta
    {
        public string Error { get; set; }
        public string Mensaje { get; set; }
    }
}
