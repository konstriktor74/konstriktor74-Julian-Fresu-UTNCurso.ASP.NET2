namespace UTNCurso.Core.Domain
{
    public class Error
    {
        public string ComponentName { get; set; }

        public string Message { get; set; }

        public Error(string name, string msg)
        {
            ComponentName = name;
            Message = msg;
        }
    }
}
