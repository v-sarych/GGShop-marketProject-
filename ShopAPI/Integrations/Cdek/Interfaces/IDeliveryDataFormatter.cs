using Integrations.Cdek.Entities.RegisterOrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrations.Cdek.Interfaces
{
    public interface IDeliveryDataFormatter
    {
        Task<RegisterOrder> GetOrder(Guid id);
    }
}
