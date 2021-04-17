using System;

namespace CampoMinado
{
    class Program
    {
        static void Main(string[] args)
        {
            bool jogando = true;

            while (jogando)
            {
                Console.Clear();
                Console.WriteLine("Digite qual o tamanho do campo que voce deseja no formato: (ex: 10x10)");
                int tamanhoI, tamanhoJ;
                LerTamanhoDoCampo(out tamanhoI, out tamanhoJ);

                Console.WriteLine("Digite a porcentagem de bombas que voce desaja, minimo: 1, maximo: 100");
                int porcentagemDeBombas = LerPorcentagemDeBombas();

                Campo campo = Campo.ComBombasAleatorias(tamanhoI, tamanhoJ, porcentagemDeBombas);

                bool naoExplodiuAinda = true;
                bool abriuTodasAsBombas = false;

                while (naoExplodiuAinda && !abriuTodasAsBombas)
                {
                    Console.Clear();

                    Console.WriteLine(campo.ToStringComPosicoes());

                    Console.Write("Digite as posicoes que voce deseja jogar, coloque um M no final para marcar ");
                    bool ehParaMarcar;
                    int i, j;
                    LerPosicoesJogadas(out i, out j, out ehParaMarcar);


                    if (ehParaMarcar)
                        campo.Marcar(i-1, j-1);
                    else
                    {
                        naoExplodiuAinda = !campo.AbrirEhBomba(i-1, j-1);
                        abriuTodasAsBombas = campo.DescobriuTodasAsBombas();
                    }
                }

                if (!naoExplodiuAinda)
                {
                    Console.WriteLine("Puts, voce explodiu");
                } else
                {
                    Console.WriteLine("Parabens!");
                }

                Console.Write("Digite S para sair ou qualquer coisa para jogar de novo: ");
                jogando = Console.ReadLine() != "S";
            }
        }

        private static void LerTamanhoDoCampo(out int tamanhoI, out int tamanhoJ)
        {
            while (true)
            {
                string input = Console.ReadLine();
                string[] inputSeparado = input.Split("x");

                if (inputSeparado.Length != 2)
                {
                    Console.WriteLine("Digite o tamanho no formato NxN");
                    continue;
                }

                try
                {
                    tamanhoI = Convert.ToInt32(inputSeparado[0]);
                    tamanhoJ = Convert.ToInt32(inputSeparado[1]);
                    return;
                } catch (Exception)
                {
                    Console.WriteLine("Digite o tamanho no formato NxN, ex: 10x10");
                    continue;
                }
            }
        }
        
        private static int LerPorcentagemDeBombas()
        {
            while (true)
            {
                try
                {
                    int porcentagem = Convert.ToInt32(Console.ReadLine());

                    if (porcentagem < 1 || porcentagem > 100)
                    {
                        Console.WriteLine("Por favor digite um numero entre 1 e 100");
                        continue;
                    }

                    return porcentagem;
                }
                catch (Exception)
                {
                    Console.WriteLine("Digite um numero!");
                    continue;
                }
            }
        }

        private static void LerPosicoesJogadas(out int i, out int j, out bool ehParaMarcar)
        {
            while (true)
            {
                string input = Console.ReadLine();
                string[] inputSeparado = input.Split(" ");

                if (inputSeparado.Length != 2 && inputSeparado.Length != 3)
                {
                    Console.WriteLine("Digite as posicoes que voce deseja jogar, ex 2 6, ou coloque um M no final para marcar");
                    continue;
                }

                if (inputSeparado.Length == 3)
                {
                    if (inputSeparado[2] == "M")
                        ehParaMarcar = true;
                    else
                    {
                        Console.WriteLine("Digite apenas M no final para marcar");
                        continue;
                    }
                }
                else
                    ehParaMarcar = false;

                try
                {
                    i = Convert.ToInt32(inputSeparado[0]);
                    j = Convert.ToInt32(inputSeparado[1]);
                    return;
                }
                catch (Exception)
                {
                    Console.WriteLine("Digite as posicoes que voce deseja jogar, ex 2 6, ou coloque um M no final para marcar");
                    continue;
                }
            }
        }
    }
}
