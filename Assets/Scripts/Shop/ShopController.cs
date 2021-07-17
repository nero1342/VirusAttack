using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopController : MonoBehaviour
{
int moneyAmount;
int isBulletUnlocked;

	public Text lifeAmountText, moneyAmountText, bulletAmountText;
    int bulletPrice = 5, lifePrice = 10, vaccinePrice = 20, linePrice = 5;
	int bulletAmount, lifeAmount;
	public Button bulletBuyButton, lifeBuyButton, vaccineBuyButton, lineBuyButton;
    // Start is called before the first frame update
    void Start()
    {
		// PlayerPrefs.DeleteAll ();
        moneyAmount = PlayerPrefs.GetInt ("MoneyAmount");
}

// Update is called once per frame
void Update()
{
        moneyAmount = PlayerPrefs.GetInt("MoneyAmount");
        moneyAmountText.text = "Money: " + moneyAmount.ToString() + "$";
        
		lifeAmount = PlayerPrefs.GetInt("MaxLifeAmount");
        lifeAmountText.text = "x " + lifeAmount.ToString();
		if (moneyAmount >= lifePrice)
			lifeBuyButton.interactable = true;
		else
			lifeBuyButton.interactable = false;	

		bulletAmount = PlayerPrefs.GetInt ("BulletAmount");
        bulletAmountText.text = "x " + bulletAmount.ToString();
        if (moneyAmount >= bulletPrice)
			bulletBuyButton.interactable = true;
		else
			bulletBuyButton.interactable = false;	

		
		if (moneyAmount >= vaccinePrice)
			vaccineBuyButton.interactable = true;
		else
			vaccineBuyButton.interactable = false;	

		if (PlayerPrefs.GetInt ("LineUnlocked") == 1) {
			lineBuyButton.interactable = false;	
		}
		if (moneyAmount >= linePrice)
			lineBuyButton.interactable = true;
		else
			lineBuyButton.interactable = false;	

    }

    public void buyBullet()
	{
		moneyAmount -= bulletPrice;
		bulletAmount += 1;
		PlayerPrefs.SetInt ("BulletAmount", bulletAmount);
		PlayerPrefs.SetInt ("MoneyAmount", moneyAmount);
	}

	public void buyLife()
	{
		moneyAmount -= lifePrice;
		lifeAmount += 1;
		PlayerPrefs.SetInt ("MaxLifeAmount", lifeAmount);
		PlayerPrefs.SetInt ("MoneyAmount", moneyAmount);
	}

	public void buyVaccine()
	{
		moneyAmount -= vaccinePrice;
	}
	public void buyLine()
	{
		moneyAmount -= linePrice;
		linePrice += 1;
		PlayerPrefs.SetInt ("LineUnlocked", 1);
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
