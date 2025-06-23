using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using tryThresholds.IP_tools;
using VoltasBeko;

namespace VoltasBeko
{

    internal static class ApiController
    {

        static ApiController()
        {
            Console.WriteLine("ip from contructor " + Ip);
            ServerCheckTimer.Interval = 3500;
            ServerCheckTimer.Enabled = true;
            ServerCheckTimer.Tick += ServerCheckTimer_Tick;
            ServerCheckTimer.Start();
        }



        private static void ServerCheckTimer_Tick(object sender, EventArgs e)
        {
            CheckServerRunning();
        }

        private static bool checkServerFlag = true;
        private static System.Windows.Forms.Timer ServerCheckTimer = new System.Windows.Forms.Timer();
        public static event Action<bool> ApiConnectionEvent;

        public static string Ip = "127.0.0.1";
        public static string Port = "5003";

        public static string ProcessImage(Bitmap inputImage, string endPoint = "predict", string port = "5003")
        {

            string imageString = BitmapToBase64(inputImage);
            inputImage.Save($@"inputImage.bmp");

            string ResponseString = "";
            HttpWebResponse response = null;
            try
            {
                var request = (HttpWebRequest)WebRequest.Create($"http://{Ip}:{port}/{endPoint}");
                request.Accept = "application/json";
                request.Method = "POST";
                request.Timeout = 10000;// int.MaxValue;//limit waitTime
                JavaScriptSerializer jss = new JavaScriptSerializer();
                jss.MaxJsonLength = int.MaxValue;
                var imageDict = new Dictionary<string, string>
                {
                    { "image", imageString }
                };

                var myContent = jss.Serialize(imageDict);

                //Console.WriteLine(JsonConvert.SerializeObject(myContent));

                var data = Encoding.ASCII.GetBytes(myContent);

                request.ContentType = "application/json";
                request.ContentLength = data.Length;

                Console.WriteLine($"Request Write to API ");
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                response = (HttpWebResponse)request.GetResponse();
                ResponseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                //ConsoleExtension.WriteWithColor(ResponseString);
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
                return ex.ToString();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }


            return ResponseString;
        }


        //public static string ProcessBackImage(ApiDataBackCam objDetections,  string endPoint = "back_Detection", string port = "5004")
        //{

        //    //string imageString = BitmapToBase64(inputImage);
        //    //inputImage.Save($@"E:\Software\VoltasBeko\VoltasBeko\Images\{DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss")}.bmp");

        //    string ResponseString = "";
        //    HttpWebResponse response = null;
        //    try
        //    {
        //        var request = (HttpWebRequest)WebRequest.Create($"http://{Ip}:{port}/{endPoint}");
        //        request.Accept = "application/json";
        //        request.Method = "POST";
        //        request.Timeout = 5000;// int.MaxValue;//limit waitTime
        //        JavaScriptSerializer jss = new JavaScriptSerializer();
        //        jss.MaxJsonLength = int.MaxValue;
        //        //var requestData = new
        //        //{
        //        //    image = imageString,
        //        //    detectionData = objDetections
        //        //};

        //        var myContent = jss.Serialize(objDetections);

        //        ConsoleExtension.WriteWithColor(JsonConvert.SerializeObject(myContent), ConsoleColor.Yellow);

        //        var data = Encoding.ASCII.GetBytes(myContent);

        //        request.ContentType = "application/json";
        //        request.ContentLength = data.Length;

        //        Console.WriteLine($"Request Write to API ");
        //        using (var stream = request.GetRequestStream())
        //        {
        //            stream.Write(data, 0, data.Length);
        //        }

        //        response = (HttpWebResponse)request.GetResponse();
        //        ResponseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

        //        //ConsoleExtension.WriteWithColor(ResponseString);
        //    }
        //    catch (WebException ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return ex.ToString();

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);

        //    }


        //    return ResponseString;
        //}

        public static string BitmapToBase64(Bitmap image)
        {
            string base64String = "";
            using (MemoryStream m = new MemoryStream())
            {
                image.Save(m, ImageFormat.Jpeg);
                byte[] imageBytes = m.ToArray();
                base64String = Convert.ToBase64String(imageBytes);
            }
            return base64String;
        }

        public static Bitmap ConvertBase64ToBitmap(string base64String)
        {
            try
            {
                byte[] imageBytes = Convert.FromBase64String(base64String);

                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    Bitmap bitmap = new Bitmap(ms);
                    return new Bitmap(bitmap); // Clone the bitmap to prevent memory leaks
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during the conversion
                Console.WriteLine($"Error converting base64 to bitmap: {ex.Message}");
                return null;
            }
        }

        public static async Task ShutdownServer(int port)
        {
            await Task.Delay(100);
            checkServerFlag = false;
            ServerCheckTimer.Stop();

            string baseUrl = $"http://{Ip}:{port}";

            using (var httpClient = new HttpClient())
            {
                try
                {
                    // Send a GET request to the /shutdown endpoint
                    HttpResponseMessage response = await httpClient.GetAsync($"{baseUrl}/shutdown");

                    if (response.IsSuccessStatusCode)
                    {
                        // Server shutdown request was successful
                        Console.WriteLine($"Server {port} shutdown request sent successfully.");
                    }
                    else
                    {
                        Console.WriteLine($"Server {port} shutdown request failed with status code: " + response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
        }

        public static async void CheckServerRunning()
        {

            string baseUrl = $"http://{Ip}:{Port}";

            using (var httpClient = new HttpClient())
            {
                try
                {
                    // Send a GET request to the /shutdown endpoint
                    HttpResponseMessage response = await httpClient.GetAsync($"{baseUrl}/ServerCheck");

                    if (response.IsSuccessStatusCode)
                    {
                        // Server shutdown request was successful
                        //ConsoleExtension.WriteWithColor($"Server {Port} running successfully.", ConsoleColor.Green);
                        ApiConnectionEvent?.Invoke(true);

                    }
                    else
                    {
                        ConsoleExtension.WriteWithColor($"Server {Port} running failed with status code: " + response.StatusCode, ConsoleColor.Red);
                        ApiConnectionEvent?.Invoke(false);

                    }
                }
                catch (Exception ex)
                {
                    ConsoleExtension.WriteWithColor("An error occurred in CheckServerRunning: " + ex.Message, ConsoleColor.Red);
                    ApiConnectionEvent?.Invoke(false);

                }
            }
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        public static (string text, bool status) RunAnacondaCmd(string fileName = "singal_image_pass")
        {
            try
            {

                // Setting working directory and create process
                var workingDirectory = $@"{AppData.ProjectDirectory}\Voltas_api_image";


                Process processAnaconda = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        RedirectStandardInput = true,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        WorkingDirectory = workingDirectory
                    }
                };
                processAnaconda.Start();

                using (var sw = processAnaconda.StandardInput)
                {
                    if (sw.BaseStream.CanWrite)
                    {
                        // Vital to activate Anaconda
                        sw.WriteLine("C:\\Users\\Admin\\anaconda3\\Scripts\\activate.bat");
                        // Activate environment
                        sw.WriteLine("conda activate pytorch");
                        // running python script.
                        //ConsoleExtension.WriteWithColor($"File name for server is {fileName}", ConsoleColor.Green);
                        sw.WriteLine($"python {fileName}.py");
                    }
                }


                ConsoleExtension.WriteWithColor("Api Started successfully", ConsoleColor.Yellow);
                return ("Api Running", true);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while starting Detection API {ex.Message}");
                return ("Api not running", false);
            }
        }
    }
}
