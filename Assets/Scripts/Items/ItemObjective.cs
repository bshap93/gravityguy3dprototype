using Polyperfect.Crafting.Framework;
using Polyperfect.Crafting.Integration;

namespace Items

{
    [System.Serializable]
    public class ItemObjective
    {
        public Objective objective;
        public RuntimeID targetItemID;
        public int requiredAmount;
        public int currentAmount;
        public ChildSlotsInventory itemTargetInventory;

        public ItemObjective(Objective objective,
            RuntimeID targetItemID,
            int requiredAmount,
            int currentAmount,
            ChildSlotsInventory itemTargetInventory
        )
        {
            this.objective = objective;
            this.targetItemID = targetItemID;
            this.requiredAmount = requiredAmount;
            this.currentAmount = currentAmount;
            this.itemTargetInventory = itemTargetInventory;
        }
    }
}
