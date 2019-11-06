using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2
{
    class TokensC
    {
        public enum TipoEspecífico
        {
            PALABRA_RESERVADA,
            PR_CLASS,
            PR_STATIC,
            PR_VOID,
            // declaracion de variables
            pr_int,
            pr_float,
            pr_char,
            pr_string,
            pr_bool,
            // cosas que abren y cierran
            llave_abrir,
            llave_cerrar,
            parentesis_abrir,
            parentesis_cerrar,
            corchete_abrir,
            corchete_cerrar,
            //cosos x
            coma,
            puntoycoma,
            igual

        }
    }
}
