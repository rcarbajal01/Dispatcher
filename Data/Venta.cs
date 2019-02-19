using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Venta
    {
        public int idVenta { get; set; }

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

        public int Num_Externo { get; set; }

        public int Num_Interno { get; set; }

        public int CP { get; set; }

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
    }
}
