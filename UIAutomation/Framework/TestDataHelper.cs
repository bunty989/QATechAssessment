using CsvHelper;

namespace UIAutomation.Framework
{
    public class TestDataHelper
    {
            public static string GetTestDataPath(string testDataFileName)
            {
                var type = typeof(TestDataHelper);
                var path = new FileInfo(type.Assembly.Location);
                var MyEnvironment = path.DirectoryName?.ToString();
                var dataPath = Path.Combine(MyEnvironment, "Data", testDataFileName);
                return dataPath;
            }

            public static List<string> ReadInCSV(string dataFileName)
            {
                var absolutePath = GetTestDataPath(dataFileName);
                var result = new List<string>();

                using var csv = CsvReader.FromFile(absolutePath, new CsvReaderOptions
                {
                    HasHeader = false
                });
                {
                    csv.ReadHeader();
                    while (csv.Read())
                    {
                        for (int i = 0; i < csv.FieldCount; i++)
                        {
                            var value = csv.GetString(i);
                            result.Add(value);
                        }
                    }
                }
                return result;
            }
        }
    }
