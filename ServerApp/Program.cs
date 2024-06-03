using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerApp
{
    public partial class ServerForm : Form
    {
        private TcpListener server;
        private TcpClient client;
        private NetworkStream stream;
        private byte[] buffer = new byte[1024];
        private Random random = new Random();
        private string[] responses = { "Hello!", "How can I help you?", "Goodbye!", "Nice to meet you!" };

        public bool InvokeRequired { get; private set; }

        public ServerForm(string[] responses)
        {
            InitializeComponent();
            Task.Run(() => StartServer());
            this.responses = responses;
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }

        private async void StartServer()
        {
            server = new TcpListener(IPAddress.Any, 8888);
            server.Start();
            AppendText("Server started...");

            client = await server.AcceptTcpClientAsync();
            stream = client.GetStream();
            AppendText("Client connected...");

            while (true)
            {
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                AppendText("Client: " + message);

                if (message == "Bye")
                {
                    break;
                }

                string response = responses[random.Next(responses.Length)];
                byte[] responseBytes = Encoding.ASCII.GetBytes(response);
                await stream.WriteAsync(responseBytes, 0, responseBytes.Length);
                AppendText("Server: " + response);
            }

            stream.Close();
            client.Close();
            server.Stop();
            AppendText("Connection closed...");
        }

        private void AppendText(string text)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(AppendText), new object[] { text });
            }
            else
            {
                textBox1.AppendText(text + Environment.NewLine);
            }
        }

        private void Invoke(Action<string> action, object[] objects)
        {
            throw new NotImplementedException();
        }
    }

    internal class textBox1
    {
        internal static void AppendText(string v)
        {
            throw new NotImplementedException();
        }
    }

    public class Form
    {
        private TcpListener server;

        private TcpClient client;

        private NetworkStream stream;

        private byte[] buffer = new byte[1024];

        private Random random = new Random();

        private string[] responses = { "Hello!", "How can I help you?", "Goodbye!", "Nice to meet you!" };

        private void AppendText(string text)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(AppendText), new object[] { text });
            }
            else
            {
                textBox1.AppendText(text + Environment.NewLine);
            }
        }

        private async void StartServer()
        {
            server = new TcpListener(IPAddress.Any, 8888);
            server.Start();
            AppendText("Server started...");

            client = await server.AcceptTcpClientAsync();
            stream = client.GetStream();
            AppendText("Client connected...");

            while (true)
            {
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                AppendText("Client: " + message);

                if (message == "Bye")
                {
                    break;
                }

                string response = responses[random.Next(responses.Length)];
                byte[] responseBytes = Encoding.ASCII.GetBytes(response);
                await stream.WriteAsync(responseBytes, 0, responseBytes.Length);
                AppendText("Server: " + response);
            }

            stream.Close();
            client.Close();
            server.Stop();
            AppendText("Connection closed...");
        }
    }
}
