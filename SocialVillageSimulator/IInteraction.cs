namespace Jochum.SocialVillageSimulator
{
    public class Interaction<T>
    {
        public string Dialogue { get; set; }
        public InteractionType InteractionType { get; set; }
        public T Further{ get; set; }
    }

    public enum InteractionType
    {
        Invalid,
        Greet,
        Introduce,
        Farewell,
        GetToKnow,
        Information,
        ShareWith,
        Request,
        Give,
        Threaten
    }

    public enum Topic
    {
        Invalid,
        OnesSelf,
        Profession,
        Hobby,
        Name,
        FamilyStatus,
        LivingAt,
        Past,
        Religion,
        Goals,
        Opinion,
        RelationToPerson
    }

    public enum Information
    {
        Invalid,
        WhereAmI,
        News,
        Gossip,
        WhereProfession,
        WherePerson,
        WhereLocation
    }
}