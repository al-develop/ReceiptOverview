using System;
using ReactiveUI;

namespace ReceiptOverview.ViewModels;

public class DateViewModel : ViewModelBase
{
    private int _day;
    private int _month;
    private int _year;
    private string _date;


    public string Date
    {
        get => _date;
        set => this.RaiseAndSetIfChanged(ref _date, value);
    }


    public int Year
    {
        get => _year;
        set
        {
            this.RaiseAndSetIfChanged(ref _year, value);
            SetDateString();
        }
    }


    public int Month
    {
        get => _month;
        set
        {
            this.RaiseAndSetIfChanged(ref _month, value);
            SetDateString();
        }
    }


    public int Day
    {
        get => _day;
        set
        {
            this.RaiseAndSetIfChanged(ref _day, value);
            SetDateString();
        }
    }


    public DateViewModel(int day, int month, int year)
    {
        this.Day = day;
        this.Month = month;
        this.Year = year;
        SetDateString();
    }

    public DateViewModel(DateTime date)
    {
        this.Day = date.Day;
        this.Month = date.Month;
        this.Year = date.Year;
        SetDateString();
    }
    
    private void SetDateString() => Date = $"{Day}.{Month}.{Year}";
}