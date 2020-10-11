//
// namespace EtAlii.xTechnology.Diagnostics
// {
//     using System.Diagnostics;
//
//     public class DebugLogger : ILogger
//     {
//         public LogLevel Level { get; set; } = LogLevel.Info;
//
//         public void Verbose(string message)
//         {
//             if (Level >= LogLevel.Verbose)
//             {
//                 Debug.WriteLine(message);
//             }
//         }
//
//         public void Verbose(string message, params object[] args)
//         {
//             if (Level >= LogLevel.Verbose)
//             {
//                 message = string.Format(message, args);
//                 Debug.WriteLine(message);
//             }
//         }
//
//         public void Info(string message)
//         {
//             if (Level >= LogLevel.Info)
//             {
//                 Debug.WriteLine(message);
//             }
//         }
//
//         public void Info(string message, params object[] args)
//         {
//             if (Level >= LogLevel.Info)
//             {
//                 message = string.Format(message, args);
//                 Debug.WriteLine(message);
//             }
//         }
//
//         public void Warning(string message)
//         {
//             if (Level >= LogLevel.Warning)
//             {
//                 Debug.WriteLine(message);
//             }
//         }
//
//         public void Warning(string message, params object[] args)
//         {
//             if (Level >= LogLevel.Warning)
//             {
//                 message = string.Format(message, args);
//                 Debug.WriteLine(message);
//             }
//         }
//
//         public void Critical(string message, System.Exception e, params object[] args)
//         {
//             if (Level >= LogLevel.Critical)
//             {
//                 message = string.Format(message, args);
//                 Debug.WriteLine(message);
//             }
//         }
//
//         public void Critical(string message, System.Exception e)
//         {
//             if (Level >= LogLevel.Critical)
//             {
//                 Debug.WriteLine(e.ToString());
//             }
//         }
//     }
// }
