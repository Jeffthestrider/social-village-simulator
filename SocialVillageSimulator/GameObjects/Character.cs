using System;
using System.Collections.Generic;
using System.Linq;
using Jochum.SocialVillageSimulator.Interactions;
using Jochum.SocialVillageSimulator.SocialAspects;

namespace Jochum.SocialVillageSimulator.GameObjects
{
    public class Character: IEquatable<Character>
    {
        private readonly IInteractionGenerator _interactionGenerator;

        public IActionResponseMapper ResponseMapper { private set; get; }

        public int Id { get; set; }

        public bool IsPc { get; set; } 

        public string Name { get; set; }

        public Mood Mood { get; set; }

        public float MoodModifier { get; set; }

        public Gender Gender { get; set; }

        public IList<Item> Possessions { get; set; }

        public Character(IInteractionGenerator interactionGenerator, IActionResponseMapper responseMapper)
        {
            _interactionGenerator = interactionGenerator;
            ResponseMapper = responseMapper;

            Possessions = new List<Item>();
        }

        public Interaction BeInteractedWith(Interaction interaction, Character replyingTo)
        {
            // Make changes here
            
            return _interactionGenerator.GetResponse(this, interaction.Action, replyingTo);
        }

        public Interaction InteractWith(Interaction interaction, Character speakingTo)
        {
            var response = speakingTo.BeInteractedWith(interaction, this);

            // Make changes here

            return response;
        }

        public IList<Item> GetPossessionsOfType(ItemType type)
        {
            ItemType itemType = (ItemType) type;
            
            return Possessions.Where(p => p.ItemType == type).ToList();

        }

        public override string ToString()
        {
            var pc = IsPc ? "PC" : "NPC";
            var possesions = string.Join(", ", Possessions.Select(p => p.Name));

            return $@"{pc} Name: {Name}
Gender: {Gender}                
Mood: {Mood}    Mood Modifier: {MoodModifier}
Possessions: {possesions}";
        }

        public bool Equals(Character other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Character) obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
