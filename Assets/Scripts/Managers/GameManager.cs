using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {

	#region Singleton
	public static GameManager instance;

	private void createInstance() {
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

	[SerializeField]
	private Item[] items;
	public static Dictionary<string, Item> itemLookUp = new Dictionary<string, Item>();

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

	private void Awake() {
		this.createInstance();
		this.BuildLookUps();
	}

	private void Start() {
		this.goldResource = new Resource("Gold");
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

	private void BuildLookUps() {
		GameManager.itemLookUp = new Dictionary<string, Item>();
		foreach (Item item in this.items) {
			if (!GameManager.itemLookUp.ContainsKey(item.slug)) {
				GameManager.itemLookUp.Add(item.slug, item);
			}
		}
	}

}
