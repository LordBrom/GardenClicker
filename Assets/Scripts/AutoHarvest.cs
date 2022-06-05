using UnityEngine;

public class AutoHarvest : ButtonWithIndicator {

	#region Inspector Assignments

	#endregion
	#region Variables

	#endregion
	#region Unity Methods

	private void Awake() {
		GetComponent<Unlockable>().Condition = this.UnlockCondition;
	}

	protected override void Update() {
		base.Update();
		if (this.isHovering) {
			TooltipManager.instance.SetToolTip("Auto Harvest: " + (!this.ActiveCondition() ? "On" : "Off"));
		}
	}

	#endregion

	public void HandleAutoHarvestButton() {
		GardenManager.instance.autoHarvestActive = !GardenManager.instance.autoHarvestActive;
	}

	protected override bool ActiveCondition() {
		return !GardenManager.instance.autoHarvestActive;
	}

	private bool UnlockCondition() {
		return UpgradeManager.instance.HasUpgrade("auto_harvest");
	}

}
