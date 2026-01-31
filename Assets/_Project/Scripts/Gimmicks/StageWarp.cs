using System.Collections;
using UnityEngine;

public class StageWarp : MonoBehaviour
{
    public Transform warpPoint;
    public CameraFader fader;   //フェード管理
    public string playerTag = "Player";
    
    private bool warping = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (warping) return;

        if (other.CompareTag(playerTag))
        {
            StartCoroutine(Warp(other.transform));
        }
    }
    IEnumerator Warp(Transform player)
    {
        warping = true;
        yield return fader.FadeOut();
        player.position = warpPoint.position;
        Time.timeScale = 0.0f;
        yield return new WaitForSecondsRealtime(0.2f);
        Time.timeScale = 1.0f;
        yield return fader.FadeIn();
        warping = false;
    }
}
