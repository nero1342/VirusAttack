using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Text moneyText, bulletText;
	int moneyAmount, bulletAmount;
    // Start is called before the first frame update
    void Start()
    {
        // PlayerPrefs.DeleteAll ();
    }

    // Update is called once per frame
    void Update()
    {
        moneyAmount = PlayerPrefs.GetInt ("MoneyAmount");
		moneyText.text = "Money: " + moneyAmount.ToString() + "$";

        bulletAmount = PlayerPrefs.GetInt ("BulletAmount");
		bulletText.text = "x " + bulletAmount.ToString();
    }

    public void gotoShop()
	{
		PlayerPrefs.SetInt ("MoneyAmount", moneyAmount);
        PlayerPrefs.SetString ("PreviousScene", SceneManager.GetActiveScene().name);
		SceneManager.LoadScene ("ShopScene");
	}
}
