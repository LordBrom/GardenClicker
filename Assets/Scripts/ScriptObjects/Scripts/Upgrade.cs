using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Custom/Upgrade")]
public class Upgrade : ScriptableObject {

	public new string name;
	public string slug = "-";
	public int cost;

	public Upgrade[] requiredUpgrades;

	public Upgrade() {
	}

	public bool IsAvailable() {
		foreach (Upgrade upgrade in requiredUpgrades) {
			if (!UpgradeManager.instance.HasUpgrade(upgrade.slug)) {
				return false;
			}
		}
		return true;
	}

}
