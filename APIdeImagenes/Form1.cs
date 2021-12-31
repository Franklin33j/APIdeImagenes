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

namespace APIdeImagenes
{
    public partial class Form1 : Form
    {
        private Bitmap bitmap;
        public Form1()
        {
            InitializeComponent();

        }

        private void btnConsulta_Click(object sender, EventArgs e)
        {

            string url = "https://source.unsplash.com/random";

            //if (!CheckConnection(url)) MessageBox.Show("No hay conexion");
            
                try
                {
                    WebClient client = new WebClient();
                    Stream stream = client.OpenRead(url);
                    bitmap = new Bitmap(stream);
                    stream.Flush();
                    stream.Close();
                }
                catch (Exception a)
                {
                    Console.WriteLine(a.Message);
                }
                img.BackgroundImage = bitmap;
            
            
        }

        private static bool CheckConnection(String URL)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                request.Timeout = 5000;
                request.Credentials = CredentialCache.DefaultNetworkCredentials;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        private void img_Click(object sender, EventArgs e)
        {
        }
    }
}
