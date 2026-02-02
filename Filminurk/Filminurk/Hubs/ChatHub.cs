using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Filminurk.Core.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;


namespace Filminurk.Hubs
{
    public class ChatHub : Hub
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ChatHub(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task SendMessage(string message)
        {
            var user = await _userManager.GetUserAsync(Context.User);

            string userName = user?.DisplayName ?? "User";

            await Clients.All.SendAsync("ReceiveMessage", userName, message);
        }
    }
}