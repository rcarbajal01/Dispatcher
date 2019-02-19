using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Activacion
    {
        public decimal dn { get; set; }
        public string preg_1 { get; set; }
        public string preg_2 { get; set; }
        public string preg_3 { get; set; }
        public string preg_3_Dia { get; set; }
        public string preg_3_Hora { get; set; }
        public string preg_4 { get; set; }
        public string preg_5 { get; set; }
        public string Se_realizaron_cambios { get; set; }
        public string preg_6 { get; set; }
        public string preg_7 { get; set; }
        public DateTime Fecha_Encuesta { get; set; }
        public string cod_plan_mig { get; set; }
        public string usuario { get; set; }
        public string Estatus { get; set; }
        public string Motivos_No_Aceptacion { get; set; }
        public string Equipo { get; set; }
        public string Nombre_Completo { get; set; }
        public DateTime Fecha_Nacimiento { get; set; }
        public string Sexo { get; set; }
        public string Localidad_Nacimiento { get; set; }
        public string Calle { get; set; }
        public string Colonia { get; set; }
        public string Num_Externo { get; set; }
        public string Num_Interno { get; set; }
        public string CP { get; set; }
        public string Ciudad { get; set; }
        public string Correo_Elctronico { get; set; }
        public string RFC { get; set; }
        public string EnvioContrato { get; set; }
        public string SubMotivos { get; set; }
        public string Municipio { get; set; }
        public string TelefonoFijo { get; set; }
        public string TelefonoMovil { get; set; }

        public Activacion()
        {
            dn = 0;
            preg_1 = "";
            preg_2 = "";
            preg_3 = "";
            preg_3_Dia = "";
            preg_3_Hora = "";
            preg_4 = "";
            preg_5 = "";
            Se_realizaron_cambios = "";
            preg_6 = "";
            preg_7 = "";
            Fecha_Encuesta = Convert.ToDateTime("1900/01/01");
            cod_plan_mig = "";
            usuario = "";
            Estatus = "";
            Motivos_No_Aceptacion = "";
            Equipo = "";
            Nombre_Completo = "";
            Fecha_Nacimiento = Convert.ToDateTime("1900/01/01");
            Sexo = "";
            Localidad_Nacimiento = "";
            Calle = "";
            Colonia = "";
            Num_Externo = "";
            Num_Interno = "";
            CP = "";
            Ciudad = "";
            Correo_Elctronico = "";
            RFC = "";
            EnvioContrato = "";
            SubMotivos = "";
            Municipio = "";
            TelefonoFijo = "";
            TelefonoMovil = "";
        }
    }
}
