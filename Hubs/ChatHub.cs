using GTCOAgileCOEPortal.Data;
using GTCOAgileCOEPortal.Dtos;
using GTCOAgileCOEPortal.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace GTCOAgileCOEPortal.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ChatService _chatService;
        private readonly Database _db;
        public string ProductOwnerGroup = "ProductOwner";
        public string EnterpriseCoachGroup = "EnterpriseCoach";
        public string ScrumMastersGroup = "ScrumMasters";
        public string Developers = "Developers";

        public ChatHub(ChatService chatService, Database db)
        {
            _chatService = chatService;
            _db = db;
        }

        public async Task AddUserConnectionId(string userId)
        {
            _chatService.AddUserConnectionId(userId, Context.ConnectionId);
            await DisplayOnlineUsers();
        }

        public async Task CreateProductOwnerChat()
        {
            await CreateGroupChat(ProductOwnerGroup);
        }

        public async Task ReceiveProductOwnerChatMessage(MessageDto message)
        {
            await ReceiveGroupMessage(ProductOwnerGroup, message);
            //message.Timestamp = DateTime.Now;
             _db.ProdOwnerMessHistory.Add(message);
        }

         //helper funcion
        public async Task CreateGroupChat(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Caller.SendAsync("UserConnected");
        }

        //helper function
        public async Task ReceiveGroupMessage(string groupName, MessageDto message)
        {
            await Clients.Group(groupName).SendAsync($"New{groupName}Message", message);
        }

        //helper function
        private async Task DisplayOnlineUsers()
        {
            var onlineUsers = _chatService.GetOnlineUsers();
            await Clients.Groups("GeneralGroup").SendAsync("OnlineUsers", onlineUsers);
        }
    }
}
