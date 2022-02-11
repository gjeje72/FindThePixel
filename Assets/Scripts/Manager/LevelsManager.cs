using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsManager : MonoBehaviour
{
    public List<GameObject> levels;

    // Start is called before the first frame update
    void Start()
    {
        foreach(var level in levels)
        {
            // Show level only if completed.
            if (PlayerPrefs.GetInt($"isLevel{int.Parse(level.name.Substring(5)) + 1}Done") == 1)
                level.SetActive(true);
        }
    }

    /// <summary>
    ///     Method used to load scene.
    /// </summary>
    /// <param name="sceneIndex">The scene index.</param>
    public void LoadScene(int sceneIndex)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneIndex);
    }
}
