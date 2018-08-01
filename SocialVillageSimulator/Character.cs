namespace Jochum.SocialVillageSimulator
{
    public class Character
    {
        public string Name { get; set; }

        public Mood Mood { get; set; }

        public Response BeInteractedWith<T>(Interaction<T> interaction, Character replyingTo)
        {
            return Responses.ResponseGenerator.GetResponse(this, interaction, replyingTo);
        }

        public Response InteractWith<T>(Interaction<T> interaction, Character speakingTo)
        {
            var response = speakingTo.BeInteractedWith<T>(interaction, this);

            // Knowledge changes, other changes to character from response here.

            return response;
        }

    }

    public enum Mood
    {
        Happy,
        Sad,
        Melancholy,
        Angry
    }
}
