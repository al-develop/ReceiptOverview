using System;
using System.Threading.Tasks;
using Avalonia.ReactiveUI;
using ReactiveUI;
using ReceiptOverview.ViewModels;

namespace ReceiptOverview.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    // private MainWindowViewModel vm;
    public MainWindow()
    {
        InitializeComponent();
        // vm = new MainWindowViewModel();
        // this.DataContext = vm;
        
        this.WhenActivated(action => action(ViewModel!.ShowDialog.RegisterHandler(ShowMessageBox)));
    }

    private async Task ShowMessageBox(InteractionContext<SimpleMessageBoxViewModel, bool?> interaction)
    {
        var dialog = new SimpleMessageBox();
        dialog.DataContext = interaction.Input;

        var result = await dialog.ShowDialog<bool?>(this);
        interaction.SetOutput(result);
    }
}