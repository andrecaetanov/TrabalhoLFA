using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using TrabalhoLFA.Entidades;

namespace TrabalhoLFA
{
    // Obrigatório:
    // - Definir mensagens de início e rever mensagens de fim de execução
    // - Refatorar método de validação

    // Opcional:
    // - Receber nomes dos arquivos de entrada que devem estar na raiz do projeto
    // - Validar AFD
    // - Não duplicar estados na desserialização do json

    // Regras de validação do AFD:
    //- Alfabeto não pode ser vazio
    //- Estados não pode ser vazio
    //- Possuir no mínimo uma transição
    //- Todos os estados na função programa devem fazer parte da lista de estados
    //- Todos os simbolos da função programa devem fazer parte do alfabeto
    //- Todos os estados finais devem fazer parte da lista de estados
    //- O estado inicial deve estar contido na lista de estados
    //- Não pode existir uma transição em que o estado de origem é um estado final
    //- Deve possuir um estado inicial
    //- Deve possuir no minimo um estado final em que é um estado destino em alguma das transições

    class Program
    {
        static void Main(string[] args)
        {
            var afdJson = File.ReadAllText("afd-entrada.json");
            var afd = JsonSerializer.Deserialize<AFD>(afdJson);

            var texto = File.ReadAllText("tokens.txt");
            var textoValido = ValidarTextoPossibilitandoLeituraCaractereExtra(texto, afd);

            if (textoValido)
                Console.WriteLine("Texto válido!");
            else
                Console.WriteLine("Erro léxico encontrado.");
        }

        static public bool ValidarTexto(string texto, AFD afd)
        {
            var estadoAtual = afd.EstadoInicial;
            var token = string.Empty;

            foreach (var simbolo in texto)
            {
                var transicao = afd.FuncaoPrograma.SingleOrDefault(transicao =>
                    transicao.Origem.Equals(estadoAtual) &&
                    transicao.Simbolo == simbolo);

                if (transicao == null)
                    return false;

                token = string.Concat(token, simbolo);
                estadoAtual = transicao.Destino;

                if (afd.EstadosFinais.Any(estado => estado.Equals(estadoAtual)))
                {
                    Console.WriteLine(token);
                    estadoAtual = afd.EstadoInicial;
                    token = string.Empty;
                }
            }

            return afd.EstadosFinais.Any(estado => estado.Equals(estadoAtual));
        }

        static public bool ValidarTextoPossibilitandoLeituraCaractereExtra(string texto, AFD afd)
        {
            var estadoAtual = afd.EstadoInicial;
            var token = string.Empty;

            var i = 0;
            while (i < texto.Length)
            {
                var simbolo = texto[i];

                var transicao = afd.FuncaoPrograma.SingleOrDefault(transicao =>
                    transicao.Origem.Equals(estadoAtual) &&
                    transicao.Simbolo == simbolo);

                if (transicao == null)
                    return false;

                estadoAtual = transicao.Destino;

                var estadoFinal = afd.EstadosFinais.SingleOrDefault(estado => estado.Equals(estadoAtual));
                var estadoAtualEhFinal = estadoFinal != null;

                if (estadoAtualEhFinal)
                {
                    if (estadoFinal.LeCaractereExtra)
                        Console.WriteLine(token);
                    else
                    {
                        token = string.Concat(token, simbolo);
                        Console.WriteLine(token); 
                        i++;
                    }

                    token = string.Empty;
                    estadoAtual = afd.EstadoInicial; 
                }
                else
                {
                    token = string.Concat(token, simbolo);
                    i++;
                }
            }

            return afd.EstadosFinais.Any(estado => estado.Equals(estadoAtual));
        }
    }
}
