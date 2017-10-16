using Hangfire.Console;
using Hangfire.Console.Progress;
using Hangfire.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBPRMonitor
{
    public class HangFireConsole
    {

        PerformContext _context;
        IProgressBar _progressBar;

        public enum ActivityType { Info, Warning, Exception }

        public HangFireConsole(PerformContext context, IProgressBar progressBar)
        { this._context = context; this._progressBar = progressBar; }

        public void Progress(int value)
        { try { if (this._progressBar != null) { this._progressBar.SetValue(value); } } catch { } }

        public void Write(string logmsg, ActivityType activityType, Exception exception = null)
        {
            try
            {
                if (_context != null)
                {
                    StringBuilder sbBody = new StringBuilder(logmsg); sbBody.AppendLine();
                    try { if (exception != null) { sbBody.AppendLine(exception.Message); sbBody.AppendLine(exception.StackTrace.ToString()); sbBody.AppendLine(); } } catch { }
                    string dt = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
                    switch (activityType)
                    {
                        case ActivityType.Info: try { _context.SetTextColor(ConsoleTextColor.White); } catch { } break;
                        case ActivityType.Warning: try { _context.SetTextColor(ConsoleTextColor.Yellow); } catch { } break;
                        case ActivityType.Exception: try { _context.SetTextColor(ConsoleTextColor.Red); } catch { } break;
                        default: try { _context.SetTextColor(ConsoleTextColor.Blue); } catch { } break;
                    }
                    try { _context.WriteLine(dt + ":: " + sbBody.ToString()); _context.ResetTextColor(); } catch { }
                }
            }
            catch { }
        }

    }
}
