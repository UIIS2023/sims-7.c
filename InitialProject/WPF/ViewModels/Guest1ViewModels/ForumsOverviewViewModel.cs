using InitialProject.Controller;
using InitialProject.Domain.Models;
using InitialProject.WPF.Views.Guest1Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace InitialProject.WPF.ViewModels.Guest1ViewModels
{
    public class ForumsOverviewViewModel : ViewModelBase, IClose
    {
        public Action Close { get; set; }

        private readonly ForumController _forumController;
        private readonly LocationController _locationController;

        public ObservableCollection<Forum> Forums { get; set; }
        public Forum SelectedForum { get; set; }

        public User guest { get; set; }

        public ForumsOverviewViewModel(User user)
        {

            guest = user;

            _forumController = new ForumController();
            _locationController = new LocationController();

            Forums = new ObservableCollection<Forum>(_forumController.GetAll());

            for (int i = 0; i < Forums.Count; ++i)
            {
                Forums[i].Location = _locationController.GetAll().Find(a => a.Id == Forums[i].Location.Id);

            }
        }

        private void ForumCreation_Click()
        {
            ForumCreationWindow forumCreationWindow = new ForumCreationWindow(guest);
            forumCreationWindow.Show();
        }

        private void Forum_Click()
        {
            if (SelectedForum == null)
            {
                MessageBox.Show("Prvo morate odabrati forum!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                ForumWindow forumWindow = new ForumWindow(guest, SelectedForum);
                forumWindow.Show();
            }
        }

        private void CancelButton_Click()
        {
            this.Close();
        }




        private ICommand _cancelCommand;

        public ICommand CancelCommand
        {
            get
            {
                return _cancelCommand ?? (_cancelCommand = new CommandBase(() => CancelButton_Click(), true));
            }
        }

        private ICommand _createForumCommand;
        public ICommand CreateForumCommand
        {
            get
            {
                return _createForumCommand ?? (_createForumCommand = new CommandBase(() => ForumCreation_Click(), true));
            }
        }

        private ICommand _forumCommand;
        public ICommand ForumCommand
        {
            get
            {
                return _forumCommand ?? (_forumCommand = new CommandBase(() => Forum_Click(), true));
            }
        }
    }
}
