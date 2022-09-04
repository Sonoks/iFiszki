namespace iFiszki;

public partial class App : Application
{
    const int WindowWidth = 470;
    const int WindowHeight = 670;
    public App()
	{
		InitializeComponent();
        MainPage = new AppShell();
	}

    protected override Window CreateWindow(IActivationState activationState)
    {
        Window window = base.CreateWindow(activationState);


        // Manipulate Window object

        return window;
    }
}
