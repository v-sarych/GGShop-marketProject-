using Integrations.Cdek.Entities.DeliveryCalculationData;
using Integrations.Cdek.Entities.RegisterOrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrations.Cdek.Interfaces
{
    public interface IDeliveryDataCreator
    {
        Task<RegisterOrder> CreateRegisterOrderObject(Guid id);
        Task<DeliveryWidgetCalculationDataDTO> GetDataForWidget(Guid id);
    }
}
