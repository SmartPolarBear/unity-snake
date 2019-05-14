using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public GameObject gameOverPanel;

    public static bool isPaused = false;
    private static GameObject gameOverPanelStatic;

    public static void MainMenu()
    {
        SceneManager.LoadScene("Main");
    }

    public static void FailGame()
    {
        gameOverPanelStatic.SetActive(true);
        isPaused = true;
    }

    public static void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void ExitGame()
    {
        Application.Quit();
    }

    private void Awake()
    {
        if (gameOverPanelStatic == null)
        {
            gameOverPanelStatic = gameOverPanel;
        }

        isPaused = false;

    }

    private void Start()
    {
        var buttons = gameOverPanel.GetComponentsInChildren<Button>();
        var restartBtn = (Button)(from b in buttons where b.name.Contains("Restart") select b).First();
        restartBtn.onClick.AddListener(() =>
        {
            GameController.RestartGame();
        });

        var exitBtn = (Button)(from b in buttons where b.name.Contains("Exit") select b).First();
        exitBtn.onClick.AddListener(() =>
        {
            GameController.MainMenu();
        });


    }

    // Update is called once per frame
    void Update()
    {

    }
}
