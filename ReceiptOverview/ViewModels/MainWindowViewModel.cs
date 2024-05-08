using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ReactiveUI;

namespace ReceiptOverview.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private PositionViewModel _currentPosition;
        private EntryViewModel _currentEntry;
        private ObservableCollection<PositionViewModel> _positions;

        public ObservableCollection<PositionViewModel> Positions
        {
            get => _positions;
            set => this.RaiseAndSetIfChanged(ref _positions, value);
        }

        public EntryViewModel CurrentEntry
        {
            get => _currentEntry;
            set => this.RaiseAndSetIfChanged(ref _currentEntry, value);
        }

        public PositionViewModel CurrentPosition
        {
            get => _currentPosition;
            set => this.RaiseAndSetIfChanged(ref _currentPosition, value);
        }

        private bool _canEdit;

        public bool CanEdit
        {
            get => _canEdit;
            set => this.RaiseAndSetIfChanged(ref _canEdit, value);
        }
        public ICommand NewPositionCommand { get; }
        public ICommand RemovePositionCommand { get; }
        public ICommand NewEntryCommand { get; }
        public ICommand RemoveEntryCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand ExportToCsvCommand { get; }

        public Func<int, bool> DialogAction { get; set; }
        
        public MainWindowViewModel()
        {
            CanEdit = true;
            
            Positions = new ObservableCollection<PositionViewModel>();
            CurrentPosition = new PositionViewModel();
            CurrentEntry = new EntryViewModel();

            NewPositionCommand = ReactiveCommand.Create(() => CreateNewPosition());
            RemovePositionCommand = ReactiveCommand.Create((object o) => RemovePosition(o));
            NewEntryCommand = ReactiveCommand.Create(() => CreateNewEntry());
            RemoveEntryCommand = ReactiveCommand.Create((object o) => RemoveEntry(o));
            SaveCommand = ReactiveCommand.Create(() => Save());
            ExportToCsvCommand = ReactiveCommand.Create(() => ExportToCsv());
            
            LoadPositions();
        }   


        private void LoadPositions()
        {
            Positions.Add(new PositionViewModel()
            {
                Id = 001,
                Date = DateTime.Now.ToString("dd.MM.yyyy"),
                Entries = new ObservableCollection<EntryViewModel>(),
                Total = 12.99m.ToString("0.00 €")
            });
        }

        private void CreateNewPosition()
        {
        }

        private void RemovePosition(object param)
        {
            CanEdit = false;
            bool confirm = DialogAction.Invoke(CurrentPosition.Id);
            CanEdit = true;
        }

        private void CreateNewEntry()
        {
        }

        private void RemoveEntry(object param)
        {
        }

        private void Save()
        {
        }

        private void ExportToCsv()
        {
        }
    }
}