namespace Core.Application.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string name, object key) : base($"{name} ({key}) was not found")
        {

        }

        public NotFoundException(string key, string value) : base($"Trường {key} = {value} không hợp lệ!")
        {

        }

        public NotFoundException(string name) : base($"Trường {name} không hợp lệ!")
        {

        }
    }
}
