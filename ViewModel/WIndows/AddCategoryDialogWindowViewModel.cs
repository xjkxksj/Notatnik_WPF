using System.Windows.Input;

namespace Notatnik_WPF
{
    internal class AddCategoryDialogWindowViewModel : BaseViewModel, ICloseWindows
    {
        public Repository Repository { get; set; } = Repository.Instance;

        public ICommand AddCategoryToRepositoryCommand { get; set; }
        public Action Close { get; set; }

        public AddCategoryDialogWindowViewModel()
        {
            AddCategoryToRepositoryCommand = new RelayCommand(AddCategoryToRepository);
        }
        private void AddCategoryToRepository()
        {
            Repository.Categories.Add(new Category { Name = "New Category" });
            Close?.Invoke();
        }

        public bool CanClose()
        {
            return true;
        }
    }
}