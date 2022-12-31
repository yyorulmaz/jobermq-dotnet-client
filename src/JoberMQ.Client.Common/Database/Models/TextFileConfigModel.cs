namespace JoberMQ.Client.Common.Database.Models
{
    public class TextFileConfigModel
    {
        public string DbPath { get; set; }
        public string DbFolderPath { get; set; }
        public string DbFileName { get; set; }
        public char DbFileSeparator { get; set; }
        public char DbArchiveFileSeparator { get; set; }
        public string DbFileExtension { get; set; }
        public int MaxRowCount { get; set; }
    }
}
