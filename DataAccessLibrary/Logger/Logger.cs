using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLibrary
{
    public class Logger : ILogger
    {
        private readonly ILoggerRepository _logRepo;

        public Logger()
        {
            _logRepo = new LoggerRepository();
        }

        public Logger(ILoggerRepository logRepo)
        {
            _logRepo = logRepo;
        }

        public void AddLogEntry(LogEntry entry)
        {
            entry.Timestamp = DateTime.Now.ToString();
            _logRepo.Add(entry);
        }

        public IList<LogEntry> GetAllEntries()
        {
            return _logRepo.GetAll();
        }

        public IList<LogEntry> GetAllEntries(int severity)
        {
            return _logRepo.GetAll(d => d.Severity.Equals(severity));
        }

        public IList<LogEntry> GetAllEntriesOfType(int type)
        {
            return _logRepo.GetAll(d => d.Type.Equals(type));
        }

        public IList<LogEntry> GetEntriesOfTypePerDateTime(int type, DateTime dt)
        {
            return _logRepo.GetAll(d => d.Type.Equals(type), d => d.Timestamp.Equals(dt));
        }

        public IList<LogEntry> GetEntriesPerDateTime(DateTime dt)
        {
            return _logRepo.GetAll(d => d.Timestamp.Equals(dt));
        }

        public LogEntry GetLogEntryById(int id)
        {
            return _logRepo.GetSingle(d => d.ID.Equals(id));
        }

        public void RemoveLogEntry(LogEntry entry)
        {
            _logRepo.Remove(entry);
        }

        public void UpdateLogEntry(LogEntry entry)
        {
            _logRepo.Update(entry);
        }

        public void AddLogEntries(params LogEntry[] entries)
        {
            _logRepo.Add(entries);
        }

        public void UpdateLogEntries(params LogEntry[] entries)
        {
            _logRepo.Update(entries);
        }
        public void RemoveLogEntries(params LogEntry[] entries)
        {
            _logRepo.Remove(entries);
        }
    }
}
