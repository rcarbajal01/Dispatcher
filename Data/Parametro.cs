using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class Parametro
    {
        #region Member Variables
        protected string _Nombre;
        protected object _Valor;
        #endregion

        #region Constructors
        public Parametro() { }

        public Parametro(string name, object value)
        {
            _Nombre = name;
            _Valor = value;
        }
        public Parametro(object value)
        {
            _Valor = value;
        }
        #endregion

        #region Public Properties

        public virtual String Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        public virtual object Valor
        {
            get { return _Valor; }
            set { _Valor = value; }
        }

        #endregion
    }


}
