public class ArticleView
{
    public int Index { get; set; }
    public Article Article { get; set; }

    public ArticleView(int index, Article article)
    {
        Index = index;
        Article = article;
    }
}
