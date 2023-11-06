public class ArticleViewModel
{
    public int Index { get; set; }
    public Article Article { get; set; }

    public ArticleViewModel(int index, Article article)
    {
        Index = index;
        Article = article;
    }
}
