using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItem : MonoBehaviour {

	#region Inspector Assignments

	[SerializeField]
	private TextMeshProUGUI itemName;
	[SerializeField]
	private TextMeshProUGUI itemDescription;
	[SerializeField]
	private TextMeshProUGUI itemCost;

	#endregion
	#region Variables

	private UpgradePurchase upgradePurchase;
	private Item item;
	private Button button;
	private int cost;

	#endregion
	#region Unity Methods

	private void Start() {
		this.button = GetComponent<Button>();
	}

	private void Update() {
		this.button.interactable = GameManager.instance.goldResource.amountHeld >= this.cost;
		//if (UpgradeManager.instance.upgrades[this.upgradePurchase.slug].purchased) {
		//	Destroy(this.gameObject);
		//}
	}

	#endregion

	public void SetUpgradePurchase(UpgradePurchase upgradePurchase) {
		this.upgradePurchase = upgradePurchase;
		this.itemName.text = upgradePurchase.name;
		this.itemDescription.text = upgradePurchase.description;
		this.cost = upgradePurchase.cost;
		this.itemCost.text = this.cost.ToString();
	}

	public void SetItem(Item item) {
		this.item = item;
		this.itemName.text = item.name;
		this.itemDescription.text = item.description;
		this.cost = (item.sellValue * 10);
		this.itemCost.text = this.cost.ToString();
	}

	public void PurchaseItem() {
		if (GameManager.instance.goldResource.SpendResource(this.cost)) {
			if (this.upgradePurchase == null) {
				InventoryManager.instance.AddToInventory(this.item, 1);
			} else {
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
				ShopMenu.instance.PopulateShopItems();
			}
		}
	}
}
