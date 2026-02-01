using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlowManager : MonoBehaviour
{
    public static GameFlowManager Instance;

    string lastPlaySceneName;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // プレイシーンに入るときに呼ぶ
    public void SetLastPlayScene(string sceneName)
    {
        lastPlaySceneName = sceneName;
    }

    // ゲーム終了（ビルド時のみ有効）
    public void QuitGame()
    {
#if UNITY_EDITOR
        // エディタ上では停止
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // 現在のシーンをやり直す
    public void RestartScene()
    {
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.name);
    }

    // 任意のシーンに移動（タイトルなど）
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
