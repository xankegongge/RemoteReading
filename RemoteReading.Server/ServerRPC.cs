using System;
using System.Collections.Generic;
using System.Text;
using Thrift;
using Thrift.Collections;
using Thrift.Protocol;
using Thrift.Transport;
using Thrift.Server;
using ESPlus.Application.CustomizeInfo.Server;
using ESPlus.Application.CustomizeInfo;
using ESPlus.Application.Basic.Server;
using ESPlus.Application;
using ESFramework.Server.UserManagement;
using ESPlus.Rapid;
using ESPlus.Serialization;
using System.Drawing;
using ESFramework;
using ESBasic.Security;
namespace RemoteReading.Server
{
    class ServerRPC
    {
        private GlobalCache globalCache;
        private IRapidServerEngine rapidServerEngine;
        public ServerRPC()
        {

        }
        public ServerRPC(GlobalCache bi,IRapidServerEngine rapid)
        {
            this.globalCache = bi;
            this.rapidServerEngine = rapid;
        }
        public void Start()
        {
            TServerSocket serverTransport = new TServerSocket(4532, 0, false);
            if (this.globalCache != null)
            {
                BusinessImpl bi = new BusinessImpl(this.globalCache, this.rapidServerEngine);
                RegisterRPC.Processor processor = new RegisterRPC.Processor(bi);
                TServer server = new TSimpleServer(processor, serverTransport);
                Console.WriteLine("Starting server on port 4532 ...");
                server.Serve();
            }
            else
            {
                Console.WriteLine("starting failed!");
            }
        } 
    }
}
