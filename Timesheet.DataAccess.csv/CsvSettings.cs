namespace Timesheet.DataAccess.csv
{
    public class CsvSettings
    {
        public CsvSettings(char delimiter, string path)
        {
            Delimeter = delimiter;
            Path = path;
        }
        public char Delimeter { get; }
        public string Path { get; }
    }
}
