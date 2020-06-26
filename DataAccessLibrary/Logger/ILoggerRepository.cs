using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLibrary
{
    public interface ILoggerRepository : IGenericDataRepository<LogEntry>
    {
    }
}
