using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace CompanyTask.Domain.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TResult> GetFilteredFirstOrDefault<TResult>(
     Expression<Func<TEntity, TResult>> select,
     Expression<Func<TEntity, bool>> where,
     Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby = null,
     Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null
);

        Task<List<TResult>> GetFilteredList<TResult>(
            Expression<Func<TEntity, TResult>> select,
            Expression<Func<TEntity, bool>> where,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null
            );

        /// <summary>
        /// Dinamik olarak entity listesi döndürür.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetDefaults(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// Dinamik olarak where işlemi sağlar.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<TEntity> GetDefault(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// Kayıt varsa true, yoksa false döndürür.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<bool> Any(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// Yeni Oluşturma <typeparamref name="TEntity" />
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> Add(TEntity entity);

        /// <summary>
        /// Güncelleme <typeparamref name="TEntity" />
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> Update(TEntity entity);

        /// <summary>
        /// Silme <typeparamref name="TEntity" />
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<bool> Delete(TEntity entity);

        /// <summary>
        /// Kaydetme
        /// </summary>
        /// <returns></returns>
        Task<int> Save();
    }
}
