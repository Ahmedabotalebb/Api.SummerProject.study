using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Contracts
{
    public interface ISpecifications<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        //Where EXpression Condition is Criteria
        public Expression<Func<TEntity,bool>>? Criteria { get;}

        //INCLUDE 
        public List<Expression<Func<TEntity,object>>> IncludeExpression {  get;}
        public Expression<Func<TEntity,Object>> OrederBy { get;}
        public Expression<Func<TEntity,Object>> OrederByDesc { get; }

        public int Take { get;}
        public int Skip { get; } 
        public bool IsPagenated { get; set; }   
    }
}
