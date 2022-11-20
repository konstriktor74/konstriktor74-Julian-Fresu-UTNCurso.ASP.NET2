using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
                return await client.GetFromJsonAsync<IEnumerable<TodoItemDto>>("http://localhost:5200/todos");
            }
        }

        public async Task<Result> CreateTodoItem(TodoItemDto todoItem)
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                var response = await client.PostAsJsonAsync("http://localhost:5200/todos", todoItem);
                var result = JsonConvert.DeserializeObject<Result>(await response.Content.ReadAsStringAsync());

                return result;
            }
        }

        public async Task<TodoItemDto> GetAsync(long value)
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                return await client.GetFromJsonAsync<TodoItemDto>($"http://localhost:5200/todos/{value}");
            }
        }

        public async Task<IEnumerable<TodoItemDto>> Search(string taskDescription, bool? isCompleted)
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                return await client.GetFromJsonAsync<IEnumerable<TodoItemDto>>($"http://localhost:5200/todos/search?taskDescription={taskDescription}&isCompleted={isCompleted}");
            }
        }
    }
}
