using System;

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
