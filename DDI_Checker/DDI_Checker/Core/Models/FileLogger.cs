using System.IO;

namespace DDI_Checker.Core.Models
{
    public class FileLogger : ASearchLogger
    {
        private readonly string _filePath;

        public FileLogger(string filePath)
        {
            _filePath = filePath;
            Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
        }

        protected override void LogResult()
        {
            File.AppendAllText(_filePath, GetResultMessage() + "\n\n");
        }

        protected override void LogSearch()
        {
            File.AppendAllText(_filePath, GetSearchMessage() + "\n\n");
        }
    }
}
