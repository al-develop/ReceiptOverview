using System.Reactive;
using System.Windows.Input;
using ReactiveUI;

namespace ReceiptOverview.ViewModels;

public class SimpleMessageBoxViewModel : ViewModelBase
{
    public bool Result { get; set; }

    private string _title;
    private string _message;
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
    
    public SimpleMessageBoxViewModel(string _title, string _message)
    {
        this.Title = _title;
        this.Message = _message;

        ConfirmCommand = ReactiveCommand.Create(() => { Result = true; return Result; });
        CancelCommand = ReactiveCommand.Create(() => { Result = true; return Result; });
    }
}