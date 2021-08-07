using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using TrabalhoLFA.Entidades;

namespace TrabalhoLFA
{
    // TODO: Validar AFD
    // TODO: Definir mensagens de início e fim de execução
    // TODO: Validar com o professor a forma para delimitar as palavras no arquivo texto (outros), definir os arquivos de entrada e indicar erros léxicos
    class Program
    {
        static void Main(string[] args)
        {
            var afdJson = File.ReadAllText("afd.json");
            var afd = JsonSerializer.Deserialize<AFD>(afdJson);

            var tokensText = File.ReadAllText("tokens.txt");
            var tokens = tokensText.Split(" ");

            foreach (var token in tokens)
            {
                if (ValidarPalavra(token, afd))
                    Console.WriteLine(token + ": Valido");
                else
                    Console.WriteLine(token + ": Invalido");
            }
        }

        static public bool ValidarPalavra(string palavra, AFD afd)
        {
            var estadoAtual = afd.EstadoInicial;

            foreach (var simbolo in palavra)
            {
                var transicao = afd.FuncaoPrograma.SingleOrDefault(transicao => 
                    transicao.Origem.Equals(estadoAtual) &&
                    transicao.Simbolo == simbolo);

                if (transicao == null)
                    return false;

                estadoAtual = transicao.Destino;
            }

            return afd.EstadosFinais.Any(estado => estado.Equals(estadoAtual));
        }
    }
}
