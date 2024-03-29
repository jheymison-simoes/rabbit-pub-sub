﻿namespace RabbitPub.Domain.Models;

public abstract class Entity
{
    public Guid Id { get; }
    public int Code { get; protected set; }
    public DateTime CreatedAt { get; protected set; }

    protected Entity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
    }
}