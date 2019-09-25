using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CsvHelper;
using NUnit.Framework;

namespace APITest.Library
{
    public class TestDataHelper
    {
        public static string GetTestDataPath(string testDataFileName)
        {
            Type type = typeof(TestDataHelper);
            FileInfo path = new FileInfo(type.Assembly.Location);
            var MyEnvironment = path.DirectoryName.ToString();
            var dataPath = Path.Combine(MyEnvironment,"Data", testDataFileName);
            return dataPath;
        }

        public static List<string> ReadInCSV(string dataFileName)
        {
            var absolutePath = GetTestDataPath(dataFileName);
            List<string> result = new List<string>();
            string value;
            using (TextReader fileReader = File.OpenText(Path.GetFullPath(absolutePath)))
            {
                var csv = new CsvReader(fileReader);
                csv.Configuration.HasHeaderRecord = false;
                while (csv.Read())
                {
                    for (int i = 0; csv.TryGetField<string>(i, out value); i++)
                    {
                        result.Add(value);
                    }
                }
            }
            return result;
        }
    }
}
