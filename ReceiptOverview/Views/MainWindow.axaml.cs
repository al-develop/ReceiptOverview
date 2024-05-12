using System;
using System.Threading.Tasks;
using Avalonia.Input;
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

    // avalonia ui HotKey Gestures only execute command-bound code, but not the code behind code
    // to implement the behaviour "save entry and set focus on TbxItemItem", the KeyDown on the window is used
    private void Window_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            if (BtnSaveEntry.Command != null && BtnSaveEntry.Command.CanExecute(null))
            {
                BtnSaveEntry.Command.Execute(null);
                TbxItemName.Focus();
                e.Handled = true;
            }
        }
    }
    
    private void TbxItemName_OnGotFocus(object? sender, GotFocusEventArgs e)
    {
        TbxItemName.SelectAll();
    }

    private void TbxCategory_OnGotFocus(object? sender, GotFocusEventArgs e)
    {
        TbxCategory.SelectAll();
    }

    private void TbxPrice_OnGotFocus(object? sender, GotFocusEventArgs e)
    {
        TbxPrice.SelectAll();
    }

    private void TbxDay_OnGotFocus(object? sender, GotFocusEventArgs e)
    {
        TbxDay.SelectAll();
    }

    private void TbxYear_OnGotFocus(object? sender, GotFocusEventArgs e)
    {
        TbxMonth.SelectAll();
    }

    private void TbxMonth_OnGotFocus(object? sender, GotFocusEventArgs e)
    {
        TbxYear.SelectAll();
    }

    private void BtnNewPos_OnClick(object? sender, RoutedEventArgs e)
    {
        TbxDay.Focus();
    }
}