using System.IO;
using System.Reflection;

namespace JoberMQ.Client.Common.Helpers
{
    public class ResourceFileHelper
    {
        public static string GetString(Assembly ass, string folderName, string fileName)
        {
            using (var str = GetResource(ass, folderName, fileName))
            {
                if (str == null)
                    return null;

                using (var strR = new StreamReader(str, System.Text.Encoding.UTF8))
                {
                    return strR.ReadToEnd();
                }
            }
        }

        public static Stream GetResource(Assembly ass, string folderName, string fileName)
        {
            string path;
            var assName = ass.GetName().Name;
            if (folderName != null)
                path = $"{assName}.{folderName}.{fileName}";
            else
                path = $"{assName}.{fileName}";

            return ass.GetManifestResourceStream(path);
        }
    }
}
