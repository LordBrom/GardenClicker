using UnityEngine;
using NateMills.UnityUtility;

public class ShopMenu : HidableMenu {

	#region Inspector Assignments

	[SerializeField]
	private Transform shopContainerTransform;
	[SerializeField]
	private GameObject shopItemPrefab;

	#endregion
	#region Variables

	#endregion
	#region Unity Methods

	protected override void Start() {
		base.Start();

		foreach (UpgradePurchase upgradePurchase in UpgradeManager.instance.upgrades.Values) {
			if (!upgradePurchase.purchased) {
				Instantiate(this.shopItemPrefab, this.shopContainerTransform).GetComponent<ShopItem>().SetUpgradePurchase(upgradePurchase);
			}
		}
	}

	private void Update() {

	}

	#endregion
}
