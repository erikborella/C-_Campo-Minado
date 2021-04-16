using System;
using System.Collections.Generic;
using System.Text;

namespace CampoMinado
{
    class Celula
    {
        private bool ehBomba;
        private bool estaAberto;
        private int bombasAdjacentes;

        private int posicaoI;
        private int posicaoJ;

        public Celula(int i , int j)
        {
            this.EhBomba = false;
            this.estaAberto = false;
            this.bombasAdjacentes = 0;

            this.PosicaoI = i;
            this.PosicaoJ = j;
        }

        public bool EhBomba { get => ehBomba; set => ehBomba = value; }
        public bool EstaAberto { get => estaAberto; set => estaAberto = value; }
        public int BombasAdjacentes { get => bombasAdjacentes; set => bombasAdjacentes = value; }
        public int PosicaoI { get => posicaoI; set => posicaoI = value; }
        public int PosicaoJ { get => posicaoJ; set => posicaoJ = value; }

        public override string ToString()
        {
            if (!EstaAberto)
                return " ";
            else if (EhBomba)
                return "X";
            else if (BombasAdjacentes == 0)
                return "-";
            else
                return BombasAdjacentes.ToString();
        }
    }
}
