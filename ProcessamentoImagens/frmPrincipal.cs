using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace ProcessamentoImagens
{
    public partial class frmPrincipal : Form
    {
        private Image image;
        private Bitmap imageBitmap;
        private Bitmap processedImage;

        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void btnAbrirImagem_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = "";
            openFileDialog.Filter = "Arquivos de Imagem (*.jpg;*.gif;*.bmp;*.png)|*.jpg;*.gif;*.bmp;*.png";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                image = Image.FromFile(openFileDialog.FileName);
                pictBoxImg1.Image = image;
                pictBoxImg1.SizeMode = PictureBoxSizeMode.Normal;
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            pictBoxImg1.Image = null;
            pictBoxImg2.Image = null;
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {

        }

        private void btnAfinamentoComDMA_Click(object sender, EventArgs e)
        {
            if(image == null)
            {
                MessageBox.Show("Abra uma imagem primeiro.");
            }
            else {
                Bitmap imgDest = new Bitmap(image);
                imageBitmap = (Bitmap)image;
                Filtros.afinamentoDMA(imageBitmap, imgDest);
                pictBoxImg2.Image = imgDest;
                this.processedImage = imgDest;
            }
        }

        private void btnExtracaoContornosComDMA_Click(object sender, EventArgs e)
        {
            if(this.processedImage == null)
            {
                MessageBox.Show("Realize o afinamento primeiro.");
            }
            else
            {
                Bitmap imgDest = new Bitmap(image);
                imageBitmap = (Bitmap)this.processedImage;
                Filtros.extracaoDMA(imageBitmap, imgDest);
                pictBoxImg2.Image = imgDest;
            }
        }
    }
}
