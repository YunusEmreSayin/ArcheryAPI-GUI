using ArcheryGUI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace ArcheryGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void btngrafik_Click(object sender, EventArgs e)
        {
            requestModel dataModel = new requestModel();
            dataModel.Name = txtarcher.Text;
            

            await getChart(dataModel);
            MessageBox.Show("Grafik Oluşturuldu","Grafik Oluşturma Başarılı");
            txtarcher.Clear();
        }
        private async void Form1_Load(object sender, EventArgs e)
        {
            while (true)
            {

                await getDateTime();
                
            }
        }
        private async Task getChart(requestModel model)
        {
            var client=new HttpClient();
            client.BaseAddress = new Uri("http://localhost:80");
            
            JavaScriptSerializer js= new JavaScriptSerializer();
            var SerializedData=js.Serialize(model);

            StringContent content= new StringContent(SerializedData,Encoding.UTF8,"application/json");

            try
            {
                var response= await client.PostAsync("/createResults", content);
                var responseString=await response.Content.ReadAsStringAsync();

                dataModel datamodel = new dataModel();
                datamodel = JsonConvert.DeserializeObject<dataModel>(responseString);
                await setChart(datamodel);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private async Task setChart(dataModel model)
        {
            string imageData = model.imageBytes;
            
            MemoryStream stream = new MemoryStream(Convert.FromBase64String(imageData));
            Image image = Image.FromStream(stream);
            
            picresult.Image = image;
        }


        private async Task getDateTime()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:80");

            try
            {
                var response = await client.GetAsync("getTarihSaat");
                var jsonString = await response.Content.ReadAsStringAsync();
                JObject jsonObject = JObject.Parse(jsonString);
                lbltime.Text = (string)jsonObject["Date"];
                await Task.Delay(1000);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
