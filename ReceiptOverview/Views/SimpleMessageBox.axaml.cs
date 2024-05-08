using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace ReceiptOverview.Views
{
    public partial class SimpleMessageBox : Window
    {
        public bool Result { get; set; }
        public SimpleMessageBox(string title, string message)
        {
            InitializeComponent();
            this.Title = title;
            this.TxtMessage.Text = message;
        }
        private void BtnConfirm_OnClick(object? sender, RoutedEventArgs e)
        {
            this.Result = true;
            Close();
        }

        private void BtnCancel_OnClick(object? sender, RoutedEventArgs e)
        {
            this.Result = false;
            Close();
        }
    }
}