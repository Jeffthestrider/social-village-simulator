using System;
using System.Collections.Generic;
using System.Linq;
using Jochum.SocialVillageSimulator.Interactions;
using Jochum.SocialVillageSimulator.SocialAspects;
using Jochum.SocialVillageSimulator.DataReader;
using Jochum.SocialVillageSimulator.GameObjects;
using Jochum.SocialVillageSimulator.Parsers;

namespace Jochum.SocialVillageSimulator
{
    class Program
    {
        static Character CreatePlayer(IInteractionGenerator interactionGenerator)
        {
            return new Character(interactionGenerator, new ActionResponseMapper())
            {
                Name = "Jeff",
                Gender = Gender.Male,
                Mood = Mood.Happy,
                IsPc = true
            };
        }

        static Character CreateNpc(IInteractionGenerator interactionGenerator)
        {
            return new Character(interactionGenerator, new ActionResponseMapper())
            {
                Name = "Jill",
                Gender = Gender.Female,
                Mood = Mood.Happy,
                IsPc = false
            };
        }

        static string GetInteractionList()
        {
            var values = Enum.GetValues(typeof(ActionVerb));

            List<string> options = new List<string>();

            foreach (var value in values)
            {
                options.Add($"{(int)value}: {(ActionVerb)value}");
            }
            
            var result = "Type one of the numbers to initiate a type of interaction.\n\n";
            result += string.Join("\n", options);

            return result;
        }

        static void Main(string[] args)
        {
            SeedRandom.InitializeRandom(4);

            IGameDataReader gameDataReader = new GameDataJsonFileReader(new ActionParser(), "Data\\Interactions.json");

            var interactionGenerator = new InteractionGenerator(
                gameDataReader.GetInteractions().ToList(), 
                new CriteriaParser(),
                new StringTemplateReplacer());

            Character player = CreatePlayer(interactionGenerator);
            Character npc = CreateNpc(interactionGenerator);

            string npcResponse = "You are standing in front of a villager. She is standing there, looking bored.";

            WriteResponse(npcResponse);

            char playerInput = AskInput();

            while (playerInput != 'e')
            {
                if (playerInput == 'h')
                {
                    WriteResponse("Press the numbers to choose an interaction. Press e to exit. Press h for help.", npcResponse);

                    playerInput = AskInput();
                    continue;
                }

                int playerChoice = 0;
                var interactionTypeMaxValue = (int)Enum.GetValues(typeof(ActionVerb)).Cast<ActionVerb>().Last();

                bool isGood = int.TryParse(playerInput.ToString(), out playerChoice);
                isGood = isGood && playerChoice > 0 && playerChoice <= interactionTypeMaxValue;

                if (isGood)
                {
                    ActionVerb playerChoiceType = (ActionVerb)playerChoice;
                    string objectText = string.Empty;


                    if (playerChoiceType == ActionVerb.RequestItemType)
                    {
                        objectText = "Sword";
                    }

                    var interaction = interactionGenerator.GetInteraction(
                        player, 
                        new ParsedAction {
                            Adverb = ActionAdverb.Neutrally,
                            Verb = playerChoiceType}, 
                        npc);

                    if (interaction != null)
                    {
                        interaction.Action.Object = objectText;

                        var interactionResult = player.InteractWith(interaction, npc);

                        if (interactionResult != null)
                        {
                            npcResponse = $"{interactionResult.BodyLanguage}\n\n{npc.Name}: {interactionResult.Dialogue}";
                        }
                        else
                        {
                            npcResponse = GetInvalidResponse(interactionGenerator, player, npc);
                        }
                    }
                    else
                    {
                        npcResponse = GetInvalidResponse(interactionGenerator, player, npc);
                    }
                }
                else
                {
                    npcResponse = GetInvalidResponse(interactionGenerator, player, npc);
                }

                WriteResponse(npcResponse);

                playerInput = AskInput();
            }
                
        }

        private static string GetInvalidResponse(InteractionGenerator interactionGenerator, Character player, Character npc)
        {
            string npcResponse;
            var invalidCriteria = interactionGenerator.GetInvalidInteraction(npc, player);

            var cantHandleResponse = invalidCriteria;

            npcResponse = $"{cantHandleResponse.BodyLanguage}\n\n{npc.Name}: {cantHandleResponse.Dialogue}";
            return npcResponse;
        }

        private static void WriteResponse(params string[] lines)
        {
            Console.Clear();
            Console.WriteLine(string.Join("\n", lines));
            Console.WriteLine(GetInteractionList());
        }

        private static char AskInput()
        {
            var input = Console.ReadKey(true);

            return input.KeyChar;
        }
    }
}

