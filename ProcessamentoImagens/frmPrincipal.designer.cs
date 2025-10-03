namespace ProcessamentoImagens
{
    partial class frmPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrincipal));
            this.pictBoxImg1 = new System.Windows.Forms.PictureBox();
            this.pictBoxImg2 = new System.Windows.Forms.PictureBox();
            this.btnAbrirImagem = new System.Windows.Forms.Button();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnAfinamentoComDMA = new System.Windows.Forms.Button();
            this.btnContornoComDMA = new System.Windows.Forms.Button();
            this.btnRetMinComDMA = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictBoxImg1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictBoxImg2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictBoxImg1
            // 
            this.pictBoxImg1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pictBoxImg1.Location = new System.Drawing.Point(12, 10);
            this.pictBoxImg1.Name = "pictBoxImg1";
            this.pictBoxImg1.Size = new System.Drawing.Size(918, 614);
            this.pictBoxImg1.TabIndex = 102;
            this.pictBoxImg1.TabStop = false;
            // 
            // pictBoxImg2
            // 
            this.pictBoxImg2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pictBoxImg2.Location = new System.Drawing.Point(952, 10);
            this.pictBoxImg2.Name = "pictBoxImg2";
            this.pictBoxImg2.Size = new System.Drawing.Size(918, 614);
            this.pictBoxImg2.TabIndex = 105;
            this.pictBoxImg2.TabStop = false;
            // 
            // btnAbrirImagem
            // 
            this.btnAbrirImagem.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAbrirImagem.Location = new System.Drawing.Point(13, 630);
            this.btnAbrirImagem.Name = "btnAbrirImagem";
            this.btnAbrirImagem.Size = new System.Drawing.Size(134, 42);
            this.btnAbrirImagem.TabIndex = 106;
            this.btnAbrirImagem.Text = "Abrir Imagem";
            this.btnAbrirImagem.UseVisualStyleBackColor = false;
            this.btnAbrirImagem.Click += new System.EventHandler(this.btnAbrirImagem_Click);
            // 
            // btnLimpar
            // 
            this.btnLimpar.Location = new System.Drawing.Point(13, 677);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(134, 42);
            this.btnLimpar.TabIndex = 107;
            this.btnLimpar.Text = "Limpar";
            this.btnLimpar.UseVisualStyleBackColor = true;
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
            // 
            // btnAfinamentoComDMA
            // 
            this.btnAfinamentoComDMA.Location = new System.Drawing.Point(390, 630);
            this.btnAfinamentoComDMA.Name = "btnAfinamentoComDMA";
            this.btnAfinamentoComDMA.Size = new System.Drawing.Size(277, 42);
            this.btnAfinamentoComDMA.TabIndex = 112;
            this.btnAfinamentoComDMA.Text = "Afinamento de Zhang Suen Com DMA";
            this.btnAfinamentoComDMA.UseVisualStyleBackColor = true;
            this.btnAfinamentoComDMA.Click += new System.EventHandler(this.btnAfinamentoComDMA_Click);
            // 
            // btnContornoComDMA
            // 
            this.btnContornoComDMA.Location = new System.Drawing.Point(674, 630);
            this.btnContornoComDMA.Name = "btnContornoComDMA";
            this.btnContornoComDMA.Size = new System.Drawing.Size(277, 42);
            this.btnContornoComDMA.TabIndex = 113;
            this.btnContornoComDMA.Text = "Extração de Contornos Com DMA";
            this.btnContornoComDMA.UseVisualStyleBackColor = true;
            this.btnContornoComDMA.Click += new System.EventHandler(this.btnExtracaoContornosComDMA_Click);
            // 
            // btnRetMinComDMA
            // 
            this.btnRetMinComDMA.Location = new System.Drawing.Point(957, 630);
            this.btnRetMinComDMA.Name = "btnRetMinComDMA";
            this.btnRetMinComDMA.Size = new System.Drawing.Size(277, 42);
            this.btnRetMinComDMA.TabIndex = 114;
            this.btnRetMinComDMA.Text = "Calcular o Retângulo Minimo Com DMA";
            this.btnRetMinComDMA.UseVisualStyleBackColor = true;
            this.btnRetMinComDMA.Click += new System.EventHandler(this.btnRetMinComDMA_Click);
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1882, 753);
            this.Controls.Add(this.btnRetMinComDMA);
            this.Controls.Add(this.btnContornoComDMA);
            this.Controls.Add(this.btnAfinamentoComDMA);
            this.Controls.Add(this.btnLimpar);
            this.Controls.Add(this.btnAbrirImagem);
            this.Controls.Add(this.pictBoxImg2);
            this.Controls.Add(this.pictBoxImg1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Trabalho Bimestral - TTC";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.frmPrincipal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictBoxImg1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictBoxImg2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictBoxImg1;
        private System.Windows.Forms.PictureBox pictBoxImg2;
        private System.Windows.Forms.Button btnAbrirImagem;
        private System.Windows.Forms.Button btnLimpar;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button btnAfinamentoComDMA;
        private System.Windows.Forms.Button btnContornoComDMA;
        private System.Windows.Forms.Button btnRetMinComDMA;
    }
}

