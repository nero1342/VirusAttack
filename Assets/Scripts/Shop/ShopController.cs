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

	public Button bulletUnlockButton;
    // Start is called before the first frame update
    void Start()
    {
        moneyAmount = PlayerPrefs.GetInt ("MoneyAmount");
    }

    // Update is called once per frame
    void Update()
    {
        moneyAmountText.text = "Money: " + moneyAmount.ToString() + "$";

		isBulletUnlocked = PlayerPrefs.GetInt ("IsBulletUnlocked");
        if (isBulletUnlocked == 1) {
            bulletPriceText.text = "Unlocked!";  
        }
   	    if (moneyAmount >= bulletPrice && isBulletUnlocked == 0)
			bulletUnlockButton.interactable = true;
		else
			bulletUnlockButton.interactable = false;	
    }

    public void unlockBullet()
	{
		moneyAmount -= bulletPrice;
		PlayerPrefs.SetInt ("IsBulletUnlocked", 1);
		bulletPriceText.text = "Unlocked!";
		bulletUnlockButton.gameObject.SetActive (false);
	}

	public void exitShop()
	{
		PlayerPrefs.SetInt ("MoneyAmount", moneyAmount);
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
