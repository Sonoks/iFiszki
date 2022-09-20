using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Newtonsoft.Json;

namespace iFiszki
{
    public class RowsViewModel: INotifyPropertyChanged
    {
        private readonly FileData _file;

        public event PropertyChangedEventHandler PropertyChanged;

        public string DictionaryName { get; set; }
        public IList<Row> Rows { get; private set; }
        public ICommand AddCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }

        public string OriginalWord { get; set; } = "";
        public string TranslatedWord { get; set; } = "";

        public string StrA { get; set; }
        public string StrB { get; set; }

        public RowsViewModel(FileData file)
        {
            _file = file;
            ReadFile();
            DictionaryName = _file.Name;
            AddCommand = new Command(() => AddRow());
            SaveCommand = new Command(() => Save());
        }

        private void AddRow()
        {
            if (!string.IsNullOrEmpty(StrA) && !string.IsNullOrEmpty(StrB))
            {
                Rows.Add(new Row
                {
                    OriginalWord = StrA,
                    OriginalWordCounter = 0,
                    TranslatedWord = StrB,
                    TranslatedWordCounter = 0,
                });

                StrB = StrA = "";

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
                ((Command)AddCommand).ChangeCanExecute();
            }
        }

        private void Save()
        {
            var txt = JsonConvert.SerializeObject(Rows);
            File.WriteAllText(_file.Path, txt);
        }

        private void ReadFile()
        {
            var txt = File.ReadAllText(_file.Path);
            var rows = JsonConvert.DeserializeObject<List<Row>>(txt);

            Rows = rows != null
                ? new ObservableCollection<Row>(rows)
                : new ObservableCollection<Row>();
        }
    }
}
