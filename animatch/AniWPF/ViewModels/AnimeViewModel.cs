using AniBLL.Models;
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
            get { return this.animeService.GetById(this.id).Imdbrate; }
        }

        public double AnimeRateForLine
        {
            get { return this.animeService.GetById(this.id).Imdbrate * 40; }
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

        public List<string> AnimeGenres
        {
            get
            {
                return this.animeGenreService.GetGenresForAnime(this.id);
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            AnimeViewModel other = (AnimeViewModel)obj;
            return id == other.id;
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
