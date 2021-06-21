using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoSistemas.Web.Models
{
    public class Paciente
    {
        public string usu_nome { get; set; }

        public string email { get; set; }

        public string telefone { get; set; }

        public string sexo { get; set; }

        public string bairro { get; set; }
    }
}
