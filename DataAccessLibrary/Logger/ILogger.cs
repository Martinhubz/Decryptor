using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLibrary
{
    public interface ILogger
    {
        IList<LogEntry> GetAllEntries();
        IList<LogEntry> GetAllEntries(int severity);
        IList<LogEntry> GetAllEntriesOfType(int type);
        LogEntry GetLogEntryById(int id);
        IList<LogEntry> GetEntriesPerDateTime(DateTime dt);
        IList<LogEntry> GetEntriesOfTypePerDateTime(int type, DateTime dt);
        void AddLogEntry(LogEntry entry);
        void UpdateLogEntry(LogEntry entry);
        void RemoveLogEntry(LogEntry entry);
        void AddLogEntries(params LogEntry[] entries);
        void UpdateLogEntries(params LogEntry[] entries);
        void RemoveLogEntries(params LogEntry[] entries);

    }

    public enum LogSeverity
    {
        High = 3,
        Medium = 2,
        Low = 1
    }

    public enum EntryType
    {
        Info = 0,
        Error = 3
    }

    public enum Issuer : int
    {
        Auth = 0,
        Decryptor = 1
    }

}
