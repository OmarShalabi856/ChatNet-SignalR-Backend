using ChatNet_Backend.DataService;
using ChatNet_Backend.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatNet_Backend.Hubs
{
    public class ChatHub:Hub
    {
        public readonly SharedDb _sharedDb;

        public ChatHub(SharedDb sharedDb)=>_sharedDb = sharedDb;

        public async Task JoinChat(UserConnection conn)
        {
            
            await Clients.All
                .SendAsync("ReceiveMessage","admin",$"{conn.Username} Has Joined The Chat!");
        }

        public async Task JoinSpecificChatRoom(UserConnection conn)
        {
            _sharedDb.connections[Context.ConnectionId] = conn;
            await Groups.AddToGroupAsync(Context.ConnectionId, conn.Chatroom);
            await Clients.Group(conn.Chatroom)
                .SendAsync("ReceiveMessage", "admin", $"{conn.Username} Has Joined The Chat Room!");
        }

        public async Task SendMessage(string msg)
        {
            if(_sharedDb.connections.TryGetValue(Context.ConnectionId, out var conn))
            {
                await Clients.Group(conn.Chatroom).SendAsync("ReceiveSpecificMessage", conn.Username, msg);
            }
        }
    }
}
