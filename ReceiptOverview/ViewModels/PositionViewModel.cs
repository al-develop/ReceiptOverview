using System;
using System.Collections.ObjectModel;
using ReactiveUI;

namespace ReceiptOverview.ViewModels;

public class PositionViewModel : ViewModelBase
{
    private int _id;
    private string _date;
    private string _total;
    private ObservableCollection<EntryViewModel> _entries;

    public ObservableCollection<EntryViewModel> Entries
    {
        get => _entries;
        set => this.RaiseAndSetIfChanged(ref _entries, value);
    }

    public string Total
    {
        get => _total;
        set => this.RaiseAndSetIfChanged(ref _total, value);
    }

    public string Date
    {
        get => _date;
        set => this.RaiseAndSetIfChanged(ref _date, value);
    }

    public int Id
    {
        get => _id;
        set => this.RaiseAndSetIfChanged(ref _id, value);
    }
}