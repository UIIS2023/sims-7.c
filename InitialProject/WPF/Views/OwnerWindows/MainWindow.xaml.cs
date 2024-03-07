using InitialProject.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace InitialProject.WPF.Views.OwnerWindows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int SelectedTab { get; set; }
        public RelayCommand NextTabCommand { get; set; }

        public RelayCommand PreviousTabCommand { get; set; }

        public RelayCommand LogOutCommand { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            NextTabCommand = new RelayCommand(NextTab, CanNextTab);
            PreviousTabCommand = new RelayCommand(PreviousTab, CanPreviousTab);
            LogOutCommand = new RelayCommand(Button_Click);
        }

        private void NextTab(object param)
        {
            TabControl1.SelectedIndex += 1;
        }

        private bool CanNextTab(object param)
        {
            return TabControl1.SelectedIndex != 5;
        }

        private bool CanPreviousTab(object param)
        {
            return TabControl1.SelectedIndex != 0;
        }

        private void PreviousTab(object param)
        {
            TabControl1.SelectedIndex -= 1;
        }


        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedTab = TabControl1.SelectedIndex;
        }

        private void Button_Click(object sender)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            this.Close();
        }
    }
    
}
