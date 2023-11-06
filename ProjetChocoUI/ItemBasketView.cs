public class ItemBasketView
{
    public int Quantity { get; set; }
    public Article Article { get; set; }

    public ItemBasketView(int QuantityAdd, Article article)
    {
        Quantity = QuantityAdd;
        Article = article;
    }
}