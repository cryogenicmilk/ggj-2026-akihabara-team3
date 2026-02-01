using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PageSwitcher : MonoBehaviour
{
    [Header("Pages")]
    [SerializeField] GameObject[] pages;   // 0:初期世界, 1:ページ2, 2:ページ3

    [Header("Mask UI")]
    [SerializeField] GameObject maskUIRoot;
    [SerializeField] Image currentMaskImage;
    [SerializeField] Image nextMaskImage;
    [SerializeField] Image prevMaskImage;
    [SerializeField] MaskInfo[] maskInfos;

    [Header("Fade")]
    [SerializeField] CanvasGroup fadeOverlay;
    [SerializeField] float fadeDuration = 0.3f;

    int currentMaskIndex = -1;     // 装備中の仮面（-1 = 未装備）
    int activePageIndex = 0;       // 表示中のページ

    bool isFading;
    bool[] unlockedMasks;

    void Start()
    {
        unlockedMasks = new bool[pages.Length];

        // 初期化
        for (int i = 0; i < pages.Length; i++)
        {
            unlockedMasks[i] = false;
            pages[i].SetActive(false);
        }

        // 初期世界（ページ1）
        activePageIndex = 0;
        pages[activePageIndex].SetActive(true);

        fadeOverlay.alpha = 0f;
        maskUIRoot.SetActive(false);

        UpdateMaskUI();
    }

    void Update()
    {
        if (isFading) return;
        if (!AnyMaskUnlocked()) return;

        if (Input.GetKeyDown(KeyCode.E))
            StartCoroutine(FadeSwitch(GetNextMask()));

        if (Input.GetKeyDown(KeyCode.Q))
            StartCoroutine(FadeSwitch(GetPrevMask()));
    }

    // ===== 仮面取得 =====
    public void UnlockPage(int maskIndex)
    {
        if (maskIndex < 0 || maskIndex >= unlockedMasks.Length)
            return;

        unlockedMasks[maskIndex] = true;

        maskUIRoot.SetActive(true);
        UpdateMaskUI();
    }

    // ===== フェード付き切替 =====
    IEnumerator FadeSwitch(int nextMaskIndex)
    {
        if (nextMaskIndex == -1) yield break;
        if (!unlockedMasks[nextMaskIndex]) yield break;
        if (currentMaskIndex == nextMaskIndex) yield break;

        isFading = true;

        yield return FadeOverlay(0f, 1f);

        // 今のページをOFF
        pages[activePageIndex].SetActive(false);

        // 仮面装備
        currentMaskIndex = nextMaskIndex;

        // ★ 仮面 → ページ変換（1つ先）
        activePageIndex = (currentMaskIndex + 1) % pages.Length;

        // 新ページON
        pages[activePageIndex].SetActive(true);

        UpdateMaskUI();

        yield return FadeOverlay(1f, 0f);

        isFading = false;
    }

    // ===== 次の仮面 =====
    int GetNextMask()
    {
        if (currentMaskIndex == -1)
            return GetFirstUnlockedMask();

        int index = currentMaskIndex;

        for (int i = 0; i < unlockedMasks.Length; i++)
        {
            index = (index + 1) % unlockedMasks.Length;
            if (unlockedMasks[index])
                return index;
        }

        return currentMaskIndex;
    }

    // ===== 前の仮面 =====
    int GetPrevMask()
    {
        if (currentMaskIndex == -1)
            return GetFirstUnlockedMask();

        int index = currentMaskIndex;

        for (int i = 0; i < unlockedMasks.Length; i++)
        {
            index = (index - 1 + unlockedMasks.Length) % unlockedMasks.Length;
            if (unlockedMasks[index])
                return index;
        }

        return currentMaskIndex;
    }

    int GetFirstUnlockedMask()
    {
        for (int i = 0; i < unlockedMasks.Length; i++)
            if (unlockedMasks[i])
                return i;

        return -1;
    }

    bool AnyMaskUnlocked()
    {
        for (int i = 0; i < unlockedMasks.Length; i++)
            if (unlockedMasks[i]) return true;

        return false;
    }

    // ===== UI =====
    void UpdateMaskUI()
    {
        if (currentMaskIndex == -1)
        {
            currentMaskImage.enabled = false;

            int next = GetFirstUnlockedMask();
            if (next != -1)
            {
                nextMaskImage.sprite = maskInfos[next].icon;
                nextMaskImage.enabled = true;
            }
            else
            {
                nextMaskImage.enabled = false;
            }

            prevMaskImage.enabled = false;
            return;
        }

        currentMaskImage.sprite = maskInfos[currentMaskIndex].icon;
        currentMaskImage.enabled = true;

        int nextIndex = GetNextMask();
        nextMaskImage.enabled = nextIndex != currentMaskIndex;
        if (nextMaskImage.enabled)
            nextMaskImage.sprite = maskInfos[nextIndex].icon;

        int prevIndex = GetPrevMask();
        prevMaskImage.enabled = prevIndex != currentMaskIndex;
        if (prevMaskImage.enabled)
            prevMaskImage.sprite = maskInfos[prevIndex].icon;
    }

    // ===== Fade =====
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
