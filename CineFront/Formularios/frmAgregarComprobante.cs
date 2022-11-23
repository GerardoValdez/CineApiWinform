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
//using System.Text.Json;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestSharp.Serialization.Json;

namespace CineFront
{
    public partial class frmAgregarComprobante : Form
    {
        private Comprobante comprobanteCargado = null;
        private Comprobante comprobanteNuevo;
        frmComprobantes formComprobantes = new frmComprobantes();

        public frmAgregarComprobante()
        {
            InitializeComponent();
            comprobanteNuevo = new Comprobante();
            btnAceptar.Show();
            btnEditar.Hide();
        }

        public frmAgregarComprobante(Comprobante comprobanteCargado)
        {
            InitializeComponent();
            this.comprobanteCargado = comprobanteCargado;
            btnAceptar.Hide();
            btnEditar.Show();
        }

        private async void frmAgregarEntrada_Load(object sender, EventArgs e)
        {
            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtPrecio.Text = 200.ToString();

            //COMBO FUNCION
            string jsonfunciones = await GetHttp("https://localhost:44301/funciones");
            var lstfunciones = JsonConvert.DeserializeObject<List<Funcion>>(jsonfunciones);
            cboFuncion.DataSource = lstfunciones;
            cboFuncion.ValueMember = "IdFuncion";
            cboFuncion.DisplayMember = "Descripcion";
            cboFuncion.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFuncion.SelectedIndex = -1;

            //COMBO CLIENTES
            string jsonclientes = await GetHttp("https://localhost:44301/clientes");
            var lstClientes = JsonConvert.DeserializeObject<List<Cliente>>(jsonclientes);
            cboCliente.DataSource = lstClientes;
            cboCliente.ValueMember = "IdCliente";
            cboCliente.DisplayMember = "Nombre";
            cboCliente.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCliente.SelectedIndex = -1;

            //COMBO FORMA PAGO
            string jsonFormaPagos = await GetHttp("https://localhost:44301/formaPago");
            var lstFormaPagos = JsonConvert.DeserializeObject<List<FormaPago>>(jsonFormaPagos);
            cboFormaPago.DataSource = lstFormaPagos;
            cboFormaPago.ValueMember = "IdFormaPago";
            cboFormaPago.DisplayMember = "Descripcion";
            cboFormaPago.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFormaPago.SelectedIndex = -1;

            //COMBO FORMA COMPRA
            string jsonFormacompras = await GetHttp("https://localhost:44301/formaCompra");
            var lstFormacompras = JsonConvert.DeserializeObject<List<FormaCompra>>(jsonFormacompras);
            cboFormaCompra.DataSource = lstFormacompras;
            cboFormaCompra.ValueMember = "IdFormaCompra";
            cboFormaCompra.DisplayMember = "Descripcion";
            cboFormaCompra.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFormaCompra.SelectedIndex = -1;


            if (comprobanteCargado != null)     //cargar datos de la pelicula seleccionada
            {
                txtFecha.Text = comprobanteCargado.Fecha.ToString();
                cboCliente.SelectedValue = comprobanteCargado.IdCliente;
                cboFormaCompra.SelectedValue = comprobanteCargado.IdFormaCompra;
                cboFormaPago.SelectedValue = comprobanteCargado.IdFormaPago;
            }
        }

        //************************************* METODOS *************************************

        //GET
        public async Task<string> GetHttp(string url)
        {
            WebRequest oRequest = WebRequest.Create(url);
            WebResponse oResponse = oRequest.GetResponse();
            StreamReader sr = new StreamReader(oResponse.GetResponseStream());
            return await sr.ReadToEndAsync();
        }

        //POST
        public async Task<string> PostComprobante(Comprobante comprobante)
        {
            string url = "https://localhost:44301/cargarComprobante";
            var client = new HttpClient();

            var data = JsonConvert.SerializeObject(comprobante);
            HttpContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            var HttpResponse = await client.PostAsync(url, content);

            if (HttpResponse.IsSuccessStatusCode)
            {
                MessageBox.Show("Se cargo con exito la compra");
                return "OK";
            }
            MessageBox.Show("Hubo un error al cargar la compra");
            return "false";
        }

        //PUT
        public async Task<string> PutComprobante(Comprobante comprobante)
        {
            string url = "https://localhost:44301/actualizarComprobante";
            var client = new HttpClient();

            var data = JsonConvert.SerializeObject(comprobante);
            HttpContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            var HttpResponse = await client.PutAsync(url, content);

            if (HttpResponse.IsSuccessStatusCode)
            {
                MessageBox.Show("Se edito con exito la compra");
                return "OK";
            }
            MessageBox.Show("Hubo un error al cargar la compra");
            return "false";
        }
        //************************************* BOTONES *************************************

        //AGREGAR FUNCION
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                Funcion funcion = (Funcion)cboFuncion.SelectedItem;
                funcion.Precio = double.Parse(txtPrecio.Text);
                int cantidad = Convert.ToInt32(nudCantidad.Value);

                DetalleComprobante detalle = new DetalleComprobante(funcion, cantidad);
                comprobanteNuevo.AgregarDetalle(detalle);
                dgvComprobante.Rows.Add(new object[] { funcion.IdFuncion, funcion.Descripcion, funcion.Precio, nudCantidad.Value });

                var total = comprobanteNuevo.CalcularTotal();
                textBox1.Text = total.ToString();
            }
        }

        private bool validar()
        {
            if(cboCliente.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un cliente", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                return false;
            }
            if(cboFuncion.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione una funcion", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                return false;
            }
            if(cboFormaCompra.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione una forma de compra", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                return false;
            }
            if(cboFormaPago.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione una forma de pago", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                return false;
            }
            if (nudCantidad.Value == 0)
            {
                MessageBox.Show("Ingrese una cantidad valida", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                return false;
            }
            double number;
            if(false == double.TryParse(txtPrecio.Text, out number))
            {
                MessageBox.Show("Ingrese un precio", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                return false; 
            } 
            if(double.Parse(txtPrecio.Text) <= 0) 
            {
                MessageBox.Show("El precio debe ser mayor a 0", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                return false; 
            }

            return true;
        }

        //ELIMINAR FUNCION
        private void dgvComprobante_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvComprobante.CurrentCell.ColumnIndex == 4)
            {
                comprobanteNuevo.QuitarDetalle(dgvComprobante.CurrentRow.Index);
                dgvComprobante.Rows.Remove(dgvComprobante.CurrentRow);

                var total = comprobanteNuevo.CalcularTotal();
                textBox1.Text = total.ToString();
            }
        }

        //EDITAR
        private async void btnEditar_Click(object sender, EventArgs e)
        {
            if (cboCliente.SelectedIndex == -1)
            {
                MessageBox.Show("Debe ingresar un cliente!", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (dgvComprobante.Rows.Count == 0)
            {
                MessageBox.Show("Debe ingresar al menos una funcion!", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            comprobanteNuevo.IdCliente = (int)cboCliente.SelectedValue;
            comprobanteNuevo.IdFormaCompra = (int)cboFormaCompra.SelectedValue;
            comprobanteNuevo.IdFormaPago = (int)cboFormaPago.SelectedValue;

            await PutComprobante(comprobanteNuevo);
            formComprobantes.Show();
            this.Close();
        }

        //CARGAR
        private async void btnAceptar_Click(object sender, EventArgs e)
        {
            if (cboCliente.SelectedIndex == -1)
            {
                MessageBox.Show("Debe ingresar un cliente!", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (dgvComprobante.Rows.Count == 0)
            {
                MessageBox.Show("Debe ingresar al menos una funcion!", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            comprobanteNuevo.IdCliente = (int)cboCliente.SelectedValue;
            comprobanteNuevo.IdFormaCompra = (int)cboFormaCompra.SelectedValue;
            comprobanteNuevo.IdFormaPago = (int)cboFormaPago.SelectedValue;

            await PostComprobante(comprobanteNuevo);
            formComprobantes.Show();
            this.Close();
        }

        //CANCELAR
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
            frmComprobantes comprobantes = new frmComprobantes();
            comprobantes.Show();
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

        private void frmAgregarEntrada_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
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

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >=58 && e.KeyChar <= 255))
            {
                MessageBox.Show("Sólo Números.", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                e.Handled = true;
                return; 
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
