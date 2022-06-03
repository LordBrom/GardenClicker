using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {

	#region Singleton
	public static InventoryManager instance;

	private void Awake() {
		if (instance != null) {
			Destroy(gameObject);
			return;
		}
		instance = this;
	}
	#endregion
	#region Inspector Assignments

	[SerializeField]
	private GameObject inventoryItemPrefab;
	[SerializeField]
	private Seed[] startSeeds;

	private int inventorySize = 20;

	#endregion
	#region Variables

	private InventoryItem[] inventory;

	#endregion
	#region Unity Methods

	private void Start() {
		this.BuildInventory();
		//foreach (Seed seed in this.startSeeds) {
		//	this.AddToInventory(seed);
		//}

	}

	private void Update() {

	}

	#endregion
	#region Save/Load

	public string GetSaveString() {
		List<string> inventoryList = new List<string>();
		for (int i = 0; i < this.inventory.Length; i++) {
			if (this.inventory[i].item == null) {
				continue;
			}
			inventoryList.Add(i + ":" + this.inventory[i].item.id + ":" + this.inventory[i].itemCount);
		}
		return string.Join('|', inventoryList);
	}

	public void LoadSaveString(string saveString) {
		if (saveString == "") {
			return;
		}
		this.BuildInventory();

		string[] inventoryList = saveString.Split("|");
		foreach (string inventoryDetail in inventoryList) {
			string[] inventoryDetailSplit = inventoryDetail.Split(':');
			this.inventory[int.Parse(inventoryDetailSplit[0])].SetItem(Item.lookup[int.Parse(inventoryDetailSplit[1])], int.Parse(inventoryDetailSplit[2]));
		}
	}

	#endregion

	private void BuildInventory() {
		if (this.inventory != null) {
			foreach (InventoryItem inventoryItem in this.inventory) {
				Destroy(inventoryItem.gameObject);
			}
		}
		this.inventory = new InventoryItem[this.inventorySize];
		for (int i = 0; i < this.inventorySize; i++) {
			this.inventory[i] = Instantiate(this.inventoryItemPrefab, this.transform).GetComponent<InventoryItem>();
		}
	}

	public bool AddToInventory(Item item, int count = 1) {
		int useSlot = -1;
		for (int i = 0; i < this.inventory.Length; i++) {
			if (useSlot == -1 && this.inventory[i].item == null) {
				useSlot = i;
			}
			if (this.inventory[i].item == item) {
				useSlot = i;
				break;
			}
		}

		if (useSlot == -1) {
			return false;
		}
		this.inventory[useSlot].AddItem(item, count);
		return true;
	}

	public bool RemoveFromInventory(Item item, int count) {
		for (int i = 0; i < this.inventory.Length; i++) {
			if (this.inventory[i].item == item) {
				if (this.inventory[i].itemCount >= count) {
					this.inventory[i].itemCount -= count;
					if (this.inventory[i].itemCount == 0) {
						this.inventory[i].ClearItem();
					}
					return true;
				}
				return false;
			}
		}
		return false;
	}
}
