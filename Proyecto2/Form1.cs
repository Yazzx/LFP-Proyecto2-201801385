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
        public String mihtml;
        public Boolean Docabierto = false;

        String path;
        int contaerrores = 0;
        String entrada;

        public Form1()
        {
            InitializeComponent();
            fastColoredTextBox1.Text = "class AiudaPolisia" + "\r\n" + "  {" + "\r\n" + "    static void Main()" + "\r\n" + "    { " + "\r\n" + "\r\n" + "    }" + "\r\n" + "  }";
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
            fastColoredTextBox1.Text = "class AiudaPolisia" + "\r\n" + "  {" + "\r\n" + "    static void Main()" + "\r\n" + "    { " + "\r\n" + "\r\n" + "    }" + "\r\n" + "  }";

            fastColoredTextBox2.Text = " ";
            richTextBox3.Text = " ";
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.Text = "class AiudaPolisia" + "\r\n" + "  {" + "\r\n" + "    static void Main()" + "\r\n" + "    { " + "\r\n" + "\r\n" + "    }" + "\r\n" + "  }";

            fastColoredTextBox2.Text = " ";
            richTextBox3.Text = " ";
        }

        private void GenerarReportesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void GenerarTraducciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            entrada = fastColoredTextBox1.Text;
            Console.WriteLine("INICIANDO EL ANALIZADOR LÉXICO");
            AnalizadorLéxico funcionaxfa = new AnalizadorLéxico();
            LinkedList<TokenC> listanueva = funcionaxfa.escanear(entrada);
            funcionaxfa.imprimirListaTokens(listanueva);

            Console.WriteLine("INICIANDO ANALIZADOR SINTÁCTICO");
            listanueva.AddLast(new TokenC(TokenC.Tipo.ultimo, "ultimo"));
            AnalizadorSintáctico awadeuwu = new AnalizadorSintáctico();
            awadeuwu.parsear(listanueva);
            Console.WriteLine("Ya terminó de analizar");

            fastColoredTextBox2.Text = awadeuwu.cadena_traducción;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            entrada = fastColoredTextBox1.Text;
            Console.WriteLine("INICIANDO EL ANALIZADOR LÉXICO");
            AnalizadorLéxico funcionaxfa = new AnalizadorLéxico();
            LinkedList<TokenC> listanueva = funcionaxfa.escanear(entrada);
            funcionaxfa.imprimirListaTokens(listanueva);

            Console.WriteLine("INICIANDO ANALIZADOR SINTÁCTICO");
            listanueva.AddLast(new TokenC(TokenC.Tipo.ultimo, "ultimo"));
            AnalizadorSintáctico awadeuwu = new AnalizadorSintáctico();
            awadeuwu.parsear(listanueva);
            Console.WriteLine("Ya terminó de analizar");

            fastColoredTextBox2.Text = awadeuwu.cadena_traducción;
        }

        private void fastColoredTextBox2_Load(object sender, EventArgs e)
        {

        }
    }
}
