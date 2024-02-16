using Limak.Application.DTOs.HubDTOs;
using Limak.Persistence.Utilities.Exceptions.Identity;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Limak.Persistence.Implementations.Hubs;

public class ChatHub:Hub
{
    public static List<UserConnectionDto> Connections = new();

    public override Task OnConnectedAsync()
    {
        AddConnectionId();
        return base.OnConnectedAsync();
    }


    public override Task OnDisconnectedAsync(Exception? exception)
    {

        RemoveConnectionIds();
        return base.OnDisconnectedAsync(exception);
    }
    public async Task SendMessage(string connectionId, string message)
    {
        await Clients.Client(connectionId).SendAsync("ReceiveChatMessage", message);
    }
    private void AddConnectionId()
    {
        var userIdClaim = Context.User?.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            throw new UnAuthorizedException();

        var userId = int.Parse(userIdClaim.Value);

        var connection = Connections.FirstOrDefault(x => x.UserId == userId);
        if (connection is null)
        {
            connection = new UserConnectionDto() { UserId = userId, ConnectionIds = new() { Context.ConnectionId } };
            Connections.Add(connection);
        }
        else
        {
            connection.ConnectionIds.Add(Context.ConnectionId);
        }
    }

    private void RemoveConnectionIds()
    {
        var userIdClaim = Context.User?.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim is not null)
        {
            var userId = int.Parse(userIdClaim.Value);
            Connections.RemoveAll(x => x.UserId == userId);
        }
    }
}
