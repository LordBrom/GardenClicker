using UnityEngine;

namespace NateMills.UnityUtility {
	public class Cooldown {

		#region Variables

		public bool isReady { get; private set; }
		public float cooldownTime { get; private set; }
		public float currentCooldown { get; private set; }
		public float tickMultiplier = 1;

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
			this.currentCooldown -= tickAmount * this.tickMultiplier;
			if (this.currentCooldown <= 0) {
				this.currentCooldown = 0;
				this.isReady = true;
				return true;
			}
			return false;
		}

		public void ClearCooldown() {
			this.isReady = true;
			this.currentCooldown = 0;
		}

		public void StartCooldown() {
			this.isReady = false;
			this.currentCooldown = this.cooldownTime;
			this.tickMultiplier = 1;
		}
		public void StartCooldown(float presetAmount) {
			this.isReady = false;
			this.currentCooldown = presetAmount;
			this.tickMultiplier = 1;
		}

		public float PercentComplete(bool asDecimal = true) {
			return ((this.cooldownTime - this.currentCooldown) / this.cooldownTime) * (asDecimal ? 1 : 100);
		}
		public float PercentRemaining(bool asDecimal = true) {
			return (this.currentCooldown / this.cooldownTime) * (asDecimal ? 1 : 100);
		}

		public float GetRemainingTime() {
			return this.currentCooldown / this.tickMultiplier;
		}
	}
}
