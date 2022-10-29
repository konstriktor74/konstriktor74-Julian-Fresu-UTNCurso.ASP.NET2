using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using UTNCurso.BLL.Services.Tests.Dummies;
using UTNCurso.BLL.Services.Tests.Stub;

namespace UTNCurso.BLL.Services.Tests.Spy
{
    internal class SpyLogger<T> : ILogger<T>
    {
        public int HowManyTimesCalled { get; set; }

        public IDisposable BeginScope<TState>(TState state)
        {
            return new StubDisposable(); // Test double Stub object
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            HowManyTimesCalled++;
        }
    }
}
