using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductNameInValid = "Ürün ismi gecersiz";
        public static string ProductUnitPriceInValid = "Ürün Fiyatı gecersiz";

        public static string ProductRemoved = "Ürün Silindi";
        public static string CategoryRemoved = "Catagori Silindi";

        public static string ProductUpdated = "Ürün Güncellendi";
        public static string UserRegistered="Üye Kayıtı Başarılı";
        public static string SuccessfulLogin = "Üye Giriş Başarılı";
        public static string UserNotFound="Kullanıcı Bulunamadı";
        public static string PasswordError="Parola Hatası";
        public static string UserAlreadyExists="Kullanıcı Mevcut";
        public static string AccessTokenCreated = "Token oluşturuldu";

        public static string ProductAdded = "Ürün eklendi";
        public static string MaintenanceTime = "Sistem bakımda";
        public static string ProductsListed = "Ürünler listelendi";
        public static string ProductCountOfCategoryError = "Bir kategoride en fazla 10 ürün olabilir";
        public static string ProductNameAlreadyExists = "Bu isimde zaten başka bir ürün var";
        public static string CategoryLimitExceded = "Kategori limiti aşıldığı için yeni ürün eklenemiyor";
        public static string AuthorizationDenied = "Yetkiniz yok.";
    }
}
