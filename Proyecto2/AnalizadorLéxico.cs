using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2
{
    class AnalizadorLéxico
    {
        private LinkedList<TokenC> Salida;
        private int estado;
        private String auxlex;

        int contador;

        int contaerror = 1;
        int contafila = 1;
        int contacolumna = 1;
        String descerror = "caracter desconocido";

        int contatoken = 1;

        public LinkedList<ErrorLéxico> ListaDeErrores = new LinkedList<ErrorLéxico>();
        public LinkedList<ObjToken> ListaTokens = new LinkedList<ObjToken>();

        Boolean prabierta = true;

        public LinkedList<TokenC> escanear(String entrada)
        {
            entrada = entrada + '#';
            Salida = new LinkedList<TokenC>();
            estado = 0;
            auxlex = "";
            Boolean escadena = false;
            // cambiar a esstring si es necesario

            Char c;
            for (int i = 0; i < entrada.Length - 1; i++)
            {
                c = entrada.ElementAt(i);

                switch (estado)
                {
                    case 0:
                        if (char.IsLetter(c))
                        {
                            estado = 1;
                            auxlex += c;
                            contacolumna++;
                        }
                        else if (char.IsDigit(c))
                        {
                            estado = 2;
                            auxlex += c;

                            contador = 1;
                            contacolumna++;
                        }
                        else if (c.CompareTo(';') == 0)
                        {
                            auxlex += c;
                            ObjToken o1 = new ObjToken(contatoken, contafila, contacolumna, auxlex, 1, "Punto y coma");
                            ListaTokens.AddLast(o1);
                            contatoken++;
                            contacolumna++;
                            agregarToken(TokenC.Tipo.puntoycoma);                            
                        }
                        // a ver que pasa
                        else if (c.CompareTo(' ') == 0)
                        {
                            auxlex += c;
                            ObjToken o1 = new ObjToken(contatoken, contafila, contacolumna, auxlex, 2, "espacio");
                            ListaTokens.AddLast(o1);
                            contatoken++;
                            contacolumna++;
                            agregarToken(TokenC.Tipo.espacio);
                        }
                        else if (c.CompareTo(',') == 0)
                        {
                            auxlex += c;
                            ObjToken o1 = new ObjToken(contatoken, contafila, contacolumna, auxlex, 3, "coma");
                            ListaTokens.AddLast(o1);
                            contatoken++;
                            contacolumna++;
                            agregarToken(TokenC.Tipo.puntoycoma);
                        }
                        else
                        {
                            if (c.CompareTo('#') == 0 && i == entrada.Length - 1)
                            {
                                //Hemos concluido el análisis léxico.
                                Console.WriteLine("Hemos concluido el analiss con exito");
                            }
                            else
                            {
                                Console.WriteLine("Error lexico con: " + c);
                                estado = 0;
                            }
                        }
                        break;
                    case 1:
                        if (Char.IsLetter(c))
                        {
                            estado = 1;
                            auxlex += c;
                            contacolumna++;
                            //así voy juntando caracteres
                        }
                        else if (c.CompareTo(' ') == 0 || c.CompareTo(',') == 0 || c.CompareTo(';') == 0 
                            || c.CompareTo('\t') == 0 || c.CompareTo('\n') == 0)
                        {
                            contacolumna++;                
                            if (prabierta == true)
                            {
                                prabierta = false;
                                estado = 2;
                            }
                            else 
                            {
                              estado = 3;
                            }
                            i -= 1;
                        }
                        else
                        {
                            estado = 3;
                            i -= 1;
                        }
                        break;
                    case 2:
                        if (auxlex.Equals("class"))
                        {
                            ObjToken o1 = new ObjToken(contatoken, contafila, contacolumna, auxlex, 10, "Palabra reservada - clase");
                            ListaTokens.AddLast(o1);
                            contatoken++;
                            agregarToken(TokenC.Tipo.PR_CLASS);
                             i -= 1;                                                 
                        } 
                        else if (auxlex.Equals("static"))
                        {
                            ObjToken o1 = new ObjToken(contatoken, contafila, contacolumna, auxlex, 11, "Palabra reservada - static");
                            ListaTokens.AddLast(o1);
                            contatoken++;
                            agregarToken(TokenC.Tipo.PR_STATIC);
                            i -= 1;
                        }
                        else if (auxlex.Equals("void"))
                        {
                            ObjToken o1 = new ObjToken(contatoken, contafila, contacolumna, auxlex, 11, "Palabra reservada - void");
                            ListaTokens.AddLast(o1);
                            contatoken++;
                            agregarToken(TokenC.Tipo.PR_VOID);
                            i -= 1;
                        }
                        else if (auxlex.Equals("int"))
                        {
                            ObjToken o1 = new ObjToken(contatoken, contafila, contacolumna, auxlex, 11, "Palabra reservada - int");
                            ListaTokens.AddLast(o1);
                            contatoken++;
                            agregarToken(TokenC.Tipo.pr_int);
                            i -= 1;
                        }
                        else if (auxlex.Equals("float"))
                        {
                            ObjToken o1 = new ObjToken(contatoken, contafila, contacolumna, auxlex, 11, "Palabra reservada - float");
                            ListaTokens.AddLast(o1);
                            contatoken++;
                            agregarToken(TokenC.Tipo.pr_float);
                            i -= 1;
                        }
                        else if (auxlex.Equals("String") || auxlex.Equals("string"))
                        {
                            ObjToken o1 = new ObjToken(contatoken, contafila, contacolumna, auxlex, 11, "Palabra reservada - String");
                            ListaTokens.AddLast(o1);
                            contatoken++;
                            agregarToken(TokenC.Tipo.pr_string);
                            i -= 1;
                        }
                        else if (auxlex.Equals("bool"))
                        {
                            ObjToken o1 = new ObjToken(contatoken, contafila, contacolumna, auxlex, 11, "Palabra reservada - bool");
                            ListaTokens.AddLast(o1);
                            contatoken++;
                            agregarToken(TokenC.Tipo.pr_bool);
                            i -= 1;
                        }
                        else if (auxlex.Equals("bool"))
                        {
                            ObjToken o1 = new ObjToken(contatoken, contafila, contacolumna, auxlex, 11, "Palabra reservada - bool");
                            ListaTokens.AddLast(o1);
                            contatoken++;
                            agregarToken(TokenC.Tipo.pr_bool);
                            i -= 1;
                        }
                        else if (auxlex.Equals("false"))
                        {
                            ObjToken o1 = new ObjToken(contatoken, contafila, contacolumna, auxlex, 11, "Palabra reservada - false");
                            ListaTokens.AddLast(o1);
                            contatoken++;
                            agregarToken(TokenC.Tipo.un_false);
                            i -= 1;
                        }
                        else if (auxlex.Equals("char"))
                        {
                            ObjToken o1 = new ObjToken(contatoken, contafila, contacolumna, auxlex, 11, "Palabra reservada - char");
                            ListaTokens.AddLast(o1);
                            contatoken++;
                            agregarToken(TokenC.Tipo.pr_char);
                            i -= 1;
                        }
                        else if (auxlex.Equals(""))
                        {
                            ObjToken o1 = new ObjToken(contatoken, contafila, contacolumna, auxlex, 11, "Palabra reservada - bool");
                            ListaTokens.AddLast(o1);
                            contatoken++;
                            agregarToken(TokenC.Tipo);
                            i -= 1;
                        }


                        break;
                    
                        

                }
                              



            }
         return Salida;
        }

        public void agregarToken(TokenC.Tipo tipo)
        {
            Salida.AddLast(new TokenC(tipo, auxlex));
            auxlex = "";
            estado = 0;
        }

        public void imprimirListaTokens(LinkedList<TokenC> lista)
        {
            foreach (TokenC item in lista)
            {
                Console.WriteLine(item.getTipo() + ": " + item.getValorToken());
            }
        }

    }

}
