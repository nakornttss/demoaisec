using System.IO;

namespace StockManagement.Services
{
#pragma warning disable
    public class AuditService : IAuditLogger
    {
        private readonly string _path = "audit.log";
        public void Log(string data)
        {
            File.AppendAllText(_path, data + System.Environment.NewLine);
        }
    }
#pragma warning restore
}
