using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DynamicData;
using ReactiveUI;
using ReceiptOverview.Logic;

namespace ReceiptOverview.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private CentralLogic Logic { get; }

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
                this.AutofillEntry();
                this.RaiseAndSetIfChanged(ref _currentEntry, value);
            }
        }
        public PositionViewModel CurrentPosition
        {
            get => _currentPosition;
            set
            {
                LoadEntries();
                if (value == null)
                    value = new PositionViewModel();
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
        public ICommand CheckDbConnectionCommand { get; }

        public Interaction<SimpleMessageBoxViewModel, bool?> ShowDialog { get; }

        public MainWindowViewModel()
        {
            Logic = new CentralLogic();

            MainUiActive = true;
            CanDeleteEntry = false;
            CanDeletePosition = false;

            Positions = new ObservableCollection<PositionViewModel>();
            CurrentPosition = new PositionViewModel();
            CurrentEntry = new EntryViewModel();
            ShowDialog = new Interaction<SimpleMessageBoxViewModel, bool?>();

            RemovePositionCommand = ReactiveCommand.CreateFromTask(async () => await RemovePosition());
            RemoveEntryCommand = ReactiveCommand.CreateFromTask(async () => await RemoveEntry());

            NewPositionCommand = ReactiveCommand.Create(() => CreateNewPosition());
            NewEntryCommand = ReactiveCommand.Create(() => SaveEntry());
            SaveCommand = ReactiveCommand.Create(() => Save());
            ExportToCsvCommand = ReactiveCommand.Create(() => ExportToCsv());
            CheckDbConnectionCommand = ReactiveCommand.Create(async () => await CheckDbConnection());

            LoadPositions();
        }

        private void LoadPositions()
        {
            var data = Logic.GetPositions();
            foreach (var model in data)
            {
                PositionViewModel vm = new();
                vm.ModelToVm(model);
                this.Positions.Add(vm);
            }

            // save guard
            foreach (var position in Positions)
            {
                if (position.Entries == null)
                    position.Entries = new();
            }

            CurrentPosition = Positions.FirstOrDefault();
        }

        // Called, when CurrentPosition changes
        private void LoadEntries()
        {
            try
            {
                if (CurrentPosition == null || CurrentPosition.Id == 0)
                    return;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"In MainWindowViewModel: {ex.Message}");
                return;
            }

            CurrentPosition.Entries = new();
            var data = Logic.GetEntriesForPosition(CurrentPosition.Id);
            foreach (var model in data)
            {
                EntryViewModel vm = new();
                vm.ModelToVm(model);
                CurrentPosition.Entries.Add(vm);
            }
        }

        private void CreateNewPosition()
        {
            // if the month and year were set from a previous position, continue to use them
            // this makes entering several receipts from the same month of the same year easier
            int previousMonth = 1;
            int previousYear = 2000;
            if (CurrentPosition != null && CurrentPosition.Id != 0)
            {
                previousMonth = CurrentPosition.Date.Month;
                previousYear = CurrentPosition.Date.Year;
            }

            CurrentPosition = new();
            CurrentPosition.Id = 0;
            CurrentPosition.Date = new DateViewModel(1, previousMonth, previousYear);
            CurrentPosition.Total = string.Empty;
            CurrentPosition.Entries = new ObservableCollection<EntryViewModel>();

            int positionId = Logic.SavePosition(CurrentPosition.VmToModel());
            CurrentPosition.Id = positionId;

            CreateEmptyCurrentEntry(positionId);

            Positions.Add(CurrentPosition);
            CanDeletePosition = true;
        }

        private void SaveEntry()
        {
            int entryId = Logic.SaveEntry(CurrentEntry.VmToModel());
            EntryViewModel entryWithId = CurrentEntry;
            entryWithId.Id = entryId;
            // the entry was saved and we got the id.
            // Before reseting the field, the id in the CurrentPosition.Entries needs to be updated
            CurrentPosition.Entries.Replace(CurrentEntry, entryWithId);
            CurrentPosition.Total = CurrentPosition.Entries.Sum(s => Convert.ToDecimal(s.Price)).ToString();
            
            // reset entry fields
            CreateEmptyCurrentEntry(CurrentPosition.Id);

            CanDeleteEntry = true;
        }

        private async Task RemovePosition()
        {
            if (CurrentPosition == null! || CurrentPosition.Id == 0)
                return;

            MainUiActive = false;

            bool confirm = await ShowDeleteDialog("Delete Position",
                $"Are you sure, you want to delete the position {CurrentPosition.Id}?");

            if (!confirm)
                return;

            // Code to remove a position from the datasource, and then from the collection
            Logic.DeletePosition(CurrentPosition.VmToModel());
            this.Positions.Remove(CurrentPosition);
            MainUiActive = true;
        }

        private async Task RemoveEntry()
        {
            if (CurrentEntry == null! || CurrentEntry.Id == 0)
                return;

            MainUiActive = false;

            SimpleMessageBoxViewModel dialog = new("Delete Entry",
                $"Are you sure, you want to delete the entry {CurrentEntry.Id}?");
            await ShowDialog.Handle(dialog);
            bool confirm = dialog.Result;

            if (!confirm)
                return;

            // Code to remove an entry from the db, and then from the collection
            Logic.DeleteEntry(CurrentEntry.VmToModel());
            CurrentPosition.Entries.Remove(CurrentEntry);

            MainUiActive = true;
        }

        private void Save()
        {
            Logic.SavePosition(CurrentPosition.VmToModel());

            foreach (var entry in CurrentPosition.Entries)
            {
                if (entry.Id == 0)
                    continue;
                Logic.SaveEntry(entry.VmToModel());
            }
        }

        private void ExportToCsv()
        {
        }

        private async Task CheckDbConnection()
        {
            bool canConnect = Logic.CheckDbConnection();
            var title = "Checking connection to the Database";
            var message = canConnect ? "Connection successfull" : GetConnectionErrorMessage();

            SimpleMessageBoxViewModel dialog = new(title, message, false);
            await ShowDialog.Handle(dialog);
        }

        private string GetConnectionErrorMessage()
        {
            StringBuilder errorMessage = new();
            errorMessage.AppendLine("Connection failed. Please verify that");
            errorMessage.AppendLine("   - /Data/data.db exists");
            errorMessage.AppendLine(
                "   - all tables were created. Run 'create_db.sql' on an existing, but empty data.db file to create tables.");
            errorMessage.AppendLine("('create_db.sql' can be found in the /Data/ directory of the application)");
            return errorMessage.ToString();
        }

        private async Task<bool> ShowDeleteDialog(string title, string message)
        {
            SimpleMessageBoxViewModel dialog = new(title, message);
            await ShowDialog.Handle(dialog);
            return dialog.Result;
        }

        private void AutofillEntry()
        {
            if (CurrentPosition == null || CurrentPosition.Entries == null || CurrentEntry == null)
                return;

            // check, if there is any entry with the same Item already stored in other Positions, then take the first
            //  occurence and fill the other fields.
            // do not use the entry ID for the search, as each entry needs to be a unique database entity, because 
            //  they differ in their PositionId.
            try
            {
                if (Positions.Any(a => a.Entries.Any(b => b.Item == CurrentEntry.Item)))
                {
                    // CurrentEntry.Item = Positions.Select(s => s.Entries.Select(s => s.Item).First()).First();
                    CurrentEntry.Category = Positions.Select(s => s.Entries.Select(t => t.Category).First()).First();
                    CurrentEntry.Price = Positions.Select(s => s.Entries.Select(t => t.Price).First()).First();
                }
            }
            catch (InvalidOperationException)
            {
                return;
            }
        }

        private void CreateEmptyCurrentEntry(int positionId)
        {
            CurrentEntry = new();
            CurrentEntry.Id = 0;
            CurrentEntry.PositionId = positionId;
            CurrentPosition.Entries.Add(CurrentEntry);
        }
    }
}