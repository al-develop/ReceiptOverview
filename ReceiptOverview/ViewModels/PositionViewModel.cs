using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        set
        {
            this.RaiseAndSetIfChanged(ref _entries, value);
        }
    }
    public string Total
    {
        get
        {
            if (string.IsNullOrEmpty(_total))
                _total = 0.0m.ToString();
            return _total;
        }
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

    public Position VmToModel()
    {
        Position mapped = new Position();
        mapped.Id = this.Id;
        mapped.Date = new DateTime(Date.Year, Date.Month, Date.Day);
        mapped.Total = decimal.Parse(this.Total);
        mapped.Entries = new List<Entry>();
        
        foreach (var entryViewModel in this.Entries)
        {
            Entry entry = entryViewModel.VmToModel();
            mapped.Entries.Add(entry);
        }

        return mapped;
    }

    public void ModelToVm(Position position)
    {
        this.Id = position.Id;
        this.Date = new DateViewModel(position.Date);
        this.Total = position.Total.ToString();
        this.Entries = new();
    }
}