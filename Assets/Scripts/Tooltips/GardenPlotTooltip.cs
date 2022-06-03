using UnityEngine;
using TMPro;
using NateMills.UnityUtility;

public class GardenPlotTooltip : HidableMenu {

	#region Inspector Assignments

	[SerializeField]
	private TextMeshProUGUI nameText;

	#endregion
	#region Variables

	#endregion
	#region Unity Methods

	#endregion

	public void ShowTooltip(GardenPlot gardenPlot) {
		if (gardenPlot.flower == null) {
			this.nameText.text = "Nothing Planted";
		} else {
			this.nameText.text = gardenPlot.flower.name;
		}
		this.ShowMenu();
	}
	public void ClearTooltip() {
		this.HideMenu();
	}
}
