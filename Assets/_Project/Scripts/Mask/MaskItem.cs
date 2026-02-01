using UnityEngine;

public class MaskItem : MonoBehaviour
{
    [SerializeField] int unlockPageIndex;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FindFirstObjectByType<PageSwitcher>()
                .UnlockPage(unlockPageIndex);

            Destroy(gameObject);
        }
    }
}
