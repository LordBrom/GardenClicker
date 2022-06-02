using UnityEngine;

public class UpgradeManager : MonoBehaviour {

	#region Inspector Assignments

	[SerializeField]
	private Upgrade[] upgradesToLoad;

	#endregion
	#region Variables

	public UpgradePurchase[] upgrades { get; private set; }

	#endregion
	#region Unity Methods

	private void Start() {
		this.upgrades = new UpgradePurchase[this.upgradesToLoad.Length];
		for (int i = 0; i < this.upgradesToLoad.Length; i++) {
			this.upgrades[i] = new UpgradePurchase(this.upgradesToLoad[i]);
		}
	}

	private void Update() {

	}

	#endregion
}

public class UpgradePurchase {
	public string name;
	public int id;
	public int cost;
	public bool purchased;

	public UpgradePurchase(Upgrade upgrade) {
		this.name = upgrade.name;
		this.id = upgrade.id;
		this.cost = upgrade.cost;
		this.purchased = false;
	}
}
