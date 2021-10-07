// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Infrastructure.Constants.Application;
using CleanArchitecture.Razor.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using SmartAdmin.WebUI.Extensions;

namespace SmartAdmin.WebUI.Hubs
{
    public class SignalRHub : Hub
    {
        
        private static readonly ConcurrentDictionary<string, bool> _onlineUsers = new ConcurrentDictionary<string, bool>();
        private readonly ICurrentUserService _currentUserService;
        private readonly UserManager<ApplicationUser> _userManager;

        public SignalRHub(
            ICurrentUserService currentUserService,
            UserManager<ApplicationUser> userManager
            )
        {
            _currentUserService = currentUserService;
            _userManager = userManager;
        }
        public override async Task OnConnectedAsync()
        {
            var userId = _currentUserService.UserId;
            if (userId is not null)
            {
               if( _onlineUsers.TryAdd(userId, true))
                {
                    var user =await _userManager.FindByIdAsync(userId);
                    if(user is not null)
                    {
                        user.IsLive = true;
                        await _userManager.UpdateAsync(user);
                        await Clients.All.SendAsync(ApplicationConstants.SignalR.ConnectUser, new { user.Id, user.DisplayName });
                    }
                    
                }

                await UpdateOnlineUsers();
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = _currentUserService.UserId;
            if (userId is not null)
            {
                //try to remove key from dictionary
                if (!_onlineUsers.TryRemove(userId, out _))
                {
                    //if not possible to remove key from dictionary, then try to mark key as not existing in cache
                    _onlineUsers.TryUpdate(userId, false, true);
                }
                else
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if(user is not null)
                    {
                        user.IsLive = false;
                        await _userManager.UpdateAsync(user);
                        await Clients.All.SendAsync(ApplicationConstants.SignalR.DisconnectUser, new { user.Id, user.DisplayName });
                    }
                   
                   
                }

                await UpdateOnlineUsers();
            }

            await base.OnDisconnectedAsync(exception);
        }

        private Task UpdateOnlineUsers()
        {
            var count = GetOnlineUsersCount();
            return Clients.All.SendAsync("UpdateOnlineUsers", count);
        }

        public static int GetOnlineUsersCount()
        {
            return _onlineUsers.Count(p => p.Value);
        }
    }
}
