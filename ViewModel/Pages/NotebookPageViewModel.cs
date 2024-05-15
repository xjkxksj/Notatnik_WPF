﻿using Microsoft.Win32;
using Notatnik_WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Notatnik_WPF;
internal class NotebookPageViewModel
{
    public Repository Repository { get; set; } = Repository.Instance;
    public ICommand AddNoteCommand { get; set; }

    public NotebookPageViewModel()
    {
        AddNoteCommand = new RelayCommand(AddNote);
    
    }
    private void AddNote()
    {
        AddNoteDialogWindowViewModel addNoteVM = new AddNoteDialogWindowViewModel();
        AddNoteDialogWindow addNoteView = new AddNoteDialogWindow();

        addNoteView.DataContext = addNoteVM;
        addNoteView.ShowDialog();
    }
}
