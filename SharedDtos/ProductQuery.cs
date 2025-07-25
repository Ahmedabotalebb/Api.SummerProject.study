using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductQuery
    {
        private const int DefualtPageSize = 5;
        private const int MaxSize = 10;

        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public ProductSortingOptions SortingOption { get; set; }
        public string? SearchValue { get; set; }

        private int pagesize = DefualtPageSize;

        public int PageSize
        {
            get { return pagesize; }
            set { pagesize = value > MaxSize? MaxSize : value; }
        }

        public int PageIndex { get; set; } = 1;
        public bool IsPagenated { get; set; }

    }
}
