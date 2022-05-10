

namespace Core.Utilities.Results
{
    public interface IDataResult<T>:IResult //içindekiler var(success,message)
    {
        T Data { get; } //Ürün,category vs. vs.
    }
}
