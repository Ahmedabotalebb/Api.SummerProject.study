using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    static class SpecificationEvaluator
    {
        static public IQueryable<TEntity> CreateQuery<TEntity,TKey>(IQueryable<TEntity> InputQuery , ISpecifications<TEntity,TKey> specifications ) where TEntity : BaseEntity<TKey>
        {
            var Query = InputQuery;
            if(specifications.Criteria is not null)
            { 
                Query= Query.Where(specifications.Criteria);
            }

            if(specifications.OrederBy is  not null)
            {
                Query = Query.OrderBy(specifications.OrederBy);
            }
            if(specifications.OrederByDesc is  not null)
            {
                Query = Query.OrderByDescending(specifications.OrederByDesc);
            }

            if(specifications.IncludeExpression is not null && specifications.IncludeExpression.Count > 0)
            {
                Query = specifications.IncludeExpression.Aggregate(Query, (CurrentQuery, IncludeExp) => CurrentQuery.Include(IncludeExp));
            }

            if (specifications.IsPagenated)
            {
                Query=Query.Skip(specifications.Skip).Take(specifications.Take);
            }

            return Query;
        }
    }           
}
