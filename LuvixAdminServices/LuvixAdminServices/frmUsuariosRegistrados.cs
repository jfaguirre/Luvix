using LuvixAdminServices.ModelosDTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LuvixAdminServices
{
    public partial class frmUsuariosRegistrados : Form
    {
        public frmUsuariosRegistrados()
        {
            InitializeComponent();
        }

        private void frmUsuariosRegistrados_Load(object sender, EventArgs e)
        {
            CargarUsuarios();
        }


        private async void CargarUsuarios()
        {
            string apiUrl = "http://localhost:5206/api/Usuario/lista-usuarios";

            try
            {
                using (var client = new HttpClient())
                {                    
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(jsonResponse);

                        // Asigna la lista al DataGridView
                        dgvUsuarios.DataSource = apiResponse.value;
                    }
                    else
                    {
                        MessageBox.Show($"Error: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar con la API: " + ex.Message);
            }
        }       
    }
}
