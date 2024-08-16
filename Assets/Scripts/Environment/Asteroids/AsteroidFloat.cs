using UnityEngine;
using DG.Tweening;

public class AsteroidFloatLinear : MonoBehaviour
{
    public float totalDuration = 100f;
    public int loopCount = 2000;
    public float floatDistance = 0.5f;
    public Vector3 rotationAmount = new Vector3(360f, 360f, 360f);

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
        CreateFloatSequence();
    }

    private void CreateFloatSequence()
    {
        // Calculate duration for each part of the animation
        float singleLoopDuration = totalDuration / loopCount;
        float halfLoopDuration = singleLoopDuration / 2f;

        // Create a sequence of animations
        Sequence floatSequence = DOTween.Sequence();

        // Add incremental floating movement
        floatSequence.Append(
                transform.DOMoveY(startPosition.y + floatDistance, halfLoopDuration)
                    .SetEase(Ease.Linear))
            .Append(
                transform.DOMoveY(startPosition.y, halfLoopDuration)
                    .SetEase(Ease.Linear));

        // Set the sequence to loop the specified number of times
        floatSequence.SetLoops(loopCount, LoopType.Restart);

        // Add rotation
        transform.DORotate(rotationAmount, totalDuration, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(loopCount, LoopType.Restart);

        // Optional: Add some subtle horizontal movement
        float randomX = Random.Range(-0.2f, 0.2f);
        float randomZ = Random.Range(-0.2f, 0.2f);
        transform.DOMove(startPosition + new Vector3(randomX, 0, randomZ), totalDuration)
            .SetEase(Ease.Linear)
            .SetLoops(loopCount, LoopType.Yoyo);
    }

    private void OnDestroy()
    {
        // Kill all tweens associated with this transform when the object is destroyed
        transform.DOKill();
    }
}
