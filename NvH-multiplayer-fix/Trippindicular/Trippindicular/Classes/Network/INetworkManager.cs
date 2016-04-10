using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

public interface INetworkManager : IDisposable
    {
        void Connect(string ipstring);
        void Disconnect();

        NetIncomingMessage ReadMessage();

        void Recycle(NetIncomingMessage im);

        NetOutgoingMessage CreateMessage();

        void SendMessage(string p);
    }

