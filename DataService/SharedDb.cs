using ChatNet_Backend.Models;
using System.Collections.Concurrent;

namespace ChatNet_Backend.DataService
{
    public class SharedDb
    {

        private readonly ConcurrentDictionary<string, UserConnection> _connections = new();
        public ConcurrentDictionary<string, UserConnection> connections => _connections;
    }
}
