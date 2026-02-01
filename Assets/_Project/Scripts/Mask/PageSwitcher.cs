using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PageSwitcher : MonoBehaviour
{
    [Header("Pages")]
    [SerializeField] GameObject[] pages;

    [Header("Mask UI")]
    [SerializeField] GameObject maskUIRoot;
    [SerializeField] Image currentMaskImage;
    [SerializeField] Image nextMaskImage;
    [SerializeField] Image prevMaskImage;
    [SerializeField] MaskInfo[] maskInfos;

    [Header("Fade")]
    [SerializeField] CanvasGroup fadeOverlay;
    [SerializeField] float fadeDuration = 0.3f;

    int currentIndex = 0;          // 今つけている仮面

    bool isFading;
    bool[] unlockedPages;

    void Start()
    {
        unlockedPages = new bool[pages.Length];

        // 全ページ未解放
        for (int i = 0; i < unlockedPages.Length; i++)
            unlockedPages[i] = false;

        // ページ0（通常世界）のみ表示
        for (int i = 0; i < pages.Length; i++)
            pages[i].SetActive(i == 0);

        currentIndex = 0;

        fadeOverlay.alpha = 0f;
        maskUIRoot.SetActive(false);

        UpdateMaskUI();
    }

    void Update()
    {
        if (isFading) return;
        if (!AnyPageUnlocked()) return;

        if (Input.GetKeyDown(KeyCode.E))
            StartCoroutine(FadeSwitch(GetNextIndex()));

        if (Input.GetKeyDown(KeyCode.Q))
            StartCoroutine(FadeSwitch(GetPrevIndex()));
    }

    public void UnlockPage(int pageIndex)
    {
        if (pageIndex < 1 || pageIndex >= pages.Length)
            return;

        unlockedPages[pageIndex] = true;

        maskUIRoot.SetActive(true);
        UpdateMaskUI();
    }
    IEnumerator FadeSwitch(int nextIndex)
    {
        if (nextIndex == currentIndex) yield break;
        if (!unlockedPages[nextIndex]) yield break;

        isFading = true;

        yield return FadeOverlay(0f, 1f);

        pages[currentIndex].SetActive(false);
        currentIndex = nextIndex;
        pages[currentIndex].SetActive(true);

        UpdateMaskUI();

        yield return FadeOverlay(1f, 0f);

        isFading = false;
    }


    int GetNextIndex()
    {
        int index = currentIndex;

        for (int i = 0; i < pages.Length; i++)
        {
            index = (index + 1) % pages.Length;
            if (unlockedPages[index])
                return index;
        }

        return currentIndex;
    }

    int GetPrevIndex()
    {
        int index = currentIndex;

        for (int i = 0; i < pages.Length; i++)
        {
            index = (index - 1 + pages.Length) % pages.Length;
            if (unlockedPages[index])
                return index;
        }

        return currentIndex;
    }

    bool AnyPageUnlocked()
    {
        for (int i = 1; i < unlockedPages.Length; i++)
            if (unlockedPages[i]) return true;

        return false;
    }
    void UpdateMaskUI()
    {
        // ===== currentMask：今つけている仮面 =====
        if (currentIndex >= 1)
        {
            currentMaskImage.sprite = maskInfos[currentIndex].icon;
            currentMaskImage.enabled = true;
        }
        else
        {
            currentMaskImage.enabled = false;
        }

        // ===== nextMask：Eキーで次につける仮面 =====
        int nextIndex = GetNextIndex();
        if (nextIndex >= 1 && nextIndex != currentIndex)
        {
            nextMaskImage.sprite = maskInfos[nextIndex].icon;
            nextMaskImage.enabled = true;
        }
        else
        {
            nextMaskImage.enabled = false;
        }

        // ===== prevMask：Qキーで前につける仮面 =====
        if (GetUnlockedMaskCount() >= 3)
        {
            int prevIndex = GetPrevIndex();
            if (prevIndex >= 1 && prevIndex != currentIndex)
            {
                prevMaskImage.sprite = maskInfos[prevIndex].icon;
                prevMaskImage.enabled = true;
            }
            else
            {
                prevMaskImage.enabled = false;
            }
        }
        else
        {
            prevMaskImage.enabled = false;
        }

    }

    int GetUnlockedMaskCount()
    {
        int count = 0;
        for (int i = 1; i < unlockedPages.Length; i++)
        {
            if (unlockedPages[i]) count++;
        }
        return count;
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
