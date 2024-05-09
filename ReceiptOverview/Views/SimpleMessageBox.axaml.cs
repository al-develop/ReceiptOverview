using Avalonia.Controls;
using Avalonia.ReactiveUI;
using ReactiveUI;
using ReceiptOverview.ViewModels;
using System;

namespace ReceiptOverview.Views
{
    public partial class SimpleMessageBox : ReactiveWindow<SimpleMessageBoxViewModel>
    {
        public SimpleMessageBox()
        {
            InitializeComponent();
            if (Design.IsDesignMode) 
                return;
            
            this.WhenActivated(action => action(ViewModel!.ConfirmCommand.Subscribe(Close)));
            this.WhenActivated(action => action(ViewModel!.CancelCommand.Subscribe(Close)));
        }

        private void Close(bool messageBoxResult)
        {
            this.Close();
        }
    }
}