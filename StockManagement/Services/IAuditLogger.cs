namespace StockManagement.Services
{
    public interface IAuditLogger
    {
        void Log(string data);
    }
}
