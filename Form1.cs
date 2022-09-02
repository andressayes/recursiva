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
using SpApp.src.Models.Entity;
using SpApp.ViewModels;


namespace SpApp
{
    public partial class Form1 : Form
    {
        private List<Socio> Socios = new List<Socio>();
        private List<SociosXEquipo> SociosxEquipo = new List<SociosXEquipo>();
        private int CantSocios = 0;
        private int CantEquipos = 0;
        private List<string> Equipos = new List<string>();
        private List<NombreCantVecesViewModel> CantAparicionesNombreDeEquipo = new List<NombreCantVecesViewModel>();
        private List<NombreCantVecesViewModel> nombresMasComunes = new List<NombreCantVecesViewModel>();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.openFileDialog.ShowDialog();
                textFile.Text = openFileDialog.FileName;
                if (BindData(textFile.Text))
                {
                    this.CantSocios = this.Socios.Count;
                    this.lblCantSocios.Text = this.CantSocios.ToString();
                    this.CantEquipos = this.SociosxEquipo.Count;
                    this.lblCantEquipos.Text = this.CantEquipos.ToString();
                }
            }
            catch (Exception)
            {

                
            }
            
            

        }
        private void AsociarAEquipo(string equipo,int indiceSocio) 
        {
            int indice = -1;
            indice = this.SociosxEquipo.FindIndex(elem => elem.Equipo == equipo);
    
            System.Diagnostics.Debug.WriteLine(indice);
            if (indice >= 0)
            {
                this.SociosxEquipo[indice].Socios.Add(indiceSocio);
            }
            else
            {

                this.SociosxEquipo.Add(new SociosXEquipo(equipo));
                var lastIndice = this.SociosxEquipo.Count - 1;
                this.SociosxEquipo[lastIndice].Socios = new List<int> { indiceSocio };
                
            }
        }

        private void AgregarEquipo(string equipo) 
        {
            int indice = -1;
            indice = this.Equipos.FindIndex(elem => elem == equipo);
            if (indice == -1)
            {
                this.Equipos.Add(equipo);
                this.cboEquipos.Items.Add(equipo);
                this.cboEquipos2.Items.Add(equipo);
            }
        }

        private Boolean BindData(string filePath)
        {
            DataTable dt = new DataTable();
            StreamReader r = new StreamReader(filePath, Encoding.GetEncoding("iso-8859-1"));
            string data1 = r.ReadToEnd();
             
            
            char[] delims = new[] { '\r', '\n' };
            string[] lines = data1.Split(delims, StringSplitOptions.RemoveEmptyEntries);

                      
            if (lines.Length > 0)
            {
                //first line to create header
                string firstLine = lines[0];
                string[] headerLabels = firstLine.Split(';');
                foreach (string headerWord in headerLabels)
                {
                    dt.Columns.Add(new DataColumn(headerWord));
                }
                //For Data
                this.CantSocios = lines.Length;

                for (int i = 1; i < lines.Length; i++)
                {
                    string[] dataWords = lines[i].Split(';');

                    this.Socios.Add(new Socio(dataWords[0], Int32.Parse(dataWords[1]), dataWords[2], dataWords[3], dataWords[4]));
                    AgregarEquipo(dataWords[2]);
                    AsociarAEquipo(dataWords[2], i - 1);
                    //System.Diagnostics.Debug.WriteLine(dataWords);
                    DataRow dr = dt.NewRow();
                    int columnIndex = 0;
                    foreach (string headerWord in headerLabels)
                    {
                        dr[headerWord] = dataWords[columnIndex++];
                    }
                    dt.Rows.Add(dr);
                }
            }
            if (dt.Rows.Count > 0)
            {
                dataGridView1.DataSource = dt;
                return true;
            }
            return false;

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void cboEquipos2_SelectedIndex(object sender, System.EventArgs e)
        {
            string equipo = (string)this.cboEquipos2.SelectedItem;
            int cantidad = Int32.Parse(this.cantNombres.Text);
            int indice = -1;
            indice = this.SociosxEquipo.FindIndex(elem => elem.Equipo == equipo);
            if (indice > -1)
            {
                BuscarXNombreMasComunxEquipo(indice, cantidad);
                List<NombreCantVecesViewModel>  nombresMasComunesOrdenados = new List<NombreCantVecesViewModel>();
               nombresMasComunesOrdenados = this.CantAparicionesNombreDeEquipo.OrderByDescending(elemn => elemn.cant).ToList();
                this.textBox1.Text = "";
                for (int i = 0; i < cantidad; i++)
                {
                    
                    this.textBox1.Text += (nombresMasComunesOrdenados[i].nombre+":" + nombresMasComunesOrdenados[i].cant + "\r\n");
                    
                }


            }
        }

        private void BuscarXNombreMasComunxEquipo(int equipo, int cantidad) 
        {
            this.CantAparicionesNombreDeEquipo = new List<NombreCantVecesViewModel>();
            
            string nombreNuevo = "";
            int indiceSocio = 0;    
            for (int i = 0; i < this.SociosxEquipo[equipo].Socios.Count; i++) 
            {
                indiceSocio = this.SociosxEquipo[equipo].Socios[i];
                nombreNuevo = this.Socios[indiceSocio].Nombre;
                ActualizarCantidades(nombreNuevo);
            }


        }

        private void ActualizarCantidades(string nombreNuevo)
        {
            int indice = -1;
            indice = this.CantAparicionesNombreDeEquipo.FindIndex(elem => elem.nombre == nombreNuevo);
            if (indice > -1)
            {
                this.CantAparicionesNombreDeEquipo[indice].cant = this.CantAparicionesNombreDeEquipo[indice].cant + 1;
            }
            else {
                this.CantAparicionesNombreDeEquipo.Add(new NombreCantVecesViewModel(nombreNuevo, 1));
            }
        }

        private void cboEquipos_SelectedIndex(object sender, System.EventArgs e) 
        {
            
            string equipo = (string )this.cboEquipos.SelectedItem;
            int indice = -1;
            indice = this.SociosxEquipo.FindIndex(elem => elem.Equipo == equipo);
            if (indice > -1)
            {
                int edades = 0;
                for (int i = 0; i < this.SociosxEquipo[indice].Socios.Count; i++)
                {
                    int edad = Socios[this.SociosxEquipo[indice].Socios[i]].Edad;
                    edades = edades + edad;
                }
                int promedio = edades / this.SociosxEquipo[indice].Socios.Count;
                lblPromedio.Text = promedio.ToString();
            
            }
        }

       
    }
}
