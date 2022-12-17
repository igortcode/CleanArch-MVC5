using System;

namespace ExMvc.Domain.Entities
{
    public abstract class Entity
    {
        public string Id { get; protected set; }
        public DateTime DataCadastro { get; protected set; }

        public Entity()
        {
            Id= Guid.NewGuid().ToString();
            DataCadastro= DateTime.Now;
        }
    }
}
