namespace Core.Application.Interfaces.Common
{
    public interface IDateTimeService
    {
        public DateTimeOffset Now => DateTimeOffset.UtcNow;
    }
}
