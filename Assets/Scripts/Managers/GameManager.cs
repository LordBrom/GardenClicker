using UnityEngine;
using TMPro;
using NateMills.UnityUtility;

public class GameManager : MonoBehaviour {

	#region Singleton
	public static GameManager instance;

	private void Awake() {
		if (instance != null) {
			Destroy(gameObject);
			return;
		}
		instance = this;
	}
	#endregion
	#region Inspector Assignments

	public Seed activeSeed;

	[SerializeField]
	private GameObject gardenPlotPrefab;
	[SerializeField]
	private Transform gardenTransform;

	#endregion
	#region Variables

	public CurserMode activeCurserMode { get; private set; }

	public enum CurserMode {
		None,
		Water,
		Seed,
	}

	public Resource goldResource;
	[SerializeField]
	private TextMeshProUGUI goldText;

	#endregion

	#region Unity Methods
	private void Start() {
		this.goldResource = new Resource("Gold");
		this.goldResource.GainResource(100);

		SaveLoad.LoadState();
	}

	private void Update() {
		this.goldText.text = Formatter.NumberFormat(this.goldResource.amountHeld);
	}
	#endregion

	public void ClearCursorMode() {
		this.activeCurserMode = CurserMode.None;
		this.activeSeed = null;
	}
	public void SetCursorMode(CurserMode curserMode) {
		this.activeCurserMode = curserMode;
	}
	public void SetCursorMode(Seed seed) {
		this.activeCurserMode = CurserMode.Seed;
		this.activeSeed = seed;
	}

	public void SaveGame() {
		SaveLoad.SaveState();
	}
}
