using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItem : MonoBehaviour {

	#region Inspector Assignments

	//[SerializeField]
	//private Image itemImage;
	[SerializeField]
	private TextMeshProUGUI itemName;
	[SerializeField]
	private TextMeshProUGUI itemCost;

	#endregion
	#region Variables

	private UpgradePurchase upgradePurchase;
	private Button button;

	#endregion
	#region Unity Methods

	private void Start() {
		this.button = GetComponent<Button>();
	}

	private void Update() {
		this.button.interactable = GameManager.instance.goldResource.amountHeld >= this.upgradePurchase.cost;
	}

	#endregion

	public void SetUpgradePurchase(UpgradePurchase upgradePurchase) {
		this.upgradePurchase = upgradePurchase;
		//this.itemImage = null;
		this.itemName.text = upgradePurchase.name;
		this.itemCost.text = upgradePurchase.cost.ToString();
	}

	public void PurchaseItem() {
		if (GameManager.instance.goldResource.SpendResource(this.upgradePurchase.cost)) {
			UpgradeManager.instance.upgrades[this.upgradePurchase.slug].purchased = true;
			Destroy(gameObject);
		}
	}
}
