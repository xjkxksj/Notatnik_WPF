using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;


namespace Notatnik_WPF
{
    internal class SearchFiltersViewModel
    {
        public ObservableCollection<Category> Categories { get; set; } = new ObservableCollection<Category>();
        public ICommand OpenAddCategoryCommand { get; set; }
        public ICommand DeleteCategoryCommand { get; set; }

        public SearchFiltersViewModel()
        {
            OpenAddCategoryCommand = new RelayCommand(OpenAddCategory);
            DeleteCategoryCommand = new RelayCommand(DeleteCategory);
            Messenger.Subscribe<List<Category>>("CategoryChanged", CategoryChanged);
        }
        private void DeleteCategory(object catObject)
        {
            Category category = catObject as Category;
            Messenger.Send("CategoryDeleted", category);
        }
        private void CategoryChanged(List<Category> categories)
        {
            Categories.Clear();
            foreach (var category in categories)
            {
                Categories.Add(category);
            }
        }   
        private void OpenAddCategory()
        {
            Console.WriteLine("OpenAddCategory");
            AddCategoryDialogWindowViewModel addCategoryVM = new AddCategoryDialogWindowViewModel();
            AddCategoryDialogWindow addCategoryView = new AddCategoryDialogWindow();

            addCategoryVM.Close += () =>
            {
                AddCategoryToRepository(addCategoryVM.Category);
            };

            addCategoryView.DataContext = addCategoryVM;
            addCategoryView.ShowDialog();
        }

        private void AddCategoryToRepository(string category)
        {
            if(category == null)
                return;

            Console.WriteLine("AddCategoryToRepository");
            Messenger.Send("CreateCategory", category);
        }
    }
}