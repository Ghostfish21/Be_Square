using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startMenu : MonoBehaviour
{
    public Animator SceneCurtains;
    public Animator Player;
    private AudioManager audioSearch;
    public string SceneName;
    void Start()
    {
        audioSearch = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Scene CurrentScene = SceneManager.GetActiveScene();

        if (CurrentScene.name == "end")
        {
            StartCoroutine(GameEnd());
        }
    }

    public void StartGame()
    {
        StartCoroutine(LoadScene());

    }

    public void Cntr()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }
    IEnumerator LoadScene()
    {


        Player.SetTrigger("StartGame");
        AudioManager.instance.PlaySFX(audioSearch.startGameSFX);
        //AudioManager.instance.PlaySFX(audioSearch.startGameSFX);
        ScreenShake.instance.TriggerShake(0.2f, 0.4f);
        yield return new WaitForSeconds(1.5f);
        SceneCurtains.SetTrigger("SceneClose");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneName);
    }

    IEnumerator GameEnd()
    {
        // Ensure the "Won" trigger/parameter is only set once
        if (!Player.GetBool("Won"))
        {
            Player.SetBool("Won", true);  // Start the "Won" animation
            AudioManager.instance.PlaySFX(audioSearch.endGameSFX); // Optional: Add end game sound
        }

        // Wait for the animation to finish or a set time before transitioning
        yield return new WaitForSeconds(3f);  // Adjust this time based on the length of your "Won" animation

        Player.SetBool("Won", false);  // Stop the "Won" animation after it plays

    }
}
