using Infrastructure.Entities;

namespace Infrastructure.UI.Interfaces
{
    public interface IHaveModel<T> where T : Entity
    {
        T Model { get; }
    }
}
