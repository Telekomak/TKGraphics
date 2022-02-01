using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKGraphics.Utilities.Logger
{
    public class LogEntry
    {
        public string Title { get; set; }
        public string Source { get; set; }
        public string Message { get; set; }
        public EntryType Type { get; set; }
        public TimeSpan Time { get; set; }

        public LogEntry(string title, string source, string message, EntryType type, TimeSpan time)
        {
            Title = title;
            Source = source;
            Message = message;
            Type = type;
            Time = time;
        }

        public virtual void Write()
        {
            Console.ForegroundColor = GetColor();
            Console.WriteLine($"[{Time.Hours}:{Time.Minutes}:{Time.Seconds}:{Time.Milliseconds}] {Source}: {Type}: {Title}\n------------------------------------------------------------");
            Console.ResetColor();
            Console.WriteLine(Message);
            Console.ForegroundColor = GetColor();
            Console.WriteLine("------------------------------------------------------------\n");
            Console.ResetColor();
        }

        protected virtual ConsoleColor GetColor()
        {
            switch (Type)
            {
                case EntryType.Success:
                    return ConsoleColor.Green;
                    break;
                case EntryType.Error:
                    return ConsoleColor.Red;
                    break;
                case EntryType.Info:
                    return ConsoleColor.Blue;
                    break;
                default:
                    return ConsoleColor.White;
            }
        }

        public override string ToString()
        {
            return $"[{Time.Hours}:{Time.Minutes}:{Time.Seconds}:{Time.Milliseconds}] {Type}: {Title}\n{Message}\n";
        }
    }

    public enum EntryType
    {
        Success = 0,
        Error = 1,
        Info = 3,
    }
}
