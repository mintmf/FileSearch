namespace FileSearch
{
    internal class UserSettings: AppSettings<UserSettings>
    {
        public string Directory { get; set; }

        public string FileNameRegex { get; set; }
    }
}
