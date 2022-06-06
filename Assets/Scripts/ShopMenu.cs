using System.Collections.Generic;
using UnityEngine;
using NateMills.UnityUtility;

public class ShopMenu : HidableMenu {

	#region Singleton
	public static ShopMenu instance;

	private void createInstance() {
		if (instance != null) {
			Destroy(gameObject);
			return;
		}
		instance = this;
	}
	#endregion
	#region Inspector Assignments

	[SerializeField]
	private Transform shopContainerTransform;
	[SerializeField]
	private GameObject shopItemPrefab;

	#endregion
	#region Variables

	private List<GameObject> renderedShopItems;

	#endregion
	#region Unity Methods

	protected override void Awake() {
		base.Awake();
		this.createInstance();
	}

	protected override void Start() {
		renderedShopItems = new List<GameObject>();
		base.Start();

		this.PopulateShopItems();
	}

	private void Update() {

	}

	#endregion

	public void PopulateShopItems() {
		foreach (GameObject shopItem in this.renderedShopItems) {
			Destroy(shopItem);
		}
		this.renderedShopItems.Clear();

		foreach (string upgradeSlug in UpgradeManager.instance.upgrades.Keys) {
			UpgradePurchase upgradePurchase = UpgradeManager.instance.upgrades[upgradeSlug];
			if (!upgradePurchase.purchased && upgradePurchase.IsAvailable()) {
				GameObject newShopItem = Instantiate(this.shopItemPrefab, this.shopContainerTransform);
				newShopItem.GetComponent<ShopItem>().SetUpgradePurchase(upgradePurchase);
				this.renderedShopItems.Add(newShopItem);
			}
		}

		foreach (Item item in GameManager.itemLookUp.Values) {
			if (item.inShop) {
				GameObject newShopItem = Instantiate(this.shopItemPrefab, this.shopContainerTransform);
				newShopItem.GetComponent<ShopItem>().SetItem(item);
				this.renderedShopItems.Add(newShopItem);
			}
		}
	}

	public override void ShowMenu() {
		this.PopulateShopItems();
		base.ShowMenu();
	}

}
