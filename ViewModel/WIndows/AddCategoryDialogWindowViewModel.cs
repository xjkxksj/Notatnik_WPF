using System.Windows.Input;

namespace Notatnik_WPF
{
    internal class AddCategoryDialogWindowViewModel : BaseViewModel, ICloseWindows
    {
        public string Category { get; set; }

        public ICommand AddCategoryToRepositoryCommand { get; set; }
        public Action Close { get; set; }

        public AddCategoryDialogWindowViewModel()
        {
            AddCategoryToRepositoryCommand = new RelayCommand(AddCategoryToRepository);
        }
        private void AddCategoryToRepository()
        {
            Close?.Invoke();
        }

        public bool CanClose()
        {
            return true;
        }
    }
}