using Models;
using System;

public class ItemsPurchased
{
	public Guid IDAcheteur { get; set; }
    public Guid IDChocolat { get; set; }
    public int Quantity { get; set; }
    public DateTime DatePurchase { get; set; }

    public ItemsPurchased()
	{
	}
}
