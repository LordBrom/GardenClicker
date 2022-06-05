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
		if (UpgradeManager.instance.upgrades[this.upgradePurchase.slug].purchased) {
			Destroy(this.gameObject);
		}
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
			UpgradePurchase upgradePurchase = UpgradeManager.instance.upgrades[this.upgradePurchase.slug];
			upgradePurchase.purchased = true;
			switch (upgradePurchase.slug) {
				case "plot_size_1":
					GardenManager.instance.UpgradeGardePlotSize(2, 1);
					break;
				case "plot_size_2":
					GardenManager.instance.UpgradeGardePlotSize(2, 2);
					break;
				case "plot_size_3":
					GardenManager.instance.UpgradeGardePlotSize(3, 2);
					break;
				case "plot_size_4":
					GardenManager.instance.UpgradeGardePlotSize(3, 3);
					break;
				default:
					break;
			}
			Destroy(this.gameObject);
		}
	}
}
