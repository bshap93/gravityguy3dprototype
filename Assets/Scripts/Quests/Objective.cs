[System.Serializable]
public class Objective
{
    public string id;
    public string description;
    public bool isCompleted;

    public Objective(string id, string description)
    {
        this.id = id;
        this.description = description;
        this.isCompleted = false;
    }

    public void CompleteObjective()
    {
        isCompleted = true;
    }
}
