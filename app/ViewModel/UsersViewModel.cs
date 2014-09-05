using app.Model;
using gimnasio.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace app.ViewModel
{
    class UsersViewModel : BaseViewModel
    {
        Users usersModel;
        public RelayCommand SaveCommand { get; set; }
        
        ObservableCollection<User> _users;
        public ObservableCollection<User> users
        {
            get
            {
                _users = new ObservableCollection<User>(usersModel.All());
                return _users;
            }
        }

        object _selectedUser;
        public object selectedUser
        {
            get
            {
                return _selectedUser;
            }
            set
            {
                if (_selectedUser != value)
                {
                    _selectedUser = value;
                    OnPropertyChanged("selectedUser");
                }
            }
        }

        BindingGroup _updateBindingGroup;
        public BindingGroup updateBindingGroup
        {
            get
            {
                return _updateBindingGroup;
            }
            set
            {
                if (_updateBindingGroup != value)
                {
                    _updateBindingGroup = value;
                    OnPropertyChanged("updateBindingGroup");
                }
            }
        }

        void Save(object param)
        {
            selectedUser = new User();
        }

        public UsersViewModel()
        {
            usersModel = new Users();

            updateBindingGroup = new BindingGroup { Name = "usersGroup" };

            SaveCommand = new RelayCommand(Save);

            selectedUser = new User();
        }
    }
}
