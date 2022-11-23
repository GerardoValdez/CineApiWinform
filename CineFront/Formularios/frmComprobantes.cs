using CineBack.dominio;
using CineBack.soporte;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CineFront
{
    public partial class frmComprobantes : Form
    {
        
        public frmComprobantes()
        {
            InitializeComponent();
        }

        private void frmEntradas_Load(object sender, EventArgs e)
        {
            cargarDataGrid();
        }

        private void dgvEntradas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvEntradas.CurrentRow != null)
            {
                //btnEditar.Enabled = true;
                btnEliminar.Enabled = true;
            }
        }

        //************************************* METODOS *************************************

        //CARGAR DATAGRID
        private async void cargarDataGrid()
        {
            string respuesta = await GetHttp();
            List<ViewComprobante> lst = JsonConvert.DeserializeObject<List<ViewComprobante>>(respuesta);
            dgvEntradas.DataSource = lst;

            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;

            dgvEntradas.Refresh();
        }

        //GET
        public async Task<string> GetHttp()
        {
            string url = "https://localhost:44301/comprobantes";

            WebRequest oRequest = WebRequest.Create(url);
            WebResponse oResponse = oRequest.GetResponse();
            StreamReader sr = new StreamReader(oResponse.GetResponseStream());
            return await sr.ReadToEndAsync();
        }

        //DELETE
        public async Task<string> DeleteHttp(int id)
        {
            string url = "https://localhost:44301/borrarComprobante?nro=" + id;
            var client = new HttpClient();

            var HttpResponse = await client.DeleteAsync(url);

            if (HttpResponse.IsSuccessStatusCode)
            {
                MessageBox.Show("Se dio de baja la compra con exito");
                return "OK";
            }
            MessageBox.Show("Hubo un error a dar de baja la compra");
            return "false";
        }


        //************************************* BOTONES *************************************

        //AGREGAR
        private void btnAgregar_Click_1(object sender, EventArgs e)
        {
            frmAgregarComprobante agregarComprobante = new frmAgregarComprobante();
            agregarComprobante.Show();
            this.Close();
        }

        //EDITAR
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvEntradas.CurrentRow != null)
            {
                Comprobante comprobanteSeleccionado;
                comprobanteSeleccionado = (Comprobante)dgvEntradas.CurrentRow.DataBoundItem;

                frmAgregarComprobante editarComprobante = new frmAgregarComprobante(comprobanteSeleccionado);
                editarComprobante.Show();
                this.Close();
            }
        }

        //ELIMINAR
        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvEntradas.CurrentRow != null)
            {
                int id = (int)dgvEntradas.CurrentRow.Cells[0].Value;

                var confirmacion =
                    MessageBox.Show("Esta por eliminar la compra del cliente: " + dgvEntradas.CurrentRow.Cells["cliente"].Value, "ALERTA", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                if (confirmacion == DialogResult.OK)
                {
                    await DeleteHttp(id);
                    cargarDataGrid();
                }
                dgvEntradas.Refresh();
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

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void frmTransaccion_MouseDown(object sender, MouseEventArgs e)
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
