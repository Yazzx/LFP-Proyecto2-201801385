using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2
{
    class ErrorSintáctico
    {
        String tipoquequería, tipoquevenía, contenidoToken;
        int numero;

        public ErrorSintáctico(int numero, string tipoquequería, string tipoquevenía, string contenidoToken)
        {
            this.numero = numero;
            this.tipoquequería = tipoquequería;
            this.tipoquevenía = tipoquevenía;
            this.contenidoToken = contenidoToken;
        }


        public int getnumero()
        {
            return numero;
        }
        public void setnumero(int numero)
        {
            this.numero = numero;
        }

        public String getContenidoToken()
        {
            return contenidoToken;
        }
        public void setContenidoToken(String nuevoo)
        {
            this.contenidoToken = nuevoo;
        }

        public String getTipoquequería()
        {
            return tipoquequería;
        }
        public void setTipoquequería(String nuevoo)
        {
            this.tipoquequería = nuevoo;
        }

        public String getTipoquevenia()
        {
            return tipoquevenía;
        }
        public void setTipoquevenia(String nuevoo)
        {
            this.tipoquevenía = nuevoo;
        }
    }
}
