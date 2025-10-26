using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LuvixAdminServices
{
    public partial class frmDashboard : Form
    {
        public frmDashboard()
        {
            InitializeComponent();
            // Llamamos al método para ocultar los submenús al iniciar
            frmDashboard_Load(null, null);
        }

        // Oculatmos los paneles al iniciar el formulario
        private void frmDashboard_Load(object sender, EventArgs e)
        {
            panelSubMenuUsuarios.Visible = false;
            panelSubMenuTiendas.Visible = false;
        }

        // Ocultamos todos los submenús
        private void ocultarSubMenu()
        {
            if (panelSubMenuUsuarios.Visible == true)
                panelSubMenuUsuarios.Visible = false;
            if (panelSubMenuTiendas.Visible == true)
                panelSubMenuTiendas.Visible = false;
        }

        // Mostramos el submenú correspondiente
        private void mostrarSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                ocultarSubMenu();
                subMenu.Visible = true;
            }
            else
            {
                subMenu.Visible = false;
            }
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            mostrarSubMenu(panelSubMenuUsuarios);            
        }

        private void btnUsuariosRegistrados_Click(object sender, EventArgs e)
        {
            abrirFormularioHijo(new frmUsuariosRegistrados());
            cambiarColorBotonActivo(btnUsuariosRegistrados);
            
        }

        private void btnConfiguracionesUsuario_Click(object sender, EventArgs e)
        {            
            cambiarColorBotonActivo(btnConfiguracionesUsuario);
        }

        private void btnTiendas_Click(object sender, EventArgs e)
        {
            mostrarSubMenu(panelSubMenuTiendas);            
        }

                private void btnTiendasRegistradas_Click(object sender, EventArgs e)
        {
            abrirFormularioHijo(new frmTiendasRegistradas());
            cambiarColorBotonActivo(btnTiendasRegistradas);            
        }

        private void btnConfiguracionesTiendas_Click(object sender, EventArgs e)
        {         
            cambiarColorBotonActivo(btnConfiguracionesTiendas);
        }


        // Metodo para abrir formularios hijos dentro del panel principal
        private Form formularioActivo = null;
        private void abrirFormularioHijo(Form formularioHijo)
        {
            if (formularioActivo != null)
                formularioActivo.Close();

            formularioActivo = formularioHijo;
            formularioHijo.TopLevel = false;
            formularioHijo.FormBorderStyle = FormBorderStyle.None;
            formularioHijo.Dock = DockStyle.Fill;
            panelFormulariosHijos.Controls.Add(formularioHijo);
            panelFormulariosHijos.Tag = formularioHijo;
            formularioHijo.BringToFront();
            formularioHijo.Show();
        }
        
        private void cambiarColorBotonActivo(Button botonActivo)
        {
            // Restaurar el color de todos los botones del submenú
            foreach (Control control in panelSubMenuUsuarios.Controls)
            {
                if (control is Button btn)
                {
                    btn.BackColor = Color.SlateBlue; 
                }
            }

            foreach (Control control in panelSubMenuTiendas.Controls)
            {
                if (control is Button btn)
                {
                    btn.BackColor = Color.SlateBlue; 
                }
            }

            // Cambiar el color del botón activo
            if (botonActivo != null)
                botonActivo.BackColor = Color.DimGray;

        }
    }
}
