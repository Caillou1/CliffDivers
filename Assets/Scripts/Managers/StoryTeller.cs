using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryTeller : MonoBehaviour {
    public float TimeBetweenEachChar;

    private Text StoryText;
    private string Story;

	void Start () {
        StoryText = GameObject.Find("Story").GetComponent<Text>();
        Story = "14th February 2017 - Valentine's Day\n\nEvery woman in the world has broken up with their boyfriend. Depression is now part of these men's everyday life. Sadness is so important that they decided to kill themselves. From a cliff.\nYeah.\n\nWeirdos.";
        StartCoroutine(TellStory());
	}

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StopAllCoroutines();
            GoToTitle();
        }
    }

    void GoToTitle()
    {
        SceneManager.LoadScene("Title");
    }

    IEnumerator TellStory()
    {
        char c = Story[0];
        if(c == '\n')
        {
            yield return new WaitForSeconds(1);
        }
        StoryText.text += c;
        Story = Story.Substring(1);
        yield return new WaitForSeconds(TimeBetweenEachChar);
        if (Story.Length != 0) StartCoroutine(TellStory());
        else
        {
            yield return new WaitForSeconds(3);
            GoToTitle();
        }
    }
}
