using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace NateMills.UnityUtility {
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
		private TextMeshProUGUI tooltipText;

		#endregion
		#region Variables

		protected CanvasGroup canvasGroup;
		protected RectTransform rectTransform;

		#endregion
		#region Unity Methods

		protected virtual void Start() {
			this.canvasGroup = GetComponent<CanvasGroup>();
			this.rectTransform = GetComponent<RectTransform>();
			this.HideTooltip();
		}

		protected virtual void Update() {
			this.SetTooltipPosition();
		}

		#endregion

		public void SetHoverTooltip(string tooltipText) {
			this.tooltipText.text = tooltipText;
			this.ShowTooltip();
		}

		public void ClearHoverTooltip() {
			this.HideTooltip();
		}


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
	}

	public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

		#region Inspector Assignments

		protected string tooltipText;

		#endregion

		public void OnPointerEnter(PointerEventData eventData) {
			TooltipManager.instance.SetHoverTooltip(this.tooltipText);
		}

		public void OnPointerExit(PointerEventData eventData) {
			TooltipManager.instance.ClearHoverTooltip();
		}
	}
}
