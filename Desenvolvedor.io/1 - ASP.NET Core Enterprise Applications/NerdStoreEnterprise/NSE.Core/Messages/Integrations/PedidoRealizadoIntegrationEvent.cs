using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSE.Core.Messages.Integrations
{
    public class PedidoRealizadoIntegrationEvent : IntegrationEvent
    {
        public PedidoRealizadoIntegrationEvent(Guid clienteId)
        {
            ClienteId = clienteId;
        }

        public Guid ClienteId { get ; private set; }
    }
}
