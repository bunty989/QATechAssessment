using CsvHelper;

namespace UIAutomation.Framework
{
    public class TestDataHelper
    {
        public static string GetTestDataPath(string testDataFileName)
        {
            var type = typeof(TestDataHelper);
            var path = new FileInfo(type.Assembly.Location);
            var MyEnvironment = path.DirectoryName.ToString();
            var dataPath = Path.Combine(MyEnvironment, "Data", testDataFileName);
            return dataPath;
        }

        public static List<string> ReadInCSV(string dataFileName)
        {
            var absolutePath = GetTestDataPath(dataFileName);
            var result = new List<string>();

            using (var reader = new StreamReader(absolutePath))
            using (var csv = new CsvReader(reader, 
                new CsvHelper.Configuration.CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false
            }))
            {
                while (csv.Read())
                {
                    for (int i = 0; i < csv.Parser.Count; i++)
                    {
                        var value = csv.GetField(i);
                        result.Add(value);
                    }
                }
            }
            return result;
        }
    }
}
