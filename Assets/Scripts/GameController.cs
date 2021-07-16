using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Text moneyText;
	public static int moneyAmount;
	public int isBulletUnlocked;

    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll ();
        // moneyAmount = PlayerPrefs.GetInt ("MoneyAmount");
		// isBulletUnlocked = PlayerPrefs.GetInt ("IsBulletUnlocked");

        // if (isBulletUnlocked == 1)
		// 	bullet.SetActive (true);
		// else
		// 	bullet.SetActive (false);
    }

    // Update is called once per frame
    void Update()
    {
        moneyAmount = PlayerPrefs.GetInt ("MoneyAmount");
		isBulletUnlocked = PlayerPrefs.GetInt ("IsBulletUnlocked");
        moneyText.text = "Money: " + moneyAmount.ToString() + "$";
    }

    public void gotoShop()
	{
		PlayerPrefs.SetInt ("MoneyAmount", moneyAmount);
        PlayerPrefs.SetString ("PreviousScene", SceneManager.GetActiveScene().name);
		SceneManager.LoadScene ("ShopScene");
	}
}
