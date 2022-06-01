using UnityEngine;

namespace NateMills.UnityUtility {
	public class Cooldown {

		#region Variables

		public bool isReady { get; private set; }
		public float cooldownTime { get; private set; }
		public float currentCooldown { get; private set; }

		#endregion

		public Cooldown(float cooldownTime, bool startReady = false) {
			this.cooldownTime = cooldownTime;
			if (startReady) {
				this.isReady = true;
				this.currentCooldown = 0;
			} else {
				this.isReady = false;
				this.currentCooldown = cooldownTime;
			}
		}
		public bool TickCooldown(float tickAmount) {
			if (this.currentCooldown <= 0) {
				return true;
			}
			this.currentCooldown -= tickAmount;
			if (this.currentCooldown <= 0) {
				this.currentCooldown = 0;
				this.isReady = true;
			}
			return this.isReady;
		}

		public void ClearCooldown() {
			this.isReady = true;
			this.currentCooldown = 0;
		}

		public void StartCooldown() {
			this.isReady = false;
			this.currentCooldown = this.cooldownTime;
		}
	}
}
