using UnityEngine;
using System.Collections;

public class MouseLockBehaviour : MonoBehaviour {
    void DidLockCursor() {
        Debug.Log("Locking cursor");
        //guiTexture.enabled = false;
    }
    void DidUnlockCursor() {
        Debug.Log("Unlocking cursor");
        //guiTexture.enabled = true;
    }
    void OnMouseDown() {
        Screen.lockCursor = true;
    }
    private bool wasLocked = false;
    void Update() {
//        if (Input.GetKeyDown("escape"))
//            Screen.lockCursor = false;
        
		if(Datacore.bDisplayAllMenus && wasLocked) {
			Screen.lockCursor = false;
			Debug.Log("unlocked mouse cursor");
		} else if (!Datacore.bDisplayAllMenus && !wasLocked) {
			Screen.lockCursor = true;
			Debug.Log("locked mouse cursor");
		}
		
        if (!Screen.lockCursor && wasLocked) {
            wasLocked = false;
            DidUnlockCursor();
        } else
            if (Screen.lockCursor && !wasLocked) {
                wasLocked = true;
                DidLockCursor();
            }
    }
}

//test comment for git