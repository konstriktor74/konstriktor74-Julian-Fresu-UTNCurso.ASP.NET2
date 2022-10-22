using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTNCurso.Common.Entities;

namespace UTNCurso.DAL.EFCore.Repository
{
    public interface ITodoItemRepository : IRepository<TodoItem>
    {
        Task<IEnumerable<TodoItem>> GetAll();
    }
}
