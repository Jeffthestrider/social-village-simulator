using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jochum.SocialVillageSimulator.DataReader
{
    public interface IGameDataReader
    {
        IList<T> GetData<T>(string filename);
    }

    public class GameDataReader: IGameDataReader
    {
        public IList<T> GetData<T>(string filename)
        {
            var input = File.ReadAllText(filename);
            return JsonConvert.DeserializeObject<List<T>>(input);
        }
    }
}
