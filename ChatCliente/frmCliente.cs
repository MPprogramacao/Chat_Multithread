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
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace ChatCliente
{
    public partial class frmCliente : Form
    {
        public frmCliente()
        {
            Application.ApplicationExit += new EventHandler(OnAppLIcationExit);
            InitializeComponent();            
        }

        //Trata o nome do Usuario
        private string NomeUsuario = "Desconhecido";
        private StreamWriter stwEnviador;
        private StreamReader strRecepitor;
        private TcpClient tcpServidor;

        //Nescessário para autalizar o formulário com mensagens de outras thread
        private delegate void AtualizarLogCallBack(string strMensagem);

        //Necessário para definir formulário para o estado "disconnected" de outra thread
        private delegate void FecharConexaoCallBack(string strMorivo);
        private Thread mensagemThead;
        private IPAddress enderecaoIP;
        private bool Conectado;

        private void btnConectar_Click(object sender, EventArgs e)
        {
            //se não esta conectando aguardar conexao
            if (Conectado == false)
            {
                //Incializa conexão
                InicializaConexao();
            }
            else // Se esta conectado então desconecta
            {
                FechaConexao("Desconectado a pedido do usuário");
            }
        }

        private void InicializaConexao()
        {
            //Trata o endereço IP informado em um objeto IPAndress
            enderecaoIP = IPAddress.Parse(txtServidorIP.Text);
            //Inica uma nova conexão TCP com o servidor chat
            tcpServidor = new TcpClient();
            tcpServidor.Connect(enderecaoIP, 2502);

            //Ajuda a verifcar se estamos conectados ou não
            Conectado = true;

            //Prepara o formulário
            NomeUsuario = txtUsuario.Text;

            //Desabilita e habilita os campos apropiados
            txtServidorIP.Enabled = false;
            txtUsuario.Enabled = false;
            txtMensagem.Enabled = true;
            btnEnviar.Enabled = true;
            btnConectar.Text = "Desconectado";

            //Envia o nome do usuário ao Servidor
            stwEnviador = new StreamWriter(tcpServidor.GetStream());
            stwEnviador.WriteLine(txtUsuario.Text);
            stwEnviador.Flush();

            //Inicia o thread para receber mensagens e nova comunicação
            mensagemThead = new Thread(new ThreadStart(RecebeMensagens));
            mensagemThead.Start();
        }

        private void RecebeMensagens()
        {
            //receber a resposta do servidor
            strRecepitor = new StreamReader(tcpServidor.GetStream());
            string ConResposta = strRecepitor.ReadLine();
            //Se o primeiro caracter da resposta é 1 a conexão foi com sucesso
            if (ConResposta[0] == '1')
            {
                //Atualiza o formulário para informar que esta conectado
                this.Invoke(new AtualizarLogCallBack(this.AtualizaLog), new object[] { "Conectado com sucesso" });
            }
            else //Se o primeiro caracter não for 1 a conexão falhou
            {
                string Motivo = "Não Conectado: ";
                //Extrai o motivo da mensagem resposta. O motivo começa no 3º caracter
                Motivo += ConResposta.Substring(2, ConResposta.Length - 2);
                //Atualiza o formulário como o motivo da falha da conexão
                this.Invoke(new FecharConexaoCallBack(this.FechaConexao), new object[] { Motivo });
                //Sai do método
                return;
            }

            //Enquanto estiver conectado le as linhas que estão chegando do servidor
            while (Conectado)
            {
                //Exibe mensagems no TextBox
                this.Invoke(new AtualizarLogCallBack(this.AtualizaLog), new object[] { strRecepitor.ReadLine() });
            }
        }

        private void AtualizaLog(string strMensagem)
        {
            //Anexa o texto ao final de cada linha
            txtLog.AppendText(strMensagem + "\r\n");
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            EnviaMensagem();
        }

        private void txtMensagem_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Se pressionou a tecla Enter
            if(e.KeyChar == (char)13)
            {
                EnviaMensagem();
            }
        }

        private void EnviaMensagem()
        {
            if(txtMensagem.Lines.Length >= 1)
            {
                //escreve a mensagem na caixa de texto
                stwEnviador.WriteLine(txtMensagem.Text);
                stwEnviador.Flush();
                txtMensagem.Lines = null;
            }
            txtMensagem.Text = "";
        }

        //Fechar conexão do servidor
        private void FechaConexao(string Motivo)
        {
            //Mostra o motivo porque a conexão encerrou
            txtLog.AppendText(Motivo + "\r\n");
            //Habilita e desabilita os contrutores apropiados no formulario
            txtServidorIP.Enabled = true;
            txtUsuario.Enabled = true;
            txtMensagem.Enabled = false;
            btnEnviar.Enabled = false;
            btnConectar.Text = "Conectado";

            //Fechar os objetos
            Conectado = false;
            stwEnviador.Close();
            stwEnviador.Close();
            tcpServidor.Close();
        }

        //Tratar de evento ao sair da aplicação
        public void OnAppLIcationExit(object sender, EventArgs e)
        {
            if(Conectado == true)
            {
                //Fechar as conexao, streams, etc..
                Conectado = false;
                stwEnviador.Close();
                strRecepitor.Close();
                tcpServidor.Close();
            }
        }
    }
}
