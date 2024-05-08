using ReactiveUI;

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

}