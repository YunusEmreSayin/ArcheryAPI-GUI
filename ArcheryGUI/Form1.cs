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
            //defining dataModel object for post request
    
            await getChart(dataModel);
            //calling the method for creating chart
            MessageBox.Show("Grafik Oluşturuldu","Grafik Oluşturma Başarılı");
            txtarcher.Clear();
            //Showing messagebox for information
            //Clearing textbox after that
        }
        private async void Form1_Load(object sender, EventArgs e)
        {
            lbltime.Text = lbltime.Text = (DateTime.Now).ToString();
            //setting the time in FormLoad for nonNull text
            
            Control.CheckForIllegalCrossThreadCalls = false; //For async Methods
            
            while (true)
            {
                await getDateTime();
                //calling the method for getting current DateTime data per second
            }
        }
        private async Task getChart(requestModel model)
        {
            var client=new HttpClient();
            client.BaseAddress = new Uri("http://127.0.0.1:8000/");
            //Creating HttpClient object and defining the baseAdress url
            
            JavaScriptSerializer js= new JavaScriptSerializer();
            var SerializedData=js.Serialize(model);
            //Serializing model to json type for request

            StringContent content= new StringContent(SerializedData,Encoding.UTF8,"application/json");
            //Defining request string

            try
            {
                var response= await client.PostAsync("/createResults", content);
                //sending request ( METHOD=POST) and getting response
                
                var responseString=await response.Content.ReadAsStringAsync();
                //Converting response to string type
                
                dataModel datamodel = new dataModel();
                datamodel = JsonConvert.DeserializeObject<dataModel>(responseString);
                //Creating dataModel object for chart-method
                //Defining response string as dataModel object to datamodel variable
                
                await setChart(datamodel);
                //Calling method for creating chart and setting it for picturebox

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private async Task setChart(dataModel model)
        {
            string imageData = model.imageBytes;
            //getting PNG file as bytes in a string
            
            MemoryStream stream = new MemoryStream(Convert.FromBase64String(imageData));
            //encoding string to base64format            
            Image image = Image.FromStream(stream);
            //Creating an image from base64
        
            picresult.Image = image;
            //Setting picturebox's source
        }


        private async Task getDateTime()
        {
            await Task.Run(async () =>
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://127.0.0.1:8000/");
                //Creating HttpClient object and defining the baseAdress url

                //Exception Handling    
                try
                {
                    var response = await client.GetAsync("getTarihSaat");
                    //getting currenct DateTime data
                    
                    var jsonString = await response.Content.ReadAsStringAsync();
                    //defining response as string
                    
                    JObject jsonObject = JObject.Parse(jsonString);
                    //Converting string-type json to JObject-type
                    
                    lbltime.Text = (string)jsonObject["Date"];
                    //setting label's text 
                    
                    await Task.Delay(1000);
                    //adding delay for reliabilty

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);

                }
            });
        }
    }
}
