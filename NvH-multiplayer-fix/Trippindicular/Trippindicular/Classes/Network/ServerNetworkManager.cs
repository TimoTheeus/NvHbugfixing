using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;
using Lidgren.Network.Xna;

namespace Trippindicular.Classes
{
    class ServerNetworkManager : INetworkManager
    {
        NetServer netServer;
        private bool isDisposed;
        public void Connect(string ipstring)
        {
            var config = new NetPeerConfiguration("Asteroid")
            {
                Port = Convert.ToInt32("14242"),
                SimulatedMinimumLatency = 0.1f, 
                //SimulatedLoss = 0.1f 
            };
            config.EnableMessageType(NetIncomingMessageType.WarningMessage);
            config.EnableMessageType(NetIncomingMessageType.VerboseDebugMessage);
            config.EnableMessageType(NetIncomingMessageType.ErrorMessage);
            config.EnableMessageType(NetIncomingMessageType.Error);
            config.EnableMessageType(NetIncomingMessageType.DebugMessage);
            //config.EnableMessageType(NetIncomingMessageType.ConnectionApproval);
            config.EnableMessageType(NetIncomingMessageType.Data);
            netServer = new NetServer(config);
            netServer.Start();
        }

        /// <summary>
        /// The create message.
        /// </summary>
        /// <returns>
        /// </returns>
        public NetOutgoingMessage CreateMessage()
        {
            return this.netServer.CreateMessage();
        }

        /// <summary>
        /// The disconnect.
        /// </summary>
        public void Disconnect()
        {
            this.netServer.Shutdown("Bye");
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }

        /// <summary>
        /// The read message.
        /// </summary>
        /// <returns>
        /// </returns>
        public NetIncomingMessage ReadMessage()
        {
            return this.netServer.ReadMessage();
        }

        /// <summary>
        /// The recycle.
        /// </summary>
        /// <param name="im">
        /// The im.
        /// </param>
        public void Recycle(NetIncomingMessage im)
        {
            this.netServer.Recycle(im);
        }

        /// <summary>
        /// The send message.
        /// </summary>
        /// <param name="gameMessage">
        /// The game message.
        /// </param>
        public void SendMessage(String gameMessage)
        {
            NetOutgoingMessage om = this.netServer.CreateMessage();
            om.Write(gameMessage);
            //gameMessage.Encode(om);
            if (this.netServer.Connections.Count<NetConnection>() > 0)
            {
                NetConnection conn = this.netServer.Connections.First<NetConnection>();
                this.netServer.SendMessage(om, conn, NetDeliveryMethod.ReliableUnordered);
            }
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
