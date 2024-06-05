using System.Windows.Input;

namespace Notatnik_WPF
{
    internal class AddCategoryDialogWindowViewModel : BaseViewModel, ICloseWindows
    {
        public string Category { get; set; }

        public ICommand AddCategoryToRepositoryCommand { get; set; }
        public Action Close { get; set; }
        public bool IsDialogCanceled { get; set; } = true;

        public AddCategoryDialogWindowViewModel()
        {
            AddCategoryToRepositoryCommand = new RelayCommand(AddCategoryToRepository);
        }
        private void AddCategoryToRepository()
        {
            IsDialogCanceled = false;
            Close?.Invoke();
        }

        public bool CanClose()
        {
            return true;
        }
    }
}