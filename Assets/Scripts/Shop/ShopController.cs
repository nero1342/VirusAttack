using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopController : MonoBehaviour
{
int moneyAmount;
int isBulletUnlocked;

	public Text moneyAmountText;
	public Text bulletPriceText;
    int bulletPrice = 5;
	int bulletAmount;
	public Button bulletBuyButton;
    // Start is called before the first frame update
    void Start()
    {
		// PlayerPrefs.DeleteAll ();
        moneyAmount = PlayerPrefs.GetInt ("MoneyAmount");
}

// Update is called once per frame
void Update()
{
        moneyAmountText.text = "Money: " + moneyAmount.ToString() + "$";

		bulletAmount = PlayerPrefs.GetInt ("BulletAmount");
        if (true || moneyAmount >= bulletPrice)
			bulletBuyButton.interactable = true;
		else
			bulletBuyButton.interactable = false;	
    }

    public void buyBullet()
	{
		moneyAmount -= bulletPrice;
		bulletAmount += 1;
		PlayerPrefs.SetInt ("BulletAmount", bulletAmount);
		PlayerPrefs.SetInt ("MoneyAmount", moneyAmount);
	}

	public void exitShop()
	{
		string previousScene = PlayerPrefs.GetString("PreviousScene");
		SceneManager.LoadScene (previousScene);
	}

	// public void resetPlayerPrefs()
	// {
	// 	moneyAmount = 0;
	// 	buyButton.gameObject.SetActive (true);
	// 	riflePrice.text = "Price: 5$";
	// 	PlayerPrefs.DeleteAll ();
	// }
}
