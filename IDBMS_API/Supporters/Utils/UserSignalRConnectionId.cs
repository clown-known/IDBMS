namespace API.Supporters.Utils
{
    public class UserSignalRConnectionId
    {
        public Dictionary<string, List<string>> UserIdMapToConnectionIds { get; }
        private static UserSignalRConnectionId _instance;
        private static readonly object _lock = new();
        private UserSignalRConnectionId()
        {
            UserIdMapToConnectionIds = new Dictionary<string, List<string>>();
        }
        public static UserSignalRConnectionId GetInstance()
        {
            if( _instance == null)
            {
                lock( _lock )
                {
                    _instance = new UserSignalRConnectionId();
                }
            }
            return _instance;
        }

        public void AddConnectionIdToUserId(string connectionId, string userId)
        {
            lock( _lock )
            {
                if (UserIdMapToConnectionIds.ContainsKey(userId))
                {
                    var userIdConnections = UserIdMapToConnectionIds[userId];
                    if (!userIdConnections.Contains(connectionId))
                    {
                        userIdConnections.Add(connectionId);
                    }
                    UserIdMapToConnectionIds[userId] = userIdConnections;
                }
                else
                {
                    UserIdMapToConnectionIds[userId] = new List<string> { connectionId };
                }
            }
        }

        public void RemoveConnectionIdFromUserId(string connectionId, string userId)
        {
            lock(_lock)
            {
                if (UserIdMapToConnectionIds.ContainsKey(userId))
                {
                    var userIdConnections = UserIdMapToConnectionIds[userId];
                    userIdConnections.Remove(connectionId);
                    UserIdMapToConnectionIds[userId] = userIdConnections;
                }
            }
        }

        public void RemoveConnectionId(string connectionId)
        {
            lock(_lock)
            {
                var userIdKey = GetUserIdMapToConnectionId(connectionId);
                if (userIdKey != null)
                {
                    var value = UserIdMapToConnectionIds[userIdKey];
                    value.Remove(connectionId);
                    UserIdMapToConnectionIds[userIdKey] = value;
                }
            }
        }

        private string? GetUserIdMapToConnectionId(string connectionId)
        {
            foreach(var userId in UserIdMapToConnectionIds.Keys)
            {
                var value = UserIdMapToConnectionIds[userId];
                if (value.Contains(connectionId))
                {
                    return userId;
                }
            }
            return null;
        }
    }
}
