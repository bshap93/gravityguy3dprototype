using Polyperfect.Crafting.Framework;

namespace Items

{
    [System.Serializable]
    public class ItemObjective
    {
        public Objective objective;
        public RuntimeID targetItemID;
        public int requiredAmount;
        public int currentAmount;

        public ItemObjective(Objective objective, RuntimeID targetItemID, int requiredAmount)
        {
            this.objective = objective;
            this.targetItemID = targetItemID;
            this.requiredAmount = requiredAmount;
            this.currentAmount = 0;
        }
    }
}
