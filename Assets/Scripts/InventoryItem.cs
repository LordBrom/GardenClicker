using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class InventoryItem : ButtonWithIndicator {

	#region Inspector Assignments

	[SerializeField]
	private TextMeshProUGUI itemCountText;

	#endregion
	#region Variables

	public Item item { get; private set; }
	public int itemCount;
	private Image image;

	#endregion
	#region Unity Methods

	private void Awake() {
		this.image = GetComponent<Image>();
	}

	protected override void Update() {
		base.Update();
		this.SetItemCountText();
	}

	#endregion

	public void AddItem(Item item, int count) {
		if (this.item == null) {
			this.SetItem(item);
		}
		this.itemCount += count;
	}

	public void SetItem(Item item) {
		this.item = item;
		this.image.sprite = item.image;
		this.tooltipText = this.item.name;
		this.itemCount = 0;
	}

	public void ClearItem() {
		this.item = null;
		this.image.sprite = null;
		this.tooltipText = "";
		this.itemCount = 0;
	}

	public void HandleInventoryItemButton() {
		if (this.item != null && this.item.type == Item.Type.Seed) {
			GameManager.instance.SetCursorMode((Seed)this.item);
		}
	}

	private void SetItemCountText() {
		if (this.item == null) {
			itemCountText.text = "";
			return;
		}
		itemCountText.text = this.itemCount.ToString();
	}

	private bool InventoryItemSelected() {
		if (this.item != null &&
			GameManager.instance.activeCurserMode == GameManager.CurserMode.Seed &&
			this.item.type == Item.Type.Seed &&
			GameManager.instance.activeSeed == this.item
		) {
			return true;
		}
		return false;
	}

	protected override bool ActiveCondition() {
		return this.InventoryItemSelected();
	}

	public override void ShowTooltip() {
		if (this.item != null) {
			TooltipManager.instance.SetItemTooltip(this.item);
		}
	}

}
