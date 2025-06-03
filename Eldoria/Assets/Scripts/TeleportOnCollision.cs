using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Collections;

public class TeleportOnCollision : MonoBehaviour
{
    public string targetTag = "Player";
    public Transform destination;
    public Image fadeImage;
    public float fadeDuration = 1f;
    public float holdBlackTime = 1.5f;

    private bool isTeleporting = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isTeleporting && other.CompareTag(targetTag))
        {
            StartCoroutine(TeleportRoutine(other.gameObject));
        }
    }

    private IEnumerator TeleportRoutine(GameObject obj)
    {
        isTeleporting = true;

        // Fade out to black
        yield return StartCoroutine(Fade(0f, 1f));

        NavMeshAgent agent = obj.GetComponent<NavMeshAgent>();
        if (agent != null)
            agent.enabled = false;

        obj.transform.position = destination.position;

        if (agent != null)
            agent.enabled = true;

        yield return new WaitForSeconds(holdBlackTime);

        // Fade back in
        yield return StartCoroutine(Fade(1f, 0f));

        isTeleporting = false;
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsed = 0f;
        Color c = fadeImage.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / fadeDuration);
            c.a = Mathf.Lerp(startAlpha, endAlpha, t);
            fadeImage.color = c;
            yield return null;
        }

        c.a = endAlpha;
        fadeImage.color = c;
    }
}
