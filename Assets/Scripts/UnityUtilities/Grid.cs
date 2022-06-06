using System;
using UnityEngine;

namespace NateMills.UnityUtility {

	public class Grid<TGridObject> {
		#region Variables

		protected int width;
		protected int height;
		protected float cellSize;
		protected Vector3 originPosition;
		protected TGridObject[,] gridArray;
		protected TextMesh[,] debugTextArray;
		protected Vector3 cellOffset;

		public bool showDebug = false;

		public event EventHandler<OnGridValueChangedEventArgs> OnGridValueChanged;
		public class OnGridValueChangedEventArgs : EventArgs {
			public int x;
			public int y;
		}

		#endregion

		public Grid(int width, int height, float cellSize, Vector3 originPosition, Func<Grid<TGridObject>, int, int, TGridObject> createGridObject, bool debug = false) {
			this.width = width;
			this.height = height;
			this.cellSize = cellSize;
			this.cellOffset = new Vector3(this.cellSize, this.cellSize) * 0.5f;
			this.originPosition = originPosition;
			this.showDebug = debug;

			this.gridArray = new TGridObject[width, height];
			for (int x = 0; x < this.gridArray.GetLength(0); x++) {
				for (int y = 0; y < this.gridArray.GetLength(1); y++) {
					this.gridArray[x, y] = createGridObject(this, x, y);
				}
			}

			if (showDebug) { this.DrawDebug(); }
		}

		private void DrawDebug() {
			GameObject debugContainer = new GameObject("Grid_Debug");
			debugTextArray = new TextMesh[this.width, this.height];
			for (int x = 0; x < gridArray.GetLength(0); x++) {
				for (int y = 0; y < gridArray.GetLength(1); y++) {

					GameObject gameObject = new GameObject(x + "_" + y, typeof(TextMesh));
					Transform transform = gameObject.transform;
					transform.SetParent(debugContainer.transform);
					transform.localPosition = GetWorldPosition(x, y, true);
					TextMesh textMesh = gameObject.GetComponent<TextMesh>();
					textMesh.text = gridArray[x, y]?.ToString();
					textMesh.color = Color.white;
					textMesh.anchor = TextAnchor.MiddleCenter;
					debugTextArray[x, y] = textMesh;


					Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
					Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);

				}
			}
			Debug.DrawLine(GetWorldPosition(0, this.height), GetWorldPosition(this.width, this.height), Color.white, 100f);
			Debug.DrawLine(GetWorldPosition(this.width, 0), GetWorldPosition(this.width, this.height), Color.white, 100f);

			OnGridValueChanged += (object sender, OnGridValueChangedEventArgs eventArgs) => {
				debugTextArray[eventArgs.x, eventArgs.y].text = gridArray[eventArgs.x, eventArgs.y].ToString();
			};
		}

		public int GetHeight() {
			return this.height;
		}
		public int GetWidth() {
			return this.width;
		}
		public float GetCellSize() {
			return this.cellSize;
		}
		public Vector3 GetOriginPosition() {
			return this.originPosition;
		}
		public Vector3 GetWorldPosition(int x, int y, bool includeCellOffset = false) {
			return new Vector3(x, y) * this.cellSize + this.originPosition + (includeCellOffset ? this.cellOffset : Vector3.zero);
		}

		public void GetXY(Vector3 worldPosition, out int x, out int y) {
			x = Mathf.FloorToInt((worldPosition - this.originPosition).x / this.cellSize);
			y = Mathf.FloorToInt((worldPosition - this.originPosition).y / this.cellSize);
		}
		public void GetXY(out int x, out int y) {
			GetXY(Camera.main.ScreenToWorldPoint(Input.mousePosition), out x, out y);
		}

		public bool OnGrid(int x, int y) {
			if (x >= 0 && y >= 0 && x < width && y < height) {
				return true;
			}
			return false;
		}
		public bool OnGrid(Vector3 worldPosition) {
			GetXY(worldPosition, out int x, out int y);
			return OnGrid(x, y);
		}

		public TGridObject GetGridObject() {
			GetXY(out int x, out int y);
			return GetGridObject(x, y);
		}

		public TGridObject GetGridObject(int x, int y) {
			if (OnGrid(x, y)) {
				return gridArray[x, y];
			}
			return default(TGridObject);
		}
		public TGridObject GetGridObject(Vector3 worldPosition) {
			GetXY(worldPosition, out int x, out int y);
			return GetGridObject(x, y);
		}

		public Vector3 CellCenter(int x, int y) {
			if (OnGrid(x, y)) {
				return new Vector3(x, y) * this.cellSize + Vector3.one * (this.cellSize / 2);
			}
			return default(Vector3);
		}
		public Vector3 CellCenter(Vector3 worldPosition) {
			GetXY(worldPosition, out int x, out int y);
			return CellCenter(x, y);
		}
		public Vector3 CellCenter(int x, int y, float zPosition) {
			if (OnGrid(x, y)) {
				return new Vector3(x, y, zPosition) * this.cellSize + Vector3.one * (this.cellSize / 2);
			}
			return default(Vector3);
		}
		public Vector3 CellCenter(Vector3 worldPosition, float zPosition) {
			GetXY(worldPosition, out int x, out int y);
			return CellCenter(x, y, zPosition);
		}

		public void SetGridObject(Vector3 worldPosition, TGridObject value) {
			GetXY(worldPosition, out int x, out int y);
			SetGridObject(x, y, value);
		}
		public void SetGridObject(int x, int y, TGridObject value) {
			if (OnGrid(x, y)) {
				gridArray[x, y] = value;
				TriggerGridObjectChanged(x, y);
			}
		}
		public void TriggerGridObjectChanged(int x, int y) {
			if (OnGridValueChanged != null) OnGridValueChanged(this, new OnGridValueChangedEventArgs { x = x, y = y });
		}

		public class GridObject {
			protected Grid<TGridObject> grid;
			public int x { get; protected set; }
			public int y { get; protected set; }

			public GridObject(Grid<TGridObject> grid, int x, int y) {
				this.grid = grid;
				this.x = x;
				this.y = y;
			}

			public override string ToString() {
				return x + " " + y;
			}
		}
	}


}
