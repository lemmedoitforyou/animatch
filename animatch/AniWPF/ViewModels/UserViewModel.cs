using AniBLL.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniWPF.ViewModels
{
    public class UserViewModel : INotifyPropertyChanged
    {
        private readonly IUserService userService;
        private int id;

        public UserViewModel(IUserService userService, int id)
        {
            this.userService = userService;
            this.id = id;
        }

        public string UserName
        {
            get { return this.userService.GetById(this.id).Name; }
            set { }
        }

        public string UserText
        {
            get { return this.userService.GetById(this.id).Text; }
            set { }
        }

        public string UserLevel
        {
            get
            {
                int level = this.userService.GetById(this.id).Level;
                switch (level)
                {
                    case 1:
                        return "новачок";
                    case 2:
                        return "досвічений анімешник";
                    case 3:
                        return "любитель конкретних жанрів";
                    default:
                        return "лох";
                }
            }
            set { }
        }

        public string UserPhoto
        {
            get
            {
                return this.userService.GetById(this.id).Photo;
            }
        }

        public int UserWachedCount
        {
            get
            {
                return this.userService.GetById(this.id).WatchedCount;
            }
            set { }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
