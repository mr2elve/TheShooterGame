using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // �������� using ��� SceneManager

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // ��������� ����������� ���������� instance
    public float waitAfterDeath = 3f; // ��������� ��������� ���������� waitAfterDeath
    public int killedEnemies; // ��������� ��������� ���������� killedEnemies

    private void Awake() // �������� ����� Awake
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    /*public void PlayerDeath()
    {
        StartCoroutine(PlayerDeathCoroutine());
    }

    public IEnumerator PlayerDeathCoroutine()
    {
        yield return new WaitForSeconds(waitAfterDeath);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }*/

    public void PauseUnpause()
    {
        if (UI.instance.pauseScreen.activeInHierarchy)
        {
            UI.instance.pauseScreen.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
        }
        else
        {
            UI.instance.pauseScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
        }
    }
}