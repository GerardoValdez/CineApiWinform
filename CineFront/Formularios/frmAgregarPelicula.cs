using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using CineBack.acceso_a_datos;
using CineBack.fachada;
using CineBack.soporte;
using Clases.ApiRest;
using RestSharp;

namespace CineFront
{
    public partial class frmAgregarPelicula : Form
    {
        private Pelicula peliculaCargada = null;
        frmPeliculas formpeliculas = new frmPeliculas();

        public frmAgregarPelicula()
        {
            InitializeComponent();
            btnAceptar.Show();
            btnEditar.Hide();
        }

        public frmAgregarPelicula(Pelicula peliculaCargada)
        {
            InitializeComponent();
            this.peliculaCargada = peliculaCargada;
            btnAceptar.Hide();
            btnEditar.Show();
        }

        private void frmAgregarPelicula_Load(object sender, EventArgs e)
        {
            DataTable tabla1 = HelperDao.ObtenerInstancia().ConsultaSQL("SP_CONSULTAR_DIRECTORES", null);
            cboDirector.DataSource = tabla1;
            cboDirector.ValueMember = "id_director";
            cboDirector.DisplayMember = "nombre";
            cboDirector.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDirector.SelectedIndex = -1;

            DataTable tabla2 = HelperDao.ObtenerInstancia().ConsultaSQL("SP_CONSULTAR_IDIOMAS", null);
            cboidioma.DataSource = tabla2;
            cboidioma.ValueMember = "id_idioma";
            cboidioma.DisplayMember = "idioma";
            cboidioma.DropDownStyle = ComboBoxStyle.DropDownList;
            cboidioma.SelectedIndex = -1;

            DataTable tabla3 = HelperDao.ObtenerInstancia().ConsultaSQL("SP_CONSULTAR_CLASIFICACIONES", null);
            cboClasificacion.DataSource = tabla3;
            cboClasificacion.ValueMember = "id_clasificacion";
            cboClasificacion.DisplayMember = "clasificacion";
            cboClasificacion.DropDownStyle = ComboBoxStyle.DropDownList;
            cboClasificacion.SelectedIndex = -1;

            dtpFechaestreno.Value = DateTime.Today;

            if (peliculaCargada != null)     //cargar datos de la pelicula seleccionada
            {
                txNombre.Text = peliculaCargada.NombrePelicula;
                cboDirector.SelectedValue = peliculaCargada.IdDirector;
                cboidioma.SelectedValue = peliculaCargada.IdIdioma;
                cboClasificacion.SelectedValue = peliculaCargada.IdClasificacion;
                dtpFechaestreno.Value = peliculaCargada.FechaEstreno;
            }
        }

        //************************************* METODOS *************************************

        //POST
        public async Task<string> PostPelicula(Pelicula pelicula)
        {
            string url = "https://localhost:44301/registrarPelicula";
            var client = new HttpClient();

            var data = JsonSerializer.Serialize<Pelicula>(pelicula);
            HttpContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            var HttpResponse = await client.PostAsync(url, content);

            if (HttpResponse.IsSuccessStatusCode)
            {
                MessageBox.Show("Se cargo con exito la película");
                return "OK";
            }
            MessageBox.Show("Hubo un error al cargar la película");
            return "false";
        }

        //PUT
        public async Task<string> PutPelicula(Pelicula pelicula)
        {
            string url = "https://localhost:44301/actualizarPelicula";
            var client = new HttpClient();

            var data = JsonSerializer.Serialize<Pelicula>(pelicula);
            HttpContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            var HttpResponse = await client.PutAsync(url, content);

            if (HttpResponse.IsSuccessStatusCode)
            {
                MessageBox.Show("Se edito con exito la película");
                return "OK";
            }
            MessageBox.Show("Hubo un error al cargar la película");
            return "false";
        }

        //************************************* BOTONES *************************************

        //ACEPTAR
        private async void btnAceptar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                Pelicula pelicula = new Pelicula();

                pelicula.IdPelicula = 0;
                pelicula.NombrePelicula = txNombre.Text;
                pelicula.IdDirector = (int)cboDirector.SelectedValue;
                pelicula.IdClasificacion = (int)cboClasificacion.SelectedValue;
                pelicula.IdIdioma = (int)cboidioma.SelectedValue;
                pelicula.FechaEstreno = dtpFechaestreno.Value;

                await PostPelicula(pelicula);
                formpeliculas.Show();
                this.Close();
            }
        }

        //EDITAR
        private async void btnEditar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                Pelicula pelicula = new Pelicula();

                pelicula.IdPelicula = peliculaCargada.IdPelicula;
                pelicula.NombrePelicula = txNombre.Text;
                pelicula.IdDirector = (int)cboDirector.SelectedValue;
                pelicula.IdClasificacion = (int)cboClasificacion.SelectedValue;
                pelicula.IdIdioma = (int)cboidioma.SelectedValue;
                pelicula.FechaEstreno = dtpFechaestreno.Value;

                await PutPelicula(pelicula);
                formpeliculas.Show();
                this.Close();
            }
        }

        //CANCELAR
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            formpeliculas.Show();
            this.Close();
        }

        //SALIR
        private void label6_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Está por cerrar la aplicación.", "SALIR", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                Application.Exit();
        }


        [DllImport("user32.Dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.Dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void frmAgregarPelicula_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);

        }

        private bool validar()
        {
            if (txNombre.Text == String.Empty)
            {
                MessageBox.Show("Ingrese un nombre a la Pelicula", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                return false;
            }
            if (cboDirector.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un Director", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                return false;
            }
            if (cboidioma.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un Idioma", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                return false;
            }
            if (cboClasificacion.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione una clasificación", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                return false;
            }
            if (dtpFechaestreno.Value < DateTime.Today)
            {
                MessageBox.Show("No se puede ingresar una fecha anterior a la actual", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                return false;
            }
            return true;
        }
    }
}
