using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging; 
using System.Windows.Forms;

namespace ProcessamentoImagens
{
    public partial class frmPrincipal : Form
    {
        private Image image; //img original
        private Bitmap processedImage; //guarda resultados das imagens processadas
        private bool podeMinRet = false;
        private List<List<(int x, int y)>> todosOsPontosDoContorno;

        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            //este metodo pode ficar vazio
        }

        private void btnAbrirImagem_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = "";
            openFileDialog.Filter = "Arquivos de Imagem (*.jpg;*.gif;*.bmp;*.png)|*.jpg;*.gif;*.bmp;*.png";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictBoxImg2.Image = null;
                this.processedImage = null;
                this.podeMinRet = false; 
                image = Image.FromFile(openFileDialog.FileName);
                pictBoxImg1.Image = image;
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            pictBoxImg1.Image = null;
            pictBoxImg2.Image = null;
            this.image = null;
            this.processedImage = null;
            this.podeMinRet = false;
        }

        private void btnAfinamentoComDMA_Click(object sender, EventArgs e)
        {
            if (image == null)
            {
                MessageBox.Show("Abra uma imagem primeiro.");
            }
            else
            {
                Bitmap imgDestAfinamento = new Bitmap(image.Width, image.Height);
                Filtros.AfinamentoDMA((Bitmap)image, imgDestAfinamento);
                pictBoxImg2.Image = imgDestAfinamento;
                this.processedImage = imgDestAfinamento;
            }
        }

        private void btnExtracaoContornosComDMA_Click(object sender, EventArgs e)
        {
            if (this.processedImage == null)
            {
                MessageBox.Show("Execute o 'Afinamento' primeiro.");
            }
            else
            {
                Bitmap imgResultado = new Bitmap(this.processedImage.Width, this.processedImage.Height, PixelFormat.Format24bppRgb);
                pictBoxImg2.Image = null;

                this.todosOsPontosDoContorno = Filtros.ContornoDMA(this.processedImage, imgResultado);

                pictBoxImg2.Image = imgResultado;
                podeMinRet = true;
            }
        }

        private void btnRetMinComDMA_Click(object sender, EventArgs e)
        {
            if (!podeMinRet || this.todosOsPontosDoContorno == null || this.todosOsPontosDoContorno.Count == 0)
            {
                MessageBox.Show("Execute o 'Afinamento' e a 'Extração de Contornos' primeiro.");
            }
            else
            {
                Bitmap imagemComRetangulos = new Bitmap(pictBoxImg2.Image);

                using (Graphics g = Graphics.FromImage(imagemComRetangulos))
                {
                    using (Pen canetaVermelha = new Pen(Color.Green, 1))
                    {
                        foreach (var contorno in this.todosOsPontosDoContorno)
                        {
                            PointF[] retanguloMinimo = Filtros.EncontrarRetanguloMinimo(contorno);

                            if (retanguloMinimo != null)
                            {
                                g.DrawPolygon(canetaVermelha, retanguloMinimo);
                            }
                        }
                    }
                }
                pictBoxImg2.Image = imagemComRetangulos;
            }
        }
    }
}