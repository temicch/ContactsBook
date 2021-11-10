using System;

namespace ContactsBook.Domain.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
}
