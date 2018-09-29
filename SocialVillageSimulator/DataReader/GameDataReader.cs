using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jochum.SocialVillageSimulator.Interactions;

namespace Jochum.SocialVillageSimulator.DataReader
{
    public interface IGameDataReader
    {
        IList<Interaction> GetInteractions();
    }

    public class GameDataJsonFileReader: IGameDataReader
    {
        private readonly string _interactionFilename;
        
        public GameDataJsonFileReader(string interactionFilename)
        {
            _interactionFilename = interactionFilename;
        }

        public IList<Interaction> GetInteractions()
        {
            return GetGameData<Interaction>(_interactionFilename);
        }

        private IList<T> GetGameData<T>(string filename)
        {
            var input = File.ReadAllText(filename);
            return JsonConvert.DeserializeObject<List<T>>(input);
        }
    }
}
