using UnityEngine;

public static class SaveLoad {

	public static void SaveState() {
		SaveState saveState = new SaveState();
		saveState.UpgradeSaveState = UpgradeManager.instance.GetSaveString();
		saveState.InventorySaveState = InventoryManager.instance.GetSaveString();
		saveState.GardenPlotSaveState = GardenManager.instance.GetSaveString();
		saveState.gold = GameManager.instance.goldResource.amountHeld;
		PlayerPrefs.SetString("saveState", JsonUtility.ToJson(saveState));
		Debug.Log(PlayerPrefs.GetString("saveState"));
	}

	public static void LoadState() {
		if (!PlayerPrefs.HasKey("saveState")) {
			return;
		}
		SaveState saveState = JsonUtility.FromJson<SaveState>(PlayerPrefs.GetString("saveState"));

		UpgradeManager.instance.LoadSaveString(saveState.UpgradeSaveState);
		InventoryManager.instance.LoadSaveString(saveState.InventorySaveState);
		GardenManager.instance.LoadSaveString(saveState.GardenPlotSaveState);
	}

}

public class SaveState {

	public string UpgradeSaveState = "";
	public string InventorySaveState = "";
	public string GardenPlotSaveState = "";
	public int gold = 0;

}
