using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class PrimerFactura
    {
        public string preg_1 { get; set; }
        public string preg_2 { get; set; }
        public string preg_3 { get; set; }
        public string preg_3_Dia { get; set; }
        public string preg_3_Hora { get; set; }
        public string preg_4 { get; set; }
        public string Preg_4_Dia { get; set; }
        public string Preg_4_Hora { get; set; }
        public string preg_5 { get; set; }
        public string preg_6 { get; set; }
        public string preg_7 { get; set; }
        public string preg_8 { get; set; }
        public string preg_9 { get; set; }
        public string preg_10 { get; set; }
        public string preg_11 { get; set; }
        public string Preg_11_ClubMovistar { get; set; }
        public string Preg_11_EstatusRegistro { get; set; }
        public string OfrecimientoITAU { get; set; }
        public string preg_12 { get; set; }
        public string EsTitularTarjeta { get; set; }
        public string Ingresos7000 { get; set; }
        public string preg_12_Estatus { get; set; }
        public string Nombre_Ejecutivo_ITAU { get; set; }
        public DateTime Fecha_Encuesta { get; set; }
        public decimal dn { get; set; }
        public string cod_plan_mig { get; set; }
        public string usuario { get; set; }
        public string Estatus { get; set; }
        public string LugarPagoRecurrente { get; set; }
        public string OtroLugarPago { get; set; }
        public string EnvioReferenciaBancaria { get; set; }
        public string CorreoCliente { get; set; }
        public string DomiciliaTDC { get; set; }

        public PrimerFactura()
        {
            preg_1 = "";
            preg_2 = "";
            preg_3 = "";
            preg_3_Dia = "";
            preg_3_Hora = "";
            preg_4 = "";
            Preg_4_Dia = "";
            Preg_4_Hora = "";
            preg_5 = "";
            preg_6 = "";
            preg_7 = "";
            preg_8 = "";
            preg_9 = "";
            preg_10 = "";
            preg_11 = "";
            Preg_11_ClubMovistar = "";
            Preg_11_EstatusRegistro = "";
            OfrecimientoITAU = "";
            preg_12 = "";
            EsTitularTarjeta = "";
            Ingresos7000 = "";
            preg_12_Estatus = "";
            Nombre_Ejecutivo_ITAU = "";
            Fecha_Encuesta = Convert.ToDateTime("1900/01/01");
            dn = 0;
            cod_plan_mig = "";
            usuario = "";
            Estatus = "";
            LugarPagoRecurrente = "";
            OtroLugarPago = "";
            EnvioReferenciaBancaria = "";
            CorreoCliente = "";
            DomiciliaTDC = "";
        }
    }
}
