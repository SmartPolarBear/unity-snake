using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
public class StartScene : MonoBehaviour
{
    public GameObject mainCanvas;

    // Start is called before the first frame update
    void Start()
    {
        var buttons = mainCanvas.GetComponentsInChildren<Button>();

        var startGameBtn = (Button)(from b in buttons where b.name.Contains("StartGame") select b).First();
        startGameBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Game");
        });

        var exitGameBtn = (Button)(from b in buttons where b.name.Contains("ExitGame") select b).First();
        exitGameBtn.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
