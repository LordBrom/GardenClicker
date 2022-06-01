using UnityEngine;

namespace NateMills.UnityUtility {
	public class HidableMenu : MonoBehaviour {

		#region Inspector Assignments

		public bool startOpen;

		#endregion
		#region Variables

		private CanvasGroup canvasGroup;

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
			this.canvasGroup.alpha = 1;
			this.canvasGroup.interactable = true;
			this.canvasGroup.blocksRaycasts = true;
		}

		public void HideMenu() {
			this.canvasGroup.alpha = 0;
			this.canvasGroup.interactable = false;
			this.canvasGroup.blocksRaycasts = false;
		}
	}
}
