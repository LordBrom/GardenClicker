using UnityEngine;
using NateMills.UnityUtility;

public class GameManager : MonoBehaviour {

	#region Inspector Assignments

	public Flower activeFlower;

	[SerializeField]
	private GameObject gardenPlotPrefab;
	[SerializeField]
	private Transform gardenTransform;

	#endregion
	#region Variables

	private Grid<GardenPlotGridObject> gardenPlotGrid;
	private int gardenPlotWidth = 3;
	private int gardenPlotHeight = 2;

	#endregion

	#region Unity Methods
	private void Start() {
		this.gardenPlotGrid = new Grid<GardenPlotGridObject>(this.gardenPlotWidth, this.gardenPlotHeight, 1, Vector3.zero, (Grid<GardenPlotGridObject> g, int x, int y) => new GardenPlotGridObject(g, x, y));

		for (int x = 0; x < this.gardenPlotGrid.GetWidth(); x++) {
			for (int y = 0; y < this.gardenPlotGrid.GetHeight(); y++) {
				GardenPlot newGardenPlot = Instantiate(gardenPlotPrefab, gardenTransform).GetComponent<GardenPlot>();
				GardenPlotGridObject gardenPlotGridObject = this.gardenPlotGrid.GetGridObject(x, y);
				newGardenPlot.SetGridObject(gardenPlotGridObject);
				gardenPlotGridObject.SetGardenPlot(newGardenPlot);
			}
		}
	}

	private void Update() {
		if (Input.GetMouseButtonDown(0)) {
			GardenPlotGridObject clickedPlot = this.gardenPlotGrid.GetGridObject();
			if (clickedPlot != null) {
				clickedPlot.gardenPlot.WaterPlot();
			}
		}
		if (Input.GetMouseButtonDown(1)) {
			GardenPlotGridObject clickedPlot = this.gardenPlotGrid.GetGridObject();
			if (clickedPlot != null) {
				clickedPlot.gardenPlot.SetFlower(this.activeFlower);
			}
		}
	}
	#endregion
}
