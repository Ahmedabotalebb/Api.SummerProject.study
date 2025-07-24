using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;

namespace Service.Specefications
{
    abstract class BaseSpecification<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        protected BaseSpecification(Expression<Func<TEntity, bool>>? critereaExpression)
        {
            Criteria = critereaExpression;
        }

        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }

        #region Include
        public List<Expression<Func<TEntity, object>>> IncludeExpression { get; } = [];

        public void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            IncludeExpression.Add(includeExpression);
        }
        #endregion
        #region Sorting
        public Expression<Func<TEntity, object>> OrederBy { get; private set; }
        public Expression<Func<TEntity, object>> OrederByDesc { get; private set; }

        protected void AddOrderBy(Expression<Func<TEntity, object>> OrderExp) => OrederBy = OrderExp;
        protected void AddOrderByDesc(Expression<Func<TEntity, object>> OrderDescExp) => OrederByDesc = OrderDescExp;


        #endregion

        #region Pagenation
        

        public int Take { get; private set; } 
        public int Skip { get; private set; } 
        public bool IsPagenated { get; set ;} 

        public void ApplyPagenation(int pageSize , int pageIndex)
        {
            IsPagenated = true;
            Take = pageSize;
            Skip=(pageIndex-1)*pageSize;
        }
        #endregion
    }
}
