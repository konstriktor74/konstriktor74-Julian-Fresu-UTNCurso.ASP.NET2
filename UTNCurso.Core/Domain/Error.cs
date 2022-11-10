namespace UTNCurso.Core.Domain
{
    public class Error
    {
        public string ComponentName { get; private set; }

        public string Message { get; private set; }

        public Error(string name, string msg)
        {
            ComponentName = name;
            Message = msg;
        }
    }
}
