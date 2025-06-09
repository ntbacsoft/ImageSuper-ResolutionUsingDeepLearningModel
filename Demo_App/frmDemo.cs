using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestImage
{
    public partial class frmDemo : Form
    {
        private float zoomFactor = 1.0f;
        private const float ZoomStep = 0.1f;
        private Point mousePos;
        private bool isDragging = false;
        private Point startDragPoint;
        private Point startPictureBoxLocation;
        private Size originalImageSize;

        private static readonly HttpClient client = new HttpClient();

        public frmDemo()
        {
            InitializeComponent();
            Reduce_Flicker();
            Location = new Point(0, 0);
            MouseAction();
            this.AutoScroll = true;
            if (pictureBox1.Image != null)
            {
                originalImageSize.Width = pictureBox1.Width;
                originalImageSize.Height = pictureBox1.Height;
            }
            btnRespond.Enabled = false;
            ClearImages.Enabled = false;
            btnRespond.BackColor = Color.Gray;
            btnRespond.ForeColor = Color.White;
            ClearImages.BackColor = Color.Gray;
            ClearImages.ForeColor = Color.White;
            loadingGif.Parent = this;
            loadingGif.BringToFront();
        }
        private async Task UploadFile(string filePath)
        {
            using (var content = new MultipartFormDataContent())
            {
                var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                var fileContent = new StreamContent(fileStream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");

                content.Add(fileContent, "file", Path.GetFileName(filePath));
                loadingGif.Visible = true;
                HttpResponseMessage response;
                try
                {
                    response = await client.PostAsync("http://127.0.0.1:5000/upload", content);
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show("API is not running");
                    return;
                }
                string result = await response.Content.ReadAsStringAsync();
                MessageBox.Show(result);
            }
        }
        private async void btnSend_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.png;";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    await UploadFile(ofd.FileName);
                    try
                    {
                        var files = await GetFileList("download");
                        if (files.Count == 0)
                        {
                            MessageBox.Show("No files in directory");
                            return;
                        }
                        string url = $"http://127.0.0.1:5000/download/{files[0]}";
                        HttpResponseMessage response = await client.GetAsync(url);
                        if (response.IsSuccessStatusCode){
                            loadingGif.Visible = false;
                            byte[] imageBytes = await response.Content.ReadAsByteArrayAsync();
                            using (var ms = new MemoryStream(imageBytes)){
                                pictureBox1.Image = Image.FromStream(ms);
                                originalImageSize.Width = pictureBox1.Width;
                                originalImageSize.Height = pictureBox1.Height;
                                btnRespond.Enabled = true;
                                ClearImages.Enabled = true;
                                btnRespond.BackColor = Color.White;
                                btnRespond.ForeColor = Color.Black;
                                ClearImages.BackColor = Color.White;
                                ClearImages.ForeColor = Color.Black;
                            }
                        }
                        else
                        {
                            MessageBox.Show($"Error downloading image: {response.StatusCode}");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                }
            }
        }

        private async Task<List<string>> GetFileList(string route)
        {

            try
            {
                HttpResponseMessage response = await client.GetAsync($"http://127.0.0.1:5000/{route}");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    return JsonSerializer.Deserialize<List<string>>(json, options) ?? new List<string>();
                }

                MessageBox.Show("Error when calling API: " + response.StatusCode);
                return new List<string>();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot get file list: " + ex.Message);
                return new List<string>();
            }
        }

        private async void btnRespond_Click(object sender, EventArgs e)
        {
            try
            {
                var files = await GetFileList("result");
                if (files.Count == 0)
                {
                    MessageBox.Show("No files in directory");
                    return;
                }

                string url = $"http://127.0.0.1:5000/result/{files[0]}";
                string urlValue = $"http://127.0.0.1:5000/psnr";
                
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    byte[] imageBytes = await response.Content.ReadAsByteArrayAsync();
                    using (var ms = new MemoryStream(imageBytes))
                    {
                        pictureBox2.Image = Image.FromStream(ms);
                        MouseAction();
                    }
                    HttpResponseMessage resValue = await client.GetAsync(urlValue);
                    if (resValue.IsSuccessStatusCode) {
                        string json = await resValue.Content.ReadAsStringAsync();
                        var result = JsonSerializer.Deserialize<ResultModel>(json);
                        ssimTxt.Text = $"SSIM: {result.ssim:F4}";
                        psnrTxt.Text = $"PSNR: {result.psnr:F2}";
                    } else
                    {
                        MessageBox.Show($"Error Value image: {resValue.StatusCode}");
                    }
                }
                else
                {
                    MessageBox.Show($"Error downloading image: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

        }

        

        private void OnMouseWheel(object sender, MouseEventArgs e)
        {
            if (ModifierKeys == Keys.Control)
            {
                float relativeX = (float)(mousePos.X) / pictureBox1.Width;
                float relativeY = (float)(mousePos.Y) / pictureBox1.Height;
                float ScaleDefault = zoomFactor;
                if (e.Delta > 0)
                {
                    zoomFactor += ZoomStep;
                }
                else if (e.Delta < 0)
                {
                    zoomFactor = Math.Max(0.1f, zoomFactor - ZoomStep);
                }
                int newWidth = (int)(originalImageSize.Width * zoomFactor);
                int newHeight = (int)(originalImageSize.Height * zoomFactor);
                if (newWidth < originalImageSize.Width)
                {
                    newWidth = originalImageSize.Width;
                    newHeight = originalImageSize.Height;
                }
                pictureBox1.Left -= (int)((newWidth - pictureBox1.Width) * relativeX);
                pictureBox1.Top -= (int)((newHeight - pictureBox1.Height) * relativeY);
                pictureBox2.Left -= (int)((newWidth - pictureBox2.Width) * relativeX);
                pictureBox2.Top -= (int)((newHeight - pictureBox2.Height) * relativeY);

                pictureBox1.Width = newWidth;
                pictureBox1.Height = newHeight;
                pictureBox2.Width = newWidth;
                pictureBox2.Height = newHeight;
                pictureBox1.Invalidate();
                pictureBox2.Invalidate();
            }
        }
        private void Reduce_Flicker()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
        }
        private void MouseAction()
        {
            pictureBox1.MouseWheel += OnMouseWheel;
            pictureBox2.MouseWheel += OnMouseWheel;
            pictureBox1.MouseDown += OnMouseDown;
            pictureBox1.MouseMove += OnMouseMove;
            pictureBox1.MouseUp += OnMouseUp;
            pictureBox2.MouseDown += OnMouseDown;
            pictureBox2.MouseMove += OnMouseMove;
            pictureBox2.MouseUp += OnMouseUp;
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                startDragPoint = e.Location;
                startPictureBoxLocation = pictureBox1.Location;
            }
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPoint = e.Location;
                int offsetX = currentPoint.X - startDragPoint.X;
                int offsetY = currentPoint.Y - startDragPoint.Y;

                pictureBox1.Location = new Point(
                    startPictureBoxLocation.X + offsetX,
                    startPictureBoxLocation.Y + offsetY
                );
                pictureBox2.Location = new Point(
                    startPictureBoxLocation.X + offsetX,
                    startPictureBoxLocation.Y + offsetY
                );
            }
            else
                mousePos = e.Location;
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }

        private void ClearImages_Click(object sender, EventArgs e)
        {
            btnRespond.Enabled = false;
            ClearImages.Enabled = false;
            pictureBox1.Image = null;
            pictureBox2.Image = null;
            zoomFactor = 1.0f;

            pictureBox1.Width = originalImageSize.Width;
            pictureBox1.Height = originalImageSize.Height;
            pictureBox2.Width = originalImageSize.Width;
            pictureBox2.Height = originalImageSize.Height;

            pictureBox1.Location = new Point(0, 0);
            pictureBox2.Location = new Point(0, 0);
            btnRespond.BackColor = Color.Gray;
            btnRespond.ForeColor = Color.White;
            ClearImages.BackColor = Color.Gray;
            ClearImages.ForeColor = Color.White;
            psnrTxt.Text = "PSNR: ";
            ssimTxt.Text = "SSIM: ";
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            base.OnPaint(e);
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
        }

    }
    public class ResultModel
    {
        public double psnr { get; set; }
        public double ssim { get; set; }
    }
}
