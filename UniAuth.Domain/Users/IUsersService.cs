﻿namespace UniAuth.Domain.Users
{
    internal interface IUsersService
    {
        Task<User> Create(string usernameAuthId, CancellationToken cancellationToken = default);
    }
}
