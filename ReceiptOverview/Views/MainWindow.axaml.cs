using System;
using Avalonia.Controls;
using ReceiptOverview.ViewModels;

namespace ReceiptOverview.Views;

public partial class MainWindow : Window
{
    public MainWindowViewModel MainVM { get; set; }
    public MainWindow()
    {
        InitializeComponent();
        this.DataContext = MainVM = new MainWindowViewModel();
        this.MainVM.DialogAction = new Func<int, bool>(ShowDialog);
    }

    private bool ShowDialog(int positionId)
    {
        SimpleMessageBox box = new SimpleMessageBox($"Delete Position {positionId}", $"Are you sure, you want to delete the Position {positionId}?");
        box.ShowDialog(this);
        return box.Result;
    }
}