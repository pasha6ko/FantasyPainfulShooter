using System.Collections;
using UnityEngine;

public class TextAnimation : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private GameObject textGroup;

    private float scaleFactor = 1.2f;
    private float animationDuration = 0.8f;

    private Vector3 initialScale;

    private void Start()
    {
        initialScale = textGroup.transform.localScale;
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        while (true)
        {
            yield return ScaleAnimation(initialScale * scaleFactor, animationDuration);
            yield return ScaleAnimation(initialScale, animationDuration);
        }
    }

    private IEnumerator ScaleAnimation(Vector3 targetScale, float duration)
    {
        float timer = 0f;
        Vector3 startScale = textGroup.transform.localScale;

        while (timer < duration)
        {
            float progress = timer / duration;
            textGroup.transform.localScale = Vector3.Lerp(startScale, targetScale, progress);
            timer += Time.deltaTime;
            yield return null;
        }

        textGroup.transform.localScale = targetScale;
    }
}
