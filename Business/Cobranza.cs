using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Cobranza
    {
        public decimal dn { get; set; }

        public string confirmaPago { get; set; }

        public string reflejadoFE { get; set; }

        public string pasado24hrs { get; set; }

        public string comprobante { get; set; }

        public string motivoAdeudo { get; set; }

        public string tieneDuda { get; set; }

        public DateTime Fecha_Cobranza { get; set; }

        public string Usuario_Cobranza { get; set; }

        public string Estatus_Cobranza { get; set; }
        
        public Cobranza()
        {
            dn = 0;

            confirmaPago = "";

            reflejadoFE = "";

            pasado24hrs = "";

            comprobante = "";

            motivoAdeudo = "";

            tieneDuda = "";

            Fecha_Cobranza = Convert.ToDateTime("1900/01/01");

            Usuario_Cobranza = "";

            Estatus_Cobranza = "";
            
        }
    }
}
