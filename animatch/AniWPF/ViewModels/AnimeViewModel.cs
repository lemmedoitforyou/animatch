using AniBLL.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniWPF.ViewModels
{
    public class AnimeViewModel : INotifyPropertyChanged
    {
        private readonly IAnimeService animeService;
        private readonly IAddedAnimeService addedAnime;
        private readonly IAnimeGenreService animeGenreService;

        private int id;

        public AnimeViewModel(IAnimeService animeService, IAddedAnimeService addedAnime, IAnimeGenreService animeGenreService, int id)
        {
            this.addedAnime = addedAnime;
            this.animeService = animeService;
            this.animeGenreService = animeGenreService;
            this.id = id;
        }

        public string AnimeName
        {
            get { return this.animeService.GetById(this.id).Name; }
        }

        public string AnimeText
        {
            get { return this.animeService.GetById(this.id).Text; }
        }

        public double AnimeRate
        {
            get { return Math.Round(this.animeService.GetById(this.id).Imdbrate, 2); }
        }

        public string AnimePhoto
        {
            get { return this.animeService.GetById(this.id).Photo; }
        }

        public int AnimeYear
        {
            get { return this.animeService.GetById(this.id).Year; }
        }

        public string UserLikedAnime
        {
            get { return $"{this.addedAnime.CountUserWhoAddAnime(this.id)} користувачів вподобали це аніме"; }
        }

        public string AnimeGenres
        {
            get
            {
                List<string> temp = this.animeGenreService.GetGenresForAnime(this.id);
                string result = "";
                foreach (string item in temp)
                {
                    result += item + " ";
                }
                return result;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
