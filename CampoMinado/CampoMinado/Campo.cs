using System;
using System.Collections.Generic;
using System.Text;

namespace CampoMinado
{
    class Campo
    {
        private Celula[,] celulas;
        private int numeroDeBombas;

        private Campo(int tamanhoX, int tamanhoY, int numeroDeBombas)
        {
            celulas = new Celula[tamanhoX, tamanhoY];
            this.numeroDeBombas = numeroDeBombas;
        }

        #region Construtor com bombas

        public static Campo ComBombasAleatorias(int tamanhoX, int tamanhoY, int porcentagemDeBombas)
        {
            Random random = new Random();

            double porcentagem = (Convert.ToDouble(porcentagemDeBombas) / 100);
            int quantidadeDeBombas = (int)(porcentagem * (tamanhoX * tamanhoY));

            Campo campo = CriarCampo(tamanhoX, tamanhoY, quantidadeDeBombas);

            int contadorDeBombasGeradas = 0;

            while (contadorDeBombasGeradas < quantidadeDeBombas)
            {
                int posicaoX = random.Next(0, tamanhoX);
                int posicaoY = random.Next(0, tamanhoY);

                if (!campo.celulas[posicaoX, posicaoY].EhBomba)
                {
                    campo.celulas[posicaoX, posicaoY].EhBomba = true;
                    contadorDeBombasGeradas++;
                }
            }

            campo.AtualizarValoresDasCelulas();

            return campo;
        }

        private static Campo CriarCampo(int tamanhoX, int tamanhoY, int quantidadeDeBombas)
        {
            Campo campo = new Campo(tamanhoX, tamanhoY, quantidadeDeBombas);

            for (int i = 0; i < campo.celulas.GetLength(0); i++)
            {
                for (int j = 0; j < campo.celulas.GetLength(1); j++)
                {
                    campo.celulas[i, j] = new Celula(i, j);
                }
            }

            return campo;
        }

        #endregion

        private bool EhPosicaoValida(int i, int j)
        {
            return i >= 0 && i < celulas.GetLength(0) &&
                j >= 0 && j < celulas.GetLength(1);
        }

        private void AtualizarValoresDasCelulas() {
            for (int i = 0; i < this.celulas.GetLength(0); i++)
            {
                for (int j = 0; j < this.celulas.GetLength(1); j++)
                {
                    Celula celulaAtual = celulas[i, j];

                    if (celulaAtual.EhBomba)
                        continue;

                    for (int celulaI = -1; celulaI <= 1; celulaI++)
                    {
                        for (int celulaJ = -1; celulaJ <= 1; celulaJ++)
                        {
                            if ((celulaI != 0 || celulaJ != 0) && EhPosicaoValida(i + celulaI, j + celulaJ))
                            {
                                if (this.celulas[i + celulaI, j + celulaJ].EhBomba)
                                    celulaAtual.BombasAdjacentes++;
                            }
                        }
                    }

                }
            }
        }

        // Implementado usando busca em largura (BFS)
        public void AbrirCasasVazias(int i, int j)
        {
            List<Celula> fila = new List<Celula>();
            int ponteiro = 0;

            fila.Add(this.celulas[i, j]);
            this.celulas[i, j].EstaAberto = true;

            while (ponteiro < fila.Count)
            {
                Celula atual = fila[ponteiro];
                ponteiro++;

                if (atual.BombasAdjacentes == 0)
                {
                    AdicionaCelularAdjacentes(fila, atual);
                }
            }
        }

        private void AdicionaCelularAdjacentes(List<Celula> fila, Celula atual)
        {

            for (int celulaI = -1; celulaI <= 1; celulaI++)
            {
                for (int celulaJ = -1; celulaJ <= 1; celulaJ++)
                {
                    if (EhPosicaoValida(atual.PosicaoI + celulaI, atual.PosicaoJ + celulaJ))
                        if (!this.celulas[atual.PosicaoI + celulaI, atual.PosicaoJ + celulaJ].EstaAberto)
                        {
                            this.celulas[atual.PosicaoI + celulaI, atual.PosicaoJ + celulaJ].EstaAberto = true;
                            fila.Add(this.celulas[atual.PosicaoI + celulaI, atual.PosicaoJ + celulaJ]);
                        }
                }
            }
        }

        public bool AbrirEhBomba(int i, int j)
        {
            if (!EhPosicaoValida(i, j))
                return false;

            if (this.celulas[i, j].EstaMarcado)
                return false;

            if (this.celulas[i, j].EstaAberto)
                return false;

            if (this.celulas[i, j].EhBomba)
            {
                this.celulas[i, j].EstaAberto = true;
                return true;
            }

            if (this.celulas[i, j].BombasAdjacentes != 0)
                this.celulas[i, j].EstaAberto = true;

            if (this.celulas[i, j].BombasAdjacentes == 0)
                AbrirCasasVazias(i, j);

            return false;
        }

        public void Marcar(int i, int j)
        {
            if (EhPosicaoValida(i, j))
                this.celulas[i, j].EstaMarcado = !this.celulas[i, j].EstaMarcado;

        }

        public bool DescobriuTodasAsBombas()
        {
            int casasSemBombaAbertas = 0;

            for (int i = 0; i < this.celulas.GetLength(0); i++)
            {
                for (int j = 0; j < this.celulas.GetLength(1); j++)
                {
                    Celula atual = this.celulas[i, j];

                    if (atual.EstaAberto)
                        casasSemBombaAbertas++;
                }
            }

            return casasSemBombaAbertas == (this.celulas.GetLength(0) * this.celulas.GetLength(1)) - this.numeroDeBombas;
        }

        private string PegarEspacosVazios(string str, int offset)
        {
            StringBuilder espacos = new StringBuilder();

            int nEspacos = offset - str.Length;

            for (int i = 0; i < nEspacos; i++)
                espacos.Append(" ");

            return espacos.ToString();
        }

        public string ToStringComPosicoes()
        {
            StringBuilder saida = new StringBuilder();

            int offsetAEsquerda = (this.celulas.GetLength(0)).ToString().Length;
            int offfsetDoMeio = (this.celulas.GetLength(1)).ToString().Length;

            saida.Append(" ");
            saida.Append(PegarEspacosVazios("", offsetAEsquerda));

            for (int i = 0; i < this.celulas.GetLength(1); i++)
            {
                if (i != 0)
                    saida.Append("|");

                string str = $"{i + 1}";
                saida.Append(str);
                saida.Append(PegarEspacosVazios(str, offfsetDoMeio));
            }

            saida.AppendLine();

            for (int i = 0; i < this.celulas.GetLength(0); i++)
            {
                saida.Append(i + 1);
                saida.Append(PegarEspacosVazios($"{i + 1}", offsetAEsquerda + 1));
                for (int j = 0; j < this.celulas.GetLength(1); j++)
                {
                    saida.Append(this.celulas[i, j].ToString());
                    saida.Append(PegarEspacosVazios(this.celulas[i, j].ToString(), offfsetDoMeio+1));
                }
                saida.AppendLine();
            }
     
            return saida.ToString();
        }

        public override string ToString()
        {
            StringBuilder saida = new StringBuilder();

            for (int i = 0; i < this.celulas.GetLength(0); i++)
            {
                for (int j = 0; j < this.celulas.GetLength(1); j++) {
                    saida.Append(this.celulas[i, j].ToString() +  " ");
                }
                saida.AppendLine();
            }
            return saida.ToString();
        }
    }
}
