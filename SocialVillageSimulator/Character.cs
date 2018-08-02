using Jochum.SocialVillageSimulator.Interactions;
using Jochum.SocialVillageSimulator.SocialAspects;

namespace Jochum.SocialVillageSimulator
{
    public class Character
    {
        public string Name { get; set; }

        public Mood Mood { get; set; }

        public Interaction BeInteractedWith(Interaction interaction, Character replyingTo)
        {
            // Make changes here

            return InteractionGenerator.GetResponse(this, interaction, replyingTo);
        }

        public Interaction InteractWith(Interaction interaction, Character speakingTo)
        {
            var response = speakingTo.BeInteractedWith(interaction, this);

            // Make changes here

            return response;
        }

    }
}
