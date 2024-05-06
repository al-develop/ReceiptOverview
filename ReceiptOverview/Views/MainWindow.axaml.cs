using Avalonia.Controls;
using ReceiptOverview.ViewModels;

namespace ReceiptOverview.Views;

public partial class MainWindow : Window
{
    private MainWindowViewModel vm;
    public MainWindow()
    {
        InitializeComponent();
        this.DataContext = vm = new MainWindowViewModel();
    }
}