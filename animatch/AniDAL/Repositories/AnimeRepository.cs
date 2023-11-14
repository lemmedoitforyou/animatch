using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniDAL.DataBaseClasses;
using AniDAL.DbContext;

namespace AniDAL.Repositories
{
    public interface IAnimeRepository: IGenericRepository<Anime>
    {
    }
    public class AnimeRepository : GenericRepository<Anime>, IAnimeRepository
    {
    }
}
