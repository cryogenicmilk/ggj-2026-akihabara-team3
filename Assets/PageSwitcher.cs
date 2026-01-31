using UnityEngine;

public class PageSwitcher : MonoBehaviour
{
    [SerializeField] GameObject[] pages;

    int currentIndex;

    void Start()
    {
        // シーン1で使っていたページ番号を復元
        currentIndex = Mathf.Clamp(
            PageState.CurrentPageIndex,
            0,
            pages.Length - 1
        );

        ShowOnly(currentIndex);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            Next();

        if (Input.GetKeyDown(KeyCode.Q))
            Previous();
    }

    void Next()
    {
        currentIndex++;
        if (currentIndex >= pages.Length)
            currentIndex = 0;

        Apply();
    }

    void Previous()
    {
        currentIndex--;
        if (currentIndex < 0)
            currentIndex = pages.Length - 1;

        Apply();
    }

    void Apply()
    {
        PageState.CurrentPageIndex = currentIndex; // ★保存
        ShowOnly(currentIndex);
    }

    void ShowOnly(int index)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == index);
        }
    }
}
