using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace ProcessamentoImagens
{
    class Filtros
    {
        public static void AfinamentoDMA(Bitmap imageBitmapSrc, Bitmap imgBitmapDest)
        {
            int width = imageBitmapSrc.Width;
            int height = imageBitmapSrc.Height;
            int pixelSize = 3;
            byte cor;

            BitmapData bitmapDataSrc = imageBitmapSrc.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bitmapDataDst = imgBitmapDest.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bitmapDataSrc.Stride;

            unsafe
            {
                byte* src = (byte*)bitmapDataSrc.Scan0.ToPointer();
                byte* dst = (byte*)bitmapDataDst.Scan0.ToPointer();

                int[,] matrizBinaria = new int[width, height];
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        byte b = src[y * stride + x * pixelSize]; //pega a posicao do pixel
                        if (b < 128)
                            matrizBinaria[x, y] = 1; //preto
                        else
                            matrizBinaria[x, y] = 0; //branco
                    }
                }

                bool mudancaOcorreu = true;
                while (mudancaOcorreu)
                {
                    mudancaOcorreu = false;
                    List<Point> pixelsParaApagar = new List<Point>();

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

                                bool condicao2 = false;
                                int vizinhosPretos = P2 + P3 + P4 + P5 + P6 + P7 + P8 + P9;
                                if (vizinhosPretos >= 2 && vizinhosPretos <= 6)
                                    condicao2 = true;

                                bool condicao3 = false;
                                bool condicao4 = false;

                                if (P2 * P4 * P8 == 0)
                                    condicao3 = true;

                                if (P2 * P6 * P8 == 0)
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
                        foreach (Point p in pixelsParaApagar) { 
                            matrizBinaria[p.X, p.Y] = 0; 
                        }
                        pixelsParaApagar.Clear();
                    }

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

                                bool condicao2 = false;
                                int vizinhosPretos = P2 + P3 + P4 + P5 + P6 + P7 + P8 + P9;
                                if (vizinhosPretos >= 2 && vizinhosPretos <= 6)
                                    condicao2 = true;

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
                        foreach (Point p in pixelsParaApagar) { 
                            matrizBinaria[p.X, p.Y] = 0; 
                        }
                        pixelsParaApagar.Clear();
                    }
                }

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        if (matrizBinaria[x, y] == 1)
                            cor = 0; //preto
                        else
                            cor = 255; //branco
                        int pos = y * stride + x * pixelSize;
                        dst[pos] = cor;     // B
                        dst[pos + 1] = cor; // G
                        dst[pos + 2] = cor; // R
                    }
                }
            } 

            imageBitmapSrc.UnlockBits(bitmapDataSrc);
            imgBitmapDest.UnlockBits(bitmapDataDst);
        }

        public static List<List<(int x, int y)>> ContornoDMA(Bitmap img, Bitmap dest)
        {
            int width = img.Width;
            int height = img.Height;
            int pixelSize = 3;

            BitmapData srcBMD = img.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData destBMD = dest.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            List<List<(int x, int y)>> todosOsContornos = new List<List<(int x, int y)>>();
            bool[,] visitados = new bool[width, height];

            unsafe
            {
                byte* src = (byte*)srcBMD.Scan0.ToPointer();
                byte* destP = (byte*)destBMD.Scan0.ToPointer();

                for (int y = 1; y < height - 1; y++)
                {
                    for (int x = 1; x < width - 1; x++)
                    {
                        byte* pAtual = src + y * srcBMD.Stride + x * pixelSize;
                        int tomAtual = (*(pAtual) + *(pAtual + 1) + *(pAtual + 2)) / 3;

                        byte* pProximo = pAtual + pixelSize;
                        int tomProximo = (*(pProximo) + *(pProximo + 1) + *(pProximo + 2)) / 3;

                        // transicao de branco -> preto
                        if (tomAtual > 127 && tomProximo < 127 && !visitados[x, y])
                        {
                            List<(int x, int y)> contornoAtual = new List<(int x, int y)>();
                            Queue<(int x, int y)> fila = new Queue<(int x, int y)>();

                            fila.Enqueue((x, y));
                            visitados[x, y] = true;

                            // ceguinho começa aqui
                            while (fila.Count > 0)
                            {
                                var ponto = fila.Dequeue();
                                contornoAtual.Add(ponto);

                                // vizinhos
                                (int, int)[] vizinhosCoords = {
                                    (ponto.x + 1, ponto.y),
                                    (ponto.x + 1, ponto.y - 1),
                                    (ponto.x, ponto.y - 1),
                                    (ponto.x - 1, ponto.y - 1),
                                    (ponto.x - 1, ponto.y),
                                    (ponto.x - 1, ponto.y + 1),
                                    (ponto.x, ponto.y + 1),
                                    (ponto.x + 1, ponto.y + 1)
                                };

                                // sentido horario na iteracao
                                for (int i = 0; i < 8; i++)
                                {
                                    var vizinho = vizinhosCoords[i];
                                    var proximoVizinho = vizinhosCoords[(i + 1) % 8];

                                    byte* pVizinho = src + vizinho.Item2 * srcBMD.Stride + vizinho.Item1 * pixelSize;
                                    int tomVizinho = (*(pVizinho) + *(pVizinho + 1) + *(pVizinho + 2)) / 3;

                                    byte* pProximoVizinho = src + proximoVizinho.Item2 * srcBMD.Stride + proximoVizinho.Item1 * pixelSize;
                                    int tomProximoV = (*(pProximoVizinho) + *(pProximoVizinho + 1) + *(pProximoVizinho + 2)) / 3;

                                    if (tomVizinho > 127 && tomProximoV < 127 && !visitados[vizinho.Item1, vizinho.Item2])
                                    {
                                        fila.Enqueue(vizinho);
                                        visitados[vizinho.Item1, vizinho.Item2] = true;
                                    }
                                }
                            }
                            todosOsContornos.Add(contornoAtual);
                        }
                    }
                }
                // pinto a img de destino com pixels brancos
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        byte* pD = destP + y * destBMD.Stride + x * pixelSize;
                        *(pD) = 255;
                        *(pD + 1) = 255;
                        *(pD + 2) = 255;
                    }
                }

                foreach (var contorno in todosOsContornos)
                {
                    foreach (var ponto in contorno)
                    {
                        byte* pD = destP + ponto.y * destBMD.Stride + ponto.x * pixelSize;
                        *(pD) = 0;
                        *(pD + 1) = 0;
                        *(pD + 2) = 0;
                    }
                }
            }

            img.UnlockBits(srcBMD);
            dest.UnlockBits(destBMD);

            return todosOsContornos;
        }

        public static PointF[] EncontrarRetanguloMinimo(List<(int x, int y)> contorno)
        {
            if (contorno != null || contorno.Count != 0)
            {
                int minX = contorno[0].x;
                int maxX = contorno[0].x;
                int minY = contorno[0].y;
                int maxY = contorno[0].y;

                foreach (var ponto in contorno)
                {
                    if (ponto.x < minX) minX = ponto.x;
                    if (ponto.x > maxX) maxX = ponto.x;
                    if (ponto.y < minY) minY = ponto.y;
                    if (ponto.y > maxY) maxY = ponto.y;
                }

                return new PointF[]
                {
                    new PointF(minX, minY),
                    new PointF(maxX, minY),
                    new PointF(maxX, maxY),
                    new PointF(minX, maxY)
                };
            }
            else
                return null;
        }
    }
}


