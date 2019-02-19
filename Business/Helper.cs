using System;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Reflection;
using System.Web;
using Data;
using System.Data.OleDb;
using System.ComponentModel;
using System.Data.SqlClient;

namespace Business
{
    public class Helper : IDisposable
    {
        private bool disposing;
        ControlConnection Connection = new ControlConnection();

        /// <summary>
        /// Método de IDisposable para desechar la clase.
        /// </summary>
        public void Dispose()
        {
            // Llamo al método que contiene la lógica
            // para liberar los recursos de esta clase.
            Dispose(true);
        }

        /// <summary>
        /// Método sobrecargado de Dispose que será el que
        /// libera los recursos, controla que solo se ejecute
        /// dicha lógica una vez y evita que el GC tenga que
        /// llamar al destructor de clase.
        /// </summary>
        /// <param name=”b”></param>
        protected virtual void Dispose(bool b)
        {
            // Si no se esta destruyendo ya…
            if (!disposing)
            {
                // La marco como desechada ó desechandose,
                // de forma que no se puede ejecutar este código
                // dos veces.
                disposing = true;

                // Indico al GC que no llame al destructor
                // de esta clase al recolectarla.
                GC.SuppressFinalize(this);

                // … libero los recursos… 
            }
        }

        /// <summary>
        /// Destructor de clase.
        /// En caso de que se nos olvide “desechar” la clase,
        /// el GC llamará al destructor, que tambén ejecuta la lógica
        /// anterior para liberar los recursos.
        /// </summary>
        ~Helper()
        {
            // Llamo al método que contiene la lógica
            // para liberar los recursos de esta clase.
            Dispose(true);
        }

        /// <summary>
        /// Carga de DNS para proceos
        /// </summary>
        /// <param name="file"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public string loadDnsFile(string file, string user)
        {
            string result = string.Empty;
            if (System.IO.File.Exists(file))
            {
                string strConn;
                if (file.EndsWith("xlsx"))
                    strConn = @"Data Source=" + file + ";Provider=Microsoft.ACE.OLEDB.12.0; Extended Properties=Excel 12.0";
                else
                    strConn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + file + ";Extended Properties='Excel 8.0;HDR=YES;'";
                OleDbConnection OleDbConn = new OleDbConnection(strConn);

                decimal Dn = 0;
                DateTime date = DateTime.Now;
                string Estatus = "Activo"; ;
                Int32 idFile = 0;

                OleDbConn.Open();
                DataTable dtHojasExcel = OleDbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                String hojaExcel = String.Empty;

                if (!dtHojasExcel.Rows.Count.Equals(0))
                    hojaExcel = dtHojasExcel.Rows[0]["TABLE_NAME"].ToString();

                OleDbDataAdapter OleDbAdapter = new OleDbDataAdapter();
                DataTable dt = new DataTable();
                OleDbCommand selectCMD = new OleDbCommand(String.Concat("SELECT * FROM [", hojaExcel, "] "), OleDbConn);
                OleDbAdapter.SelectCommand = selectCMD;
                OleDbAdapter.Fill(dt);
                Helper hlp = new Helper();

                string fileName = file;
                DateTime dateFile = DateTime.Now;
                IList parameterFile = new ArrayList();
                parameterFile.Add(new Parametro("name", fileName));
                parameterFile.Add(new Parametro("date", date));                
                idFile = Convert.ToInt32(Connection.Run("udp_File_ins", parameterFile, ReturnDataType.Scalar));

                int contador = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr[0] == DBNull.Value)
                        break;
                    Dn = Convert.ToDecimal(dr[0]);
                    string Nombre = Convert.ToString(dr[1]);
                    string Referencia1 = Convert.ToString(dr[2]);
                    string Referencia2 = Convert.ToString(dr[3]);
                    string Cac = Convert.ToString(dr[4]);
                    string Fvc = Convert.ToString(dr[5]);
                    string Contacto = String.Empty;
                    string Tipificacion = String.Empty;
                    string Queja = String.Empty;
                    contador++;


                    IList parametersRecord = new ArrayList();

                    parametersRecord.Add(new Parametro("Dn", Dn));
                    parametersRecord.Add(new Parametro("Nombre", Nombre));
                    parametersRecord.Add(new Parametro("Referencia1", Referencia1));
                    parametersRecord.Add(new Parametro("Referencia2", Referencia2));
                    parametersRecord.Add(new Parametro("Cac", Cac));
                    parametersRecord.Add(new Parametro("Fvc", Fvc));
                    parametersRecord.Add(new Parametro("Fecha", DateTime.Now));
                    parametersRecord.Add(new Parametro("Contacto", Contacto));
                    parametersRecord.Add(new Parametro("Tipificacion", Tipificacion));
                    parametersRecord.Add(new Parametro("Queja", Queja));
                    parametersRecord.Add(new Parametro("Estatus", Estatus));
                    parametersRecord.Add(new Parametro("IdFile", idFile));



                    Connection.Run("udp_Record_ins", parametersRecord, ReturnDataType.Nothing);
                }
                result = "Registros agregados: " + contador.ToString();
                OleDbConn.Close();
            }
            else
                result = "El archivo no existe.";

            return result;
        }        

        public static DataTable ConvertToDatatable<T>(List<T> data)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    table.Columns.Add(prop.Name, prop.PropertyType.GetGenericArguments()[0]);
                else
                    table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }



        //Function to check LoginCredentials
        public string checkLogin(string user, string password)
        {
            SqlConnection conn = null;

            conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            conn.Open();

            if (conn.State != ConnectionState.Open)
            {
                conn.Close();
                throw new Exception("Can't connect to the DB, please check the credentials");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("SELECT isLogged FROM [User] " +
                                                   "WHERE username COLLATE Latin1_General_CS_AS = '" + user + "' and password = '" + password + "'", conn);

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;

                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count > 0)
                {

                    Int32 isLogged = (Int32)dt.Rows[0]["isLogged"];


                    if (isLogged != 1)
                    {

                        SqlCommand cmd2 = new SqlCommand("update [User] set islogged=1  where username='" + user + "'", conn);
                        SqlDataAdapter sda2 = new SqlDataAdapter();
                        cmd2.ExecuteNonQuery();
                        conn.Close();
                        return "no loggeado";
                    }
                    else
                        return "loggeado";
                }
                else
                {
                    conn.Close();
                    return "no existe";
                }                
            }            
        }


        public void logout(string usr)
        {

            SqlConnection conn = null;

            conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            conn.Open();

            if (conn.State != ConnectionState.Open)
            {
                throw new Exception("Can't connect to the DB, please check the credentials");

            }
            else
            {
                SqlCommand cmd2 = new SqlCommand("update [User] set islogged=0  where username='" + usr + "'", conn);
                SqlDataAdapter sda2 = new SqlDataAdapter();
                cmd2.ExecuteNonQuery();


            }

            conn.Close();
        }





        public string usrType(string user)
        {
            SqlConnection conn = null;
            string usrType = "";

            conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            conn.Open();

            if (conn.State != ConnectionState.Open)
            {
                throw new Exception("Can't connect to the DB, please check the credentials");

            }
            else
            {

                SqlCommand cmd = new SqlCommand("SELECT profile FROM [User] " +
                                                   "WHERE username COLLATE Latin1_General_CS_AS = '" + user + "' ", conn);

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;

                DataTable dt = new DataTable();
                sda.Fill(dt);



                usrType = (string)dt.Rows[0]["profile"];

                return usrType;

            }


            conn.Close();

        }




        public DataTable getUsers()
        {

            SqlConnection conn = null;

            conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            conn.Open();

            if (conn.State != ConnectionState.Open)
            {
                throw new Exception("Can't connect to the DB, please check the credentials");

            }
            else
            {
                SqlCommand cmd = new SqlCommand("SELECT idUser, username, name  FROM [User] ORDER BY username", conn);
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;

                DataTable dt = new DataTable();
                sda.Fill(dt);
                return dt;


            }

            conn.Close();
        }


        public DataTable getInfo()
        {

            SqlConnection conn = null;

            conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            conn.Open();

            if (conn.State != ConnectionState.Open)
            {
                throw new Exception("Can't connect to the DB, please check the credentials");

            }
            else
            {
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 idRecord, Dn, Nombre, Referencia1, Referencia2, Cac, Fvc, Contacto, Tipificacion, Queja FROM Record  where Estatus not in ('Promesa', 'Rechazo', 'Proceso') and contador =0 order by idRecord desc", conn);

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;

                DataTable dt = new DataTable();
                sda.Fill(dt);


                if (dt.Rows.Count > 0)
                {
                    Object id = dt.Rows[0]["idRecord"];

                    SqlCommand cmd2 = new SqlCommand("update Record set Contador=Contador + 1 where idRecord=" + id, conn);
                    SqlDataAdapter sda2 = new SqlDataAdapter();
                    cmd2.ExecuteNonQuery();

                }
                conn.Close();
                return dt;
            }            
        }


        public DataTable findReg(string dn)
        {

            SqlConnection conn = null;

            conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            conn.Open();

            if (conn.State != ConnectionState.Open)
            {
                throw new Exception("Can't connect to the DB, please check the credentials");

            }
            else
            {
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 idRecord, dn, FVC, name1, name2, lastName, middleName, Phone1, Phone2, dateBorn, gender, cnip, CURP, userVal, email, statusDesc, modifiedby  FROM Record  where dn ='" + dn + "' order by idRecord", conn);

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;

                DataTable dt = new DataTable();
                sda.Fill(dt);
                conn.Close();
                return dt;
            }
        }





        public DataTable getFileId(int id)
        {

            SqlConnection conn = null;

            conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            conn.Open();
            DataTable dt = new DataTable();
            if (conn.State != ConnectionState.Open)
            {
                throw new Exception("Can't connect to the DB, please check the credentials");

            }
            else
            {
                SqlCommand cmd = new SqlCommand("SELECT *  FROM Record WHERE idFile=" + id + " ORDER BY idRecord", conn);
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;

                
                sda.Fill(dt);
                


            }

            conn.Close();
            return dt;
        }


        public DataTable getHistory()
        {

            SqlConnection conn = null;

            conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            conn.Open();

            if (conn.State != ConnectionState.Open)
            {
                throw new Exception("Can't connect to the DB, please check the credentials");

            }
            else
            {
                //SqlCommand cmd = new SqlCommand("SELECT idFile, name, date  FROM [File] ORDER BY idFile", conn);
                SqlCommand cmd = new SqlCommand("select distinct[file].idFile,  Nombre=name, Fecha=date from [file]", conn);
                
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;

                DataTable dt = new DataTable();
                sda.Fill(dt);
                conn.Close();
                return dt;
            }
        }

        public void deleteUser(string id)
        {

            SqlConnection conn = null;

            conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            conn.Open();

            if (conn.State != ConnectionState.Open)
            {
                throw new Exception("Can't connect to the DB, please check the credentials");

            }
            else
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM [User] WHERE idUser = '" + id + "'", conn);
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }



        public void delDB(string id)
        {

            int myid = Convert.ToInt32(id);

            SqlConnection conn = null;

            conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            conn.Open();

            if (conn.State != ConnectionState.Open)
            {
                throw new Exception("Can't connect to the DB, please check the credentials");

            }
            else
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Record WHERE idFile = " + myid, conn);
                cmd.ExecuteNonQuery();

                SqlCommand cmd2 = new SqlCommand("DELETE FROM [File] WHERE idFile = " + myid, conn);
                cmd2.ExecuteNonQuery();
            }
            conn.Close();
        }



        public void activateDB(string id)
        {

            int myid = Convert.ToInt32(id);

            SqlConnection conn = null;

            conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            conn.Open();

            if (conn.State != ConnectionState.Open)
            {
                throw new Exception("Can't connect to the DB, please check the credentials");

            }
            else
            {
                SqlCommand cmd = new SqlCommand("update Record set usedb = 1 where idFile = " + myid, conn);
                cmd.ExecuteNonQuery();

                SqlCommand cmd2 = new SqlCommand("update Record set usedb = 0 where idFile != " + myid, conn);
                cmd2.ExecuteNonQuery();
            }
            conn.Close();
        }




        public void addUser(string name, string usr, string pwd, string utype)
        {

            SqlConnection conn = null;

            conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            conn.Open();

            if (conn.State != ConnectionState.Open)
            {
                throw new Exception("Can't connect to the DB, please check the credentials");

            }
            else
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO [User] VALUES('" + name + "','" + usr + "','" + pwd + "','active','" + utype + "',0)", conn);
                cmd.ExecuteNonQuery();
            }
            conn.Close();

        }

        public void updateReg(int id, string Estatus, string Contacto, string Tipificacion, string Queja, string modifiedby, DateTime modifiedDate, string referencia1, string referencia2, string cac)
        {

            SqlConnection conn = null;

            conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            conn.Open();

            if (conn.State != ConnectionState.Open)
            {
                throw new Exception("Can't connect to the DB, please check the credentials");

            }
            else
            {
                SqlCommand cmd = new SqlCommand("update Record set Estatus='" + Estatus + "', Contacto='" + Contacto + "',Tipificacion='" + Tipificacion +
                    "', queja='" + Queja + "', Despachador='" + modifiedby + "', Fecha='" + modifiedDate.ToString("yyyy/MM/dd hh:mm") +
                    "', Referencia1='" + referencia1 + "', Referencia2='" + referencia2 + "', CAC='" + cac + "' where idRecord=" + id, conn);
                cmd.ExecuteNonQuery();
            }
            conn.Close();

        }

        public void recycleReg()
        {

            SqlConnection conn = null;

            conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            conn.Open();

            if (conn.State != ConnectionState.Open)
            {
                throw new Exception("Can't connect to the DB, please check the credentials");

            }
            else
            {
                SqlCommand cmd = new SqlCommand("update Record set status=0 where status=2", conn);
                cmd.ExecuteNonQuery();
            }
            conn.Close();

        }

        /// <summary>
        /// Cargar registro de DN vendido
        /// </summary>
        /// <param name="venta"></param>
        public void addVenta(Venta venta)
        {
            IList parameters = new ArrayList();

            parameters.Add(new Parametro("dn", venta.dn));
            parameters.Add(new Parametro("plan_carga", venta.plan_carga));
            parameters.Add(new Parametro("plan_vendido", venta.plan_vendido));
            parameters.Add(new Parametro("preg_1", venta.preg_1));
            parameters.Add(new Parametro("preg_1_Dia", venta.preg_1_Dia));
            parameters.Add(new Parametro("preg_1_Hora", venta.preg_1_Hora));
            parameters.Add(new Parametro("preg_2", venta.preg_2));
            parameters.Add(new Parametro("preg_3", venta.preg_3));
            parameters.Add(new Parametro("Tipo_Celular", venta.Tipo_Celular));
            parameters.Add(new Parametro("EdadCliente", venta.EdadCliente));
            parameters.Add(new Parametro("preg_4", venta.preg_4));
            parameters.Add(new Parametro("preg_5", venta.preg_5));
            parameters.Add(new Parametro("preg_6", venta.preg_6));
            parameters.Add(new Parametro("preg_7", venta.preg_7));
            parameters.Add(new Parametro("preg_8", venta.preg_8));
            parameters.Add(new Parametro("preg_9", venta.preg_9));
            parameters.Add(new Parametro("Modulo_SMS", venta.Modulo_SMS));
            parameters.Add(new Parametro("preg_10", venta.preg_10));
            parameters.Add(new Parametro("Nombre_Completo", venta.Nombre_Completo));
            parameters.Add(new Parametro("Fecha_Nacimiento = DateTime.MinValue;", venta.Fecha_Nacimiento));
            parameters.Add(new Parametro("Sexo", venta.Sexo));
            parameters.Add(new Parametro("Localidad_Nacimiento", venta.Localidad_Nacimiento));
            parameters.Add(new Parametro("Calle", venta.Calle));
            parameters.Add(new Parametro("Colonia", venta.Colonia));
            parameters.Add(new Parametro("Num_Externo", venta.Num_Externo));
            parameters.Add(new Parametro("Num_Interno", venta.Num_Interno));
            parameters.Add(new Parametro("CP", venta.CP));
            parameters.Add(new Parametro("Ciudad", venta.Ciudad));
            parameters.Add(new Parametro("Correo_Elctronico", venta.Correo_Elctronico));
            parameters.Add(new Parametro("RFC", venta.RFC));
            parameters.Add(new Parametro("Fecha_Venta = DateTime.MinValue;", venta.Fecha_Venta));
            parameters.Add(new Parametro("Usuario_Vent", venta.Usuario_Vent));
            parameters.Add(new Parametro("Estatus_Venta", venta.Estatus_Venta));
            parameters.Add(new Parametro("Motivos_No_Aceptacion", venta.Motivos_No_Aceptacion));
            parameters.Add(new Parametro("SubMotivos", venta.SubMotivos));
            parameters.Add(new Parametro("TipoPlan_OtraLinea", venta.TipoPlan_OtraLinea));
            parameters.Add(new Parametro("Compañia", venta.Compañia));
            parameters.Add(new Parametro("realizar_consulta_scoring", venta.realizar_consulta_scoring));
            parameters.Add(new Parametro("Puntaje_Scoring", venta.Puntaje_Scoring));
            parameters.Add(new Parametro("DeseaHorarioEspecifico", venta.DeseaHorarioEspecifico));
            parameters.Add(new Parametro("DiaClienteDeseaLlamada", venta.DiaClienteDeseaLlamada));
            parameters.Add(new Parametro("HoraClienteDeseaLlamada", venta.HoraClienteDeseaLlamada));
            parameters.Add(new Parametro("Municipio", venta.Municipio));
            parameters.Add(new Parametro("TelefonoFijo", venta.TelefonoFijo));
            parameters.Add(new Parametro("TelefonoMovil", venta.TelefonoMovil));


            Connection.Run("sp_insert_venta", parameters, ReturnDataType.Nothing);

        }

        /// <summary>
        /// Cargar registro de DN vendido
        /// </summary>
        /// <param name="venta"></param>
        public void updateVenta(Venta venta)
        {
            IList parameters = new ArrayList();

            parameters.Add(new Parametro("dn", venta.dn));
            parameters.Add(new Parametro("plan_carga", venta.plan_carga));
            parameters.Add(new Parametro("plan_vendido", venta.plan_vendido));
            parameters.Add(new Parametro("preg_1", venta.preg_1));
            parameters.Add(new Parametro("preg_1_Dia", venta.preg_1_Dia));
            parameters.Add(new Parametro("preg_1_Hora", venta.preg_1_Hora));
            parameters.Add(new Parametro("preg_2", venta.preg_2));
            parameters.Add(new Parametro("preg_3", venta.preg_3));
            parameters.Add(new Parametro("Tipo_Celular", venta.Tipo_Celular));
            parameters.Add(new Parametro("EdadCliente", venta.EdadCliente));
            parameters.Add(new Parametro("preg_4", venta.preg_4));
            parameters.Add(new Parametro("preg_5", venta.preg_5));
            parameters.Add(new Parametro("preg_6", venta.preg_6));
            parameters.Add(new Parametro("preg_7", venta.preg_7));
            parameters.Add(new Parametro("preg_8", venta.preg_8));
            parameters.Add(new Parametro("preg_9", venta.preg_9));
            parameters.Add(new Parametro("Modulo_SMS", venta.Modulo_SMS));
            parameters.Add(new Parametro("preg_10", venta.preg_10));
            parameters.Add(new Parametro("Nombre_Completo", venta.Nombre_Completo));
            parameters.Add(new Parametro("Fecha_Nacimiento = DateTime.MinValue;", venta.Fecha_Nacimiento));
            parameters.Add(new Parametro("Sexo", venta.Sexo));
            parameters.Add(new Parametro("Localidad_Nacimiento", venta.Localidad_Nacimiento));
            parameters.Add(new Parametro("Calle", venta.Calle));
            parameters.Add(new Parametro("Colonia", venta.Colonia));
            parameters.Add(new Parametro("Num_Externo", venta.Num_Externo));
            parameters.Add(new Parametro("Num_Interno", venta.Num_Interno));
            parameters.Add(new Parametro("CP", venta.CP));
            parameters.Add(new Parametro("Ciudad", venta.Ciudad));
            parameters.Add(new Parametro("Correo_Elctronico", venta.Correo_Elctronico));
            parameters.Add(new Parametro("RFC", venta.RFC));
            parameters.Add(new Parametro("Fecha_Venta = DateTime.MinValue;", venta.Fecha_Venta));
            parameters.Add(new Parametro("Usuario_Vent", venta.Usuario_Vent));
            parameters.Add(new Parametro("Estatus_Venta", venta.Estatus_Venta));
            parameters.Add(new Parametro("Motivos_No_Aceptacion", venta.Motivos_No_Aceptacion));
            parameters.Add(new Parametro("SubMotivos", venta.SubMotivos));
            parameters.Add(new Parametro("TipoPlan_OtraLinea", venta.TipoPlan_OtraLinea));
            parameters.Add(new Parametro("Compañia", venta.Compañia));
            parameters.Add(new Parametro("realizar_consulta_scoring", venta.realizar_consulta_scoring));
            parameters.Add(new Parametro("Puntaje_Scoring", venta.Puntaje_Scoring));
            parameters.Add(new Parametro("DeseaHorarioEspecifico", venta.DeseaHorarioEspecifico));
            parameters.Add(new Parametro("DiaClienteDeseaLlamada", venta.DiaClienteDeseaLlamada));
            parameters.Add(new Parametro("HoraClienteDeseaLlamada", venta.HoraClienteDeseaLlamada));
            parameters.Add(new Parametro("Municipio", venta.Municipio));
            parameters.Add(new Parametro("TelefonoFijo", venta.TelefonoFijo));
            parameters.Add(new Parametro("TelefonoMovil", venta.TelefonoMovil));


            Connection.Run("sp_update_venta", parameters, ReturnDataType.Nothing);

        }

        /// <summary>
        /// Cargar registro de DN vendido
        /// </summary>
        /// <param name="cobranza"></param>
        public void addCobranza(Cobranza cobranza)
        {
            IList parameters = new ArrayList();

            parameters.Add(new Parametro("dn", cobranza.dn));
            parameters.Add(new Parametro("confirmaPago", cobranza.confirmaPago));
            parameters.Add(new Parametro("reflejadoFE", cobranza.reflejadoFE));
            parameters.Add(new Parametro("pasado24hrs", cobranza.pasado24hrs));
            parameters.Add(new Parametro("comprobante", cobranza.comprobante));
            parameters.Add(new Parametro("motivoAdeudo", cobranza.motivoAdeudo));
            parameters.Add(new Parametro("tieneDuda", cobranza.tieneDuda));
            parameters.Add(new Parametro("Fecha_Cobranza", cobranza.Fecha_Cobranza));
            parameters.Add(new Parametro("Usuario_Cobranza", cobranza.Usuario_Cobranza));
            parameters.Add(new Parametro("Estatus_Cobranza", cobranza.Estatus_Cobranza));


            Connection.Run("sp_insert_cobranza", parameters, ReturnDataType.Nothing);

        }

        /// <summary>
        /// Cargar registro de DN vendido
        /// </summary>
        /// <param name="cobranza"></param>
        public void updateCobranza(Cobranza cobranza)
        {
            IList parameters = new ArrayList();

            parameters.Add(new Parametro("dn", cobranza.dn));
            parameters.Add(new Parametro("confirmaPago", cobranza.confirmaPago));
            parameters.Add(new Parametro("reflejadoFE", cobranza.reflejadoFE));
            parameters.Add(new Parametro("pasado24hrs", cobranza.pasado24hrs));
            parameters.Add(new Parametro("comprobante", cobranza.comprobante));
            parameters.Add(new Parametro("motivoAdeudo", cobranza.motivoAdeudo));
            parameters.Add(new Parametro("tieneDuda", cobranza.tieneDuda));
            parameters.Add(new Parametro("Fecha_Cobranza", cobranza.Fecha_Cobranza));
            parameters.Add(new Parametro("Usuario_Cobranza", cobranza.Usuario_Cobranza));
            parameters.Add(new Parametro("Estatus_Cobranza", cobranza.Estatus_Cobranza));


            Connection.Run("sp_update_cobranza", parameters, ReturnDataType.Nothing);

        }

        public DataTable isValidDn(string dn)
        {
            SqlConnection conn = null;

            conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            conn.Open();
            DataTable dt = new DataTable();
            if (conn.State != ConnectionState.Open)
            {
                throw new Exception("Can't connect to the DB, please check the credentials");

            }
            else
            {

                SqlCommand cmd = new SqlCommand("SELECT Top 1 Record.status, " +
                "Record.dateIni, " +
                "Record.dateFin, " +
                "Ventas.*, " +
                "Estatus_Activacion=Activaciones.Estatus, " +
                "Fecha_Activacion=Activaciones.Fecha_Encuesta, " +
                "Estatus_PrimerFactura=PrimerFactura.Estatus, " +
                "Fecha_PrimerFactura=PrimerFactura.Fecha_Encuesta, " +
                "Cobranzas.Estatus_Cobranza, " +
                "Cobranzas.Fecha_Cobranza FROM Record " +
                    "LEFT JOIN Ventas on Ventas.dn = Record.dn " +
                    "LEFT JOIN Activaciones on Activaciones.dn = Record.dn " +
                    "LEFT JOIN PrimerFactura on PrimerFactura.dn = Record.dn " +
                    "LEFT JOIN Cobranzas on Cobranzas.dn = Record.dn " +
                                                   "WHERE Record.dn = '" + dn + "' ORDER  BY CASE " +
                    "WHEN idCobranza is not null THEN idCobranza " +
                    "WHEN idPrimerFactura is not null THEN idPrimerFactura " +
                    "WHEN idActivaciones is not null THEN idActivaciones " +
                    "WHEN idVentas is not null THEN idVentas " +
                    "END DESC,idVentas DESC; ", conn);

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;

                sda.Fill(dt);
            }
            conn.Close();
            return dt;         
        }

        public void updateDn(string statusDn, string dn)
        {
            SqlConnection conn = null;

            conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            conn.Open();

            if (conn.State != ConnectionState.Open)
            {
                throw new Exception("Can't connect to the DB, please check the credentials");

            }
            else
            {
                SqlCommand cmd2 = new SqlCommand("update Record set status='" + statusDn + "'  where dn='" + dn + "'", conn);
                SqlDataAdapter sda2 = new SqlDataAdapter();
                cmd2.ExecuteNonQuery();


            }

            conn.Close();
        }

        public DataTable GetRecordsVenta(string p1, string p2)
        {
            SqlConnection conn = null;

            conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            conn.Open();
            DataTable dt = new DataTable();
            if (conn.State != ConnectionState.Open)
            {
                throw new Exception("Can't connect to the DB, please check the credentials");

            }
            else
            {

                SqlCommand cmd = new SqlCommand("SELECT [DN]" +
                  ",[NOMBRE]" +
                  ",[REFERENCIA1]" +
                  ",[REFERENCIA2]" +
                  ",[CAC]" +
                  ",[FVC]" +
                  ",[FECHA]" +
                  ",[CONTACTO]" +
                  ",[TIPIFICACION]" +
                  ",[QUEJA]" +
                  ",[DESPACHADOR]" +
                  " FROM Record " +
                    "WHERE CAST(Fecha AS DATE) >= '" + p1 + "' and CAST(Fecha AS DATE) <= '" + p2 + "'", conn);

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;

                sda.Fill(dt);
            }
            conn.Close();
            return dt; 
        }

        public DataTable GetRecordsActivacion(string p1, string p2)
        {
            SqlConnection conn = null;

            conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            conn.Open();
            DataTable dt = new DataTable();
            if (conn.State != ConnectionState.Open)
            {
                throw new Exception("Can't connect to the DB, please check the credentials");

            }
            else
            {

                SqlCommand cmd = new SqlCommand("SELECT [dn]" +
                  ",[preg_1] " +
                  ",[preg_2] " +
                  ",[preg_3] " +
                  ",[preg_3_Dia] " +
                  ",[preg_3_Hora] " +
                  ",[preg_4] " +
                  ",[preg_5] " +
                  ",[Se_realizaron_cambios] " +
                  ",[preg_6] " +
                  ",[preg_7] " +
                  ",[Fecha_Encuesta] " +
                  ",[cod_plan_mig] " +
                  ",[usuario] " +
                  ",[Estatus] " +
                  ",[Motivos_No_Aceptacion] " +
                  ",[Equipo] " +
                  ",[Nombre_Completo] " +
                  ",[Fecha_Nacimiento] " +
                  ",[Sexo] " +
                  ",[Localidad_Nacimiento] " +
                  ",[Calle] " +
                  ",[Colonia] " +
                  ",[Num_Externo] " +
                  ",[Num_Interno] " +
                  ",[CP] " +
                  ",[Ciudad] " +
                  ",[Correo_Elctronico] " +
                  ",[RFC] " +
                  ",[EnvioContrato] " +
                  ",[SubMotivos]  FROM Activaciones " +
                    "WHERE CAST(Fecha_Encuesta AS DATE) >= '" + p1 + "' and CAST(Fecha_Encuesta AS DATE) <= '" + p2 + "'", conn);

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;

                sda.Fill(dt);
            }
            conn.Close();
            return dt;
        }

        public DataTable GetRecordsPrimerFactura(string p1, string p2)
        {
            SqlConnection conn = null;

            conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            conn.Open();
            DataTable dt = new DataTable();
            if (conn.State != ConnectionState.Open)
            {
                throw new Exception("Can't connect to the DB, please check the credentials");

            }
            else
            {

                SqlCommand cmd = new SqlCommand("SELECT [preg_1]"+
                  ",[preg_2]"+
                  ",[preg_3]"+
                  ",[preg_3_Dia]"+
                  ",[preg_3_Hora]"+
                  ",[preg_4]"+
                  ",[Preg_4_Dia]"+
                  ",[Preg_4_Hora]"+
                  ",[preg_5]"+
                  ",[preg_6]"+
                  ",[preg_7]"+
                  ",[preg_8]"+
                  ",[preg_9]"+
                  ",[preg_10]"+
                  ",[preg_11]"+
                  ",[Preg_11_ClubMovistar]"+
                  ",[Preg_11_EstatusRegistro]"+
                  ",[OfrecimientoITAU]"+
                  ",[preg_12]"+
                  ",[EsTitularTarjeta]"+
                  ",[Ingresos7000]"+
                  ",[preg_12_Estatus]"+
                  ",[Nombre_Ejecutivo_ITAU]"+
                  ",[Fecha_Encuesta]"+
                  ",[dn]"+
                  ",[cod_plan_mig]"+
                  ",[usuario]"+
                  ",[Estatus]"+
                  ",[LugarPagoRecurrente]"+
                  ",[OtroLugarPago]"+
                  ",[EnvioReferenciaBancaria]"+
                  ",[CorreoCliente]"+
                  ",[DomiciliaTDC]"+
                  "FROM [dbo].[PrimerFactura]"+
                    "WHERE CAST(Fecha_Encuesta AS DATE) >= '" + p1 + "' and CAST(Fecha_Encuesta AS DATE) <= '" + p2 + "'", conn);

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;

                sda.Fill(dt);
            }
            conn.Close();
            return dt;
        }

        public DataTable GetRecordsCobranza(string p1, string p2)
        {
            SqlConnection conn = null;

            conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            conn.Open();
            DataTable dt = new DataTable();
            if (conn.State != ConnectionState.Open)
            {
                throw new Exception("Can't connect to the DB, please check the credentials");

            }
            else
            {

                SqlCommand cmd = new SqlCommand("SELECT [dn]"+
                      ",[confirmaPago]"+
                      ",[reflejadoFE]"+
                      ",[pasado24hrs]"+
                      ",[comprobante]"+
                      ",[motivoAdeudo]"+
                      ",[tieneDuda]"+
                      ",[Fecha_Cobranza]"+
                      ",[Usuario_Cobranza]"+
                      ",[Estatus_Cobranza]"+
                    "FROM [dbo].[Cobranzas]" +
                    "WHERE CAST(Fecha_Cobranza AS DATE) >= '" + p1 + "' and CAST(Fecha_Cobranza AS DATE) <= '" + p2 + "'", conn);

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;

                sda.Fill(dt);
            }
            conn.Close();
            return dt;
        }

        public void addActivacion(Activacion activacion)
        {
            IList parameters = new ArrayList();

            parameters.Add(new Parametro("dn", activacion.dn));
            parameters.Add(new Parametro("preg_1 ", activacion.preg_1));
            parameters.Add(new Parametro("preg_2 ", activacion.preg_2));
            parameters.Add(new Parametro("preg_3 ", activacion.preg_3));
            parameters.Add(new Parametro("preg_3_Dia ", activacion.preg_3_Dia));
            parameters.Add(new Parametro("preg_3_Hora ", activacion.preg_3_Hora));
            parameters.Add(new Parametro("preg_4 ", activacion.preg_4));
            parameters.Add(new Parametro("preg_5 ", activacion.preg_5));
            parameters.Add(new Parametro("Se_realizaron_cambios ", activacion.Se_realizaron_cambios));
            parameters.Add(new Parametro("preg_6 ", activacion.preg_6));
            parameters.Add(new Parametro("preg_7 ", activacion.preg_7));
            parameters.Add(new Parametro("Fecha_Encuesta", activacion.Fecha_Encuesta));
            parameters.Add(new Parametro("cod_plan_mig ", activacion.cod_plan_mig));
            parameters.Add(new Parametro("usuario ", activacion.usuario));
            parameters.Add(new Parametro("Estatus ", activacion.Estatus));
            parameters.Add(new Parametro("Motivos_No_Aceptacion ", activacion.Motivos_No_Aceptacion));
            parameters.Add(new Parametro("Equipo ", activacion.Equipo));
            parameters.Add(new Parametro("Nombre_Completo ", activacion.Nombre_Completo));
            parameters.Add(new Parametro("Fecha_Nacimiento", activacion.Fecha_Nacimiento));
            parameters.Add(new Parametro("Sexo ", activacion.Sexo));
            parameters.Add(new Parametro("Localidad_Nacimiento ", activacion.Localidad_Nacimiento));
            parameters.Add(new Parametro("Calle ", activacion.Calle));
            parameters.Add(new Parametro("Colonia ", activacion.Colonia));
            parameters.Add(new Parametro("Num_Externo ", activacion.Num_Externo));
            parameters.Add(new Parametro("Num_Interno ", activacion.Num_Interno));
            parameters.Add(new Parametro("CP ", activacion.CP));
            parameters.Add(new Parametro("Ciudad ", activacion.Ciudad));
            parameters.Add(new Parametro("Correo_Elctronico ", activacion.Correo_Elctronico));
            parameters.Add(new Parametro("RFC ", activacion.RFC));
            parameters.Add(new Parametro("EnvioContrato ", activacion.EnvioContrato));
            parameters.Add(new Parametro("SubMotivos ", activacion.SubMotivos));
            parameters.Add(new Parametro("Municipio ", activacion.Municipio));
            parameters.Add(new Parametro("TelefonoFijo ", activacion.TelefonoFijo));
            parameters.Add(new Parametro("TelefonoMovil", activacion.TelefonoMovil));

            Connection.Run("sp_insert_activacion", parameters, ReturnDataType.Nothing);
        }

        public void updateActivacion(Activacion activacion)
        {
            IList parameters = new ArrayList();

            parameters.Add(new Parametro("dn", activacion.dn));
            parameters.Add(new Parametro("preg_1 ", activacion.preg_1));
            parameters.Add(new Parametro("preg_2 ", activacion.preg_2));
            parameters.Add(new Parametro("preg_3 ", activacion.preg_3));
            parameters.Add(new Parametro("preg_3_Dia ", activacion.preg_3_Dia));
            parameters.Add(new Parametro("preg_3_Hora ", activacion.preg_3_Hora));
            parameters.Add(new Parametro("preg_4 ", activacion.preg_4));
            parameters.Add(new Parametro("preg_5 ", activacion.preg_5));
            parameters.Add(new Parametro("Se_realizaron_cambios ", activacion.Se_realizaron_cambios));
            parameters.Add(new Parametro("preg_6 ", activacion.preg_6));
            parameters.Add(new Parametro("preg_7 ", activacion.preg_7));
            parameters.Add(new Parametro("Fecha_Encuesta", activacion.Fecha_Encuesta));
            parameters.Add(new Parametro("cod_plan_mig ", activacion.cod_plan_mig));
            parameters.Add(new Parametro("usuario ", activacion.usuario));
            parameters.Add(new Parametro("Estatus ", activacion.Estatus));
            parameters.Add(new Parametro("Motivos_No_Aceptacion ", activacion.Motivos_No_Aceptacion));
            parameters.Add(new Parametro("Equipo ", activacion.Equipo));
            parameters.Add(new Parametro("Nombre_Completo ", activacion.Nombre_Completo));
            parameters.Add(new Parametro("Fecha_Nacimiento", activacion.Fecha_Nacimiento));
            parameters.Add(new Parametro("Sexo ", activacion.Sexo));
            parameters.Add(new Parametro("Localidad_Nacimiento ", activacion.Localidad_Nacimiento));
            parameters.Add(new Parametro("Calle ", activacion.Calle));
            parameters.Add(new Parametro("Colonia ", activacion.Colonia));
            parameters.Add(new Parametro("Num_Externo ", activacion.Num_Externo));
            parameters.Add(new Parametro("Num_Interno ", activacion.Num_Interno));
            parameters.Add(new Parametro("CP ", activacion.CP));
            parameters.Add(new Parametro("Ciudad ", activacion.Ciudad));
            parameters.Add(new Parametro("Correo_Elctronico ", activacion.Correo_Elctronico));
            parameters.Add(new Parametro("RFC ", activacion.RFC));
            parameters.Add(new Parametro("EnvioContrato ", activacion.EnvioContrato));
            parameters.Add(new Parametro("SubMotivos ", activacion.SubMotivos));
            parameters.Add(new Parametro("Municipio ", activacion.Municipio));
            parameters.Add(new Parametro("TelefonoFijo ", activacion.TelefonoFijo));
            parameters.Add(new Parametro("TelefonoMovil", activacion.TelefonoMovil));

            Connection.Run("sp_update_activacion", parameters, ReturnDataType.Nothing);
        }

        public void addPrimerFactura(PrimerFactura primerFactura)
        {
            IList parameters = new ArrayList();

            parameters.Add(new Parametro("preg_1", primerFactura.preg_1));
            parameters.Add(new Parametro("preg_2", primerFactura.preg_2));
            parameters.Add(new Parametro("preg_3", primerFactura.preg_3));
            parameters.Add(new Parametro("preg_3_Dia", primerFactura.preg_3_Dia));
            parameters.Add(new Parametro("preg_3_Hora", primerFactura.preg_3_Hora));
            parameters.Add(new Parametro("preg_4", primerFactura.preg_4));
            parameters.Add(new Parametro("Preg_4_Dia", primerFactura.Preg_4_Dia));
            parameters.Add(new Parametro("Preg_4_Hora", primerFactura.Preg_4_Hora));
            parameters.Add(new Parametro("preg_5", primerFactura.preg_5));
            parameters.Add(new Parametro("preg_6", primerFactura.preg_6));
            parameters.Add(new Parametro("preg_7", primerFactura.preg_7));
            parameters.Add(new Parametro("preg_8", primerFactura.preg_8));
            parameters.Add(new Parametro("preg_9", primerFactura.preg_9));
            parameters.Add(new Parametro("preg_10", primerFactura.preg_10));
            parameters.Add(new Parametro("preg_11", primerFactura.preg_11));
            parameters.Add(new Parametro("Preg_11_ClubMovistar", primerFactura.Preg_11_ClubMovistar));
            parameters.Add(new Parametro("Preg_11_EstatusRegistro", primerFactura.Preg_11_EstatusRegistro));
            parameters.Add(new Parametro("OfrecimientoITAU", primerFactura.OfrecimientoITAU));
            parameters.Add(new Parametro("preg_12", primerFactura.preg_12));
            parameters.Add(new Parametro("EsTitularTarjeta", primerFactura.EsTitularTarjeta));
            parameters.Add(new Parametro("Ingresos7000", primerFactura.Ingresos7000));
            parameters.Add(new Parametro("preg_12_Estatus", primerFactura.preg_12_Estatus));
            parameters.Add(new Parametro("Nombre_Ejecutivo_ITAU", primerFactura.Nombre_Ejecutivo_ITAU));
            parameters.Add(new Parametro("Fecha_Encuesta", primerFactura.Fecha_Encuesta));
            parameters.Add(new Parametro("dn", primerFactura.dn));
            parameters.Add(new Parametro("cod_plan_mig", primerFactura.cod_plan_mig));
            parameters.Add(new Parametro("usuario", primerFactura.usuario));
            parameters.Add(new Parametro("Estatus", primerFactura.Estatus));
            parameters.Add(new Parametro("LugarPagoRecurrente", primerFactura.LugarPagoRecurrente));
            parameters.Add(new Parametro("OtroLugarPago", primerFactura.OtroLugarPago));
            parameters.Add(new Parametro("EnvioReferenciaBancaria", primerFactura.EnvioReferenciaBancaria));
            parameters.Add(new Parametro("CorreoCliente", primerFactura.CorreoCliente));
            parameters.Add(new Parametro("DomiciliaTDC", primerFactura.DomiciliaTDC));


            Connection.Run("sp_insert_primerFactura", parameters, ReturnDataType.Nothing);
        }

        public void updatePrimerFactura(PrimerFactura primerFactura)
        {
            IList parameters = new ArrayList();

            parameters.Add(new Parametro("preg_1", primerFactura.preg_1));
            parameters.Add(new Parametro("preg_2", primerFactura.preg_2));
            parameters.Add(new Parametro("preg_3", primerFactura.preg_3));
            parameters.Add(new Parametro("preg_3_Dia", primerFactura.preg_3_Dia));
            parameters.Add(new Parametro("preg_3_Hora", primerFactura.preg_3_Hora));
            parameters.Add(new Parametro("preg_4", primerFactura.preg_4));
            parameters.Add(new Parametro("Preg_4_Dia", primerFactura.Preg_4_Dia));
            parameters.Add(new Parametro("Preg_4_Hora", primerFactura.Preg_4_Hora));
            parameters.Add(new Parametro("preg_5", primerFactura.preg_5));
            parameters.Add(new Parametro("preg_6", primerFactura.preg_6));
            parameters.Add(new Parametro("preg_7", primerFactura.preg_7));
            parameters.Add(new Parametro("preg_8", primerFactura.preg_8));
            parameters.Add(new Parametro("preg_9", primerFactura.preg_9));
            parameters.Add(new Parametro("preg_10", primerFactura.preg_10));
            parameters.Add(new Parametro("preg_11", primerFactura.preg_11));
            parameters.Add(new Parametro("Preg_11_ClubMovistar", primerFactura.Preg_11_ClubMovistar));
            parameters.Add(new Parametro("Preg_11_EstatusRegistro", primerFactura.Preg_11_EstatusRegistro));
            parameters.Add(new Parametro("OfrecimientoITAU", primerFactura.OfrecimientoITAU));
            parameters.Add(new Parametro("preg_12", primerFactura.preg_12));
            parameters.Add(new Parametro("EsTitularTarjeta", primerFactura.EsTitularTarjeta));
            parameters.Add(new Parametro("Ingresos7000", primerFactura.Ingresos7000));
            parameters.Add(new Parametro("preg_12_Estatus", primerFactura.preg_12_Estatus));
            parameters.Add(new Parametro("Nombre_Ejecutivo_ITAU", primerFactura.Nombre_Ejecutivo_ITAU));
            parameters.Add(new Parametro("Fecha_Encuesta", primerFactura.Fecha_Encuesta));
            parameters.Add(new Parametro("dn", primerFactura.dn));
            parameters.Add(new Parametro("cod_plan_mig", primerFactura.cod_plan_mig));
            parameters.Add(new Parametro("usuario", primerFactura.usuario));
            parameters.Add(new Parametro("Estatus", primerFactura.Estatus));
            parameters.Add(new Parametro("LugarPagoRecurrente", primerFactura.LugarPagoRecurrente));
            parameters.Add(new Parametro("OtroLugarPago", primerFactura.OtroLugarPago));
            parameters.Add(new Parametro("EnvioReferenciaBancaria", primerFactura.EnvioReferenciaBancaria));
            parameters.Add(new Parametro("CorreoCliente", primerFactura.CorreoCliente));
            parameters.Add(new Parametro("DomiciliaTDC", primerFactura.DomiciliaTDC));

            Connection.Run("sp_update_primerFactura", parameters, ReturnDataType.Nothing);
        }
    }
}
