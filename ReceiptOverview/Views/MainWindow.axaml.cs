using System;
using System.Threading.Tasks;
using Avalonia.Interactivity;
using Avalonia.ReactiveUI;
using ReactiveUI;
using ReceiptOverview.ViewModels;

namespace ReceiptOverview.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
        this.WhenActivated(action => action(ViewModel!.ShowDialog.RegisterHandler(ShowMessageBox)));
    }

    private async Task ShowMessageBox(InteractionContext<SimpleMessageBoxViewModel, bool?> interaction)
    {
        var dialog = new SimpleMessageBox();
        dialog.DataContext = interaction.Input;

        var result = await dialog.ShowDialog<bool?>(this);
        interaction.SetOutput(result);
    }

    private void BtnSaveEntry_OnClick(object? sender, RoutedEventArgs e)
    {
        TbxItemName.Focus();
    }
}