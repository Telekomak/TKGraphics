using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKGraphics.Utilities.Logger
{
    public class SyntaxErrorEntry : LogEntry
    {
        private string[] _code;
        private int _line;
        private int _offset;

        public SyntaxErrorEntry(TimeSpan time, string title, string source, string errorMessage, string code, int line, int lineCount) : base(title, source, errorMessage, EntryType.Error, time)
        {
            _code = ConstrainCode(code.Split("\r\n"), lineCount, line, out _offset);
            _line = line - 1;
        }

        private string[] ConstrainCode(string[] code, int limit, int line, out int offset)
        {
            if (limit < 0 || line < 0) throw new ArgumentOutOfRangeException("Argument out of range!");
            
            if (code.Length > limit)
            {
                offset = line - limit / 2 < 0 ?
                    0 : line + limit / 2 >= code.Length ?
                        line - (limit / 2 + ((code.Length - 1) - line)) : line - limit / 2;

                string[] retData = new string[limit + 1];
                Array.Copy(code, offset, retData, 0, limit);
                return retData;
            }

            offset = 0;
            return code;
        }
        public override void Write()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"[{Time.Hours}:{Time.Minutes}:{Time.Seconds}:{Time.Milliseconds}] {Source}: {Type}: {Title}\n------------------------------------------------------------");
            Console.ResetColor();

            Console.WriteLine($"{Message}\nCode:");

            for (int i = 0; i < _code.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                for (int j = (i + 1 + _offset).ToString().Length; j <= _code.Length.ToString().Length; j++) Console.Write(" ");
                Console.Write($"{_offset + 1 + i}| ");
                Console.ResetColor();

                if (_offset + i == _line)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine(_code[i]);
                    Console.ResetColor();
                }
                else Console.WriteLine(_code[i]);
            }

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("------------------------------------------------------------\n");
            Console.ResetColor();
        }
    }
}
