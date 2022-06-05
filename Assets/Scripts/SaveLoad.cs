using UnityEngine;

public static class SaveLoad {

	public static void SaveState() {
		SaveState saveState = new SaveState();
		saveState.UpgradeSaveState = UpgradeManager.instance.GetSaveString();
		saveState.InventorySaveState = InventoryManager.instance.GetSaveString();
		saveState.GardenPlotSaveState = GardenManager.instance.GetSaveString();
		saveState.gold = GameManager.instance.goldResource.amountHeld;
		saveState.sprinklerActive = GardenManager.instance.sprinklerActive;
		saveState.sprinklerTime = GardenManager.instance.sprinklerCooldown.currentCooldown;
		saveState.autoHarvestActive = GardenManager.instance.autoHarvestActive;
		saveState.autoReplantActive = GardenManager.instance.autoReplantActive;

		PlayerPrefs.SetString("saveState", JsonUtility.ToJson(saveState));
	}

	public static void LoadState(bool loadNewGame = false) {
		SaveState saveState;
		if (!loadNewGame && PlayerPrefs.HasKey("saveState")) {
			saveState = JsonUtility.FromJson<SaveState>(PlayerPrefs.GetString("saveState"));
		} else {
			string startSeedID = GameManager.itemLookUp["grass_seed"].slug;
			saveState = new SaveState();
			saveState.GardenPlotSaveState = "1_1||";
			saveState.InventorySaveState = "0:" + startSeedID + ":1";
		}

		UpgradeManager.instance.LoadSaveString(saveState.UpgradeSaveState);
		InventoryManager.instance.LoadSaveString(saveState.InventorySaveState);
		GardenManager.instance.LoadSaveString(saveState.GardenPlotSaveState);
		GameManager.instance.goldResource.SetResource(saveState.gold);
		GardenManager.instance.LoadOtherSettings(saveState.sprinklerActive, saveState.sprinklerTime, saveState.autoHarvestActive, saveState.autoReplantActive);
	}

}

public class SaveState {

	public string UpgradeSaveState = "";
	public string InventorySaveState = "";
	public string GardenPlotSaveState = "";
	public int gold = 0;
	public bool sprinklerActive = false;
	public float sprinklerTime = 0;
	public bool autoHarvestActive = false;
	public bool autoReplantActive = false;

}
