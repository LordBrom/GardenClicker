using UnityEngine;
using TMPro;
using NateMills.UnityUtility;

public class BasicTooltip : HidableMenu {

	#region Inspector Assignments

	[SerializeField]
	private TextMeshProUGUI tooltipText;

	#endregion
	#region Variables

	#endregion
	#region Unity Methods

	#endregion

	public void ShowTooltip(string tooltipText) {
		this.tooltipText.text = tooltipText;
		this.ShowMenu();
	}
	public void ClearTooltip() {
		this.tooltipText.text = "";
		this.HideMenu();
	}
}
