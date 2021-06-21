using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoSistemas.Web.Models
{
    public class Token
    {
        public string mensagem { get; set; }

        public string proximaEtapa { get; set; }

        public string statusCode { get; set; }

        public string data { get; set; }
    }
}
