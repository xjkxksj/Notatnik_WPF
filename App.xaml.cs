using System.Configuration;
using System.Data;
using System;
using System.Windows;
using System.Windows.Forms; // Dodane dla NotifyIcon
using System.Drawing; // Dodane dla Icon
using Application = System.Windows.Application;

namespace Notatnik_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private NotifyIcon notifyIcon;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            CreateNotifyIcon();
        }

        private void CreateNotifyIcon()
        {
            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = new Icon("notebook.ico"); // Użyj swojej ikony
            notifyIcon.Text = "NotebookWPF";
            notifyIcon.Visible = true;

            // Dodanie menu kontekstowego
            var contextMenu = new System.Windows.Forms.ContextMenuStrip();
            contextMenu.Items.Add("Open", null, OnOpenClicked);
            contextMenu.Items.Add("Exit", null, OnExitClicked);
            notifyIcon.ContextMenuStrip = contextMenu;

            // Obsługa podwójnego kliknięcia
            notifyIcon.DoubleClick += NotifyIcon_DoubleClick;
        }

        private void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            ShowMainWindow();
        }

        private void OnOpenClicked(object sender, EventArgs e)
        {
            ShowMainWindow();
        }

        private void OnExitClicked(object sender, EventArgs e)
        {
            notifyIcon.Visible = false;
            Application.Current.Shutdown();
        }

        private void ShowMainWindow()
        {
            var mainWindow = Application.Current.MainWindow;
            if (mainWindow != null)
            {
                mainWindow.Show();
                mainWindow.WindowState = WindowState.Normal;
                mainWindow.Activate();
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            notifyIcon.Visible = false;
            notifyIcon.Dispose();
            base.OnExit(e);
        }
    }

}
