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
		if (!gardenPlot.isWatered) {
			this.wateredText.text = "No";
		} else if (UpgradeManager.instance.HasUpgrade("water_tooltip")) {
			this.wateredText.text = Formatter.TimeFormat(gardenPlot.wateredCooldown.GetRemainingTime());
		} else {
			this.wateredText.text = "Yes";
		}
		this.ShowMenu();
	}
	public void ClearTooltip() {
		this.HideMenu();
	}
}
