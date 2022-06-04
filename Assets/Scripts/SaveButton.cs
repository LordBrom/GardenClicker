using UnityEngine;
using NateMills.UnityUtility;

public class SaveButton : Tooltip {

	#region Inspector Assignments

	#endregion
	#region Variables

	private Cooldown autoSaveCooldown;

	#endregion
	#region Unity Methods

	private void Start() {
		this.autoSaveCooldown = new Cooldown(30);

	}

	private void Update() {
		if (this.isHovering) {
			TooltipManager.instance.SetToolTip("Click to save (Auto saves in " + Formatter.TimeFormat(this.autoSaveCooldown.currentCooldown) + ")");
		}

		if (this.autoSaveCooldown.TickCooldown(Time.deltaTime)) {
			SaveLoad.SaveState();
			this.autoSaveCooldown.StartCooldown();
		}
	}

	#endregion
	public override void ShowTooltip() {
		//TooltipManager.instance.SetToolTip(this.tooltipText);
	}

	public void HandleSaveGameButton() {
		SaveLoad.SaveState();
	}
}
