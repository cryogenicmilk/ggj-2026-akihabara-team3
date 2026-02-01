using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    private bool isOpen = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isOpen) return;

        if (collision.CompareTag("Player"))
        {
            KeyMemory keyMemory = collision.GetComponent<KeyMemory>();
            if (keyMemory != null && keyMemory.UseKey())
            {
                OpenDoor();
            }
        }
    }

    void OpenDoor()
    {
        isOpen = true;
        Debug.Log("扉が開いた！");
        gameObject.SetActive(false); // 扉を消す（開いた表現）
    }
}
