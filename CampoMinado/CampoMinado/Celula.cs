using System;
using System.Collections.Generic;
using System.Text;

namespace CampoMinado
{
    class Celula
    {
        private bool ehBomba;
        private bool estaAberto;
        private bool estaMarcado;
        private int bombasAdjacentes;

        private int posicaoI;
        private int posicaoJ;

        public Celula(int i , int j)
        {
            this.EhBomba = false;
            this.estaAberto = false;
            this.EstaMarcado = false;
            this.bombasAdjacentes = 0;

            this.PosicaoI = i;
            this.PosicaoJ = j;
        }

        public bool EhBomba { get => ehBomba; set => ehBomba = value; }
        public bool EstaAberto { get => estaAberto; set => estaAberto = value; }
        public int BombasAdjacentes { get => bombasAdjacentes; set => bombasAdjacentes = value; }
        public int PosicaoI { get => posicaoI; set => posicaoI = value; }
        public int PosicaoJ { get => posicaoJ; set => posicaoJ = value; }
        public bool EstaMarcado { get => estaMarcado; set => estaMarcado = value; }

        public override string ToString()
        {
            if (EstaMarcado)
                return "p";
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
