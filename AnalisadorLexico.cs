using System;
using System.Linq;
using TrabalhoLFA.Entidades;

namespace TrabalhoLFA
{
    public class AnalisadorLexico
    {
        public bool ValidarTexto(string texto, AFD afd)
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

            return String.IsNullOrEmpty(token);
        }
    }
}
