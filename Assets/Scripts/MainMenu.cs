using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("MoneyAmount", 9999);

        PlayerPrefs.SetInt("BulletAmount", 9999);

        PlayerPrefs.SetInt("LifeAmount", 9999);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Debug.Log("QUIT!!!");
        Application.Quit(); 
    }

}
