using UnityEngine;
using TMPro;
using NateMills.UnityUtility;

public class GardenPlotTooltip : HidableMenu {

	#region Inspector Assignments

	[SerializeField]
	private TextMeshProUGUI nameText;
	[SerializeField]
	private TextMeshProUGUI growthTimeText;
	[SerializeField]
	private TextMeshProUGUI wateredText;

	#endregion
	#region Variables

	#endregion
	#region Unity Methods

	#endregion

	public void ShowTooltip(GardenPlot gardenPlot) {
		if (gardenPlot.flower == null) {
			this.nameText.text = "Nothing Planted";
			this.growthTimeText.text = "";
		} else {
			this.nameText.text = gardenPlot.flower.name;
			this.growthTimeText.text = Formatter.TimeFormat(gardenPlot.flowerGrowth.GetRemainingTime());
		}
		this.wateredText.text = gardenPlot.isWatered ? "Yes" : "No";
		this.ShowMenu();
	}
	public void ClearTooltip() {
		this.HideMenu();
	}
}
