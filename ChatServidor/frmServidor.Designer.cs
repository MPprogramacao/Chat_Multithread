
namespace ChatServidor
{
    partial class frmServidor
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblEnderecoIP = new System.Windows.Forms.Label();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.btnAtender = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblEnderecoIP
            // 
            this.lblEnderecoIP.AutoSize = true;
            this.lblEnderecoIP.Location = new System.Drawing.Point(12, 27);
            this.lblEnderecoIP.Name = "lblEnderecoIP";
            this.lblEnderecoIP.Size = new System.Drawing.Size(69, 13);
            this.lblEnderecoIP.TabIndex = 0;
            this.lblEnderecoIP.Text = "Endereço IP:";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(87, 24);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(182, 20);
            this.txtIP.TabIndex = 1;
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(15, 50);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(362, 233);
            this.txtLog.TabIndex = 3;
            // 
            // btnAtender
            // 
            this.btnAtender.Location = new System.Drawing.Point(275, 22);
            this.btnAtender.Name = "btnAtender";
            this.btnAtender.Size = new System.Drawing.Size(102, 23);
            this.btnAtender.TabIndex = 4;
            this.btnAtender.Text = "Iniciar Atendimeto";
            this.btnAtender.UseVisualStyleBackColor = true;
            this.btnAtender.Click += new System.EventHandler(this.btnAtender_Click);
            // 
            // frmServidor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 295);
            this.Controls.Add(this.btnAtender);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.lblEnderecoIP);
            this.Name = "frmServidor";
            this.Text = "Chat Servidor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblEnderecoIP;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btnAtender;
    }
}

