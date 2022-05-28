using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using WebApplication3;



namespace WebApplication3.Repositories
{

    public abstract class AbstractRepository<TEntity, TKey>
        where TEntity : class
    {

        public abstract List<TEntity> GetAll();
        public abstract TEntity GetById(TKey id);
        public abstract void Save(TEntity entity);
        public abstract void Update(TEntity entity);
        public abstract void Delete(TEntity entity);
        public abstract void DeleteById(TKey id);
    }
}
