namespace Core.Application.Interfaces
{
    public interface IDateTimeService
    {
        public DateTimeOffset Now => DateTimeOffset.UtcNow;
    }
}
