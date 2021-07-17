using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void Setup() {
        gameObject.SetActive(true);
        Debug.Log("Setup");
    }

    public void RestartButton() {
        Debug.Log("Restart");
        gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MenuButton() {
        Debug.Log("Menu");
        SceneManager.LoadScene("Menu");
    }
}
