using System;

namespace TrabalhoLFA.Entidades
{
    public class Estado : IEquatable<Estado>
    {
        public string Nome { get; set; }

        public bool Equals(Estado other)
        {
            return Nome == other.Nome;
        }
    }
}
