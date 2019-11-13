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
        LinkedList<TokenC> listaTokens;
        public String cadena_traducción = "#Texto en Python \n \n", cadena_auxiliar;

        //BANDERAS 

        bool esint = false;
        Boolean esfloat = false;
        Boolean eschar = false;
        Boolean esstring = false;
        Boolean esbool = false;

        public void parsear(LinkedList<TokenC> tokens)
        {
            this.listaTokens = tokens;
            controlToken = 0;
            tokenActual = listaTokens.ElementAt(controlToken);
            clase();
        }
        public void clase()
        {
            emparejar(TokenC.Tipo.PR_CLASS);
            emparejar(TokenC.Tipo.nombre_algo);
            emparejar(TokenC.Tipo.llave_abrir);
            emparejar(TokenC.Tipo.PR_STATIC);
            emparejar(TokenC.Tipo.PR_VOID);
            emparejar(TokenC.Tipo.pr_main);
            emparejar(TokenC.Tipo.parentesis_abrir);
            // aquí va el string args
            emparejar(TokenC.Tipo.parentesis_cerrar);
            emparejar(TokenC.Tipo.llave_abrir);
            // todo el metodo dentro de la clase;
            contenido();

            emparejar(TokenC.Tipo.llave_cerrar);
            emparejar(TokenC.Tipo.llave_cerrar);

            
        }
        public void contenido()
        {
            if (tokenActual.getTipo() == TokenC.Tipo.pr_int || tokenActual.getTipo() == TokenC.Tipo.pr_float
                || tokenActual.getTipo() == TokenC.Tipo.pr_string || tokenActual.getTipo() == TokenC.Tipo.pr_bool
                || tokenActual.getTipo() == TokenC.Tipo.pr_char)
            {
                declaracionVariables();
            }

        }

        public void declaracionVariables()
        {
            if (tokenActual.getTipo() == TokenC.Tipo.pr_int)
            {
                emparejar(TokenC.Tipo.pr_int);
                esint = true;
            }
            else if (tokenActual.getTipo() == TokenC.Tipo.pr_float)
            {
                emparejar(TokenC.Tipo.pr_float);
                esfloat = true;
            }
            else if (tokenActual.getTipo() == TokenC.Tipo.pr_string)
            {
                emparejar(TokenC.Tipo.pr_string);
                esstring = true;
            } 
            else if (tokenActual.getTipo() == TokenC.Tipo.pr_bool)
            {
                emparejar(TokenC.Tipo.pr_bool);
                esbool = true;
            } 
            else if (tokenActual.getTipo() == TokenC.Tipo.pr_char)
            {
                emparejar(TokenC.Tipo.pr_char);
                eschar = true;
            }

            cadena_auxiliar = "";
            cadena_auxiliar += tokenActual.getValorToken();
            emparejar(TokenC.Tipo.nombre_algo);                     

            // si viene un igual empareja
            if (tokenActual.getTipo() == TokenC.Tipo.igual)
            {
                emparejar(TokenC.Tipo.igual);
                cadena_traducción += cadena_auxiliar +  " = ";
                asignación();
            }
            else
            {
                cadena_auxiliar = "";
            }

            otradeclaración();

            cadena_traducción += "\n";
            emparejar(TokenC.Tipo.puntoycoma);
            esint = false;
            esfloat = false;
            eschar = false;
            esstring = false;
            esbool = false;
            
            contenido();
        }

        public void asignación()
        {
            Console.WriteLine(tokenActual.getTipo());
            Console.WriteLine(esint + " " + esfloat + " " + eschar + " " + esstring + " " + esbool);

            if (esint)
            {
                cadena_traducción += tokenActual.getValorToken();
                emparejar(TokenC.Tipo.numero);
                //esint = false;
            }
            if (esfloat)
            {
                cadena_traducción += tokenActual.getValorToken();
                //esfloat = false;
                emparejar(TokenC.Tipo.float_algo);
            }
            if (eschar)
            {
                cadena_traducción += tokenActual.getValorToken();
                //eschar = false;
                emparejar(TokenC.Tipo.comillas_simples);
                cadena_traducción += tokenActual.getValorToken();
                emparejar(TokenC.Tipo.caracter);
                cadena_traducción += tokenActual.getValorToken();
                emparejar(TokenC.Tipo.comillas_simples);
            }
            if (esstring)
            {
                //esstring = false;
                cadena_traducción += tokenActual.getValorToken();
                emparejar(TokenC.Tipo.comillas_dobles);
                cadena_traducción += tokenActual.getValorToken();
                emparejar(TokenC.Tipo.cadena);
                cadena_traducción += tokenActual.getValorToken();
                emparejar(TokenC.Tipo.comillas_dobles);
            }
            if (esbool)
            {
                if (tokenActual.getTipo() == TokenC.Tipo.un_false)
                {
                    cadena_traducción += tokenActual.getValorToken();
                    emparejar(TokenC.Tipo.un_false);
                }
                if (tokenActual.getTipo() == TokenC.Tipo.un_true)
                {
                    cadena_traducción += tokenActual.getValorToken();
                    emparejar(TokenC.Tipo.un_true);
                }
            }
        }
        public void otradeclaración()
        {
            if (tokenActual.getTipo() == TokenC.Tipo.coma)
            {
                emparejar(TokenC.Tipo.coma);

                cadena_auxiliar = "";
                cadena_auxiliar += tokenActual.getValorToken();
                emparejar(TokenC.Tipo.nombre_algo);

                if (tokenActual.getTipo() == TokenC.Tipo.igual)
                {
                    cadena_traducción += "\n";
                    emparejar(TokenC.Tipo.igual);
                    cadena_traducción += cadena_auxiliar + " = ";
                    asignación();
                }
                else
                {
                    cadena_auxiliar = "";
                }

                otradeclaración();
            }
        }
       

        public void emparejar(TokenC.Tipo tip)
        {
            if (tokenActual.getTipo() != tip)
            {
                Console.WriteLine("Error se esperaba " + getTipodeError(tip) + " :C");
            }            
            if (tokenActual.getTipo() != TokenC.Tipo.ultimo)
            {
                controlToken += 1;
                tokenActual = listaTokens.ElementAt(controlToken);
            }
        }
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
