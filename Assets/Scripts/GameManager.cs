using UnityEngine;
using NateMills.UnityUtils;

public class GameManager : MonoBehaviour {

	#region Inspector Assignments

	#endregion
	#region Variables

	private Grid<GardenPlot> gardenPlotGrid;
	private int gardenPlotWidth = 3;
	private int gardenPlotHeight = 2;

	#endregion

	#region Unity Methods
	private void Start() {
		this.gardenPlotGrid = new Grid<GardenPlot>(this.gardenPlotWidth, this.gardenPlotHeight, 10, Vector3.zero, (Grid<GardenPlot> g, int x, int y) => new GardenPlot(g, x, y), true);
	}

	private void Update() {
		if (Input.GetMouseButtonDown(0)) {
			this.gardenPlotGrid.GetXY(out int x, out int y);
			Debug.Log(x + ", " + y);
		}
	}
	#endregion
}
