using System.Collections;
using UnityEngine;

public class StageWarp : MonoBehaviour
{
    public Transform warpPoint;
    public CameraFader fader;   //フェード管理
    public string playerTag = "player";
    
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
        yield return new WaitForSeconds(0.2f);
        yield return fader.FadeIn();
        warping = false;
    }
}
