using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2
{
    class TokenC
    {
        public enum Tipo
        {

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
            igual,
            cadena,
            numero, 
            espacio,
            comillas_simples,
            comillas_dobles,
            // operadores
            mas,
            menos,
            por, 
            dividido,
            console_writeline,
            mayor_que,
            menor_que,
            diferente_que,
            igualigual,
            mayor_igual,
            menor_igual,
            masmas,
            menosmenos,
            // otras sentencias
            un_if,
            un_else,
            un_switch,
            un_case,
            un_break,
            un_while,
            un_new,
            un_for,
            un_true,
            un_false,
            // otros
            comentario_unalinea,
            cmultilinea_abrir,
            cmultilinea_cerrar,

            // otros pt.2 

            nombre_algo,
            float_algo,
            cuerpo_com1l,
            cuerpo_comml
            



        }

        private Tipo tipoToken;
        private string valor;

        public TokenC(Tipo tipoDelToken, String val)
        {
            this.tipoToken = tipoDelToken;
            this.valor = val;
        }

        public String getValorToken()
        {
            return valor;
        }

        public Tipo getTipo()
        {
            return this.tipoToken;
        }

        public String GetTipoString()
        {
            switch (tipoToken)
            {
                case Tipo.coma:
                    return "coma";
                case Tipo.corchete_abrir:
                    return "corchete abrir";
                case Tipo.corchete_cerrar:
                    return "corchete cerrar";
                case Tipo.igual:
                    return "signo igual";
                case Tipo.llave_abrir:
                    return "llave abrir";
                case Tipo.llave_cerrar:
                    return "llave cerrar";
                case Tipo.parentesis_abrir:
                    return "parentesis abrir";
                case Tipo.parentesis_cerrar:
                    return "parentesis cerrar";
                case Tipo.pr_bool:
                    return "pr_bool";
                case Tipo.pr_char:
                    return "pr_char";
                case Tipo.PR_CLASS:
                    return "pr_class";
                case Tipo.pr_float:
                    return "pr_float";
                case Tipo.pr_int:
                    return "pr_int";
                case Tipo.PR_STATIC:
                    return "static";
                case Tipo.pr_string:
                    return "pr_string";
                case Tipo.PR_VOID:
                    return "pr_void";
                case Tipo.puntoycoma:
                    return "signo punto y coma";
                case Tipo.cadena:
                    return "cadena de caracteres";
                case Tipo.numero:
                    return "numero";
                case Tipo.espacio:
                    return "espacio";
                case Tipo.comillas_dobles:
                    return "comillas dobles";
                case Tipo.comillas_simples:
                    return "comillas simples";
                case Tipo.mas:
                    return "signo mas";
                case Tipo.menos:
                    return "signo menos";
                case Tipo.dividido:
                    return "signo dividido";
                case Tipo.por:
                    return "signo por";
                case Tipo.console_writeline:
                    return "Console.Writeline";
                case Tipo.mayor_que:
                    return "mayor que";
                case Tipo.menor_que:
                    return "menor que";
                case Tipo.diferente_que:
                    return "diferente que";
                case Tipo.igualigual:
                    return "igual igual";
                case Tipo.mayor_igual:
                    return "mayor igual";
                case Tipo.menor_igual:
                    return "menor igual";
                case Tipo.masmas:
                    return "mas mas";
                case Tipo.menosmenos:
                    return "menos menos";
                case Tipo.un_if:
                    return "un if";
                case Tipo.un_else:
                    return "un else";
                case Tipo.un_switch:
                    return "un switch";
                case Tipo.un_case:
                    return "un case";
                case Tipo.un_break:
                    return "un break";
                case Tipo.un_while:
                    return "un while";
                case Tipo.un_new:
                    return "un new";
                case Tipo.un_for:
                    return "un for";
                case Tipo.comentario_unalinea:
                    return "comentario una linea";
                case Tipo.cmultilinea_abrir:
                    return "comentario abrir";
                case Tipo.cmultilinea_cerrar:
                    return "comentario cerrar";
                case Tipo.un_true:
                    return "verdadero";
                case Tipo.un_false:
                    return "falso";
                case Tipo.float_algo:
                    return "número float";
                case Tipo.cuerpo_com1l:
                    return "cuerpo comentario de una línea";
                case Tipo.cuerpo_comml:
                    return "cuerpo cometario 2+ líneas";
                default:
                    return "desconocido";
            }
        }
    }
}
