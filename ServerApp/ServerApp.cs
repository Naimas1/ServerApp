using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerApp
{
    public partial class ServerForm : Form
    {
        public ServerForm(TcpListener server)
        {
            this.server = server;
        }

        public ServerForm(TcpClient client)
        {
            this.client = client;
        }

        public ServerForm(string[] responses)
        {
            this.responses = responses;
        }

        public ServerForm()
        {
            InitializeComponent();
            Task.Run(() =>
            {
                return StartServer();
            });
        }

        public override bool Equals(object? obj)
        {
            return obj is ServerForm form &&
                   EqualityComparer<TcpListener>.Default.Equals(this.server, form.server);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.server);
        }
    }
}
