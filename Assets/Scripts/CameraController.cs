using UnityEngine;

public class CameraController : MonoBehaviour {

	#region Inspector Assignments

	public float dragSpeed = 1.0f;
	public float scrollSpeed = 1.0f;
	public float maxScroll = 15;
	public float minScroll = 1;

	#endregion
	#region Variables

	private Vector3 dragOrigin;

	#endregion

	#region Unity Methods
	void Update() {

		Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - (Input.mouseScrollDelta.y * scrollSpeed), minScroll, maxScroll);

		if (Input.GetMouseButtonDown(1)) {
			dragOrigin = Input.mousePosition;
			return;
		}

		if (!Input.GetMouseButton(1)) {
			return;
		}

		Vector3 pos = Camera.main.ScreenToViewportPoint(dragOrigin - Input.mousePosition);
		Vector3 move = new Vector3(pos.x * dragSpeed, pos.y * dragSpeed, 0);

		transform.Translate(move, Space.World);
		dragOrigin = Input.mousePosition;
	}
	#endregion
}
