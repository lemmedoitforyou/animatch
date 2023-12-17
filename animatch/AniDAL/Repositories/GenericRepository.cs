using System.Linq.Expressions;
using AniDAL.DbContext;
using Microsoft.EntityFrameworkCore;

namespace AniDAL.Repositories
{
    public interface IGenericRepository<T>
        where T : class
    {
        IEnumerable<T> GetAll();

        T GetById(object id);

        void Insert(T obj);

        void Update(T obj);

        void Delete(int id);

        void Save();

        int GetLastId();
    }

    public class GenericRepository<T> : IGenericRepository<T>
        where T : class
    {
        protected ApplicationDbContext _context = null;

        protected DbSet<T> table = null;

        public GenericRepository()
        {
            this._context = new ApplicationDbContext();

            table = _context.Set<T>();
        }

        public GenericRepository(ApplicationDbContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public T GetById(object id)
        {
            return table.Find(id);
        }

        public void Insert(T obj)
        {
            table.Add(obj);
            _context.Entry(obj).State = EntityState.Added;

            Save();
        }

        public void Update(T obj)
        {
            var entry = _context.Entry(obj);

            if (entry.State == EntityState.Detached)
            {
                table.Attach(obj);
                entry.State = EntityState.Modified;
            }
            else
            {
                _context.Entry(obj).State = EntityState.Detached;
                table.Attach(obj);
                entry.State = EntityState.Modified;
            }
        }


        public void Delete(int id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public int GetLastId()
        {
            var dbSet = _context.Set<T>();

            var idProperty = typeof(T).GetProperty("Id");

            if (idProperty == null)
            {
                throw new InvalidOperationException("The entity does not have a property named 'Id'.");
            }

            var idType = idProperty.PropertyType;

            var parameter = Expression.Parameter(typeof(T), "e");
            var expression = Expression.Lambda<Func<T, int>>(Expression.Property(parameter, idProperty), parameter);

            var maxIdExpression = Expression.Call(
                typeof(Queryable),
                "Max",
                new Type[] { typeof(T), idType },
                Expression.Constant(dbSet),
                expression
            );

            var maxId = Expression.Lambda<Func<int>>(maxIdExpression).Compile()();

            return maxId;
        }
    }
}
