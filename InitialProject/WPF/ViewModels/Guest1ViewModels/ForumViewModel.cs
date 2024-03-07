using InitialProject.Controller;
using InitialProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace InitialProject.WPF.ViewModels.Guest1ViewModels
{
    public class ForumViewModel : ViewModelBase, IClose
    {
        public Action Close { get; set; }

        private readonly CommentController _commentController;
        public ObservableCollection<Comment> Comments { get; set; }

        public User guest { get; set; }

        public Forum forum { get; set; }

        private string _comment;
        public string Comment
        {
            get => _comment;
            set
            {
                if (value != _comment)
                {
                    _comment = value;
                    OnPropertyChanged("Comment");
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public ForumViewModel(User user, Forum selectedForum)
        {
            guest = user;

            forum = selectedForum;

            _commentController = new CommentController();


            Comments = new ObservableCollection<Comment>(_commentController.GetByForumId(forum.Id));
            Comments.Clear();
            Comments = new ObservableCollection<Comment>(_commentController.GetByForumId(forum.Id));
        }


        private void Comment_Click()
        {
            if (string.IsNullOrEmpty(Comment))
            {
                MessageBox.Show("Niste uneli komentar!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (forum.IsOpen == false)
            {
                MessageBox.Show("Ne mozete dodati komentar na zatvorene forume!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Comment comment = new Comment() { Text = Comment, Author = guest, Role = guest.Type, ForumId = forum.Id, ReportsNumber = 0 };
                _commentController.Save(comment);
                forum.Comments.Add(comment);
                MessageBox.Show("Uspešno ste ostavili komentar na forumu!", "Komentar ostavljen!", MessageBoxButton.OK);
            }
        }

        public void CancelButton_Click()
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

        private ICommand _commentCommand;

        public ICommand CommentCommand
        {
            get
            {
                return _commentCommand ?? (_commentCommand = new CommandBase(() => Comment_Click(), true));
            }
        }
    }
}
