using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public interface IDataResult<T>:IResult //içindekiler var(success,message)
    {
        T Data { get; } //Ürün,category vs. vs.

    }
}
