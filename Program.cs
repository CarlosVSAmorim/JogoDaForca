using System;
using System.Collections.Generic;

public class Program
{
    private Dictionary<string, string[]> categorias;
    private string palavraEscolhida;
    private char[] letrasEncontradas;
    private HashSet<char> letrasChutadas;
    private int quantidadeErros;
    private bool jogadorAcertou;

    public static void Main(string[] args)
    {
        Program jogo = new Program();
        jogo.Iniciar();
    }

    public void Iniciar()
    {
        InicializarCategorias();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Escolha uma categoria: FRUTAS, ANIMAIS, PAISES");

            string categoriaEscolhida;
            do
            {
                Console.Write("Digite a categoria: ");
                categoriaEscolhida = Console.ReadLine().ToUpper();
            } while (!categorias.ContainsKey(categoriaEscolhida));

            SelecionarPalavra(categoriaEscolhida);
            JogarRodada();

            Console.Clear();
            ForcaDesenho.Desenhar(quantidadeErros);

            if (jogadorAcertou)
                Console.WriteLine($"Parabéns! Você acertou a palavra: {palavraEscolhida}");
            else
                Console.WriteLine($"Que pena! A palavra era: {palavraEscolhida}");

            Console.Write("Deseja jogar novamente? (S/N): ");
            if (Console.ReadLine().ToUpper() != "S")
                break;
        }
    }

    private void InicializarCategorias()
    {
        categorias = new Dictionary<string, string[]>
        {
            { "FRUTAS", new[] { "ABACATE", "BANANA", "MANGA", "UVA", "MELANCIA" } },
            { "ANIMAIS", new[] { "CACHORRO", "GATO", "ELEFANTE", "TIGRE", "LEAO" } },
            { "PAISES", new[] { "BRASIL", "ARGENTINA", "CANADA", "ALEMANHA", "JAPAO" } }
        };
    }

    private void SelecionarPalavra(string categoria)
    {
        string[] palavras = categorias[categoria];
        palavraEscolhida = palavras[new Random().Next(palavras.Length)];
        letrasEncontradas = new string('_', palavraEscolhida.Length).ToCharArray();
        letrasChutadas = new HashSet<char>();
        quantidadeErros = 0;
        jogadorAcertou = false;
    }

    private void JogarRodada()
    {
        while (quantidadeErros <= 5 && !jogadorAcertou)
        {
            Console.Clear();
            ForcaDesenho.Desenhar(quantidadeErros);
            Console.WriteLine("Letras chutadas: " + string.Join(" ", letrasChutadas));
            Console.WriteLine("Palavra: " + string.Join(" ", letrasEncontradas));
            Console.Write("Digite uma letra ou tente adivinhar a palavra: ");
            string entrada = Console.ReadLine().ToUpper();

            if (entrada.Length > 1)
            {
                if (entrada == palavraEscolhida)
                {
                    jogadorAcertou = true;
                    break;
                }
                else
                {
                    quantidadeErros++;
                }
            }
            else if (entrada.Length == 1)
            {
                char chute = entrada[0];

                if (letrasChutadas.Contains(chute))
                {
                    Console.WriteLine("Você já chutou essa letra! Pressione Enter para continuar...");
                    Console.ReadLine();
                    continue;
                }

                letrasChutadas.Add(chute);
                bool letraFoiEncontrada = false;

                for (int i = 0; i < palavraEscolhida.Length; i++)
                {
                    if (palavraEscolhida[i] == chute)
                    {
                        letrasEncontradas[i] = chute;
                        letraFoiEncontrada = true;
                    }
                }

                if (!letraFoiEncontrada)
                    quantidadeErros++;
            }

            jogadorAcertou = new string(letrasEncontradas) == palavraEscolhida;
        }
    }
}
