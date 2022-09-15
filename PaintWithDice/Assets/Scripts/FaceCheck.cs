using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCheck : MonoBehaviour {

    private RaycastHit hit;

    public void CheckSurface() {
        if (Physics.Raycast(transform.position, Vector3.down, out hit)) {               //If it hits something...
            if (hit.transform.tag == "Paper") {                                         //...and that something is paper...
                PaintManager.GetInstance().PaintSurface(this.transform, hit.transform); //...paint paper.
            } 
            else if (hit.transform.tag == "Finish") {                                   //That something is finish...
                LevelManager.GetInstance().CheckLevelCompleted();                       //...check level is completed or not.
            } 
            else if (hit.transform.tag.Contains("Level")) {                             //That something is levels on the menu...
                MenuLevelManager.GetInstance().GoToLevel(hit.transform.tag);            //Go to that level.
            }
        }
    }
}
