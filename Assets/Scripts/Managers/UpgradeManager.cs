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

	#endregion
	#region Variables

	public Dictionary<string, UpgradePurchase> upgrades;

	#endregion
	#region Unity Methods

	private void Awake() {
		this.CreateInstance();
		this.LoadUpgrades();

	}

	private void Update() {

	}

	#endregion
	#region Save/Load

	public string GetSaveString() {
		List<string> upgradeList = new List<string>();
		foreach (string upgrade in UpgradeManager.instance.upgrades.Keys) {
			if (UpgradeManager.instance.upgrades[upgrade].purchased) {
				upgradeList.Add(upgrade);
			}
		}
		return string.Join('|', upgradeList);
	}

	public void LoadSaveString(string saveString) {
		this.LoadUpgrades();

		if (saveString == "") {
			return;
		}

		string[] upgradeList = saveString.Split("|");
		foreach (string upgrade in upgradeList) {
			this.upgrades[upgrade].purchased = true;
		}
	}

	#endregion

	private void LoadUpgrades() {
		this.upgrades = new Dictionary<string, UpgradePurchase>();

		foreach (Upgrade upgrade in GameManager.upgradeLookUp.Values) {
			this.upgrades.Add(upgrade.slug, new UpgradePurchase(upgrade));
		}
	}

	public bool HasUpgrade(string slug) {
		if (!this.upgrades.ContainsKey(slug)) {
			Debug.LogWarning("Upgrade slug '" + slug + "' not found");
			return false;
		}
		return this.upgrades[slug].purchased;
	}


}

public class UpgradePurchase {
	public string name;
	public string slug;
	public int cost;
	public bool purchased;

	public UpgradePurchase(Upgrade upgrade) {
		this.name = upgrade.name;
		this.slug = upgrade.slug;
		this.cost = upgrade.cost;
		this.purchased = false;
	}

}
