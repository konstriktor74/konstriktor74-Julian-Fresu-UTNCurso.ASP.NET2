using Microsoft.EntityFrameworkCore;
using UTNCurso.Core.Domain.Agendas.Aggregates;
using UTNCurso.Core.Interfaces;

namespace UTNCurso.Infrastructure.Repository
{
    public class AgendaRepository : Repository<Agenda, Guid>, IAgendaRepository
    {
        private readonly TodoContext _todoContext;

        public AgendaRepository(TodoContext dbContext) : base(dbContext)
        {
            _todoContext = dbContext;
        }

        public async Task<IEnumerable<Agenda>> GetAll()
        {
            return await _todoContext.Agenda.ToListAsync();
        }
    }
}
