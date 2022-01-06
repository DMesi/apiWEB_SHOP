using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiWS.Models
{
    public class RequestParams
    {
        const int maxPageSize = 10;
        public int PageNumber { get; set; } = 1; // ukoliko nije navedeno drugacije bice 1 default

        private int _pageSize = 10;

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }

        }
    }
}
