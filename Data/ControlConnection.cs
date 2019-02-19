using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;

namespace Data
{
    public enum ReturnDataType
    {
        Nothing,
        Scalar,
        Dataset
    }
    [Serializable]
    public class ControlConnection
    {
        #region Member Variables
        private string StringCon = ConfigurationManager.ConnectionStrings["conString"].ToString();
        private SqlConnection sqlConnection;
        private SqlTransaction sqlTransaction;
        #endregion

        #region Constructors
        public ControlConnection()
        {
        }
        #endregion

        public void BeginTran()
        {
            if (sqlConnection == null)
                sqlConnection = new SqlConnection(StringCon);

            if (sqlConnection.State != ConnectionState.Open)
                sqlConnection.Open();

            sqlTransaction = sqlConnection.BeginTransaction();
        }

        public void CommitTran()
        {
            if (sqlConnection != null && sqlConnection.State == ConnectionState.Open)
            {
                sqlTransaction.Commit();
                sqlTransaction = null;
                sqlConnection.Close();
                sqlConnection = null;
            }
        }

        public void RollbackTran()
        {
            if (sqlConnection != null && sqlConnection.State == ConnectionState.Open)
            {
                sqlTransaction.Rollback();
                sqlTransaction = null;
                sqlConnection.Close();
                sqlConnection = null;
            }
        }



        public object Run(string spName, IList parameters, ReturnDataType returnValue)
        {
            if (sqlConnection == null)
                sqlConnection = new SqlConnection(StringCon);

            if (sqlConnection.State != ConnectionState.Open)
                sqlConnection.Open();

            object valueReturn = null;
            SqlParameter[] SQLparameters = new SqlParameter[parameters.Count];

            int index = 0;
            foreach (Parametro obj in parameters)
            {
                SQLparameters[index] = new SqlParameter(obj.Nombre, obj.Valor);

                index++;
            }

            switch (returnValue)
            {
                case ReturnDataType.Nothing:
                    if (sqlTransaction == null)
                    {
                        SqlHelper.ExecuteNonQuery(sqlConnection, spName, SQLparameters);
                        sqlConnection.Close();
                    }
                    else
                        SqlHelper.ExecuteNonQuery(sqlTransaction, spName, SQLparameters);
                    valueReturn = null;
                    break;
                case ReturnDataType.Scalar:
                    if (sqlTransaction == null)
                    {
                        valueReturn = SqlHelper.ExecuteScalar(sqlConnection, spName, SQLparameters);
                        sqlConnection.Close();
                    }
                    else
                        valueReturn = SqlHelper.ExecuteScalar(sqlTransaction, spName, SQLparameters);
                    break;
                case ReturnDataType.Dataset:
                    if (sqlTransaction == null)
                    {
                        valueReturn = SqlHelper.ExecuteDataset(sqlConnection, spName, SQLparameters);
                        sqlConnection.Close();
                    }
                    else
                        valueReturn = SqlHelper.ExecuteDataset(sqlTransaction, spName, SQLparameters);
                    break;
                default:
                    break;
            }
            return valueReturn;
        }
    }
}
