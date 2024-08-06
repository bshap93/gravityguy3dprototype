using UnityEngine;

public class Dialoggable : MonoBehaviour
{
    public string objectName;
    public string conversationName;

    public Material outlineMaterial;
    private Renderer objectRenderer;
    private Material[] originalMaterials;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            originalMaterials = objectRenderer.materials;
        }
    }

    public void Highlight()
    {
        if (objectRenderer != null && outlineMaterial != null)
        {
            Material[] newMaterials = new Material[originalMaterials.Length + 1];
            for (int i = 0; i < originalMaterials.Length; i++)
            {
                newMaterials[i] = originalMaterials[i];
            }

            newMaterials[originalMaterials.Length] = outlineMaterial;
            objectRenderer.materials = newMaterials;
        }
    }

    public void Unhighlight()
    {
        if (objectRenderer != null)
        {
            objectRenderer.materials = originalMaterials;
        }
    }
}
