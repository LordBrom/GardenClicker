using UnityEngine;
using TMPro;
using NateMills.UnityUtility;

public class ItemTooltip : HidableMenu {

	#region Inspector Assignments

	[SerializeField]
	private TextMeshProUGUI nameText;
	[SerializeField]
	private TextMeshProUGUI descriptionText;
	[SerializeField]
	private TextMeshProUGUI sellValueText;

	#endregion
	#region Variables

	#endregion
	#region Unity Methods

	#endregion

	public void ShowTooltip(Item item) {
		this.nameText.text = item.name;
		this.descriptionText.text = item.description;
		this.sellValueText.text = item.sellValue.ToString();
		this.ShowMenu();
	}
	public void ClearTooltip() {
		this.nameText.text = "";
		this.descriptionText.text = "";
		this.sellValueText.text = "";
		this.HideMenu();
	}
}
