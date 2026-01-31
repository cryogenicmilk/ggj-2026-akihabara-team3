using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSwitcher : MonoBehaviour
{
    [SerializeField] string stage1Name = "Stage1";
    [SerializeField] string stage2Name = "Stage2";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleStage();
        }
    }

    void ToggleStage()
    {
        string current = SceneManager.GetActiveScene().name;

        if (current == stage1Name)
        {
            SceneManager.LoadScene(stage2Name);
        }
        else if (current == stage2Name)
        {
            SceneManager.LoadScene(stage1Name);
        }
    }
}
