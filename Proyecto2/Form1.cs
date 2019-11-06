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
        String rutaGlobal;
        int contaerrores = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void ArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void SalirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
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

                richTextBox1.Text = mitexto;
             
            }
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void GuardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine("GUARDAR ARCHIVO");


            SaveFileDialog dondeguardo = new SaveFileDialog();
            dondeguardo.Filter = "Text FIle|*.CS";
            if (dondeguardo.ShowDialog() == DialogResult.OK)
            {
                String midocumento;
                midocumento = richTextBox1.Text;
                path = dondeguardo.FileName;

                BinaryWriter bw = new BinaryWriter(File.Create(path));
                bw.Write(midocumento);
                bw.Dispose();
                Docabierto = true;

            }

        }

        private void AbrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine("CARGAR ARCHIVO");
            buscarArchivo();
        }

        private void LimpiarEspacioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = " ";
            richTextBox2.Text = " ";
            richTextBox3.Text = " ";
        }
    }
}
