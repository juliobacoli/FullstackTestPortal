﻿namespace FullstackTest.Api.DTOs.Request;

public class UpdateProductRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}
