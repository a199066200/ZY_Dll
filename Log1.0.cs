using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LearnStream
{
    /// <summary>
    /// 实现打印日志功能的类，默认输出到工程文件下Debug目录下的Log文件夹下，区分级别？先简单实现
    /// 
    /// </summary>
    public class LogManager
    {
        private static LogWriter l_writer;

        static LogManager()
        {
            string file = Path.Combine(Directory.GetCurrentDirectory(), "Log");
            string path = Path.Combine(file, "Log.txt");
            l_writer = new LogWriter(file, path);
        }
        public static void Log(object log)
        {
            var info = log.ToString();
            if (!string.IsNullOrEmpty(info))
                l_writer.WriteLog(info);
        }


        private class LogWriter
        {
            private readonly string _logPath;
            private readonly string _logFilePath;
            private StreamWriter stream;

            public LogWriter(string file, string path)
            {
                _logPath = path;
                _logFilePath = file;
                CheckFileExist();
                stream = File.AppendText(_logPath);
            }

            private void CheckFileExist()
            {
                if (!Directory.Exists(_logFilePath))
                    Directory.CreateDirectory(_logFilePath);
            }

            public void WriteLog(string log)
            {
                Monitor.Enter(stream);
                stream.WriteLine(log);
                stream.Flush();
                Monitor.Exit(stream);
            }
        }
    }
}
