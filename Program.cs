using System;
using System.IO;
using System.Text.Json;
using TrabalhoLFA.Entidades;

namespace TrabalhoLFA
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Iniciando execução da aplicação...");

            var afdJson = LerTextoDoArquivo(args[0]);
            var tokens = LerTextoDoArquivo(args[1]);

            var afd = JsonSerializer.Deserialize<AFD>(afdJson);

            Console.WriteLine("Iniciando validação do arquivo texto...\n");

            var analisadorLexico = new AnalisadorLexico();
            var textoEhValido = analisadorLexico.ValidarTexto(tokens, afd);

            if (textoEhValido)
                Console.WriteLine("\nTodo texto lido foi validado pelo analisador léxico.");
            else
                Console.WriteLine("\nLeitura interrompida: Foi encontrado um erro léxico.");
        }

        static string LerTextoDoArquivo(string nomeDoArquivo)
        {
            return File.ReadAllText(Environment.CurrentDirectory + @"\" + nomeDoArquivo);
        }
    }
}
