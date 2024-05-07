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


        public ICommand NewPositionCommand { get; }
        public ICommand RemovePositionCommand { get; }
        public ICommand NewEntryCommand { get; }
        public ICommand RemoveEntryCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand ExportToCsvCommand { get; }

        public MainWindowViewModel()
        {
            Positions = new ObservableCollection<PositionViewModel>();
            CurrentPosition = new PositionViewModel();
            CurrentEntry = new EntryViewModel();

            NewPositionCommand = ReactiveCommand.Create(() => CreateNewPosition());
            RemovePositionCommand = ReactiveCommand.Create(() => RemovePosition());
            NewEntryCommand = ReactiveCommand.Create(() => CreateNewEntry());
            RemoveEntryCommand = ReactiveCommand.Create(() => RemoveEntry());
            SaveCommand = ReactiveCommand.Create(() => Save());
            ExportToCsvCommand = ReactiveCommand.Create(() => ExportToCsv());
            
            LoadPositions();
        }   


        private void LoadPositions()
        {
        }

        private void CreateNewPosition()
        {
        }

        private void RemovePosition()
        {
        }

        private void CreateNewEntry()
        {
        }

        private void RemoveEntry()
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