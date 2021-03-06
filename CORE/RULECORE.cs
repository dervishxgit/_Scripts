using UnityEngine;
using System.Collections;

public class RULECORE : MonoBehaviour {
	
	/*
	 * Imported Rulecore, (Johnathan Nix) needs adjustment
	 */ 
	
	//additional notes: most likely the functionality will be copied out of here into datacore
	//so that we do not look to multiple places for these conceptually similar functions
	
	//SeekTarget3D settings
	
		
	static public void _RotateYaw(Component bot, float fTurnRate) {
		if(fTurnRate > 6.0f) fTurnRate = 6.0f;
		if(fTurnRate < -6.0f) fTurnRate = -6.0f;
		bot.transform.Rotate(fTurnRate * Vector3.up);
	}
	
	static public void _RotatePitch(Component bot, float fTurnRate) {
		if(fTurnRate > 6.0f) fTurnRate = 6.0f;
		if(fTurnRate < -6.0f) fTurnRate = -6.0f;
		bot.transform.Rotate(fTurnRate * Vector3.right);		
	}
	
	static public void _MoveForward(Component bot, float fVelocity) {
		if(fVelocity > 0.75f) fVelocity = 0.75f;
		if(fVelocity < -0.75f) fVelocity = -0.75f;
		bot.transform.Translate(fVelocity * Vector3.forward, Space.Self);	
	}
	
	// _SeekTarget3D : Seeks out the indicated target and returns true when reached (adjusted from original version below, uses settings supplied above)
	static public bool _SeekTarget3D(Component bot, Vector3 target, float fMaxVelocity) {
		float fTargetDistance;
		float zIsTargetBehindMe, zIsTargetInFrontOfMe, zIsTargetToMyLeft, zIsTargetToMyRight, zIsTargetAboveMe, zIsTargetBelowMe;
		AICORE._GetSpatialAwareness3D(bot, target, out fTargetDistance, 
			out zIsTargetBehindMe, out zIsTargetInFrontOfMe, 
			out zIsTargetToMyLeft, out zIsTargetToMyRight, 
			out zIsTargetAboveMe, out zIsTargetBelowMe);
		
		// Detect whether TARGET is sufficiently in front
		if(zIsTargetInFrontOfMe > 0.99) {
			// Satisfactally facing target	
			// No need to turn
		} else {
			// Should we turn right or left?
			if(zIsTargetToMyRight > zIsTargetToMyLeft) {
				// Turn right
				float fTurnRate;
				if(zIsTargetBehindMe > zIsTargetToMyRight) {
					fTurnRate = AICORE._Defuzzify(zIsTargetBehindMe, 0.0f, 6.0f);					
				} else {
					fTurnRate = AICORE._Defuzzify(zIsTargetToMyRight, 0.0f, 6.0f);
				}
				RULECORE._RotateYaw(bot, fTurnRate);
			} else {
				// Turn left
				float fTurnRate;
				if(zIsTargetBehindMe > zIsTargetToMyLeft) {
					fTurnRate = AICORE._Defuzzify(zIsTargetBehindMe, 0.0f, 6.0f);					
				} else {
					fTurnRate = AICORE._Defuzzify(zIsTargetToMyLeft, 0.0f, 6.0f);
				}
				RULECORE._RotateYaw(bot, -fTurnRate);
			}
		}
					
		if(fMaxVelocity > 0.0f) {
			// Only drive forward when facing nearly toward target	
			if(zIsTargetInFrontOfMe > 0.7) {
				// Only drive forward if we're far enough from target
				if(fTargetDistance >= 3.00f) {
					float fVelocity = AICORE._Defuzzify(zIsTargetInFrontOfMe, 0.0f, fMaxVelocity);
					RULECORE._MoveForward(bot, fVelocity);
				}
			}
			
			// Return whether target is reached
			return fTargetDistance < 3.00f;
		} else {
			// Return whether we're facing the target
			// Also include whether target is reached because when
			// we're very close to the target we get weird look at information
			return zIsTargetInFrontOfMe > 0.9f || fTargetDistance < 5.00f;
		}
		
	}
	
	// _SeekTarget : Seeks out the indicated target and returns true when reached
	static public bool _SeekTarget(Component bot, Vector3 target, float fMaxVelocity) {
		float fTargetDistance;
		float zIsTargetBehindMe, zIsTargetInFrontOfMe, zIsTargetToMyLeft, zIsTargetToMyRight, zIsTargetAboveMe, zIsTargetBelowMe;
		AICORE._GetSpatialAwareness3D(bot, target, out fTargetDistance, out zIsTargetBehindMe, out zIsTargetInFrontOfMe, out zIsTargetToMyLeft, out zIsTargetToMyRight, out zIsTargetAboveMe, out zIsTargetBelowMe);
		
		// Detect whether TARGET is sufficiently in front
		if(zIsTargetInFrontOfMe > 0.99) {
			// Satisfactally facing target	
			// No need to turn
		} else {
			// Should we turn right or left?
			if(zIsTargetToMyRight > zIsTargetToMyLeft) {
				// Turn right
				float fTurnRate;
				if(zIsTargetBehindMe > zIsTargetToMyRight) {
					fTurnRate = AICORE._Defuzzify(zIsTargetBehindMe, 0.0f, 6.0f);					
				} else {
					fTurnRate = AICORE._Defuzzify(zIsTargetToMyRight, 0.0f, 6.0f);
				}
				RULECORE._RotateYaw(bot, fTurnRate);
			} else {
				// Turn left
				float fTurnRate;
				if(zIsTargetBehindMe > zIsTargetToMyLeft) {
					fTurnRate = AICORE._Defuzzify(zIsTargetBehindMe, 0.0f, 6.0f);					
				} else {
					fTurnRate = AICORE._Defuzzify(zIsTargetToMyLeft, 0.0f, 6.0f);
				}
				RULECORE._RotateYaw(bot, -fTurnRate);
			}
		}
					
		if(fMaxVelocity > 0.0f) {
			// Only drive forward when facing nearly toward target	
			if(zIsTargetInFrontOfMe > 0.7) {
				// Only drive forward if we're far enough from target
				if(fTargetDistance >= 3.00f) {
					float fVelocity = AICORE._Defuzzify(zIsTargetInFrontOfMe, 0.0f, fMaxVelocity);
					RULECORE._MoveForward(bot, fVelocity);
				}
			}
			
			// Return whether target is reached
			return fTargetDistance < 3.00f;
		} else {
			// Return whether we're facing the target
			// Also include whether target is reached because when
			// we're very close to the target we get weird look at information
			return zIsTargetInFrontOfMe > 0.9f || fTargetDistance < 5.00f;
		}
		
	}
}
