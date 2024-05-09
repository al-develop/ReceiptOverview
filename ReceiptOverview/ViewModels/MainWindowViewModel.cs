using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using ReceiptOverview.Models;

namespace ReceiptOverview.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private PositionViewModel _currentPosition;
        private EntryViewModel _currentEntry;
        private ObservableCollection<PositionViewModel> _positions;
        private bool _mainUiActive;
        private bool _canDeletePosition;
        private bool _canDeleteEntry;
        
        public ObservableCollection<PositionViewModel> Positions
        {
            get => _positions;
            set => this.RaiseAndSetIfChanged(ref _positions, value);
        }
        public EntryViewModel CurrentEntry
        {
            get => _currentEntry;
            set
            {
                this.CanDeleteEntry = true;
                this.RaiseAndSetIfChanged(ref _currentEntry, value);
            }
        }
        public PositionViewModel CurrentPosition
        {
            get => _currentPosition;
            set
            {
                this.CanDeletePosition = true;
                this.RaiseAndSetIfChanged(ref _currentPosition, value);
            }
        }
        public bool CanDeleteEntry
        {
            get => _canDeleteEntry;
            set => this.RaiseAndSetIfChanged(ref _canDeleteEntry, value);
        }
        public bool CanDeletePosition
        {
            get => _canDeletePosition;
            set => this.RaiseAndSetIfChanged(ref _canDeletePosition, value);
        }
        public bool MainUiActive
        {
            get => _mainUiActive;
            set => this.RaiseAndSetIfChanged(ref _mainUiActive, value);
        }
        public ICommand NewPositionCommand { get; }
        public ICommand RemovePositionCommand { get; }
        public ICommand NewEntryCommand { get; }
        public ICommand RemoveEntryCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand ExportToCsvCommand { get; }

        public Interaction<SimpleMessageBoxViewModel, bool?> ShowDialog { get; }
        
        public MainWindowViewModel()
        {
            MainUiActive = true;
            CanDeleteEntry = false;
            CanDeletePosition = false;
            
            Positions = new ObservableCollection<PositionViewModel>();
            CurrentPosition = new PositionViewModel();
            CurrentEntry = new EntryViewModel();
            ShowDialog = new Interaction<SimpleMessageBoxViewModel, bool?>();

            RemovePositionCommand = ReactiveCommand.CreateFromTask(async () => RemovePosition());
            RemoveEntryCommand = ReactiveCommand.Create(() => RemoveEntry());
            
            NewPositionCommand = ReactiveCommand.Create(() => CreateNewPosition());
            NewEntryCommand = ReactiveCommand.Create(() => CreateNewEntry());
            SaveCommand = ReactiveCommand.Create(() => Save());
            ExportToCsvCommand = ReactiveCommand.Create(() => ExportToCsv());
            
            LoadPositions();
        }

        private void LoadPositions()
        {
            Positions.Add(new PositionViewModel()
            {
                Id = 001,
                Date = new DateViewModel(12,5,2023),
                Entries = new ObservableCollection<EntryViewModel>(),
                Total = 12.99m.ToString("0.00 €")
            });
        }

        private void CreateNewPosition()
        {
            CanDeleteEntry = false;

            PositionViewModel newPosition = new();
            newPosition.Id = 0;
            newPosition.Date = new DateViewModel(1,1,2000);
            newPosition.Total = string.Empty;

            CurrentPosition = newPosition;
        }

        private async void RemovePosition()
        {
            if(CurrentPosition == null! || CurrentPosition.Id == 0)
                return;
            
            MainUiActive = false;

            bool confirm = await ShowDeleteDialog("Delete Position", $"Are you sure, you want to delete the position {CurrentPosition.Id}?");
            
            if(!confirm)
                return;
            
            // Code to remove a position from the datasource, and then from the collection
            
            MainUiActive = true;
        }

        private void CreateNewEntry()
        {
            EntryViewModel newEntry = new();
            newEntry.Id = 0;
            newEntry.PositionId = 0;
            newEntry.Item = string.Empty;
            newEntry.Category = string.Empty;
            newEntry.Price = string.Empty;
            CurrentEntry = newEntry;
        }

        private async void RemoveEntry()
        {
            if(CurrentEntry == null! || CurrentEntry.Id == 0)
                return;
            
            MainUiActive = false;

            SimpleMessageBoxViewModel dialog = new("Delete Entry", $"Are you sure, you want to delete the entry {CurrentEntry.Id}?");
            await ShowDialog.Handle(dialog);
            bool confirm = dialog.Result;
            
            if(!confirm)
                return;
            
            // Code to remove an entry from the db, and then from the collection
            
            MainUiActive = true;
        }

        private void Save()
        {
            // store all new Entries in the CurrentPosition
            // then, call BusinessLogic, to store CurrentPosition and its Entries in the DB
            // get latest Position Id and assign it to the CurrentPosition.Id
            // also assign it to all entries Position_Id
            // then, add CurrentPosition to the collection
        }

        private void ExportToCsv()
        {
        }

        private async Task<bool> ShowDeleteDialog(string title, string message)
        {
            SimpleMessageBoxViewModel dialog = new(title, message);
            await ShowDialog.Handle(dialog);
            return dialog.Result;
        }
    }
}