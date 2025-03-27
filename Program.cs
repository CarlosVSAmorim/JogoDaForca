using System;
using System.Collections.Generic;

while (true)
{
    Dictionary<string, string[]> categorias = new Dictionary<string, string[]>
    {
        { "FRUTAS", new string[] { "ABACATE", "BANANA", "MANGA", "UVA", "MELANCIA" } },
        { "ANIMAIS", new string[] { "CACHORRO", "GATO", "ELEFANTE", "TIGRE", "LEAO" } },
        { "PAISES", new string[] { "BRASIL", "ARGENTINA", "CANADA", "ALEMANHA", "JAPAO" } }
    };

    Console.Clear();
    Console.WriteLine("Escolha uma categoria: FRUTAS, ANIMAIS, PAISES");
    string categoriaEscolhida;

    do
    {
        Console.Write("Digite a categoria: ");
        categoriaEscolhida = Console.ReadLine().ToUpper();
    } while (!categorias.ContainsKey(categoriaEscolhida));

    string[] palavras = categorias[categoriaEscolhida];
    Random random = new Random();
    string palavraEscolhida = palavras[random.Next(palavras.Length)];

    char[] letrasEncontradas = new string('_', palavraEscolhida.Length).ToCharArray();
    HashSet<char> letrasChutadas = new HashSet<char>();
    int quantidadeErros = 0;
    bool jogadorAcertou = false;

    while (quantidadeErros <= 5 && !jogadorAcertou)
    {
        Console.Clear();
        DesenharForca(quantidadeErros);
        Console.WriteLine("Letras chutadas: " + string.Join(" ", letrasChutadas));
        Console.WriteLine("Palavra: " + string.Join(" ", letrasEncontradas));
        Console.Write("Digite uma letra ou tente adivinhar a palavra: ");
        string entrada = Console.ReadLine().ToUpper();

        if (entrada.Length > 1) // Tentativa de palavra inteira
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

    Console.Clear();
    DesenharForca(quantidadeErros);

    if (jogadorAcertou)
        Console.WriteLine("Parabéns! Você acertou a palavra: " + palavraEscolhida);
    else
        Console.WriteLine("Que pena! A palavra era: " + palavraEscolhida);

    Console.Write("Deseja jogar novamente? (S/N): ");
    if (Console.ReadLine().ToUpper() != "S")
        break;
}

void DesenharForca(int erros)
{
    string[] boneco = new string[]
    {
        " |/       |        ",
        " |        " + (erros >= 1 ? " O " : "   "),
        " |       " + (erros >= 3 ? "/" : " ") + (erros >= 2 ? "x" : " ") + (erros >= 4 ? "\\" : " "),
        " |        " + (erros >= 2 ? " x " : "   "),
        " |       " + (erros >= 5 ? "/ \\" : "   ")
    };

    Console.WriteLine(" ___________        ");
    foreach (string linha in boneco) Console.WriteLine(linha);
    Console.WriteLine("_|____              ");
    Console.WriteLine("----------------------------------------------");
}
