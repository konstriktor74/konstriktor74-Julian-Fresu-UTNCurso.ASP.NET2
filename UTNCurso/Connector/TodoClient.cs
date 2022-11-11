using System.Net.Http;
using Newtonsoft.Json;
using UTNCurso.Core.Domain;
using UTNCurso.Core.DTOs;

namespace UTNCurso.Connector
{
    public class TodoClient : ITodoClient<TodoItemDto>
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TodoClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<TodoItemDto>> GetAllTodoItems()
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                return await client.GetFromJsonAsync<IEnumerable<TodoItemDto>>("http://webapi/todos");
            }
        }

        public async Task<Result> CreateTodoItem(TodoItemDto todoItem)
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                var response = await client.PostAsJsonAsync("http://webapi/todos", todoItem);
                Result result = JsonConvert.DeserializeObject<Result>(await response.Content.ReadAsStringAsync());

                return result;
            }
        }

        public async Task<TodoItemDto> GetAsync(long value)
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                return await client.GetFromJsonAsync<TodoItemDto>($"http://webapi/todos/{value}");
            }
        }
    }
}
