using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    private Text Play;
    private Text Quit;
    private Text Selected;
    private Text Score;

	void Start ()
    {
        Play = GameObject.Find("PlayButton").GetComponent<Text>();
        Quit = GameObject.Find("QuitButton").GetComponent<Text>();
        Score = GameObject.Find("Best").GetComponent<Text>();
        Selected = Play;
        Score.text = "Best : " + PlayerPrefs.GetInt("Score", 0);
    }
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangeSelected();
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            if (Selected == Play)
            {
                SceneManager.LoadScene("Main");
            }
            else
            {
                Application.Quit();
            }
        }
	}

    private void ChangeSelected()
    {
        if (Selected == Play)
        {
            Selected = Quit;
            Play.color = new Color(1, 1, 1);
            Quit.color = new Color(1, 1, 0);
        }
        else
        {
            Selected = Play;
            Play.color = new Color(1, 1, 0);
            Quit.color = new Color(1, 1, 1);
        }
    }

    private void Action()
    {

    }
}
