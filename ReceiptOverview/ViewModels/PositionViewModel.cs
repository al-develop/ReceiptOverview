using System;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices.JavaScript;
using ReactiveUI;
using ReceiptOverview.Models;

namespace ReceiptOverview.ViewModels;

public class PositionViewModel : ViewModelBase
{
    private int _id;
    private string _total;
    private ObservableCollection<EntryViewModel> _entries;
    private DateViewModel _date;
    
    public DateViewModel Date
    {
        get => _date;
        set => this.RaiseAndSetIfChanged(ref _date, value);
    }
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

    public int Id
    {
        get => _id;
        set => this.RaiseAndSetIfChanged(ref _id, value);
    }

    public DateTime ToDateTime() => new(Date.Year, Date.Month, Date.Day);
    public void FromDateTime(DateTime date)
    {
        Date.Year = date.Year;
        Date.Month = date.Month;
        Date.Day = date.Day;
    }
}