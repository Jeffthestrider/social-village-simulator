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
        IList<T> GetGameData<T>();
    }

    public class GameDataJsonFileReader: IGameDataReader
    {
        private string _filename;

        public GameDataJsonFileReader(string filename)
        {
            _filename = filename;
        }

        public IList<T> GetGameData<T>()
        {
            var input = File.ReadAllText(_filename);
            return JsonConvert.DeserializeObject<List<T>>(input);
        }
    }
}
