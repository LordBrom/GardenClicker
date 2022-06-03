using UnityEngine;

public static class SaveLoad {

	public static void SaveState() {
		SaveState saveState = new SaveState();
		saveState.UpgradeSaveState = UpgradeManager.instance.GetSaveString();
		saveState.InventorySaveState = Inventory.instance.GetSaveString();
		PlayerPrefs.SetString("saveState", JsonUtility.ToJson(saveState));
		Debug.Log(PlayerPrefs.GetString("saveState"));
	}

	public static void LoadState() {
		if (!PlayerPrefs.HasKey("saveState")) {
			return;
		}
		SaveState saveState = JsonUtility.FromJson<SaveState>(PlayerPrefs.GetString("saveState"));

		UpgradeManager.instance.LoadSaveString(saveState.UpgradeSaveState);
		Inventory.instance.LoadSaveString(saveState.InventorySaveState);
	}

}

public class SaveState {

	public string UpgradeSaveState;
	public string InventorySaveState;

}
