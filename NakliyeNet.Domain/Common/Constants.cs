using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NakliyeNet.Domain.Common
{
    public static class  Constants
    {
        public static Dictionary<string, string> RequestPropertiesDict = new Dictionary<string, string>
        {
            { "RoomType", "Kaç odalı ev eşyası taşınacak?" },
            { "OldHomeType", "Eski Evden Eşya Nasıl Taşınacak?" },
    { "NewHomeType", "Yeni Evden Eşya Nasıl Taşınacak?" },
    { "PackagingType", "Paketleme için yardım gerekiyor mu?" },
    { "InsuranceType", "EMTİA sigortası ister misin?" },
    { "Description", "Nakliyeci başka neyi bilmeli / neye dikkat etmeli?" },
    { "FromCity", "Eşya/Yük Nereden Taşınacak?" },
    { "FromDistrict", "Eşya/Yük Nereden Taşınacak?" },
    { "ToCity", "Eşya/Yük Nereye Taşınacak?" },
    { "ToDistrict", "Eşya/Yük Nereye Taşınacak?" },
    { "TransportationTime", "Eşya/Yük Ne zaman Taşınacak?" },
    { "LoadType", "Hangisine İhtiyacın Var ?" },
    { "LoadWeight", "Taşınacak yükün ağırlığı yaklaşık ne kadar?" },
    { "ItemType", "Hangi Eşya Taşınacak?" }
        };
    }
}
