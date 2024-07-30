using System.Security.Cryptography.X509Certificates;

namespace Ordering.Domain.Abstraction
{
    public abstract class Aggregate<Tid> : Entity<Tid>, IAggregate<Tid>
    {
        private readonly List<IDomainEvent> _domainevents = new List<IDomainEvent>();
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainevents.AsReadOnly();

        public void AddDomainEvents(IDomainEvent domainEvent)
        { 
            _domainevents.Add(domainEvent);
        }
        public IDomainEvent[] clearDomainEvents()
        {
            IDomainEvent[] dequedevents = _domainevents.ToArray();
            _domainevents.Clear();
            return dequedevents;
        }
    }
}
