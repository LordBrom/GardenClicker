using UnityEngine;

namespace NateMills.UnityUtility {

	[RequireComponent(typeof(CanvasGroup))]
	public class HidableMenu : MonoBehaviour {

		#region Inspector Assignments

		[SerializeField]
		private bool startOpen;

		#endregion
		#region Variables

		private CanvasGroup canvasGroup;
		public bool isShown { get; private set; }

		#endregion
		#region Unity Methods

		protected virtual void Awake() {
			this.canvasGroup = GetComponent<CanvasGroup>();
		}
		protected virtual void Start() {
			if (this.startOpen) {
				this.ShowMenu();
			} else {

				this.HideMenu();
			}
		}

		#endregion

		public void ShowMenu() {
			this.isShown = true;
			this.canvasGroup.alpha = 1;
			this.canvasGroup.interactable = true;
			this.canvasGroup.blocksRaycasts = true;
		}

		public void HideMenu() {
			this.isShown = false;
			this.canvasGroup.alpha = 0;
			this.canvasGroup.interactable = false;
			this.canvasGroup.blocksRaycasts = false;
		}

		public void ToggleMenu() {
			if (this.isShown) {
				this.HideMenu();
			} else {
				this.ShowMenu();
			}
		}
	}
}
