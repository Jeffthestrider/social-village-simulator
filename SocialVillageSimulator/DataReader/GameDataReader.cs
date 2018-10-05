using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jochum.SocialVillageSimulator.Interactions;
using Jochum.SocialVillageSimulator.Parsers;

namespace Jochum.SocialVillageSimulator.DataReader
{
    public interface IGameDataReader
    {
        IEnumerable<Interaction> GetInteractions();
    }

    public class GameDataJsonFileReader: IGameDataReader
    {
        private struct InteractionJsonModel
        {

            public string Name { get; set; }
            public string Dialogue { get; set; }
            public string BodyLanguage { get; set; }
        }

        private struct InteractionCriteriaJsonModel
        {
            public IList<IList<string>> InteractionCriteriaExpressions { get; set; }
            public IList<InteractionJsonModel> Interactions { get; set; }
            public string ActionText { get; set; }
        }
        
        private readonly string _interactionFilename;
        
        public GameDataJsonFileReader(string interactionFilename)
        {
            _interactionFilename = interactionFilename;
        }

        public IEnumerable<Interaction> GetInteractions()
        {
            var interactionCriteriaJsonModels = GetGameData<InteractionCriteriaJsonModel>(_interactionFilename);

            foreach (var interactionCriteriaJsonModel in interactionCriteriaJsonModels)
            {
                foreach (var interactionJsonModel in interactionCriteriaJsonModel.Interactions)
                {
                    yield return new Interaction
                    {
                        ActionText = interactionCriteriaJsonModel.ActionText,
                        BodyLanguage = interactionJsonModel.BodyLanguage,
                        Dialogue = interactionJsonModel.Dialogue,
                        Name = interactionJsonModel.Name,
                        InteractionCriteriaExpressions = interactionCriteriaJsonModel.InteractionCriteriaExpressions
                    };
                }
            }

        }

        private IList<T> GetGameData<T>(string filename)
        {
            var input = File.ReadAllText(filename);
            return JsonConvert.DeserializeObject<List<T>>(input);
        }
    }
}
