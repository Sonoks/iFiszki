using System.Windows.Input;

namespace iFiszki;

public partial class MainPage : ContentPage
{
    private FileDataViewModel ViewModel { get; set; }

    public MainPage()
	{
		InitializeComponent();
        ViewModel = new FileDataViewModel();
        BindingContext = ViewModel;
    }

    private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var item = e.SelectedItem as FileData;
        Page page = (Page)Activator.CreateInstance(typeof(DictionaryPage), item);
        await Navigation.PushAsync(page);
    }

    private async void AddBtn_Clicked(object sender, EventArgs e)
    {
        var fileName = await DisplayPromptAsync("Nowy plik", "Podaj nazwę pliku");
        ViewModel.AddCommand.Execute(fileName);
    }
}

