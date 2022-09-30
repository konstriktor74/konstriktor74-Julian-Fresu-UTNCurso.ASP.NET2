namespace UTNCurso.BLL.POCOs
{
    public class Result
    {
        public List<Error> _errors { get; private set; }

        public IEnumerable<Error> Errors => _errors;

        public bool IsSuccessful => !_errors.Any();

        public int StatusCode { get; private set; }

        public Result()
        {
            _errors = new List<Error>();
        }

        public void AddError(string name, string msg)
        {
            _errors.Add(new Error(name, msg));
        }

        public void SetStatus(int code)
        {
            StatusCode = code;
        }
    }
}
