using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IRequestItemRepository : IBaseRepository
    {
        bool DeleteRequestItem(RequestItem? requestItem);

        public RequestItem EditRequestItem(int requestItemId, RequestItem requestItem);

        public RequestItem AddRequestItem(RequestItem item);


    }
}
