using UnityEngine;

public class ResetGameButton : Tooltip {

	#region Inspector Assignments

	#endregion
	#region Variables

	#endregion
	#region Unity Methods

	private void Start() {
	}

	#endregion
	public override void ShowTooltip() {
		TooltipManager.instance.SetToolTip("Warning this will HARD RESET your game!");
	}

	public void HandleResetGameButton() {
		SaveLoad.LoadState(true);
		SaveLoad.SaveState();
	}
}
