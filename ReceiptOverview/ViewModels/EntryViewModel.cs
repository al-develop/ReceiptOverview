using ReactiveUI;
using ReceiptOverview.Models;

namespace ReceiptOverview.ViewModels;

public class EntryViewModel : ViewModelBase
{
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
        get => _price;
        set => this.RaiseAndSetIfChanged(ref _price, value);
    }

    public string Item
    {
        get => _item;
        set => this.RaiseAndSetIfChanged(ref _item, value);
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
    
    public void ModelToVm(Entry entry)
    {
        this.Id = entry.Id;
        this.PositionId = entry.PositionId;
        this.Item = entry.Item;
        this.Category = entry.Category;
        this.Price = entry.Price.ToString();
    }
}