using Cine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CineFront
{
    public partial class frmHome : Form
    {
        public frmHome()
        {
            InitializeComponent();
        }

        //************************************* MENU *************************************

        //SALIR
        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Está por cerrar la aplicación.", "SALIR", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                Application.Exit();
        }

        //PELICULAS
        private void tablaPelículaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPeliculas peliculas = new frmPeliculas();
            peliculas.Show();
            this.Close();
        }

        //ENTRADAS
        private void ventaDeEntradasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmComprobantes transaccion = new frmComprobantes();
            transaccion.Show();
            this.Close();
        }

        //REPORTE
        private void reporteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Inicializar el proyecto Reportes.", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //CONOCENOS
        private void conocenosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConocenos conocenos = new frmConocenos();
            conocenos.Show();
            this.Close();
        }


        [DllImport("user32.Dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.Dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void panel1_MouseDown_1(object sender, MouseEventArgs e)
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
