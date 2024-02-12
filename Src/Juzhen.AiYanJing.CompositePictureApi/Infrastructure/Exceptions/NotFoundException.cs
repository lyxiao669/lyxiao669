namespace Juzhen.Infrastructure
{
    public class NotFoundException:ServiceException
    {
        public NotFoundException(string message)
            : this(message, 404)
        {
        }

        public NotFoundException(string message, int statusCode)
            : base(message, statusCode)
        {
            StatusCode = statusCode;
        }
    }
}
