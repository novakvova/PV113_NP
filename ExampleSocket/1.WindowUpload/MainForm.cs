using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace _1.WindowUpload
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string filePath = dlg.FileName;
                //MessageBox.Show("Select path", filePath);
                txtPath.Text = filePath;
            }
        }

        private void btnUploadToServer_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtPath.Text))
            {
                MessageBox.Show("ќбер≥ть фото!");
                return;
            }
            //ќтримуЇмо байти ≥з файлу
            byte[] imgBytes = File.ReadAllBytes(txtPath.Text);
            string base64 = Convert.ToBase64String(imgBytes);
            UploadDTO upload = new UploadDTO
            {
                Photo = base64
            };
            string json = JsonConvert.SerializeObject(upload);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            string serverUrl = "https://pv113.itstep.click";
            WebRequest request = WebRequest.Create($"{serverUrl}/api/gallery/upload");
            request.Method = "POST";
            request.ContentType = "application/json";
            using(var stream = request.GetRequestStream())
            {
                stream.Write(bytes, 0, bytes.Length);
            }
            try
            {
                var response = request.GetResponse();
                using(var stream = new StreamReader(response.GetResponseStream()))
                {
                    string data = stream.ReadToEnd();
                    var resp = JsonConvert.DeserializeObject<UploadResponseDTO>(data);
                    txtPath.Text = serverUrl+resp.Image;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}