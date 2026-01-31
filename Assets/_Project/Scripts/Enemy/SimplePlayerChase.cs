using Unity.VisualScripting;
using UnityEngine;

public class SimplePlayerChase : MonoBehaviour
{
    [Header("Chase Settings")]
    [SerializeField] private float chaseSpeed;
    [SerializeField] private float chaseDistance;
    [SerializeField] private Transform playerTransform;

    void Update()
    {
        if(playerTransform == null)
            return;

        Vector3 directionToPlayer = playerTransform.position - transform.position;
        //directionToPlayer.y = 0; // Ignore vertical difference

        float distanceToPlayer = directionToPlayer.magnitude;
        if (distanceToPlayer <= chaseDistance) return;
        {
            Vector3 chaseDirection = directionToPlayer.normalized;
            transform.position += chaseDirection * chaseSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player caught!");
            // Implement player caught logic here
        }
    }
}
