using Infrastructure.Interfaces;
namespace Infrastructure.Entities
{
    public abstract class BaseEntity : IBaseEntity
    {
        public required Guid Id { get; set; }
    }
}
