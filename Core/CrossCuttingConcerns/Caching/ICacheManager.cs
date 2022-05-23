
namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key);
        //Key ver, bellekte o key'e karşılık gelen data'yı verim.
        object Get(string key); //Alternatif, generic olmayan version
        void Add(string key, object value, int duration); //Duration:Zaman
        bool IsAdd(string key); //Gelen veri cache'den mi gelsin? Datadan mı? Kontrolü. Cache'de var mı?
        void Remove(string key); //Gelen key'e karşılık cache'i sil.
        void RemoveByPattern(string pattern); //İsminde get olanları cache'den sil gibi.
    }
}