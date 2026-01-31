using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PageSwitcher : MonoBehaviour
{
    [Header("Pages")]
    [SerializeField] GameObject[] pages;

    [Header("Mask UI")]
    [SerializeField] Image currentMaskImage;
    [SerializeField] Image nextMaskImage;
    [SerializeField] Image prevMaskImage;
    [SerializeField] MaskInfo[] maskInfos;

    [Header("Fade")]
    [SerializeField] CanvasGroup fadeOverlay;
    [SerializeField] float fadeDuration = 0.3f;

    int currentIndex;
    bool isFading;

    void Start()
    {
        currentIndex = Mathf.Clamp(
            PageState.CurrentPageIndex,
            0,
            pages.Length - 1
        );

        for (int i = 0; i < pages.Length; i++)
            pages[i].SetActive(i == currentIndex);

        fadeOverlay.alpha = 0f;
        UpdateMaskUI();
    }

    void Update()
    {
        if (isFading) return;

        if (Input.GetKeyDown(KeyCode.E))
            StartCoroutine(FadeSwitch(GetNextIndex()));

        if (Input.GetKeyDown(KeyCode.Q))
            StartCoroutine(FadeSwitch(GetPrevIndex()));
    }

    int GetNextIndex()
    {
        return (currentIndex + 1) % pages.Length;
    }

    int GetPrevIndex()
    {
        return (currentIndex - 1 + pages.Length) % pages.Length;
    }

    IEnumerator FadeSwitch(int nextIndex)
    {
        isFading = true;

        // フェードアウト
        yield return FadeOverlay(0f, 1f);

        pages[currentIndex].SetActive(false);
        currentIndex = nextIndex;
        pages[currentIndex].SetActive(true);
        PageState.CurrentPageIndex = currentIndex;

        UpdateMaskUI();

        // フェードイン
        yield return FadeOverlay(1f, 0f);

        isFading = false;
    }

    void UpdateMaskUI()
    {
        currentMaskImage.sprite = maskInfos[currentIndex].icon;
        nextMaskImage.sprite = maskInfos[GetNextIndex()].icon;
        prevMaskImage.sprite = maskInfos[GetPrevIndex()].icon;
    }

    IEnumerator FadeOverlay(float from, float to)
    {
        float t = 0f;
        fadeOverlay.alpha = from;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeOverlay.alpha = Mathf.Lerp(from, to, t / fadeDuration);
            yield return null;
        }

        fadeOverlay.alpha = to;
    }
}
