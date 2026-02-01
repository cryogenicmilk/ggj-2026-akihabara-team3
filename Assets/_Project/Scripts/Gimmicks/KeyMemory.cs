using UnityEngine;
using System.Collections;
public class KeyMemory : MonoBehaviour
{
    public int maxKeys = 3;
    public int currentKeys = 0;

    [Header("UI")]
    public GameObject keyUI;        // 鍵ゲットUI
    public GameObject fullKeyUI;    // これ以上持てないUI

    public bool AddKey()
    {
        if (currentKeys >= maxKeys)
        {
            Debug.Log("鍵はこれ以上持てない！");

            // 満タン用UIを表示
            if (fullKeyUI != null)
            {
                StartCoroutine(ShowFullKeyUI());
            }

            return false;
        }

        currentKeys++;
        Debug.Log("鍵を拾った！ 現在の鍵: " + currentKeys);

        // 鍵ゲットUI表示
        if (keyUI != null)
        {
            StartCoroutine(ShowKeyUI());
        }

        return true;
    }

    public bool UseKey()
    {
        if (currentKeys > 0)
        {
            currentKeys--;
            Debug.Log("鍵を使った！ 残り: " + currentKeys);
            return true;
        }

        Debug.Log("鍵がない！");
        return false;
    }

    IEnumerator ShowKeyUI()
    {
        keyUI.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        keyUI.SetActive(false);
    }

    IEnumerator ShowFullKeyUI()
    {
        fullKeyUI.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        fullKeyUI.SetActive(false);
    }
}
