using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2
{
    class AnalizadorSintáctico
    {
        int controlToken;
        TokenC tokenActual;
        LinkedList<TokenC> listaTok;

        public void F()
        {
            if (tokenActual.getTipo() == TokenC.Tipo.parentesis_abrir)
            {
                //F->  (E)
                emparejar(Token.Tipo.PARENTESIS_IZQ);
                E();
                emparejar(Token.Tipo.PARENTESIS_DER);
            }
            else
            {
                //F->  NUMERO
                emparejar(Token.Tipo.NUMERO_ENTERO);
            }
        }

        // metodo para ver lo que es con lo que tendría que venir y 
        // si es o no el último token
        public void emparejar(TokenC.Tipo tipo)
        {
            if (tokenActual.getTipo() != tipo)
            {
                Console.WriteLine("Error!! Se esperaba " + getTipoParaError(tipo) + " :C" );
            }
            if (tokenActual.getTipo() != TokenC.Tipo.ultimo)
            {
                controlToken += 1;
                tokenActual = listaTok.ElementAt(controlToken);
            }
        }

        // lista de tipos para error
        public string getTipodeError(TokenC.Tipo tipo)
        {
            switch (tipo)
            {
                case TokenC.Tipo.coma:
                    return "coma";
                case TokenC.Tipo.corchete_abrir:
                    return "corchete abrir";
                case TokenC.Tipo.corchete_cerrar:
                    return "corchete cerrar";
                case TokenC.Tipo.igual:
                    return "signo igual";
                case TokenC.Tipo.llave_abrir:
                    return "llave abrir";
                case TokenC.Tipo.llave_cerrar:
                    return "llave cerrar";
                case TokenC.Tipo.parentesis_abrir:
                    return "parentesis abrir";
                case TokenC.Tipo.parentesis_cerrar:
                    return "parentesis cerrar";
                case TokenC.Tipo.pr_bool:
                    return "pr_bool";
                case TokenC.Tipo.pr_char:
                    return "pr_char";
                case TokenC.Tipo.PR_CLASS:
                    return "pr_class";
                case TokenC.Tipo.pr_float:
                    return "pr_float";
                case TokenC.Tipo.pr_int:
                    return "pr_int";
                case TokenC.Tipo.PR_STATIC:
                    return "static";
                case TokenC.Tipo.pr_string:
                    return "pr_string";
                case TokenC.Tipo.PR_VOID:
                    return "pr_void";
                case TokenC.Tipo.puntoycoma:
                    return "signo punto y coma";
                case TokenC.Tipo.cadena:
                    return "cadena de caracteres";
                case TokenC.Tipo.numero:
                    return "numero";
                case TokenC.Tipo.espacio:
                    return "espacio";
                case TokenC.Tipo.comillas_dobles:
                    return "comillas dobles";
                case TokenC.Tipo.comillas_simples:
                    return "comillas simples";
                case TokenC.Tipo.mas:
                    return "signo mas";
                case TokenC.Tipo.menos:
                    return "signo menos";
                case TokenC.Tipo.dividido:
                    return "signo dividido";
                case TokenC.Tipo.por:
                    return "signo por";
                case TokenC.Tipo.console_writeline:
                    return "Console.Writeline";
                case TokenC.Tipo.mayor_que:
                    return "mayor que";
                case TokenC.Tipo.menor_que:
                    return "menor que";
                case TokenC.Tipo.diferente_que:
                    return "diferente que";
                case TokenC.Tipo.igualigual:
                    return "igual igual";
                case TokenC.Tipo.mayor_igual:
                    return "mayor igual";
                case TokenC.Tipo.menor_igual:
                    return "menor igual";
                case TokenC.Tipo.masmas:
                    return "mas mas";
                case TokenC.Tipo.menosmenos:
                    return "menos menos";
                case TokenC.Tipo.un_if:
                    return "un if";
                case TokenC.Tipo.un_else:
                    return "un else";
                case TokenC.Tipo.un_switch:
                    return "un switch";
                case TokenC.Tipo.un_case:
                    return "un case";
                case TokenC.Tipo.un_break:
                    return "un break";
                case TokenC.Tipo.un_while:
                    return "un while";
                case TokenC.Tipo.un_new:
                    return "un new";
                case TokenC.Tipo.un_for:
                    return "un for";
                case TokenC.Tipo.comentario_unalinea:
                    return "comentario una linea";
                case TokenC.Tipo.cmultilinea_abrir:
                    return "comentario abrir";
                case TokenC.Tipo.cmultilinea_cerrar:
                    return "comentario cerrar";
                case TokenC.Tipo.un_true:
                    return "verdadero";
                case TokenC.Tipo.un_false:
                    return "falso";
                case TokenC.Tipo.float_algo:
                    return "número float";
                case TokenC.Tipo.cuerpo_com1l:
                    return "cuerpo comentario de una línea";
                case TokenC.Tipo.cuerpo_comml:
                    return "cuerpo cometario 2+ líneas";
                case TokenC.Tipo.caracter:
                    return "caracter";
                case TokenC.Tipo.ultimo:
                    return "el último :C";
                default:
                    return "desconocido";
            }
        }

    }
}
