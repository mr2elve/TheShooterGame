using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FinalScreen : MonoBehaviour
{
    public static FinalScreen instance; // Добавлена статическая переменная instance
    public string mainMenuScene;

    public TextMeshProUGUI killedEnemiesText;

    private void Awake() // Добавлен метод Awake
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (instance.killedEnemiesText != null)
        {
            instance.killedEnemiesText.text = "KILLED ENEMIES: " + 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (instance.killedEnemiesText != null)
        {
            instance.killedEnemiesText.text = "KILLED ENEMIES: " + GameManager.instance.killedEnemies;
        }
    }

    public void MainMenu()
    {
        if (mainMenuScene != null)
        {
            SceneManager.LoadScene(mainMenuScene);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}