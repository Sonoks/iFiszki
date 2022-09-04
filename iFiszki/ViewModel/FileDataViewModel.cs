using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace iFiszki
{
    public class FileDataViewModel: INotifyPropertyChanged
    {
        private readonly string _directory = "D:\\OneDrive\\Dokumenty\\iFiszki";

        public event PropertyChangedEventHandler PropertyChanged;

        public IList<FileData> Files { get; private set; }

        public ICommand AddCommand { get; private set; }
        public ICommand SelectedCommand { get; private set; }
        public string FileName { get; private set; }

        public FileDataViewModel()
        {
            var direcoryInfo = new DirectoryInfo(_directory);
            var files = direcoryInfo
                .GetFiles("*.json")
                .ToList()
                .Select(e => new FileData 
                { 
                    Name = e.Name.Replace(".json", ""),
                    Path = e.FullName,
                })
                .ToList();

            Files = new ObservableCollection<FileData>(files);

            AddCommand = new Command<string>(
               execute: (string x) => AddFile(x));
        }

        void Selected(string fileName)
        {
            FileName = fileName;
            OnPropertyChanged(nameof(FileName));
        }

        public void AddFile(string fileName)
        {
            if (!Files.Any(e => e.Name == fileName))
            {
                var path = $"{_directory}\\{fileName}.json";
                File.Create(path);
                var file = new FileData
                {
                    Name = fileName,
                    Path = path,
                };
                Files.Add(file);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
                ((Command)AddCommand).ChangeCanExecute();
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
