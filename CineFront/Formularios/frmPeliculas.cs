using CineBack.soporte;
using Clases.ApiRest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CineFront
{
    public partial class frmPeliculas : Form
    {
        public frmPeliculas()
        {
            InitializeComponent();
        }

        private void frmPeliculas_Load(object sender, EventArgs e)
        {
            cargarDataGrid();
        }

        private void dgvPeliculas_CellContentClick(object sender, EventArgs e)
        {
            if(dgvPeliculas.CurrentRow != null)
            {
                btnEditar.Enabled = true;
                btnEliminar.Enabled = true;
            }
        }

        //************************************* METODOS *************************************

        //CARGAR DATAGRID
        private async void cargarDataGrid()
        {
            string respuesta = await GetHttp();
            List<Pelicula> lst = JsonConvert.DeserializeObject<List<Pelicula>>(respuesta);
            dgvPeliculas.DataSource = lst;
            dgvPeliculas.Columns[0].Visible = false;
            dgvPeliculas.Columns[1].Visible = false;
            dgvPeliculas.Columns[2].Visible = false;
            dgvPeliculas.Columns[3].Visible = false;
            dgvPeliculas.Columns[4].HeaderText = "PELICULA";
            dgvPeliculas.Columns[5].HeaderText = "FECHA ESTRENO";
            dgvPeliculas.Columns[4].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;

            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;

            dgvPeliculas.Refresh();
        }

        //GET
        public async Task<string> GetHttp()
        {
            string url = "https://localhost:44301/peliculas";

            WebRequest oRequest = WebRequest.Create(url);
            WebResponse oResponse = oRequest.GetResponse();
            StreamReader sr = new StreamReader(oResponse.GetResponseStream());
            return await sr.ReadToEndAsync();
        }

        //DELETE
        public async Task<string> DeleteHttp(int id)
        {
            string url = "https://localhost:44301/borrarPelicula?nro=" + id;
            var client = new HttpClient();

            var HttpResponse = await client.DeleteAsync(url);

            if (HttpResponse.IsSuccessStatusCode)
            {
                MessageBox.Show("Se elimino con exito la película");
                return "OK";
            }
            MessageBox.Show("Hubo un error al eliminar la película");
            return "false";
        }


        //************************************* BOTONES *************************************

        //AGREGAR
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAgregarPelicula agregarPelicula = new frmAgregarPelicula();
            agregarPelicula.Show();
            this.Close();
        }

        //EDITAR
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvPeliculas.CurrentRow != null)
            {
                Pelicula peliculaSeleccionada;
                peliculaSeleccionada = (Pelicula)dgvPeliculas.CurrentRow.DataBoundItem;

                frmAgregarPelicula editarPelicula = new frmAgregarPelicula(peliculaSeleccionada);
                editarPelicula.Show();
                this.Close();
            }
        }

        //ELIMINAR
        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvPeliculas.CurrentRow != null)
            {
                int id = (int)dgvPeliculas.CurrentRow.Cells[0].Value;

                var confirmacion = 
                    MessageBox.Show("Esta por eliminar la Pelicula " + dgvPeliculas.CurrentRow.Cells[4].Value, "ALERTA", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                if(confirmacion == DialogResult.OK)
                {
                    await DeleteHttp(id);
                    cargarDataGrid();
                }
                dgvPeliculas.Refresh();
            }
        }

        //VOLVER
        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
            frmHome frmHome = new frmHome();
            frmHome.Show();
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

        private void frmPeliculas_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
