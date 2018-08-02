using System;
using System.Collections.Generic;
using System.Linq;
using Jochum.SocialVillageSimulator.Interactions;
using Jochum.SocialVillageSimulator.SocialAspects;

namespace Jochum.SocialVillageSimulator
{
    class Program
    {
        static Character CreatePlayer()
        {
            return new Character {
                Name = "Jeff",
                Mood = Mood.Sad
            };
        }

        static Character CreateNpc()
        {
            return new Character {
                Name = "Jill",
                Mood = Mood.Happy
            };
        }

        static string GetInteractionList()
        {
            var values = Enum.GetValues(typeof(InteractionType));

            List<string> options = new List<string>();

            foreach (var value in values)
            {
                options.Add($"{(int)value}: {(InteractionType)value}");
            }
            
            var result = "Type one of the numbers to initiate a type of interaction.\n\n";
            result += string.Join("\n", options);

            return result;
        }

        static void Main(string[] args)
        {
            MasterRandom.InitializeRandom(4);

            Character player = CreatePlayer();
            Character npc = CreateNpc();

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
                var interactionTypeMaxValue = (int)Enum.GetValues(typeof(InteractionType)).Cast<InteractionType>().Last();

                bool isGood = int.TryParse(playerInput.ToString(), out playerChoice);
                isGood = playerChoice > 0 && playerChoice <= interactionTypeMaxValue;

                if (isGood)
                {
                    InteractionType playerChoiceType = (InteractionType)playerChoice;

                    var interaction = InteractionGenerator.GetInteraction(player, playerChoiceType, npc);

                    var interactionResult = player.InteractWith(interaction, npc);

                    npcResponse = $"{interactionResult.BodyLanguage}\n\n{npc.Name}: {interactionResult.Dialogue}";
                }
                else
                {
                    var cantHandleResponse = InteractionGenerator.GetInteraction(npc, InteractionType.Invalid, player);

                    npcResponse = $"{cantHandleResponse.BodyLanguage}\n\n{npc.Name}: {cantHandleResponse.Dialogue}";
                }

                WriteResponse(npcResponse);

                playerInput = AskInput();
            }
                
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

