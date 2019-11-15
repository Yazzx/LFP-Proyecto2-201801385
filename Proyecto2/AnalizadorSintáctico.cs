using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto2
{
    class AnalizadorSintáctico
    {
        int controlToken;
        TokenC tokenActual;
        LinkedList<TokenC> listaTokens;
        public String cadena_traducción = "", cadena_auxiliar, cadena_clave, tab = "\t", cadena_switch;
        StringBuilder mistabs = new StringBuilder();
        public Dictionary<string, string> tablavalores = new Dictionary<string, string>();
        public Dictionary<string, string> tablatipos = new Dictionary<string, string>();

        //BANDERAS 

        bool esint = false;
        Boolean esfloat = false, hayerror = false, yaimportarray = false;
        Boolean eschar = false;
        Boolean esstring = false;
        Boolean esbool = false, esarray = false;
        Boolean esprimero = true;


        public void clase()
        {
            emparejar(TokenC.Tipo.PR_CLASS);
            emparejar(TokenC.Tipo.nombre_algo);
            emparejar(TokenC.Tipo.llave_abrir);
            emparejar(TokenC.Tipo.PR_STATIC);
            emparejar(TokenC.Tipo.PR_VOID);
            emparejar(TokenC.Tipo.pr_main);
            emparejar(TokenC.Tipo.parentesis_abrir);
            emparejar(TokenC.Tipo.pr_string);
            emparejar(TokenC.Tipo.corchete_abrir);
            emparejar(TokenC.Tipo.corchete_cerrar);
            emparejar(TokenC.Tipo.nombre_algo);
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

            if (tokenActual.getTipo() == TokenC.Tipo.nombre_algo)
            {
                asignacionalopela();
            }

            if (tokenActual.getTipo() == TokenC.Tipo.console_writeline)
            {
                imprimiralgo();
            }

            if (tokenActual.getTipo() == TokenC.Tipo.un_if)
            {
                sentenciaif();
            }
            if (tokenActual.getTipo() == TokenC.Tipo.un_switch)
            {
                sentenciaswitch();
            }
            if (tokenActual.getTipo() == TokenC.Tipo.un_for)
            {
                sentenciafor();
            }


        }

        #region declaracion y asignacion con palabra reservada
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

            // si es array
            if (tokenActual.getTipo() == TokenC.Tipo.corchete_abrir)
            {
                esarray = true;
                emparejar(TokenC.Tipo.corchete_abrir);
                emparejar(TokenC.Tipo.corchete_cerrar);
                if (yaimportarray)
                {

                }
                else
                {
                    cadena_traducción = "from numpy import array\n\n" + cadena_traducción;
                    yaimportarray = true;
                }

            }

            cadena_clave = "";
            cadena_auxiliar = "";
            cadena_auxiliar += tokenActual.getValorToken();
            cadena_clave += tokenActual.getValorToken();

            try
            {

                tablavalores.Add(cadena_clave, "");
                tablatipos.Add(cadena_clave, "");
            }

            catch (ArgumentException)
            {

                MessageBox.Show("Hay dos variables con el mismo nombre:  " +
                         "= \"" + cadena_clave + "\" !! :C", "Error Sintáctico");
            }


            emparejar(TokenC.Tipo.nombre_algo);

            // si viene un igual empareja
            if (tokenActual.getTipo() == TokenC.Tipo.igual)
            {
                emparejar(TokenC.Tipo.igual);
                cadena_traducción += mistabs + cadena_auxiliar + " = ";

                // si el igual es de un array
                if (esarray)
                {
                    if (tokenActual.getTipo() == TokenC.Tipo.un_new)
                    {
                        emparejar(TokenC.Tipo.un_new);

                        if (tokenActual.getTipo() == TokenC.Tipo.pr_int && esint)
                        {
                            emparejar(TokenC.Tipo.pr_int);
                        }
                        else if (tokenActual.getTipo() == TokenC.Tipo.pr_float && esfloat)
                        {
                            emparejar(TokenC.Tipo.pr_float);
                        }
                        else if (tokenActual.getTipo() == TokenC.Tipo.pr_string && esstring)
                        {
                            emparejar(TokenC.Tipo.pr_string);
                        }
                        else if (tokenActual.getTipo() == TokenC.Tipo.pr_bool && esbool)
                        {
                            emparejar(TokenC.Tipo.pr_bool);
                        }
                        else if (tokenActual.getTipo() == TokenC.Tipo.pr_char && eschar)
                        {
                            emparejar(TokenC.Tipo.pr_char);
                        }
                        else
                        {
                            MessageBox.Show("Revisa los tipos de tu arreglo :C");
                        }

                        emparejar(TokenC.Tipo.corchete_abrir);
                        cadena_traducción += "[";
                        emparejar(TokenC.Tipo.corchete_cerrar);
                        cadena_traducción += "]";
                    }
                    else
                    {
                        emparejar(TokenC.Tipo.llave_abrir);
                        cadena_traducción += "[";
                        asignación();
                        otradeclaración();
                        emparejar(TokenC.Tipo.llave_cerrar);
                        cadena_traducción += "]";
                    }
                }
                else
                {
                    asignación();
                }

            }
            else
            {
                cadena_auxiliar = "";
            }

            otradeclaración();

            cadena_traducción += "\n";
            cadena_clave = "";
            emparejar(TokenC.Tipo.puntoycoma);
            esint = false;
            esfloat = false;
            eschar = false;
            esstring = false;
            esbool = false;
            esarray = false;

            contenido();
        }

        // aqui solo va lo que se pone desúes del igual
        public void asignación()
        {
            //Console.WriteLine(tokenActual.getTipo());
            //Console.WriteLine(esint + " " + esfloat + " " + eschar + " " + esstring + " " + esbool);

            if (esint)
            {
                cadena_traducción += tokenActual.getValorToken();

                tablatipos[cadena_clave] = "int";
                tablavalores[cadena_clave] = tokenActual.getValorToken();

                emparejar(TokenC.Tipo.numero);
                //esint = false;
            }
            if (esfloat)
            {
                cadena_traducción += tokenActual.getValorToken();

                tablatipos[cadena_clave] = "float";
                tablavalores[cadena_clave] = tokenActual.getValorToken();

                emparejar(TokenC.Tipo.float_algo);
            }
            if (eschar)
            {
                cadena_traducción += tokenActual.getValorToken();
                emparejar(TokenC.Tipo.comillas_simples);
                cadena_traducción += tokenActual.getValorToken();

                tablatipos[cadena_clave] = "char";
                tablavalores[cadena_clave] = "'" + tokenActual.getValorToken() + "'";

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

                tablatipos[cadena_clave] = "string";
                tablavalores[cadena_clave] = "'" + tokenActual.getValorToken() + "'";

                emparejar(TokenC.Tipo.cadena);
                cadena_traducción += tokenActual.getValorToken();
                emparejar(TokenC.Tipo.comillas_dobles);
            }
            if (esbool)
            {
                if (tokenActual.getTipo() == TokenC.Tipo.un_false)
                {
                    cadena_traducción += tokenActual.getValorToken();

                    tablatipos[cadena_clave] = "bool";
                    tablavalores[cadena_clave] = tokenActual.getValorToken();

                    emparejar(TokenC.Tipo.un_false);
                }
                if (tokenActual.getTipo() == TokenC.Tipo.un_true)
                {
                    cadena_traducción += tokenActual.getValorToken();

                    tablatipos[cadena_clave] = "bool";
                    tablavalores[cadena_clave] = tokenActual.getValorToken();

                    emparejar(TokenC.Tipo.un_true);
                }
            }
        }
        public void otradeclaración()
        {
            if (tokenActual.getTipo() == TokenC.Tipo.coma)
            {
                cadena_auxiliar = "";
                cadena_clave = "";
                emparejar(TokenC.Tipo.coma);
                if (esarray)
                {
                    cadena_traducción += ", ";
                    cadena_auxiliar = "";
                    cadena_auxiliar += tokenActual.getValorToken();
                    asignación();
                }
                else
                {
                    cadena_auxiliar = "";
                    cadena_clave = "";
                    cadena_clave = tokenActual.getValorToken();
                    cadena_auxiliar += tokenActual.getValorToken();

                    try
                    {

                        tablavalores.Add(cadena_clave, "");
                        tablatipos.Add(cadena_clave, "");
                    }

                    catch (ArgumentException)
                    {

                        MessageBox.Show("Hay dos variables con el mismo nombre:  " +
                                 "= \"" + cadena_clave + "\" !! :C", "Error Sintáctico");
                    }

                    emparejar(TokenC.Tipo.nombre_algo);
                }

                if (tokenActual.getTipo() == TokenC.Tipo.igual)
                {
                    cadena_traducción += "\n" + mistabs;
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
        #endregion

        #region asignación a lo pela
        public void asignacionalopela()
        {
            cadena_auxiliar = "";
            cadena_auxiliar += tokenActual.getValorToken();
            cadena_clave = "";
            cadena_clave = tokenActual.getValorToken();
            emparejar(TokenC.Tipo.nombre_algo);

            emparejar(TokenC.Tipo.igual);
            cadena_traducción += mistabs + cadena_auxiliar + " = ";

            if (tokenActual.getTipo() == TokenC.Tipo.numero)
            {
                cadena_traducción += tokenActual.getValorToken();

                tablavalores[cadena_clave] = tokenActual.getValorToken();

                emparejar(TokenC.Tipo.numero);
            }
            else if (tokenActual.getTipo() == TokenC.Tipo.float_algo)
            {
                cadena_traducción += tokenActual.getValorToken();

                tablavalores[cadena_clave] = tokenActual.getValorToken();

                emparejar(TokenC.Tipo.float_algo);
            }
            else if (tokenActual.getTipo() == TokenC.Tipo.comillas_simples)
            {
                emparejar(TokenC.Tipo.comillas_simples);
                cadena_traducción += "'" + tokenActual.getValorToken() + "'";

                tablavalores[cadena_clave] = tokenActual.getValorToken();

                emparejar(TokenC.Tipo.caracter);
                emparejar(TokenC.Tipo.comillas_simples);
            }
            else if (tokenActual.getTipo() == TokenC.Tipo.comillas_dobles)
            {
                emparejar(TokenC.Tipo.comillas_dobles);
                cadena_traducción += '"' + tokenActual.getValorToken() + '"';

                tablavalores[cadena_clave] = tokenActual.getValorToken();

                emparejar(TokenC.Tipo.cadena);
                emparejar(TokenC.Tipo.comillas_dobles);
            }
            else if (tokenActual.getTipo() == TokenC.Tipo.un_true)
            {
                cadena_traducción += "True";

                tablavalores[cadena_clave] = "True";

                emparejar(TokenC.Tipo.un_true);
            }
            else if (tokenActual.getTipo() == TokenC.Tipo.un_false)
            {
                cadena_traducción += "False";

                tablavalores[cadena_clave] = "False";

                emparejar(TokenC.Tipo.un_false);
            }
            else if (tokenActual.getTipo() == TokenC.Tipo.nombre_algo)
            {
                cadena_traducción += tokenActual.getValorToken();

                try
                {
                    string valor = tablavalores[tokenActual.getValorToken()];
                    tablavalores[cadena_clave] = valor;
                }
                catch (Exception)
                {
                    Console.WriteLine(">>>>>>>>>>>>>>>>ALGO PASÓ EN LA TABLA DE SÍMBOLOS<<<<<<<<<<<<<<<<<");
                }

                emparejar(TokenC.Tipo.nombre_algo);
            }

            cadena_auxiliar = "";
            cadena_traducción += "\n";
            emparejar(TokenC.Tipo.puntoycoma);
            contenido();
        }
        #endregion

        #region console.writeline

        public void imprimiralgo()
        {
            emparejar(TokenC.Tipo.console_writeline);
            emparejar(TokenC.Tipo.punto);
            emparejar(TokenC.Tipo.console_writeline);
            emparejar(TokenC.Tipo.parentesis_abrir);

            cadena_traducción += mistabs + "print(";

            loquevienedentrodelwriteline();

            cadena_traducción += ")";
            emparejar(TokenC.Tipo.parentesis_cerrar);

            cadena_auxiliar = "";
            cadena_traducción += "\n";
            emparejar(TokenC.Tipo.puntoycoma);
            contenido();
        }

        public void loquevienedentrodelwriteline()
        {
            if (tokenActual.getTipo() == TokenC.Tipo.comillas_dobles)
            {
                emparejar(TokenC.Tipo.comillas_dobles);
                cadena_traducción += '"';
                cadena_traducción += tokenActual.getValorToken();
                cadena_traducción += '"';
                emparejar(TokenC.Tipo.cadena);
                emparejar(TokenC.Tipo.comillas_dobles);
            }
            else if (tokenActual.getTipo() == TokenC.Tipo.comillas_simples)
            {
                emparejar(TokenC.Tipo.comillas_dobles);
                cadena_traducción += "'";
                cadena_traducción += tokenActual.getValorToken();
                cadena_traducción += "'";
                emparejar(TokenC.Tipo.caracter);
                emparejar(TokenC.Tipo.comillas_simples);
            }
            else if (tokenActual.getTipo() == TokenC.Tipo.nombre_algo)
            {
                cadena_traducción += tokenActual.getValorToken();
                emparejar(TokenC.Tipo.nombre_algo);
            }
            else if (tokenActual.getTipo() == TokenC.Tipo.numero)
            {
                cadena_traducción += tokenActual.getValorToken();
                emparejar(TokenC.Tipo.numero);
            }
            else if (tokenActual.getTipo() == TokenC.Tipo.float_algo)
            {
                cadena_traducción += tokenActual.getValorToken();
                emparejar(TokenC.Tipo.float_algo);
            }
            else if (tokenActual.getTipo() == TokenC.Tipo.un_true)
            {
                cadena_traducción += tokenActual.getValorToken();
                emparejar(TokenC.Tipo.un_true);
            }
            else if (tokenActual.getTipo() == TokenC.Tipo.un_false)
            {
                cadena_traducción += tokenActual.getValorToken();
                emparejar(TokenC.Tipo.un_false);
            }

            otrodentrowriteline();

        }

        public void otrodentrowriteline()
        {
            if (tokenActual.getTipo() == TokenC.Tipo.mas)
            {
                emparejar(TokenC.Tipo.mas);
                cadena_traducción += " + ";

                if (tokenActual.getTipo() == TokenC.Tipo.comillas_dobles)
                {
                    emparejar(TokenC.Tipo.comillas_dobles);
                    cadena_traducción += '"';
                    cadena_traducción += tokenActual.getValorToken();
                    cadena_traducción += '"';
                    emparejar(TokenC.Tipo.cadena);
                    emparejar(TokenC.Tipo.comillas_dobles);
                }
                else if (tokenActual.getTipo() == TokenC.Tipo.comillas_simples)
                {
                    emparejar(TokenC.Tipo.comillas_dobles);
                    cadena_traducción += "'";
                    cadena_traducción += tokenActual.getValorToken();
                    cadena_traducción += "'";
                    emparejar(TokenC.Tipo.caracter);
                    emparejar(TokenC.Tipo.comillas_simples);
                }
                else if (tokenActual.getTipo() == TokenC.Tipo.nombre_algo)
                {
                    cadena_traducción += tokenActual.getValorToken();
                    emparejar(TokenC.Tipo.nombre_algo);
                }
                else if (tokenActual.getTipo() == TokenC.Tipo.numero)
                {
                    cadena_traducción += tokenActual.getValorToken();
                    emparejar(TokenC.Tipo.numero);
                }
                else if (tokenActual.getTipo() == TokenC.Tipo.float_algo)
                {
                    cadena_traducción += tokenActual.getValorToken();
                    emparejar(TokenC.Tipo.float_algo);
                }
                else if (tokenActual.getTipo() == TokenC.Tipo.un_true)
                {
                    cadena_traducción += tokenActual.getValorToken();
                    emparejar(TokenC.Tipo.un_true);
                }
                else if (tokenActual.getTipo() == TokenC.Tipo.un_false)
                {
                    cadena_traducción += tokenActual.getValorToken();
                    emparejar(TokenC.Tipo.un_false);
                }



                otrodentrowriteline();
            }
        }
        #endregion

        #region if-else

        public void sentenciaif()
        {
            if (tokenActual.getTipo() == TokenC.Tipo.un_if)
            {
                cadena_auxiliar = "";
                emparejar(TokenC.Tipo.un_if);
                cadena_traducción += mistabs + "if ";
                emparejar(TokenC.Tipo.parentesis_abrir);

                comparacion();

                emparejar(TokenC.Tipo.parentesis_cerrar);
                cadena_traducción += ":";
                emparejar(TokenC.Tipo.llave_abrir);
                cadena_traducción += "\n";

                mistabs.Append(tab);
                //sentenciaif();
                contenido();

                emparejar(TokenC.Tipo.llave_cerrar);


                mistabs.Length -= tab.Length;

                sentenciaelse();
                contenido();


            }
        }

        public void comparacion()
        {
            cosita();
            operador();
            cosita();
        }
        public void cosita()
        {
            if (tokenActual.getTipo() == TokenC.Tipo.nombre_algo)
            {
                cadena_traducción += tokenActual.getValorToken() + " ";
                emparejar(TokenC.Tipo.nombre_algo);
            }
            else if (tokenActual.getTipo() == TokenC.Tipo.numero)
            {
                cadena_traducción += tokenActual.getValorToken() + " ";
                emparejar(TokenC.Tipo.numero);
            }
            else if (tokenActual.getTipo() == TokenC.Tipo.float_algo)
            {
                cadena_traducción += tokenActual.getValorToken() + " ";
                emparejar(TokenC.Tipo.float_algo);
            }
            else
            {
                MessageBox.Show("Tiene que venir un caracter antes del operador en tu if :C", "Error Sintáctico");
            }
        }

        public void operador()
        {
            if (tokenActual.getTipo() == TokenC.Tipo.mayor_que)
            {
                cadena_traducción += tokenActual.getValorToken() + " ";
                emparejar(TokenC.Tipo.mayor_que);
            }
            else if (tokenActual.getTipo() == TokenC.Tipo.menor_que)
            {
                cadena_traducción += tokenActual.getValorToken() + " ";
                emparejar(TokenC.Tipo.menor_que);
            }
            else if (tokenActual.getTipo() == TokenC.Tipo.igualigual)
            {
                cadena_traducción += tokenActual.getValorToken() + " ";
                emparejar(TokenC.Tipo.igualigual);
            }
            else if (tokenActual.getTipo() == TokenC.Tipo.diferente_que)
            {
                cadena_traducción += tokenActual.getValorToken() + " ";
                emparejar(TokenC.Tipo.diferente_que);
            }
            else
            {
                MessageBox.Show("Tiene que haber un operador entre números :c ", "Error Sintáctico");
            }
        }
        public void sentenciaelse()
        {

            if (tokenActual.getTipo() == TokenC.Tipo.un_else)
            {
                cadena_traducción += mistabs + "else: ";
                emparejar(TokenC.Tipo.un_else);
                emparejar(TokenC.Tipo.llave_abrir);
                cadena_traducción += "\n";

                mistabs.Append(tab);
                contenido();

                emparejar(TokenC.Tipo.llave_cerrar);


                mistabs.Length -= tab.Length;

                sentenciaelse();
                contenido();
            }

        }

        #endregion

        #region switch-case

        public void sentenciaswitch()
        {
            if (tokenActual.getTipo() == TokenC.Tipo.un_switch)
            {
                emparejar(TokenC.Tipo.un_switch);
                emparejar(TokenC.Tipo.parentesis_abrir);
                cadena_auxiliar = tokenActual.getValorToken();
                cadena_switch = tokenActual.getValorToken();
                emparejar(TokenC.Tipo.nombre_algo);
                emparejar(TokenC.Tipo.parentesis_cerrar);
                emparejar(TokenC.Tipo.llave_abrir);

                casosycosas();

                emparejar(TokenC.Tipo.llave_cerrar);
            }
        }

        public void casosycosas()
        {
            

            if (esprimero)
            {
                esprimero = false;
                if (tokenActual.getTipo() == TokenC.Tipo.un_case)
                {
                    emparejar(TokenC.Tipo.un_case);

                    cadena_traducción += "if " + cadena_switch + " == " + tokenActual.getValorToken() + ":\n";
                    emparejar(TokenC.Tipo.numero);
                    emparejar(TokenC.Tipo.dospuntos);

                    mistabs.Append(tab);
                    //sentenciaif();
                    contenido();

                    emparejar(TokenC.Tipo.un_break);
                    emparejar(TokenC.Tipo.puntoycoma);
                    mistabs.Length -= tab.Length;
                    casosycosas();
                }
            }
            else
            {
                if (tokenActual.getTipo() == TokenC.Tipo.un_case)
                {
                    emparejar(TokenC.Tipo.un_case);

                    cadena_traducción += "elif " + cadena_switch + " == " + tokenActual.getValorToken() + ":";
                    emparejar(TokenC.Tipo.numero);
                    emparejar(TokenC.Tipo.dospuntos);

                    mistabs.Append(tab);
                    //sentenciaif();
                    contenido();

                    emparejar(TokenC.Tipo.un_break);
                    emparejar(TokenC.Tipo.puntoycoma);
                    mistabs.Length -= tab.Length;
                    casosycosas();
                }
            }

            if (tokenActual.getTipo() == TokenC.Tipo.pr_default)
            {
                emparejar(TokenC.Tipo.pr_default);
                cadena_traducción += "else:\n";
                emparejar(TokenC.Tipo.dospuntos);

                mistabs.Append(tab);
                //sentenciaif();
                contenido();

                emparejar(TokenC.Tipo.un_break);
                emparejar(TokenC.Tipo.puntoycoma);
                cadena_switch = "";
                casosycosas();

            }
        }

        #endregion

        #region for

        public void sentenciafor()
        {
            if (tokenActual.getTipo() == TokenC.Tipo.un_for)
            {
                emparejar(TokenC.Tipo.un_for);
                emparejar(TokenC.Tipo.parentesis_abrir);
                emparejar(TokenC.Tipo.pr_int);

                declaracionVariables();

                emparejar(TokenC.Tipo.nombre_algo);

                operador();





            }
        }

        #endregion


        // ESTO SE HACE O SE HACE
        #region asignación con mate
        #endregion


        #region VOIDS DE BACKEND 

        public void emparejar(TokenC.Tipo tip)
        {
            Console.WriteLine("Matcheando con: " + tip + " \t\t\t " + tokenActual.getTipo());
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
        public void parsear(LinkedList<TokenC> tokens)
        {
            this.listaTokens = tokens;
            controlToken = 0;
            tokenActual = listaTokens.ElementAt(controlToken);
            clase();
        }

        #endregion

    }
}
