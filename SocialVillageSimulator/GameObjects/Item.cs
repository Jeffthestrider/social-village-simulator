namespace Jochum.SocialVillageSimulator.GameObjects
{
    public class Item
    {

        public string Name { get; set; }
        public ItemType ItemType { get; set; }

    }

    public enum ItemType
    {
        Sword,
        Bow,
        Staff,
        Hammer,
        Axe,
        Pickaxe,
        Potion
    }
}
