using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace ChatServidor
{
    public partial class frmServidor : Form
    {
        public frmServidor()
        {
            InitializeComponent();
        }

        private delegate void AtualizaStatusCallback(string strMensagem);

        private void btnAtender_Click(object sender, System.EventArgs e)
        {
            //Analisa o endereço IP do servidor informando o TextBox
            IPAddress enderecoIP = IPAddress.Parse(txtIP.Text);

            //Criar uma nova instacia do objeto ChatServidor
            ChatServidor mainServidor = new ChatServidor(enderecoIP);

            //Vincula o tratamento de evento StatusChanged a mainServer_StatusChanged
            ChatServidor.StatusChanged += new StatusChangedEventHandler(mainServidor_StatusChanged);


            // Inicia o atendimento das conexões
            mainServidor.IniciaAtendimento();

            //Mostra que nos iniciamos o atendimento para conexao
            txtLog.AppendText("Monitorando as conexões...\r\n");
        }

        public void mainServidor_StatusChanged(object sender, StatusChangedEventArgs e)
        {
            //Chama o método que atualiza o formulário
            this.Invoke(new AtualizaStatusCallback(this.AtualizaStatus), new object[] { e.EventMessage });
        }

        private void AtualizaStatus(string strMensagem)
        {
            //Atualiza o log com mensagens
            txtLog.AppendText(strMensagem + "\r\n");

        }
    }
}
