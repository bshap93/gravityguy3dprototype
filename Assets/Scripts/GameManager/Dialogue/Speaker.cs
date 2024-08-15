namespace Dialogue
{
    [System.Serializable]
    public class Speaker
    {
        public string id;
        public string name;

        public Speaker(string id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}
