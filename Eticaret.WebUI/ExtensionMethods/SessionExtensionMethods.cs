using Newtonsoft.Json;// jsona çevirme işlemi için kullanıyoruz.
//Database’e yazmak istemediğin ama geçici olarak lazım olan nesneleri Session’da saklayabilirsin.
namespace Eticaret.WebUI.ExtensionMethods
{
    public static class SessionExtensionMethods
    {
        public static void SetJson(this ISession session,string key,object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value)); //objeyi JSON string’e çeviriyor.
            //session.setstring(key,jsondata);Session içine string olarak kaydediyor.
        }
        public static T? GetJson<T>(this ISession session, string key) where T : class
        {
            var data = session.GetString(key);
            
            return data == null ? default(T) : JsonConvert.DeserializeObject<T>(data);
        }

    }
}
