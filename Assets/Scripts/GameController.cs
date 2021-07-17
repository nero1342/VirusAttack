using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Scripts;

public class GameController : MonoBehaviour
{
    public Text moneyText, bulletText, lifeText;
    int moneyAmount, bulletAmount, lifeAmount;

    public static GameState gameState = GameState.Start;
    private static int turn;
    Enemy[] enemies;
    [SerializeField] GameObject gunObject, gameOverScreen;
    [SerializeField] string _nextLevelName;
    Gun gun;
    Vector3 StartingPosition;
    public Button shopButton;

    // Start is called before the first frame update
    void Start()
    {
        turn = 0;
        gameState = GameState.Start;
        enemies = FindObjectsOfType<Enemy>();
        gun = gunObject.GetComponent<Gun>();
        StartingPosition = Camera.main.transform.position;

        if (PlayerPrefs.GetInt("MaxLifeAmount") < 3)
        {
            PlayerPrefs.SetInt("MaxLifeAmount", 3);
        }
        lifeAmount = PlayerPrefs.GetInt("MaxLifeAmount");
    }

    // Update is called once per frame
    void Update()
    {
        moneyAmount = PlayerPrefs.GetInt("MoneyAmount");
        moneyText.text = "Money: " + moneyAmount.ToString() + "$";
        bulletAmount = PlayerPrefs.GetInt("BulletAmount");
        bulletText.text = "x " + bulletAmount.ToString();
        lifeText.text = "x " + (lifeAmount - turn + 1).ToString();


        switch (gameState)
        {
            case GameState.Start:
                NewTurn();
                break;
            case GameState.Playing:
                if (gun.isMove())
                {
                    shopButton.interactable = false;
                }
                if (gun.isDone())
                {
                    EndTurn();
                    break;
                }
                break;
            case GameState.Won:
                gotoNextLevel();
                // StartCoroutine(gotoNextLevel());
                break;
            case GameState.Lost:
                Debug.Log("Lost");
                gameOverScreen.GetComponent<GameOver>().Setup();
                break;
            default:
                break;
        }
    }

    public void NewTurn()
    {
        // Debug.Log(turn);
        turn += 1;
        gameState = GameState.Playing;
        gun.reset();
        AnimateCameraToStartPosition();
    }

    public void EndTurn()
    {
        bool isWin = IsWin();

        if (isWin)
        {
            gameState = GameState.Won;
            return;
        }
        if (turn >= lifeAmount)
        {
            gameState = GameState.Lost;
            return;
        }
        gameState = GameState.Start;
    }

    bool IsWin()
    {
        foreach (var enemy in enemies)
        {
            if (enemy.gameObject.activeSelf)
            {
                return false;
            }
        }
        return true; ;
    }

    private void AnimateCameraToStartPosition()
    {
        //animate the camera to start
        Camera.main.transform.position = StartingPosition;
    }

    void gotoNextLevel()
    {
        // AudioSource soundSource = GetComponent<AudioSource>();
        // //Play Audio
        // soundSource.Play();

        // //Wait until it's done playing
        // while (soundSource.isPlaying)
        //     yield return null;
   
        SceneManager.LoadScene(_nextLevelName);
    }

    
    public void gotoShop()
    {
        PlayerPrefs.SetInt("MoneyAmount", moneyAmount);
        PlayerPrefs.SetInt("MaxLifeAmount", lifeAmount);
        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("ShopScene");
    }


}
