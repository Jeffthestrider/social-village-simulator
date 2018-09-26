using Jochum.SocialVillageSimulator.Interactions;
using Jochum.SocialVillageSimulator.SocialAspects;

namespace Jochum.SocialVillageSimulator
{
    public class Character
    {
        private IInteractionGenerator _interactionGenerator;

        public bool IsPc { get; set; } 

        public string Name { get; set; }

        public Mood Mood { get; set; }

        public Gender Gender { get; set; }

        public Character(IInteractionGenerator interactionGenerator)
        {
            _interactionGenerator = interactionGenerator;
        }

        public Interaction BeInteractedWith(Interaction interaction, Character replyingTo)
        {
            // Make changes here

            // TODO: Singleton? Passed in as member to character initialization?
            return _interactionGenerator.GetResponse(this, interaction, replyingTo);
        }

        public Interaction InteractWith(Interaction interaction, Character speakingTo)
        {
            var response = speakingTo.BeInteractedWith(interaction, this);

            // Make changes here

            return response;
        }

    }
}
