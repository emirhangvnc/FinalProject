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
        public static string ProductAdded = "Ürün Eklendi";
        public static string ProductNameInValid = "Ürün ismi gecersiz";
        public static string ProductUnitPriceInValid = "Ürün Fiyatı gecersiz";
        public static string ProductsListed = "Ürünler Listelendi";
        public static string MaintenanceTime = "Sistem Bakımda"; 

        public static string ProductRemoved = "Ürün Silindi";
        public static string CategoryRemoved = "Catagori Silindi";

        public static string ProductUpdated = "Ürün Güncellendi";

        public static string ProductCountOfCategoryError = "Bu Kategoride En Fazla 10 Ürün Olabilir";
        public static string ProductNameAlreadyExists = "Bu İsme Sahip Başka Ürün Var";
        public static string AuthorizationDenied= "Yetkilendirme Reddedildi";

        public static string UserRegistered="Üye Kayıtı Başarılı";
        public static string SuccessfulLogin = "Üye Giriş Başarılı";
        public static string UserNotFound="Kullanıcı Bulunamadı";
        public static string PasswordError="Parola Hatası";
        public static string UserAlreadyExists="Kullanıcı Mevcut";
        public static string AccessTokenCreated = "Token oluşturuldu";
    }
}
