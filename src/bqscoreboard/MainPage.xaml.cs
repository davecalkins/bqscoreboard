namespace bqscoreboard;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
        (BindingContext as MainPageViewModel)?.Initialize();
    }

    private void TeamNameChanged(object sender, TextChangedEventArgs e)
    {
        (BindingContext as MainPageViewModel)?.OnTeamNameChanged();
    }
}

