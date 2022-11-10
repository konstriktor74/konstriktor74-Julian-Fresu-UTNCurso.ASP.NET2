using UTNCurso.Core.Domain.Agendas.Aggregates;
using UTNCurso.Core.Domain.Agendas.Entities;
using UTNCurso.Core.Interfaces;

namespace UTNCurso.BLL.Services.Tests.Fakes
{
    public class FakeAgendaRepository : IAgendaRepository
    {
        public List<Agenda> Store { get; set; }

        public TodoItem LastAdded { get; set; }

        public FakeAgendaRepository()
        {
            var agenda = Agenda.Create();
            Store = new List<Agenda> { agenda };
        }

        public async Task Add(Agenda entity)
        {
            Store.Add(entity);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Agenda>> GetAll()
        {
            return await Task.FromResult(Store);
        }

        public async Task<Agenda> GetById(Guid id)
        {
            return await Task.FromResult(Store.FirstOrDefault(x => x.Id == id));
        }

        public Task Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task SaveChangesAsync()
        {
            await Task.CompletedTask;
        }

        public async Task Update(Agenda entity)
        {
            var storedEntity = Store.FirstOrDefault(x => x.Id == entity.Id);
            Store.Remove(storedEntity);
            Store.Add(entity);
        }
    }
}
