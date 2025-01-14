﻿using System;

namespace NSE.Core.Messages.Integrations
{
    public class PedidoBaixadoEstoqueIntegrationEvent : IntegrationEvent
    {
        public PedidoBaixadoEstoqueIntegrationEvent(Guid clienteId, Guid pedidoId)
        {
            ClienteId = clienteId;
            PedidoId = pedidoId;
        }

        public Guid ClienteId { get; private set; }
        public Guid PedidoId { get; private set; }
    }
}
