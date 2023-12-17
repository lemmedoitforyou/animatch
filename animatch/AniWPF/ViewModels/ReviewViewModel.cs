using AniBLL.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniWPF.ViewModels
{
    public class ReviewViewModel : INotifyPropertyChanged
    {
        private readonly IReviewService reviewServise;
        private readonly IUserService userService;
        private int id;

        public ReviewViewModel(IReviewService reviewService, IUserService userService, int id)
        {
            this.reviewServise = reviewService;
            this.userService = userService;
            this.id = id;
        }

        public string ReviewText
        {
            get { return this.reviewServise.GetById(this.id).Text; }
        }

        public string UserName
        {
            get { return this.userService.GetById(this.reviewServise.GetById(this.id).UserId).Name; }
        }

        public string UserPhoto
        {
            get { return this.userService.GetById(this.reviewServise.GetById(this.id).UserId).Photo; }
        }

        public string ReviewImage
        {
            get
            {
                if (this.reviewServise.GetById(this.id).Rate == 1)
                {
                    return "https://github.com/yuliiapalamar/animatch/blob/master/animatch/AniWPF/photo/SadSmile.png?raw=true";
                }
                else if (this.reviewServise.GetById(this.id).Rate == 2)
                {
                    return "https://github.com/yuliiapalamar/animatch/blob/master/animatch/AniWPF/photo/NormSmile.png?raw=true";
                }
                else if (this.reviewServise.GetById(this.id).Rate == 3)
                {
                    return "https://github.com/yuliiapalamar/animatch/blob/master/animatch/AniWPF/photo/HappySmile.png?raw=true";
                }
                else
                {
                    return "";
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
