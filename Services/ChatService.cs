﻿using System.Collections.Generic;
using System.Linq;

namespace GTCOAgileCOEPortal.Services
{
    public class ChatService
    {
        private static readonly Dictionary<string, string> Users = new Dictionary<string, string>();

        public bool AddUserToList(string userToAdd)
        {
            lock (Users)
            {
                foreach (var user in Users)
                {
                    if (user.Key.ToLower() == userToAdd.ToLower())
                    {
                        return false;
                    }
                }
            }

            Users.Add(userToAdd, null);
            return true;
        }

        //public string GetUserByConnectionId(string connectionId)
        public string GetUserIdByConnectionId(string connectionId)
        {
            lock (Users)
            {
                return Users.Where(x => x.Value == connectionId).Select(x => x.Key).FirstOrDefault();
            }
        }

        public void RemoveUserFromList(string userId)
        {
            lock (Users)
            {
                if (Users.ContainsKey(userId))
                {
                    Users.Remove(userId);
                }
            }
        }

        public string[] GetOnlineUsers()
        {
            lock (Users)
            {
                return Users.OrderBy(x => x.Key).Select(x => x.Key).ToArray();
            }
        }

        //public void AddUserConnectionId(string user, string connectionId)
        public void AddUserConnectionId(string userId, string connectionId)
        {
            lock (Users)
            {
                if (Users.ContainsKey(userId))
                {
                    if (Users[userId] != null)
                    {
                        return;
                    }
                    Users[userId] = connectionId;
                }
            }

        }

        //public string GetConnectionIdByUser(string user)
        public string GetConnectionIdByUserId(string userId)
        {
            lock (Users)
            {
                return Users.Where(x => x.Key == userId).Select(x => x.Value).FirstOrDefault();
            }
        }
    }
}
