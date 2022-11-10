using UTNCurso.Core.Domain.Agendas.Aggregates;

namespace UTNCurso.Core.Interfaces
{
    public interface IAgendaRepository : IRepository<Agenda, Guid>
    {
        Task<IEnumerable<Agenda>> GetAll();
    }
}
