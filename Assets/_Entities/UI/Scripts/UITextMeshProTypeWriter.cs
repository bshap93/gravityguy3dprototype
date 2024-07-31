using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class UITextMeshProTypeWriter : MonoBehaviour
{
    public TextMeshProUGUI txt;
    public float typingSpeed = 0.05f;
    public float scrollDuration = 30f;
    public float startY = -1000f;
    public float endY = 1000f;
    public float tiltAngle = 60f;
    public bool enableScrolling = true;

    [Header("Sound Effect")] public AudioSource audioSource;
    public AudioClip typingSoundClip;
    public AudioClip startSoundClip;
    public AudioClip endSoundClip;
    [Range(0f, 1f)] public float volumeMin = 0.8f;
    [Range(0f, 1f)] public float volumeMax = 1f;
    [Range(0.5f, 1.5f)] public float pitchMin = 0.9f;
    [Range(0.5f, 1.5f)] public float pitchMax = 1.1f;
    [Range(1, 10)] public int playEveryNthCharacter = 3; // NEW: Play sound every Nth character

    private string story;

    void Awake()
    {
        if (txt == null)
            txt = GetComponent<TextMeshProUGUI>();

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        story = txt.text;
        txt.text = "";

        Color textColor = txt.color;
        textColor.a = 0;
        txt.color = textColor;
    }

    void Start()
    {
        if (enableScrolling)
        {
            txt.rectTransform.rotation = Quaternion.Euler(tiltAngle, 0, 0);
            txt.rectTransform.anchoredPosition = new Vector2(0, startY);
        }

        StartCoroutine(PlayTextAndScroll());
    }

    IEnumerator PlayTextAndScroll()
    {
        Color textColor = txt.color;
        textColor.a = 1;
        txt.color = textColor;

        if (enableScrolling)
        {
            txt.rectTransform.DOAnchorPosY(endY, scrollDuration)
                .SetEase(Ease.Linear);
        }

        audioSource.PlayOneShot(startSoundClip, 1f);


        int characterCount = 0;
        foreach (char c in story)
        {
            txt.text += c;
            characterCount++;

            if (characterCount % playEveryNthCharacter == 0) // NEW: Check if it's time to play sound
            {
                if (audioSource != null && typingSoundClip != null)
                {
                    audioSource.pitch = Random.Range(pitchMin, pitchMax);
                    audioSource.PlayOneShot(typingSoundClip, Random.Range(volumeMin, volumeMax));
                }
            }

            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void StartEffect()
    {
        StartCoroutine(PlayTextAndScroll());
    }
}
