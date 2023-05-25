using AkiraBot.UI.Core;
using AkiraBot.UI.MVVM.ViewModels.UserControls;

namespace AkiraBot.UI.MVVM.ViewModels.Windows;

public class ApplicationWindowVM : ObservableObject
{
    private string _captionName;
    private ObservableObject _mainContent;
    
    public ApplicationWindowVM()
    {
        AvailableExchangesCommand = new BaseCommand(OpenAvailableExchanges);
        HomeViewCommand = new BaseCommand(OpenHomeView);
        OpenHomeView();
    }
    
    public string CaptionName
    {
        get => _captionName;
        set => Set(ref _captionName, value, nameof(CaptionName));
    }

    public ObservableObject MainContent
    {
        get => _mainContent;
        set => Set(ref _mainContent, value, nameof(MainContent));
    }
    
     
    public BaseCommand AvailableExchangesCommand { get;  set; }
    public BaseCommand HomeViewCommand { get; set; }

    private void OpenAvailableExchanges(object? args = null)
    {
        ChangeScreenFrame(new AvailableExchangesVM(), "Available Exchanges");
    }
    
    private void OpenHomeView(object? args = null)
    {
        ChangeScreenFrame(new HomeViewVM(), "Home");
    }

    private void ChangeScreenFrame(ObservableObject view, string captionName)
    {
        MainContent = view;
        CaptionName = captionName;
    }
}