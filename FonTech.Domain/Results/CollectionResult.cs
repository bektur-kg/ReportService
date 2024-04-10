namespace FonTech.Domain.Results;

public class CollectionResult<T> : BaseResult<IEnumerable<T>>
{
    public int Count { get; set; }
}
