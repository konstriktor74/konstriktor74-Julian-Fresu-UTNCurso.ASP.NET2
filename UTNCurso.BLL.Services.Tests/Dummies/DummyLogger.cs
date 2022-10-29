using Microsoft.Extensions.Logging;
using UTNCurso.BLL.Services.Tests.Stub;

namespace UTNCurso.BLL.Services.Tests.Dummies
{
    internal class DummyLogger<T> : ILogger<T>
    {
        public IDisposable BeginScope<TState>(TState state)
        {
            return new StubDisposable(); // Test double Stub object
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return false;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
        }
    }
}
