using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;
using System.Net;

namespace Trippindicular.Classes
{
    class ClientNetworkManager : INetworkManager
    {
        NetClient netClient;
        /// <summary>
        /// The is disposed.
        /// </summary>
        private bool isDisposed;

        public void Connect(string ipstring)
        {
            var config = new NetPeerConfiguration("Asteroid")
            {
                //SimulatedMinimumLatency = 0.2f, 
                //SimulatedLoss = 0.1f
            };
            config.EnableMessageType(NetIncomingMessageType.WarningMessage);
            config.EnableMessageType(NetIncomingMessageType.VerboseDebugMessage);
            config.EnableMessageType(NetIncomingMessageType.ErrorMessage);
            config.EnableMessageType(NetIncomingMessageType.Error);
            config.EnableMessageType(NetIncomingMessageType.DebugMessage);
            //config.EnableMessageType(NetIncomingMessageType.ConnectionApproval);

            this.netClient = new NetClient(config);
            this.netClient.Start();

            this.netClient.Connect(new IPEndPoint(NetUtility.Resolve(ipstring), Convert.ToInt32("14242")));
        }

 /// <summary>
        /// The disconnect.
        /// </summary>
        public void Disconnect()
        {
            this.netClient.Disconnect("Bye");
        }

        /// <summary>
        /// The read message.
        /// </summary>
        /// <returns>
        /// </returns>
        public NetIncomingMessage ReadMessage()
        {
            return this.netClient.ReadMessage();
        }
        /// <summary>
        /// The recycle.
        /// </summary>
        /// <param name="im">
        /// The im.
        /// </param>
        public void Recycle(NetIncomingMessage im)
        {
            this.netClient.Recycle(im);
        }

        /// <summary>
        /// The create message.
        /// </summary>
        /// <returns>
        /// </returns>
        public NetOutgoingMessage CreateMessage()
        {
            return this.netClient.CreateMessage();
        }

        public void SendMessage(string gameMessage)
        {
            NetOutgoingMessage om = this.netClient.CreateMessage();
            om.Write(gameMessage);
            //gameMessage.Encode(om);
            if (this.netClient.Connections.Count<NetConnection>() > 0)
            {
                NetConnection conn = this.netClient.Connections.First<NetConnection>();
                this.netClient.SendMessage(om, NetDeliveryMethod.ReliableUnordered);
            }
        }


        public void Dispose()
        {
            this.Dispose(true);
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        /// <param name="disposing">
        /// The disposing.
        /// </param>
        private void Dispose(bool disposing)
        {
            if (!this.isDisposed)
            {
                if (disposing)
                {
                    this.Disconnect();
                }

                this.isDisposed = true;
            }
        }

    }
}
