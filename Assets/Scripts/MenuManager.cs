using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Animator SceneCurtains;
    public Animator Player;
    public string SceneName;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

        SceneCurtains.SetTrigger("SceneClose");
        Player.SetTrigger("StartGame");
        //AudioManager.instance.PlaySFX(audioSearch.startGameSFX);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneName);
    }

}
