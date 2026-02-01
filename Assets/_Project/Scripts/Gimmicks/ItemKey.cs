using UnityEngine;

public class ItemKey : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            KeyMemory playerKey = collision.GetComponent<KeyMemory>();
            if (playerKey != null)
            {
                bool picked = playerKey.AddKey();
                if (picked)
                {
                    Destroy(gameObject); // åÆÇè¡Ç∑
                }
            }
        }
    }
}
