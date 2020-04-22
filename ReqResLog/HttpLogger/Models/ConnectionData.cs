﻿using Microsoft.AspNetCore.Http;

namespace ReqResLog.HttpLogger.Models
{
    internal class ConnectionData
    {
        public string Id { get; set; }

        public string LocalIpv4 { get; set; }

        public int LocalPort { get; set; }

        public string RemoteIpv4 { get; set; }

        public int RemotePort { get; set; }

        public static ConnectionData Create(ConnectionInfo connection)
        {
            return new ConnectionData
            {
                Id = connection.Id,
                LocalIpv4 = connection.LocalIpAddress.MapToIPv4().ToString(),
                LocalPort = connection.LocalPort,
                RemoteIpv4 = connection.RemoteIpAddress.MapToIPv4().ToString(),
                RemotePort = connection.RemotePort,
            };
        }
    }
}
