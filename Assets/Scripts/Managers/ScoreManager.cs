using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour {
    public Text ScoreText;
    public Text LifeText;

    private int score;

    public int Lifes = 5;

	void Start () {
        score = 0;
        ScoreText.text = "Score : " + score;
        LifeText.text = "Lifes : " + Lifes;
    }

    public void AddScore(int points)
    {
        score += points;
        BiggerText(ScoreText);
        ScoreText.text = "Score : " + score;
        if (points < 0)
        {
            Lifes--;
            BiggerText(LifeText);
            LifeText.text = "Lifes : " + Lifes;
        }
        if(Lifes < 0)
        {
            Lose();
        }
    }

    void BiggerText(Text texte)
    {
        texte.fontSize = 40;
        StartCoroutine(ReduceText(texte));
    }

    void Lose()
    {
        var lastBest = PlayerPrefs.GetInt("Score", 0);
        if(score > lastBest)
        {
            PlayerPrefs.SetInt("Score", score);
        }
        SceneManager.LoadScene("Title");
    }

    IEnumerator ReduceText(Text texte)
    {
        if (texte.fontSize > 30)
        {
            texte.fontSize -= 2;
            yield return new WaitForSeconds(.1f);
            StartCoroutine(ReduceText(texte));
        }
    }
}
