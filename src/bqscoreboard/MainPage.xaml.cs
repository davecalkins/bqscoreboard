namespace bqscoreboard;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
        (BindingContext as MainPageViewModel)?.Initialize();
    }
}

