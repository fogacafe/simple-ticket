﻿namespace SimpleTicket.Domain.SeedWork
{
    public abstract class Entity<T>
    {
        public T? Id { get; set; }
    }
}
