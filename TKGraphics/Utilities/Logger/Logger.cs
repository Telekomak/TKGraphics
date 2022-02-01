using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKGraphics.Utilities.Logger
{
    public class Logger
    {
        private static List<LogEntry> _entries = new List<LogEntry>();

        public IEnumerable<LogEntry> Entries
        {
            get
            {
                return _entries;
            }
        }

        public static void Log(LogEntry entry)
        {
            _entries.Add(entry);
            entry.Write();
        }
    }
}
