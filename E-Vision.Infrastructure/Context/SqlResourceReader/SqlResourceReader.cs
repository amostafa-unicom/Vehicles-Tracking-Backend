using System.IO;
using System.Text;

namespace E_Vision.Infrastructure.Context.SqlResourceReader
{
    public static class SqlResourceReader
    {
        public static string CreateSQLQuary(string filePath)
        {
            string sqlString = string.Empty;
            string path = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Substring(6), @"Context\SqlResources", filePath);
            using (var reader = new StreamReader(path, Encoding.UTF8))
                sqlString = reader.ReadToEnd();
            return sqlString;
        }
    }
}
