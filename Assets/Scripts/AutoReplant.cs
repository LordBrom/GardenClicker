using UnityEngine;

public class AutoReplant : ButtonWithIndicator {

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
			TooltipManager.instance.SetToolTip("Auto Replant: " + (!this.ActiveCondition() ? "On" : "Off"));
		}
	}

	#endregion

	public void HandleAutoReplantButton() {
		GardenManager.instance.autoReplantActive = !GardenManager.instance.autoReplantActive;
	}

	protected override bool ActiveCondition() {
		return !GardenManager.instance.autoReplantActive;
	}

	private bool UnlockCondition() {
		return UpgradeManager.instance.HasUpgrade("auto_replant");
	}

}
