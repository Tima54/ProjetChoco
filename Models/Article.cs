using Models;
using System;

public class Article : BaseModel
{
    public string Reference { get; set; }
    public float Price { get; set; }

    public Article()
	{
	}
}
