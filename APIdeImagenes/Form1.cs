using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using System.Net;
using System.IO;
using System.Net.NetworkInformation;
using System.Drawing.Imaging;

namespace APIdeImagenes
{
    public partial class Form1 : Form
    {
        private Bitmap bitmap;
        public Form1()
        {
            InitializeComponent();

        }

        //Recupera la imagen de la API
        private async Task ObtenerImagen(string url)
        {
            try
            {
                btnImagen.Enabled = false;
                WebClient client = new WebClient();
                Stream stream = await client.OpenReadTaskAsync(url);
                bitmap = new Bitmap(stream);
                stream.Flush();
                stream.Close();
               
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message+" Error ");
            }
 
        }

        private async Task<bool> CheckConnection()
        {
            WebClient client = new WebClient();
            try
            {
                Uri url = new Uri("http://www.google.com");
                using (await client.OpenReadTaskAsync(url))
                {
                    return true;
                }      
            }
            catch (WebException)
            {
                return false;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            var a= img.BackgroundImage;
            if (a != null) {
                
                Random ran = new Random();
                int aleatorio= ran.Next(0, 9);
                saveFileDialog1.FileName = "imagen"+aleatorio;
                saveFileDialog1.Filter = "Images|*.jpg; *.pgn";
                
                ImageFormat formato = ImageFormat.Jpeg;
                DialogResult result = saveFileDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    bitmap.Save(saveFileDialog1.FileName, formato);
                }
            } 
            else MessageBox.Show("No has buscado ninguna imagen");
        }

        private async void btnImagen_Click(object sender, EventArgs e)
        {
            string url = "https://source.unsplash.com/random";
            bool conexion = await CheckConnection();
            if (conexion)
            {
                await ObtenerImagen(url);
                img.BackgroundImage = bitmap;
                btnImagen.Enabled = true;
            }
            else MessageBox.Show("No hay conexion a internet", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }
}
