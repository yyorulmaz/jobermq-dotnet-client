using JoberMQ.Client.Common.Database.Base;
using JoberMQ.Client.Common.Database.Models;
using JoberMQ.Client.Common.Database.Repository.Abstraction.Text;
using Newtonsoft.Json;
using System.Text;

namespace JoberMQ.Common.Database.Repository.Implementation.Text.Default
{
    internal class DfTextRepository<T> : ITextRepository<T>
         where T : DboPropertyGuidBase, new()
    {
        #region Property Helper
        private readonly TextFileConfigModel textFileConfig;
        private bool isSetup;
        private FileStream fileStream;
        private StreamWriter streamWriter;
        private Mutex mutex;
        private int rowCounter = 1;
        private int arsiveFileCounter = 1;
        private string baseFileFullPath;
        #endregion

        #region Constructor
        public DfTextRepository(TextFileConfigModel textFileConfig)
        {
            this.textFileConfig = textFileConfig;
            baseFileFullPath = GetBaseFileFullPath();
        }
        #endregion


        public int ArsiveFileCounter { get => arsiveFileCounter; set => arsiveFileCounter = value; }


        #region Setup
        public bool CreateDatabase()
        {
            try
            {
                if (!Directory.Exists(textFileConfig.DbPath))
                    Directory.CreateDirectory(textFileConfig.DbPath);

                if (!Directory.Exists(Path.Combine(new string[] { textFileConfig.DbPath, textFileConfig.DbFolderPath })))
                    Directory.CreateDirectory(Path.Combine(new string[] { textFileConfig.DbPath, textFileConfig.DbFolderPath }));

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Setup()
        {
            try
            {
                if (isSetup)
                    return true;

                mutex = MutexCreate(false, textFileConfig.DbFileName);
                fileStream = FileStreamCreate(baseFileFullPath, 32768);
                streamWriter = StreamWriterCreate(fileStream);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion


        #region WRITE
        public bool WriteLine(string message)
        {
            try
            {
                MaxRowCountCheck();

                lock (mutex)
                {
                    streamWriter.WriteLine(message);
                    streamWriter.Flush();
                }

                rowCounter++;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool WriteLine(T message)
            => WriteLine(JsonConvert.SerializeObject(message, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
        #endregion

        #region READ
        public List<T> ReadAllData(bool isFullFileList)
        {
            List<T> list = new List<T>();

            List<DataLogFileModel> fullFileList;
            if (isFullFileList)
                fullFileList = GetFileListFull();
            else
                fullFileList = GetFileListArchive();

            if (fullFileList == null)
                return null;

            var paths = fullFileList.Select(x => x.FullPath).ToList();
            var fullData = ReadLineFile(paths);

            return fullData;
        }
        public (List<T> datas, List<DataLogFileModel> paths) ReadAllData2(bool isFullFileList)
        {
            List<T> list = new List<T>();

            List<DataLogFileModel> fullFileList;
            if (isFullFileList)
                fullFileList = GetFileListFull();
            else
                fullFileList = GetFileListArchive();

            if (fullFileList == null)
                return (null, null);

            var paths = fullFileList.Select(x => x.FullPath).ToList();
            var fullData = ReadLineFile(paths);

            return (fullData, fullFileList);
        }
        public List<T> ReadAllDataGrouping(bool isFullFileList)
            => GroupingData(ReadAllData(isFullFileList));
        public (List<T> datas, List<DataLogFileModel> paths) ReadAllDataGrouping2(bool isFullFileList)
        {
            var readAllData2 = ReadAllData2(isFullFileList);

            return (GroupingData(readAllData2.datas), readAllData2.paths);
        }
        #endregion

        #region HELPER
        private void MaxRowCountCheck()
        {
            if (rowCounter == textFileConfig.MaxRowCount)
            {
                File.Move(baseFileFullPath, GetArsiveFileFullPath(arsiveFileCounter));

                fileStream = FileStreamCreate(baseFileFullPath, 32768);
                streamWriter = StreamWriterCreate(fileStream);

                rowCounter = 1;
                arsiveFileCounter++;
            }
        }
        public string GetBaseFileFullPath()
            => Path.Combine(new string[] { textFileConfig.DbPath, textFileConfig.DbFolderPath, textFileConfig.DbFileName + textFileConfig.DbFileSeparator + textFileConfig.DbFileExtension });
        public string GetArsiveFileFullPath(int fileNumber)
            => Path.Combine(new string[] { textFileConfig.DbPath, textFileConfig.DbFolderPath, textFileConfig.DbFileName + "_" + fileNumber + textFileConfig.DbFileSeparator + textFileConfig.DbFileExtension });



        private List<DataLogFileModel> GetFileListArchive()
        {
            var fileListFull = GetFileListFull();
            fileListFull.Remove(fileListFull.FirstOrDefault(x => x.FileName == textFileConfig.DbFileName));
            return fileListFull;
        }
        private List<DataLogFileModel> GetFileListFull()
        {
            var fileList = new List<DataLogFileModel>();
            string[] files = Directory.GetFiles(Path.Combine(new string[] { textFileConfig.DbPath, textFileConfig.DbFolderPath }));

            foreach (var item in files)
            {
                var fileName = Path.GetFileNameWithoutExtension(item);
                var dataCheck = new DataLogFileModel();
                dataCheck.FullPath = item;
                dataCheck.FileName = fileName.Split(textFileConfig.DbFileSeparator)[0];

                var fileNameSplit = fileName.Split(textFileConfig.DbArchiveFileSeparator);
                if (fileNameSplit.Length > 1)
                    dataCheck.Number = Convert.ToInt32(fileNameSplit[1]);
                else
                    dataCheck.Number = 2000000000;

                fileList.Add(dataCheck);
            }

            return fileList;
        }

        private List<T> GroupingData(List<T> datas)
            => datas.GroupBy(g => g.Id)
            .Select(g => g.OrderByDescending(t => t.DataStatusType).OrderByDescending(t => t.ProcessTime).FirstOrDefault())
            .ToList();
        private List<T> ReadLineFile(List<string> filePaths)
        {
            List<T> list = new List<T>();
            foreach (var item in filePaths)
                list.AddRange(ReadLineFile(item));

            return list;
        }
        private List<T> ReadLineFile(string filePath)
        {
            List<T> list = new List<T>();

            using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
            {
                string row;
                while ((row = sr.ReadLine()) != null)
                {
                    var rowData = JsonConvert.DeserializeObject<T>(row);
                    list.Add(rowData);
                }
            }

            return list;
        }


        public FileStream FileStreamCreate(string pathFull, int? bufferSize)
        {
            if (bufferSize == null || bufferSize <= 0)
                bufferSize = 32768;

            FileShare fileShare = FileShare.ReadWrite | FileShare.Delete;

            return new FileStream(
                pathFull,
                FileMode.OpenOrCreate,
                FileAccess.ReadWrite,
                fileShare,
                bufferSize.Value);
        }
        public StreamWriter StreamWriterCreate(FileStream fileStream)
            => new StreamWriter(fileStream);
        private Mutex MutexCreate(bool initiallyOwned, string name)
            => new Mutex(initiallyOwned, name);
        #endregion




        public bool DataGroupingAndSize()
        {
            //tüm dosya listesini aldım
            var fullFileList = GetFileListFull();
            if (fullFileList == null || fullFileList.Count == 0)
                return true;

            //tüm verileri aldım
            var paths = fullFileList.Select(x => x.FullPath).ToList();
            var fullData = ReadLineFile(paths);

            //tüm veriyi grupladım
            var groupDatas = GroupingData(fullData);

            //temp dosyası var ise sildim ve yenisini oluşturdum
            var tempFile = GetArsiveFileFullPath(0);
            File.Delete(tempFile);
            File.Create(tempFile);

            //grupladığım veriyi temp dosyasına yazdım
            using (FileStream fs = FileStreamCreate(tempFile, 32768))
            {
                using (StreamWriter sw = StreamWriterCreate(fs))
                {
                    foreach (var item in groupDatas)
                        sw.WriteLine(JsonConvert.SerializeObject(item, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
                }
            }

            //temp dosyası hariç tüm dosyaları sildim
            var deleteFileList = fullFileList.Where(x => x.FullPath != tempFile).ToList();
            foreach (var item in deleteFileList)
                File.Delete(item.FullPath);


            //temp dosyasını 1 numaralı arşiv dosyasına taşıdım ve arşiv numarasını 2 olarak değiştirdim
            var arsiveFile = GetArsiveFileFullPath(1);
            File.Move(tempFile, arsiveFile);
            arsiveFileCounter = 2;

            return true;
        }
    }
}
