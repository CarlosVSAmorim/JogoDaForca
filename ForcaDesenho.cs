using System;

public static class ForcaDesenho
{
    public static void Desenhar(int erros)
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
        foreach (string linha in boneco)
            Console.WriteLine(linha);
        Console.WriteLine("_|____              ");
        Console.WriteLine("----------------------------------------------");
    }
}