using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    //Temel voidler için başlangıç
    public interface IResult
    {
        bool Success { get; } //Sade okunabilir.   Başarılı mı/Değil mi bu görev kullanılır mı
        string Message { get; } //Tue/false ise bilgilendirme verir

    }
}
