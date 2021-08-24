using System.Collections.Generic;

namespace TrabalhoLFA.Entidades
{
    public class AFD
    {
        public char[] Alfabeto { get; set; }
        public List<Estado> Estados { get; set; }
        public List<Transicao> FuncaoPrograma { get; set; }
        public Estado EstadoInicial { get; set; }
        public List<Estado> EstadosFinais { get; set; }
    }
}
