using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Venta
    {
        public decimal dn { get; set; }

        public string plan_carga { get; set; }

        public string plan_vendido { get; set; }

        public string preg_1 { get; set; }

        public string preg_1_Dia { get; set; }

        public string preg_1_Hora { get; set; }

        public string preg_2 { get; set; }

        public string preg_3 { get; set; }

        public string Tipo_Celular { get; set; }

        public int EdadCliente { get; set; }

        public string preg_4 { get; set; }

        public string preg_5 { get; set; }

        public string preg_6 { get; set; }

        public string preg_7 { get; set; }

        public string preg_8 { get; set; }

        public string preg_9 { get; set; }

        public string Modulo_SMS { get; set; }

        public string preg_10 { get; set; }

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

        public DateTime Fecha_Venta { get; set; }

        public string Usuario_Vent { get; set; }

        public string Estatus_Venta { get; set; }

        public string Motivos_No_Aceptacion { get; set; }

        public string SubMotivos { get; set; }

        public string TipoPlan_OtraLinea { get; set; }

        public string Compañia { get; set; }

        public string realizar_consulta_scoring { get; set; }

        public string Puntaje_Scoring { get; set; }

        public string DeseaHorarioEspecifico { get; set; }

        public string DiaClienteDeseaLlamada { get; set; }

        public string HoraClienteDeseaLlamada { get; set; }

        public string Municipio{ get; set; }

        public string TelefonoFijo { get; set; }

        public string TelefonoMovil { get; set; }

        public Venta()
        {
            dn = 0;

            plan_carga = "";

            plan_vendido = "";

            preg_1 = "";

            preg_1_Dia = "";

            preg_1_Hora = "";

            preg_2 = "";

            preg_3 = "";

            Tipo_Celular = "";

            EdadCliente = 0;

            preg_4 = "";

            preg_5 = "";

            preg_6 = "";

            preg_7 = "";

            preg_8 = "";

            preg_9 = "";

            Modulo_SMS = "";

            preg_10 = "";

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

            Fecha_Venta = DateTime.Now;

            Usuario_Vent = "";

            Estatus_Venta = "";

            Motivos_No_Aceptacion = "";

            SubMotivos = "";

            TipoPlan_OtraLinea = "";

            Compañia = "";

            realizar_consulta_scoring = "";

            Puntaje_Scoring = "";

            DeseaHorarioEspecifico = "";

            DiaClienteDeseaLlamada = "";

            HoraClienteDeseaLlamada = "";

            Municipio = "";

            TelefonoFijo = "";

            TelefonoMovil = "";
        }
    }
}
