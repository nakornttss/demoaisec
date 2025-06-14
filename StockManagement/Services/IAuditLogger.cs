namespace StockManagement.Services
{
    /// <summary>
    /// Interface for logging audit data.
    /// </summary>
    public interface IAuditLogger
    {
        /// <summary>
        /// Logs the provided data for auditing purposes.
        /// </summary>
        /// <param name="data">The data to log.</param>
        void Log(string data);
    }
}
