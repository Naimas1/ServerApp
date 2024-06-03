using ClientApp;
using ServerApp;
using System.Net.Sockets;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace ClientApp
{
    public partial class ClientForm : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        private byte[] buffer = new byte[1024];
        private Random random = new Random();
        private string[] responses = { "Hi!", "What do you need?", "See you!", "Pleased to meet you!" };

        public object textBoxMessage { get; private set; }
        public bool InvokeRequired { get; private set; }

        public ClientForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }

        private async void buttonConnect_Click(object sender, EventArgs e)
        {
            string ipAddress = textBoxIpAddress.Text;
            int port = int.Parse(textBoxPort.GetText());

            client = new TcpClient();
            await client.ConnectAsync(ipAddress, port);
            stream = client.GetStream();
            AppendText("Connected to server...");

            Task.Run(() => ReceiveMessages());
        }

        private async void buttonSend_Click(object sender, EventArgs e)
        {
            string message = textBoxMessage.Text;
            byte[] messageBytes = Encoding.ASCII.GetBytes(message);
            await stream.WriteAsync(messageBytes, 0, messageBytes.Length);
            AppendText("Client: " + message);

            if (message == "Bye")
            {
                stream.Close();
                client.Close();
                AppendText("Connection closed...");
            }
        }

        private async void ReceiveMessages()
        {
            while (true)
            {
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                AppendText("Server: " + message);

                if (message == "Bye")
                {
                    break;
                }

                string response = responses[random.Next(responses.Length)];
                byte[] responseBytes = Encoding.ASCII.GetBytes(response);
                await stream.WriteAsync(responseBytes, 0, responseBytes.Length);
                AppendText("Client: " + response);
            }

            stream.Close();
            client.Close();
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

    internal class textBoxPort
    {
        private static ReadOnlySpan<byte> text1;

        public static ReadOnlySpan<byte> Text { get => text; set => text = value; }
        public static ReadOnlySpan<byte> text { get => Text1; private set => Text1 = value; }
        public static ReadOnlySpan<byte> Text1 { get => text1; set => text1 = value; }
        public static ReadOnlySpan<byte> Text11 { get => text1; set => text1 = value; }

        public static ReadOnlySpan<byte> GetText()
        {
            return text;
        }

        internal static void SetText(ReadOnlySpan<byte> value)
        {
            text = value;
        }
    }

    internal class textBoxIpAddress
    {
        public static string Text { get; internal set; }
    }
}
static class Program
{
   
