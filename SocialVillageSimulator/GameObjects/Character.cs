using Jochum.SocialVillageSimulator.Interactions;
using Jochum.SocialVillageSimulator.SocialAspects;

namespace Jochum.SocialVillageSimulator.GameObjects
{
    public class Character
    {
        private readonly IInteractionGenerator _interactionGenerator;
        public IActionResponseMapper ResponseMapper { private set; get; }

        public bool IsPc { get; set; } 

        public string Name { get; set; }

        public Mood Mood { get; set; }

        public Gender Gender { get; set; }

        public Character(IInteractionGenerator interactionGenerator, IActionResponseMapper responseMapper)
        {
            _interactionGenerator = interactionGenerator;
            ResponseMapper = responseMapper;
        }

        public Interaction BeInteractedWith(Interaction interaction, Character replyingTo)
        {
            // Make changes here
            
            return _interactionGenerator.GetResponse(this, interaction.ActionText, replyingTo);
        }

        public Interaction InteractWith(Interaction interaction, Character speakingTo)
        {
            var response = speakingTo.BeInteractedWith(interaction, this);

            // Make changes here

            return response;
        }

    }
}
