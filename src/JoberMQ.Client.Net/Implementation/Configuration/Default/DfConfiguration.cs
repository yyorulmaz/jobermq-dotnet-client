using JoberMQ.Client.Net.Abstraction.Configuration;
using JoberMQ.Client.Net.Constants;
using JoberMQ.Client.Net.Enums.Account;
using JoberMQ.Client.Net.Enums.Client;
using JoberMQ.Client.Net.Enums.Endpoint;
using JoberMQ.Client.Net.Enums.Protocol;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoberMQ.Client.Net.Implementation.Configuration.Default
{
    internal class DfConfiguration : IConfiguration
    {
        ClientFactoryEnum clientFactory = ClientConst.ClientFactory;
        public ClientFactoryEnum ClientFactory => clientFactory;
        ConnectProtocolEnum connectProtocol = ClientConst.ConnectProtocol;
        public ConnectProtocolEnum ConnectProtocol => connectProtocol;


        int connectionRetryTimeout = ClientConst.ConnectionRetryTimeout;
        int IConfiguration.ConnectionRetryTimeout { get => connectionRetryTimeout; set => connectionRetryTimeout = value; }


        bool isOfflineMode = ClientConst.IsOfflineMode;
        public bool IsOfflineMode { get => isOfflineMode; set => isOfflineMode = value; }



        string userName = ClientConst.UserName;
        public string UserName { get => userName; set => userName = value; }
        string password = ClientConst.Password;
        public string Password { get => password; set => password = value; }


        EndpoindFactoryEnum endpoindFactor = ClientConst.EndpoindFactory;
        public EndpoindFactoryEnum EndpoindFactory => endpoindFactor;
        AccountFactoryEnum accountFactory = ClientConst.AccountFactory;
        public AccountFactoryEnum AccountFactory => accountFactory;
        string hostName = ClientConst.HostName;
        string IConfiguration.HostName { get => hostName; set => hostName = value; }

        int port = ClientConst.Port;
        int IConfiguration.Port { get => port; set => port = value; }

        int portSsl = ClientConst.PortSsl;
        int IConfiguration.PortSsl { get => portSsl; set => portSsl = value; }

        bool isSsl = ClientConst.IsSsl;
        bool IConfiguration.IsSsl { get => isSsl; set => isSsl = value; }



    }
}
