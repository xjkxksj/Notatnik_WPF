using System.Windows.Input;

namespace Notatnik_WPF
{
    internal class SearchFiltersViewModel
    {
        public Repository Repository { get; set; } = Repository.Instance;
        public ICommand AddCategoryCommand { get; set; }

        public SearchFiltersViewModel()
        {
            AddCategoryCommand = new RelayCommand(AddCategory);
        }

        private void AddCategory()
        {
            AddCategoryDialogWindowViewModel addCategoryVM = new AddCategoryDialogWindowViewModel();
            AddCategoryDialogWindow addCategoryView = new AddCategoryDialogWindow();

            addCategoryView.DataContext = addCategoryVM;
            addCategoryView.ShowDialog();
        }
    }
}