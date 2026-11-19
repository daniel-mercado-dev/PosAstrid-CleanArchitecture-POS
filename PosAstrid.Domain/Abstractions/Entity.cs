using System.Collections.Generic;
using System.Linq;

namespace PosAstrid.Domain.Abstractions
{
    public abstract class Entity
    {
        // Mantenemos tu int Id que necesitas para el POS
        public int Id { get; protected set; }

        private readonly List<IDomainEvent> _domainEvents = new();

        public IReadOnlyList<IDomainEvent> GetDomainEvents() => _domainEvents.ToList();

        protected void RaiseDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
          public void ClearDomainEvents() => _domainEvents.Clear();
    }
}