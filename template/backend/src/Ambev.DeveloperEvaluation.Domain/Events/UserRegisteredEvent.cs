// <copyright file="UserRegisteredEvent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    using Ambev.DeveloperEvaluation.Domain.Entities;

    public class UserRegisteredEvent
    {
        public UserRegisteredEvent(User user)
        {
            this.User = user;
        }

        public User User { get; }
    }
}
