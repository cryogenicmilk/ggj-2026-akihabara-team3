using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class CameraFader : MonoBehaviour
{
    public Image fadeImage;
    public float fadeSpeed = 1.5f;

    public IEnumerator FadeOut()
    {
        float alpha = 0.0f;
        while (alpha < 1.0f)
        {
            alpha += Time.deltaTime * fadeSpeed;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }
    public IEnumerator FadeIn()
    {
        float alpha = 1.0f;
        while (alpha > 0.0f)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }
}
