namespace Jochum.SocialVillageSimulator.GameObjects
{
    public class Item
    {

        public string Name { get; set; }

    }

    public enum WeaponType
    {
        Sword,
        Bow,
        Staff
    }

    public class Weapon : Item
    {
        public WeaponType WeaponType { get; set; }
    }

    public enum ToolType
    {
        Hammer,
        Axe,
        Pickaxe
    }

    public class Tool : Item
    {
        public ToolType ToolType { get; set; }
    }
}
