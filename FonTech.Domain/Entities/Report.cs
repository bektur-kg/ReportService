﻿using FonTech.Domain.Interfaces;

namespace FonTech.Domain.Entities;

public class Report : IEntityId<long>, IAuditable
{
    public long Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreateAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public long UpdatedBy { get; set; }

    public User User { get; set; }

    public long UserId { get; set; }
}
