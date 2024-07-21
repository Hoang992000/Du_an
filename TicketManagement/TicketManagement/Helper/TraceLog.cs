using System.Runtime.CompilerServices;
using System.Text;

namespace RFIDApp.Helper
{
    public class TraceLog
    {
        public static string Message(string message, [CallerMemberName] string callerMemberName = "", [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string filePath = "")
        {
            StringBuilder sb = new StringBuilder(message);
            sb.AppendLine("Member: " + callerMemberName);
            sb.AppendLine("Path: " + filePath);
            sb.AppendLine("Line: " + sourceLineNumber.ToString());
            return sb.ToString();
        }
    }
}
