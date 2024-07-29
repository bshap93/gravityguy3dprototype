using UnityEngine;

public class CapitalShipPartController : MonoBehaviour
{
    public AudioClip collidedSoundClip;
    AudioSource _collidedSoundSource;
    MeshCollider _meshCollider;
    // Start is called before the first frame update
    void Start()
    {
        _meshCollider = GetComponent<MeshCollider>();
        _meshCollider.convex = true;
        _collidedSoundSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _collidedSoundSource.PlayOneShot(collidedSoundClip);
        }
    }
}
