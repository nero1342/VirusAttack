using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Scripts;

public class GameController : MonoBehaviour
{
public Text moneyText, bulletText;
int moneyAmount, bulletAmount;

public static GameState gameState = GameState.Start;
private static int turn;
Enemy[] enemies;
public GameObject gunObject;
[SerializeField] string _nextLevelName;
Gun gun;
Vector3 StartingPosition;

// Start is called before the first frame update
void Start()
{
        turn = 0;
        gameState = GameState.Start;
        enemies = FindObjectsOfType<Enemy>();
        gun = gunObject.GetComponent<Gun>();
        StartingPosition = Camera.main.transform.position;
}

// Update is called once per frame
void Update()
{
        moneyAmount = PlayerPrefs.GetInt ("MoneyAmount");
		moneyText.text = "Money: " + moneyAmount.ToString() + "$";
        bulletAmount = PlayerPrefs.GetInt ("BulletAmount");
		bulletText.text = "x " + bulletAmount.ToString();
        switch (gameState)
        {
        case GameState.Start:
                NewTurn();
                break;
        case GameState.Playing:
                if (gun.isDone()) {
                        EndTurn();
                        break;
                }
                break;
        case GameState.Won:
                gotoNextLevel();
                break;
        case GameState.Lost:
                Debug.Log("Lost");
                break;
        default:
                break;
        }
}

public void NewTurn()
{
        turn += 1;
        gameState = GameState.Playing;
        gun.reset();
        AnimateCameraToStartPosition();
}

public void EndTurn()
{
        bool isWin = IsWin();

        if (isWin) {
                gameState = GameState.Won;
                return;
        }
        if (turn >= 3) {
                gameState = GameState.Lost;
                return;
        }
        gameState = GameState.Start;
}

bool IsWin() {
        foreach (var enemy in enemies)
        {
                if (enemy.gameObject.activeSelf) {
                        return false;
                }
        }
        return true;;
}

private void AnimateCameraToStartPosition()
{
        //animate the camera to start
        Camera.main.transform.position = StartingPosition;
}

void gotoNextLevel() {
        SceneManager.LoadScene(_nextLevelName);
}

public void gotoShop()
{
        PlayerPrefs.SetInt ("MoneyAmount", moneyAmount);
        PlayerPrefs.SetString ("PreviousScene", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene ("ShopScene");
}
}
