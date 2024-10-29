﻿namespace ShopDb.Entities;

public class Payment
{
    public string Status { get; set; }
    public string AdditionalDetails { get; set; }

    public Guid Id { get; set; }
    public Order Order { get; set; }

    public long UserId { get; set; }
    public User User { get; set; }
}