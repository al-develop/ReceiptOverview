using System;
using ReactiveUI;
using ReceiptOverview.Models;

namespace ReceiptOverview.ViewModels;

public class EntryViewModel : ViewModelBase
{
    public event Action<string> ItemChanged;
    
    private int _id;
    private int _positionId;
    private string _item;
    private string _price;
    private string _category;

    public string Category
    {
        get => _category;
        set => this.RaiseAndSetIfChanged(ref _category, value);
    }

    public string Price
    {
        get
        {
            if (string.IsNullOrEmpty(_price))
                _price = 0.0m.ToString();
            return _price;
        }
        set => this.RaiseAndSetIfChanged(ref _price, value);
    }

    public string Item
    {
        get => _item;
        set
        {
            this.RaiseAndSetIfChanged(ref _item, value);
            ItemChanged?.Invoke(value);
        }
    }

    public int PositionId
    {
        get => _positionId;
        set => this.RaiseAndSetIfChanged(ref _positionId, value);
    }

    public int Id
    {
        get => _id;
        set => this.RaiseAndSetIfChanged(ref _id, value);
    }

    /// <summary>
    /// The current instance of EntryViewModel will be converted to a Entry model and returned
    /// </summary>
    /// <returns></returns>
    public Entry VmToModel()
    {
        Entry mapped = new Entry();
        mapped.Id = this.Id;
        mapped.PositionId = this.PositionId;
        mapped.Item = this.Item;
        mapped.Category = this.Category;
        mapped.Price = decimal.Parse(this.Price);
        return mapped;
    }

    /// <summary>
    /// the current instance of EntryViewModel will converted from a Entry Model
    /// </summary>
    public void ModelToVm(Entry entry)
    {
        this.Id = entry.Id;
        this.PositionId = entry.PositionId;
        this.Item = entry.Item;
        this.Category = entry.Category;
        this.Price = entry.Price.ToString();
    }
}