using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ProcessamentoImagens
{
    class Filtros
    {
        //sem acesso direto a memoria
        public static void convert_to_gray(Bitmap imageBitmapSrc, Bitmap imageBitmapDest)
        {
            int width = imageBitmapSrc.Width;
            int height = imageBitmapSrc.Height;
            int r, g, b;
            Int32 gs;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    //obtendo a cor do pixel
                    Color cor = imageBitmapSrc.GetPixel(x, y);

                    r = cor.R;
                    g = cor.G;
                    b = cor.B;
                    gs = (Int32)(r * 0.2990 + g * 0.5870 + b * 0.1140);

                    //nova cor
                    Color newcolor = Color.FromArgb(gs, gs, gs);

                    imageBitmapDest.SetPixel(x, y, newcolor);
                }
            }
        }

        //sem acesso direito a memoria
        public static void negativo(Bitmap imageBitmapSrc, Bitmap imageBitmapDest)
        {
            int width = imageBitmapSrc.Width;
            int height = imageBitmapSrc.Height;
            int r, g, b;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    //obtendo a cor do pixel
                    Color cor = imageBitmapSrc.GetPixel(x, y);

                    r = cor.R;
                    g = cor.G;
                    b = cor.B;

                    //nova cor
                    Color newcolor = Color.FromArgb(255 - r, 255 - g, 255 - b);

                    imageBitmapDest.SetPixel(x, y, newcolor);
                }
            }
        }

        //com acesso direto a memória
        public static void convert_to_grayDMA(Bitmap imageBitmapSrc, Bitmap imageBitmapDest)
        {
            int width = imageBitmapSrc.Width;
            int height = imageBitmapSrc.Height;
            int pixelSize = 3;
            Int32 gs;

            //lock dados bitmap origem
            BitmapData bitmapDataSrc = imageBitmapSrc.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            //lock dados bitmap destino
            BitmapData bitmapDataDst = imageBitmapDest.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int padding = bitmapDataSrc.Stride - (width * pixelSize);

            unsafe
            {
                byte* src = (byte*)bitmapDataSrc.Scan0.ToPointer();
                byte* dst = (byte*)bitmapDataDst.Scan0.ToPointer();

                int r, g, b;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        b = *(src++); //está armazenado dessa forma: b g r 
                        g = *(src++);
                        r = *(src++);
                        gs = (Int32)(r * 0.2990 + g * 0.5870 + b * 0.1140);
                        *(dst++) = (byte)gs;
                        *(dst++) = (byte)gs;
                        *(dst++) = (byte)gs;
                    }
                    src += padding;
                    dst += padding;
                }
            }
            //unlock imagem origem
            imageBitmapSrc.UnlockBits(bitmapDataSrc);
            //unlock imagem destino
            imageBitmapDest.UnlockBits(bitmapDataDst);
        }

        //com acesso direito a memoria
        public static void negativoDMA(Bitmap imageBitmapSrc, Bitmap imageBitmapDest)
        {
            int width = imageBitmapSrc.Width;
            int height = imageBitmapSrc.Height;
            int pixelSize = 3;

            //lock dados bitmap origem 
            BitmapData bitmapDataSrc = imageBitmapSrc.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            //lock dados bitmap destino
            BitmapData bitmapDataDst = imageBitmapDest.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int padding = bitmapDataSrc.Stride - (width * pixelSize);

            unsafe
            {
                byte* src1 = (byte*)bitmapDataSrc.Scan0.ToPointer();
                byte* dst = (byte*)bitmapDataDst.Scan0.ToPointer();

                int r, g, b;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        b = *(src1++); //está armazenado dessa forma: b g r 
                        g = *(src1++);
                        r = *(src1++);

                        *(dst++) = (byte)(255 - b);
                        *(dst++) = (byte)(255 - g);
                        *(dst++) = (byte)(255 - r);
                    }
                    src1 += padding;
                    dst += padding;
                }
            }
            //unlock imagem origem 
            imageBitmapSrc.UnlockBits(bitmapDataSrc);
            //unlock imagem destino
            imageBitmapDest.UnlockBits(bitmapDataDst);
        }

        public static void afinamentoDMA(Bitmap imageBitmapSrc, Bitmap imgBitmapDest)
        {
            int width = imageBitmapSrc.Width;
            int height = imageBitmapSrc.Height;
            int pixelSize = 3; // 3 bytes por pixel (B, G, R)

            // ETAPA 1: ESTRUTURA BÁSICA DE ACESSO DIRETO
            BitmapData bitmapDataSrc = imageBitmapSrc.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bitmapDataDst = imgBitmapDest.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bitmapDataSrc.Stride;

            unsafe
            {
                byte* src = (byte*)bitmapDataSrc.Scan0.ToPointer();
                byte* dst = (byte*)bitmapDataDst.Scan0.ToPointer();

                // ETAPA 2: CRIAR UMA MATRIZ BINÁRIA PARA A LÓGICA
                int[,] matrizBinaria = new int[width, height];
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        byte b = src[y * stride + x * pixelSize];
                        matrizBinaria[x, y] = (b < 128) ? 1 : 0; // 1 para preto, 0 para branco
                    }
                }

                // ETAPA 3: LOOP PRINCIPAL DO ALGORITMO ZHANG-SUEN
                bool mudancaOcorreu = true;
                while (mudancaOcorreu) 
                {
                    mudancaOcorreu = false;
                    List<Point> pixelsParaApagar = new List<Point>();

                    // --- PRIMEIRA SUB-ITERAÇÃO ---
                    for (int y = 1; y < height - 1; y++)
                    {
                        for (int x = 1; x < width - 1; x++)
                        {
                            if (matrizBinaria[x, y] == 1)
                            {
                                int P2 = matrizBinaria[x, y - 1]; 
                                int P3 = matrizBinaria[x + 1, y - 1]; 
                                int P4 = matrizBinaria[x + 1, y];
                                int P5 = matrizBinaria[x + 1, y + 1]; 
                                int P6 = matrizBinaria[x, y + 1]; 
                                int P7 = matrizBinaria[x - 1, y + 1];
                                int P8 = matrizBinaria[x - 1, y]; 
                                int P9 = matrizBinaria[x - 1, y - 1];

                                // Condição 1: Conectividade
                                bool condicao1 = false;
                                int conectividade = 0;
                                int[] vizinhos = { P2, P3, P4, P5, P6, P7, P8, P9, P2 };
                                for (int i = 1; i < vizinhos.Length; i++)
                                {
                                    int atual = vizinhos[i];
                                    int anterior = vizinhos[i - 1];
                                    if (anterior < atual) //significa que houve uma transição de 0 para 1
                                        conectividade++;
                                }
                                if (conectividade == 1)
                                    condicao1 = true;

                                // Condição 2: Número de vizinhos pretos
                                bool condicao2 = false;
                                int vizinhosPretos = P2 + P3 + P4 + P5 + P6 + P7 + P8 + P9;
                                if(vizinhosPretos >= 2 && vizinhosPretos <= 6)
                                    condicao2 = true;

                                // CORREÇÃO: Condições 3 e 4 para a primeira sub-iteração
                                bool condicao3 = false;
                                bool condicao4 = false;
                                
                                if(P2 * P4 * P8 == 0)
                                    condicao3 = true;

                                if(P2 * P6 * P8 == 0)
                                    condicao4 = true;

                                if (condicao1 && condicao2 && condicao3 && condicao4)
                                {
                                    pixelsParaApagar.Add(new Point(x, y));
                                }
                            }
                        }
                    }

                    if (pixelsParaApagar.Count > 0)
                    {
                        mudancaOcorreu = true;
                        foreach (Point p in pixelsParaApagar) { matrizBinaria[p.X, p.Y] = 0; }
                        pixelsParaApagar.Clear();
                    }

                    // --- SEGUNDA SUB-ITERAÇÃO ---
                    for (int y = 1; y < height - 1; y++)
                    {
                        for (int x = 1; x < width - 1; x++)
                        {
                            if (matrizBinaria[x, y] == 1)
                            {
                                int P2 = matrizBinaria[x, y - 1];
                                int P3 = matrizBinaria[x + 1, y - 1];
                                int P4 = matrizBinaria[x + 1, y];
                                int P5 = matrizBinaria[x + 1, y + 1];
                                int P6 = matrizBinaria[x, y + 1];
                                int P7 = matrizBinaria[x - 1, y + 1];
                                int P8 = matrizBinaria[x - 1, y];
                                int P9 = matrizBinaria[x - 1, y - 1];

                                // Condição 1: Conectividade
                                bool condicao1 = false;
                                int conectividade = 0;
                                int[] vizinhos = { P2, P3, P4, P5, P6, P7, P8, P9, P2 };
                                for (int i = 1; i < vizinhos.Length; i++)
                                {
                                    int atual = vizinhos[i];
                                    int anterior = vizinhos[i - 1];
                                    if (anterior < atual) //significa que houve uma transição de 0 para 1
                                        conectividade++;
                                }
                                if (conectividade == 1)
                                    condicao1 = true;

                                // Condição 2: Número de vizinhos pretos
                                bool condicao2 = false;
                                int vizinhosPretos = P2 + P3 + P4 + P5 + P6 + P7 + P8 + P9;
                                if (vizinhosPretos >= 2 && vizinhosPretos <= 6)
                                    condicao2 = true;

                                // CORREÇÃO: Condições 3 e 4 para a primeira sub-iteração
                                bool condicao3 = false;
                                bool condicao4 = false;

                                if (P2 * P4 * P6 == 0) 
                                    condicao3 = true;

                                if (P4 * P6 * P8 == 0) 
                                    condicao4 = true;

                                if (condicao1 && condicao2 && condicao3 && condicao4)
                                {
                                    pixelsParaApagar.Add(new Point(x, y));
                                }
                            }
                        }
                    }

                    if (pixelsParaApagar.Count > 0)
                    {
                        mudancaOcorreu = true;
                        foreach (Point p in pixelsParaApagar) { matrizBinaria[p.X, p.Y] = 0; }
                        pixelsParaApagar.Clear();
                    }
                }

                // ETAPA 4: DESENHAR O RESULTADO FINAL NA IMAGEM DE DESTINO
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        byte cor = (byte)(matrizBinaria[x, y] == 1 ? 0 : 255);
                        int pos = y * stride + x * pixelSize;
                        dst[pos] = cor;     // Blue
                        dst[pos + 1] = cor; // Green
                        dst[pos + 2] = cor; // Red
                    }
                }
            } // Fim do bloco unsafe

            // ETAPA 5: LIBERAR AS IMAGENS
            imageBitmapSrc.UnlockBits(bitmapDataSrc);
            imgBitmapDest.UnlockBits(bitmapDataDst);
        }
    }
}

