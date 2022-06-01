using UnityEngine;

public class TooltipManager : NateMills.UnityUtility.TooltipManager {

	#region Inspector Assignments

	#endregion
	#region Variables

	#endregion
	#region Unity Methods

	protected override void Start() {
		base.Start();
	}

	protected override void Update() {
		base.Update();
	}

	#endregion


	public override void SetHoverTooltip(string tooltipText) {
		this.tooltipText.text = tooltipText;
		this.ShowTooltip();
	}
}
