using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class Star
{
    public string name;
    public float size;
    [FormerlySerializedAs("Color")] public Color color;
    public float mass;
    public Vector3 position;
}
