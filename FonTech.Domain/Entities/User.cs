﻿using FonTech.Domain.Interfaces;

namespace FonTech.Domain.Entities;

public class User : IEntityId<long>, IAuditable
{
    public long Id { get; set; }

    public string Login { get; set; }

    public string Password { get; set; }

    public List<Report> Reports { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreateAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public long UpdatedBy { get; set; }
}
