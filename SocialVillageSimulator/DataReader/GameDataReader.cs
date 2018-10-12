using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        private readonly IActionParser _parser;
        private readonly IList<string> _interactionDirectories;
        
        public GameDataJsonFileReader(IActionParser parser, IList<string> interactionDirectories)
        {
            _parser = parser;
            _interactionDirectories = interactionDirectories;
        }

        public IEnumerable<Interaction> GetInteractions()
        {
            var files = GetListOfFiles();
            List<InteractionCriteriaJsonModel> interactionCriteriaJsonModels = new List<InteractionCriteriaJsonModel>();

            foreach (var file in files)
            {
                interactionCriteriaJsonModels.AddRange(GetGameData<InteractionCriteriaJsonModel>(file));
            }

            foreach (var interactionCriteriaJsonModel in interactionCriteriaJsonModels)
            {
                foreach (var interactionJsonModel in interactionCriteriaJsonModel.Interactions)
                {
                    yield return new Interaction
                    {
                        Action = _parser.GetAction(interactionCriteriaJsonModel.ActionText),
                        BodyLanguage = interactionJsonModel.BodyLanguage,
                        Dialogue = interactionJsonModel.Dialogue,
                        Name = interactionJsonModel.Name,
                        InteractionCriteriaExpressions = interactionCriteriaJsonModel.InteractionCriteriaExpressions
                    };
                }
            }

        }

        private IList<string> GetListOfFiles()
        {
            List<string> jsonFiles = new List<string>();

            foreach (var interactionDirectory in _interactionDirectories)
            {
                if (!Directory.Exists(interactionDirectory))
                    continue;

                var directoryFiles = Directory.EnumerateFiles(interactionDirectory);
                var directoryJsonFiles = directoryFiles.Where(p => p.ToLower(CultureInfo.InvariantCulture).EndsWith(".json"));

                jsonFiles.AddRange(directoryJsonFiles);
            }

            return jsonFiles;
        }

        private IList<T> GetGameData<T>(string filename)
        {
            var input = File.ReadAllText(filename);
            return JsonConvert.DeserializeObject<List<T>>(input);
        }
    }
}
