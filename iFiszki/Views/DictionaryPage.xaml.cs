namespace iFiszki;

public partial class DictionaryPage : ContentPage
{
    public DictionaryPage(FileData file)
	{
		InitializeComponent();

		BindingContext = new RowsViewModel(file);
        oryginalWord.Focus();
    }
	 
	private void BeginBtn_Clicked(object sender, EventArgs e)
	{

	}

	private void AddBtn_Clicked(object sender, EventArgs e)
	{
		oryginalWord.Focus();
    }
}
