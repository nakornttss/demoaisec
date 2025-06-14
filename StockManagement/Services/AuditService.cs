using System.IO;

namespace StockManagement.Services
{
    /// <summary>
    /// Implementation of IAuditLogger that writes audit logs to a file.
    /// </summary>
    public class AuditService : IAuditLogger
    {
        private readonly string _path = "audit.log";

        /// <summary>
        /// Appends the provided data to the audit log file.
        /// </summary>
        /// <param name="data">The data to log.</param>
        public void Log(string data)
        {
            File.AppendAllText(_path, data + System.Environment.NewLine);
        }
    }
}
