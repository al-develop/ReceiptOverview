using System.Reactive;
using System.Windows.Input;
using ReactiveUI;

namespace ReceiptOverview.ViewModels;

public class SimpleMessageBoxViewModel : ViewModelBase
{
    public bool Result { get; set; }

    private string _title;
    private string _message;
    private bool _cancelVisible;
    private int _btnConfirmColumnSpan;
    
    public int BtnConfirmColumnSpan
    {
        get => _btnConfirmColumnSpan;
        set => this.RaiseAndSetIfChanged(ref _btnConfirmColumnSpan, value);
    }

    public bool CancelVisible
    {
        get => _cancelVisible;
        set => this.RaiseAndSetIfChanged(ref _cancelVisible, value);
    }

    public string Message
    {
        get => _message;
        set => this.RaiseAndSetIfChanged(ref _message, value);
    }
    public string Title
    {
        get => _title;
        set => this.RaiseAndSetIfChanged(ref _title, value);
    }

    public ReactiveCommand<Unit, bool> ConfirmCommand { get; set; }
    public ReactiveCommand<Unit, bool> CancelCommand { get; set; }

    public SimpleMessageBoxViewModel()
    {
        
    }
    
    public SimpleMessageBoxViewModel(string title, string message, bool displayCancelButton = true)
    {
        this.CancelVisible = displayCancelButton;
        this.BtnConfirmColumnSpan = displayCancelButton ? 1 : 2;
        this.Title = title;
        this.Message = message;

        ConfirmCommand = ReactiveCommand.Create(() => { Result = true; return Result; });
        CancelCommand = ReactiveCommand.Create(() => { Result = true; return Result; });
    }
}