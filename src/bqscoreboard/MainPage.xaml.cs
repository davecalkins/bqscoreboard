namespace bqscoreboard;

public partial class MainPage : ContentPage
{
    private MainPageViewModel _viewModel;

	public MainPage()
	{
		InitializeComponent();
        _viewModel = BindingContext as MainPageViewModel;
        _viewModel?.Initialize();
    }

    private void TeamNameChanged(object sender, TextChangedEventArgs e)
    {
        _viewModel?.OnTeamNameChanged();
    }

    private void Button_OnPressed(object sender, EventArgs e)
    {
        _viewModel?.OnButtonPressed();
    }

    private void Button_OnReleased(object sender, EventArgs e)
    {
        _viewModel?.OnButtonReleased();
    }
}

