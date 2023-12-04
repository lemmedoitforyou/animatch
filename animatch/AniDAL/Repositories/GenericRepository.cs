using AniDAL.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AniDAL.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
        void Save();
        int GetLastId();
    }

    //The following GenericRepository class Implement the IGenericRepository Interface
    //And Here T is going to be a class
    //While Creating an Instance of the GenericRepository type, we need to specify the Class Name
    //That is we need to specify the actual class name of the type T
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        //The following variable is going to hold the EmployeeDBContext instance
        protected ApplicationDbContext _context = null;

        //The following Variable is going to hold the DbSet Entity
        protected DbSet<T> table = null;

        //Using the Parameterless Constructor, 
        //we are initializing the context object and table variable
        public GenericRepository()
        {
            this._context = new ApplicationDbContext();

            //Whatever class name we specify while creating the instance of GenericRepository
            //That class name will be stored in the table variable
            table = _context.Set<T>();
        }

        //Using the Parameterized Constructor, 
        //we are initializing the context object and table variable
        public GenericRepository(ApplicationDbContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }

        //This method will return all the Records from the table
        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        //This method will return the specified record from the table
        //based on the ID which it received as an argument
        public T GetById(object id)
        {
            return table.Find(id);
        }

        //This method will Insert one object into the table
        //It will receive the object as an argument which needs to be inserted into the database
        public void Insert(T obj)
        {
            table.Add(obj);
            _context.Entry(obj).State = EntityState.Added;

            // Uncomment the following line to save changes immediately
            Save();
        }


        //This method is going to update the record in the table
        //It will receive the object as an argument
        public void Update(T obj)
        {
            //First attach the object to the table
            table.Attach(obj);
            //Then set the state of the Entity as Modified
            _context.Entry(obj).State = EntityState.Modified;
        }

        //This method is going to remove the record from the table
        //It will receive the primary key value as an argument whose information needs to be removed from the table
        public void Delete(object id)
        {
            //First, fetch the record from the table
            T existing = table.Find(id);
            //This will mark the Entity State as Deleted
            table.Remove(existing);
        }

        //This method will make the changes permanent in the database
        //That means once we call Insert, Update, and Delete Methods, 
        //Then we need to call the Save method to make the changes permanent in the database
        public void Save()
        {
            _context.SaveChanges();
        }

        public int GetLastId()
        {
            // Отримати DbSet для поточного типу T
            var dbSet = _context.Set<T>();

            // Переконатися, що сутність має властивість Id (можливо, вам слід вказати більш конкретний інтерфейс чи клас для T, що містить властивість Id)
            var idProperty = typeof(T).GetProperty("Id");

            if (idProperty == null)
            {
                throw new InvalidOperationException("The entity does not have a property named 'Id'.");
            }

            // Отримати тип Id
            var idType = idProperty.PropertyType;

            // Побудувати вираз для вибору максимального Id
            var parameter = Expression.Parameter(typeof(T), "e");
            var expression = Expression.Lambda<Func<T, int>>(Expression.Property(parameter, idProperty), parameter);

            // Створити вираз для виклику Max через Queryable
            var maxIdExpression = Expression.Call(
                typeof(Queryable),
                "Max",
                new Type[] { typeof(T), idType },
                Expression.Constant(dbSet),
                expression
            );

            // Скомпілювати вираз та викликати його
            var maxId = Expression.Lambda<Func<int>>(maxIdExpression).Compile()();

            return maxId;
        }

    }
}
