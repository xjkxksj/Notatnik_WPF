using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;


namespace Notatnik_WPF;

internal class SearchFiltersViewModel
{
    private DateTime? fromDate;
    public DateTime? FromDate
    {
        get { return fromDate; }
        set
        {
            fromDate = value;
            filterValues.FromDate = fromDate;
            Messenger.Send("SetFilters", filterValues);
        }
    }

    private DateTime? toDate;
    public DateTime? ToDate
    {
        get { return toDate; }
        set
        {
            toDate = value;
            filterValues.ToDate = toDate;
            Messenger.Send("SetFilters", filterValues);
        }
    }
    private Category selectedCategory;

    public Category SelectedCategory
    {
        get { return selectedCategory; }
        set 
        { 
            selectedCategory = value;
            filterValues.Category = selectedCategory;
            Messenger.Send("SetFilters", filterValues);
        }
    }
    private string tags;
    public string Tags
    {
        get { return tags; }
        set
        {
            string cleanedInput = Regex.Replace(value, "[ ,;.$%^*()]+", " ");
            string[] parts = cleanedInput.Split(' ');

            // Remove duplicates
            parts = parts.Distinct().ToArray();

            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i].Length > 0 && !parts[i].StartsWith("#"))
                    parts[i] = "#" + parts[i];
            }

            string output = string.Join(" ", parts);

            filterValues.Tags = new List<Tag>();
            foreach (var tag in parts)
            {
                if(tag.Length > 1) //dont add empty tags and # symbol
                    filterValues.Tags.Add(new Tag() { Name = tag });
            }
            Messenger.Send("SetFilters", filterValues);

            tags = output;
        }
    }


    public ObservableCollection<Category> Categories { get; set; } = new ObservableCollection<Category>();
    public ICommand OpenAddCategoryCommand { get; set; }
    public ICommand DeleteCategoryCommand { get; set; }
    private FilterValues filterValues = new FilterValues();
    public SearchFiltersViewModel()
    {
        OpenAddCategoryCommand = new RelayCommand(OpenAddCategory);
        DeleteCategoryCommand = new RelayCommand(DeleteCategory);
        Messenger.Subscribe<List<Category>>("CategoryChanged", CategoryChanged);
        Messenger.Subscribe<string>("ChangeSearch", ChangeSearch);
    }
    private void ChangeSearch(string searchText)
    {
        filterValues.SearchText = searchText;
        Messenger.Send("SetFilters", filterValues);
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
        selectedCategory = categories[0];
    }   
    private void OpenAddCategory()
    {
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
        if(Categories.Select(x => x.Name).Contains(category))
        {
            MessageBox.Show("Category already exists");
            return;
        }
            

        Messenger.Send("CreateCategory", category);
    }
}