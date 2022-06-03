using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TooltipManager : MonoBehaviour {

	#region Singleton

	public static TooltipManager instance;

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
	private ItemTooltip itemTooltip;
	[SerializeField]
	private GardenPlotTooltip gardenPlotTooltip;

	#endregion
	#region Variables

	private CanvasGroup canvasGroup;
	private RectTransform rectTransform;

	#endregion
	#region Unity Methods

	private void Start() {
		this.canvasGroup = GetComponent<CanvasGroup>();
		this.rectTransform = GetComponent<RectTransform>();
		this.HideTooltip();
	}

	private void Update() {
		this.SetTooltipPosition();
	}

	#endregion
	#region Tooltip Positions

	private void ShowTooltip() {
		this.canvasGroup.alpha = 1;
	}
	private void HideTooltip() {
		this.canvasGroup.alpha = 0;
	}

	private void SetTooltipPosition() {
		this.SetPivot();
		this.transform.position = Input.mousePosition;
	}
	private void SetPivot() {
		int pivotX = 0;
		int pivotY = 0;

		if (Screen.width < Input.mousePosition.x + this.rectTransform.sizeDelta.x) {
			pivotX = 1;
		}
		if (Screen.height < Input.mousePosition.y + this.rectTransform.sizeDelta.y) {
			pivotY = 1;
		}

		this.rectTransform.pivot = new Vector2(pivotX, pivotY);
	}

	public void ClearHoverTooltip() {
		this.itemTooltip.ClearTooltip();
		this.gardenPlotTooltip.ClearTooltip();
		this.HideTooltip();
	}

	#endregion

	public void SetHoverTooltip(string tooltipText) {
		this.ShowTooltip();
	}

	public void SetItemTooltip(Item item) {
		this.itemTooltip.ShowTooltip(item);
		this.ShowTooltip();
	}
	public void SetGardenPlotTooltip(GardenPlot gardenPlot) {
		this.gardenPlotTooltip.ShowTooltip(gardenPlot);
		this.ShowTooltip();
	}
}

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	#region Inspector Assignments

	public string tooltipText;

	#endregion

	public void OnPointerEnter(PointerEventData eventData) {
		this.ShowTooltip();
	}

	public void OnPointerExit(PointerEventData eventData) {
		this.HideTooltip();
	}

	public void OnMouseOver() {
		this.ShowTooltip();
	}

	public void OnMouseExit() {
		this.HideTooltip();
	}

	public virtual void ShowTooltip() {
		TooltipManager.instance.SetHoverTooltip(this.tooltipText);
	}

	public virtual void HideTooltip() {
		TooltipManager.instance.ClearHoverTooltip();
	}
}
