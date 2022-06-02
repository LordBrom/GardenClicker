using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour {

	#region Singleton
	public static UpgradeManager instance;

	private void CreateInstance() {
		if (instance != null) {
			Destroy(gameObject);
			return;
		}
		instance = this;
	}
	#endregion
	#region Inspector Assignments

	[SerializeField]
	private Upgrade[] upgradesToLoad;

	#endregion
	#region Variables

	public Dictionary<string, UpgradePurchase> upgrades;

	#endregion
	#region Unity Methods

	private void Awake() {
		this.CreateInstance();

		this.upgrades = new Dictionary<string, UpgradePurchase>();

		foreach (Upgrade upgrade in this.upgradesToLoad) {
			this.upgrades.Add(upgrade.slug, new UpgradePurchase(upgrade));
		}
	}

	private void Update() {

	}

	#endregion

	public bool HasUpgrade(string slug) {
		return this.upgrades[slug].purchased;
	}
}

public class UpgradePurchase {
	public string name;
	public string slug;
	public int id;
	public int cost;
	public bool purchased;

	public UpgradePurchase(Upgrade upgrade) {
		this.name = upgrade.name;
		this.slug = upgrade.slug;
		this.id = upgrade.id;
		this.cost = upgrade.cost;
		this.purchased = false;
	}

}
