using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Proyecto2
{

    public partial class Form1 : Form
    {

        public String rutaa;
        public String mihtml = "";
        public Boolean Docabierto = false, analizado = false;

        String path;
        int contaerrores = 0;
        String entrada;

        public Form1()
        {
            InitializeComponent();
            fastColoredTextBox1.Text = "class AiudaPolisia" + "\r\n" + "  {" + "\r\n" + "    static void Main( string[] args )" + "\r\n" + "    { " + "\r\n" + "\r\n" + "    }" + "\r\n" + "  }";
        }

        private void ArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void SalirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine("GUARDAR ARCHIVO");


            SaveFileDialog dondeguardo = new SaveFileDialog();
            dondeguardo.Filter = "Text FIle|*.CS";
            if (dondeguardo.ShowDialog() == DialogResult.OK)
            {
                String midocumento;
                midocumento = fastColoredTextBox1.Text;
                path = dondeguardo.FileName;

                BinaryWriter bw = new BinaryWriter(File.Create(path));
                bw.Write(midocumento);
                bw.Dispose();
                Docabierto = true;

            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void AcercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Acerca de la desarrolladora: \n" +
                "Nombre: Yásmin Monterroso \n" +
                "Carnet: 20180185 \n" +
                "Acerca del Curso: \n" +
                "Nombre: Lenguajes Formales de Programación", "DATOS GENERALES");
        }

        private void buscarArchivo()
        {

            OpenFileDialog abrir = new OpenFileDialog();
            abrir.DefaultExt = "CS";
            if (abrir.ShowDialog() == DialogResult.OK)
            {
                rutaa = abrir.FileName;
                Console.WriteLine("Su archivo es: " + rutaa);
                String mitexto = File.ReadAllText(rutaa, Encoding.UTF8);

                fastColoredTextBox1.Text = mitexto;

            }
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void GuardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine("CARGAR ARCHIVO");
            buscarArchivo();


        }

        private void AbrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LimpiarEspacioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.Text = "class AiudaPolisia" + "\r\n" + "  {" + "\r\n" + "    static void Main( string[] args )" + "\r\n" + "    { " + "\r\n" + "\r\n" + "    }" + "\r\n" + "  }";

            fastColoredTextBox2.Text = " ";
            richTextBox3.Text = " ";
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.Text = "class AiudaPolisia" + "\r\n" + "  {" + "\r\n" + "    static void Main( string[] args )" + "\r\n" + "    { " + "\r\n" + "\r\n" + "    }" + "\r\n" + "  }";

            fastColoredTextBox2.Text = " ";
            richTextBox3.Text = " ";
        }

        private void GenerarTraducciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hacerlamagia();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            hacerlamagia();
        }

        private void hacerlamagia()
        {
            analizado = true;
            entrada = fastColoredTextBox1.Text;
            Console.WriteLine("\n\nINICIANDO EL ANALIZADOR LÉXICO\n\n");
            AnalizadorLéxico funcionaxfa = new AnalizadorLéxico();
            LinkedList<TokenC> listanueva = funcionaxfa.escanear(entrada);
            funcionaxfa.imprimirListaTokens(listanueva);
            crearHTML(funcionaxfa);

            if (funcionaxfa.ListaDeErrores.Count > 1)
            {
                guardarHTML(mihtml);
            }

            /*Console.WriteLine("\n\nINICIANDO ANALIZADOR SINTÁCTICO \n\n");
              listanueva.AddLast(new TokenC(TokenC.Tipo.ultimo, "ultimo"));
              AnalizadorSintáctico awadeuwu = new AnalizadorSintáctico();
              awadeuwu.parsear(listanueva);
              Console.WriteLine("Ya terminó de analizar");

              fastColoredTextBox2.Text = awadeuwu.cadena_traducción;

             Console.WriteLine("\n\nTABLA DE VALORES\n\n");
             foreach (KeyValuePair<string, string> kvp in awadeuwu.tablavalores)
             {
                 Console.WriteLine("Key = {0}, Value = {1}",
                                   kvp.Key, kvp.Value);
             }

             Console.WriteLine("\n\nTABLA DE TIPOS\n\n");
             foreach (KeyValuePair<string, string> kvp in awadeuwu.tablatipos)
             {
                 Console.WriteLine("Key = {0}, Value = {1}",
                                   kvp.Key, kvp.Value);
             }*/
        }
        private void crearHTML(AnalizadorLéxico analizador)
        {
            mihtml = "<!DOCTYPE HTML>" +
                "<html>" +
                    "<head>" +
                        "<title>Mis Tokens</title>" +
                        "<meta charset=\"utf8\">" +
                "</head>" +
                "<body><h1>Reporte de Análisis</h1>" +
                "<p>Reportes tomadosd del archivo de entrada" + rutaa + "</p>" +
                "<h2>Lista de Tokens Aprobados</h2>";


            // aquí van mis tokens geniales

            mihtml = mihtml + "<table border=\"1\">"
            + "<thead>"
            + "<tr><th><strong>#</strong></th>"
            + "<th><strong>Lexema</strong></th>"
            + "<th><strong>Id Token</strong></th>"
            + "<th><strong>Token</strong></th>" +
            "<th><strong>Fila</strong></th>" +
            "<th><strong>Columna</strong></th></tr></thead>"
            + "<tbody>";

            try
            {
                Console.WriteLine("Imprimiendo Errores");
                Console.WriteLine(analizador.ListaTokens.ToString());
                Console.WriteLine("El tamaño de la lista es: " + analizador.ListaTokens.Count());


                ObjToken[] lista_tokens = new ObjToken[analizador.ListaTokens.Count()];
                analizador.ListaTokens.CopyTo(lista_tokens, 0);

                for (int i = 0; i < lista_tokens.Length; i++)
                {

                    mihtml = mihtml
                          + "<tr><td>" + lista_tokens[i].getId() + "</td>"
                          + "<td>" + lista_tokens[i].getLexema() + "</td>"
                          + "<td>" + lista_tokens[i].getIdToken() + "</td>"
                          + "<td>" + lista_tokens[i].getToken() + "</td>"
                          + "<td>" + lista_tokens[i].getFila() + "</td>"
                          + "<td>" + lista_tokens[i].getColumna() + "</td>"
                          + " </tr>";
                }

            }
            catch (Exception)
            {

                throw;
            }

            mihtml = mihtml + "</tbody></table></div><br><br><br>";

            // aquí empiezan los errores

            mihtml = mihtml + "<h2>Lista de Errores</h2>";
            mihtml = mihtml + "<table border=\"1\">"
             + "<thead>"
             + "<tr><th><strong>#</strong></th>"
             + "<th><strong>Fila</strong></th>"
             + "<th><strong>Columna</strong></th>"
             + "<th><strong>Caracter</strong></th>"
             + "<th><strong>Descripción</strong></th></tr></thead>"
             + "<tbody>";

            try
            {
                //int contador = 0;

                Console.WriteLine("Imprimiendo Errores");
                Console.WriteLine(analizador.ListaDeErrores.ToString());
                Console.WriteLine("El tamaño de la lista es: " + analizador.ListaDeErrores.Count());


                ErrorLéxico[] lista_errores = new ErrorLéxico[analizador.ListaDeErrores.Count()];
                analizador.ListaDeErrores.CopyTo(lista_errores, 0);

                for (int i = 0; i < lista_errores.Length; i++)
                {

                    mihtml = mihtml
                          + "<tr><td>" + lista_errores[i].getId() + "</td>"
                          + "<td>" + lista_errores[i].getFila() + "</td>"
                          + "<td>" + lista_errores[i].getColumna() + "</td>"
                          + "<td>" + lista_errores[i].getCaracter() + "</td>"
                          + "<td>" + lista_errores[i].getDescripción() + "</td>"
                          + " </tr>";
                    //contador++;

                }
            }
            catch (Exception)
            {

                throw;
            }

            mihtml = mihtml + "</tbody></table></div><br><br><br>";
            mihtml = mihtml + "</body></html>";
        }

        private void guardarHTML(string elhtml)
        {
            SaveFileDialog dondeguardo = new SaveFileDialog();
            dondeguardo.Filter = "HTML file|*.html";
            if (dondeguardo.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //crearHTML();
                    String path = dondeguardo.FileName;

                    BinaryWriter bw = new BinaryWriter(File.Create(path));
                    bw.Write(elhtml);
                    bw.Dispose();
                    Console.WriteLine("Tu archivo ha sido creado <3");
                    //MessageBox.Show("Tu archivo ha sido creado <3", "Ya! :D");
                }
                catch (Exception)
                {
                    //MessageBox.Show("Algo malo ha ocurrido </3", "Error :c");
                    Console.WriteLine("Algo malo ha ocurrido </3");
                    throw;
                }

                Process proceso = new Process();
                proceso.StartInfo.FileName = dondeguardo.FileName;
                proceso.Start();
            }
        }

        private void GenerarReportesToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            
        }

        private void tablaDeTokensToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (analizado)
            {
                guardarHTML(mihtml);
            }
            else
            {
                MessageBox.Show("Tienes que analizar algo antes!! :C");
            }
        }

        private void tablaDeErroresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (analizado)
            {
                guardarHTML(mihtml);
            }
            else
            {
                MessageBox.Show("Tienes que analizar algo antes!! :C");
            }
        }

        private void fastColoredTextBox2_Load(object sender, EventArgs e)
        {

        }
    }
}
