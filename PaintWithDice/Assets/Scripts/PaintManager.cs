using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintManager : MonoBehaviour {

    private static PaintManager instance;

    private Transform dice;

    private void Awake() {
        instance = this;

        dice = GameObject.Find("Dice").transform;
    }

    public void PaintSurface(Transform diceFace, Transform paper) {
        paper.GetComponent<MeshRenderer>().material.color = diceFace.GetComponent<MeshRenderer>().material.color; //Paint paper with same color of that face's color.
        int faceIndex = diceFace.GetSiblingIndex();
        paper.GetComponentInChildren<SpriteRenderer>().sprite = dice.GetChild(0).GetChild(faceIndex).GetComponentInChildren<SpriteRenderer>().sprite;

        //That is necessary for the 2, 3, 6 faces. Because these faces can print horizontal and vertical.
        paper.GetChild(0).transform.rotation = diceFace.GetChild(0).transform.rotation; 

        //On the menu, there is no Level Manager. Because of that, we check the Level Manager instance.
        if (LevelManager.GetInstance() != null) {
            //After painting, we check paper with map.
            LevelManager.GetInstance().CheckPaperCorrectness(paper);
        }
    }

    public static PaintManager GetInstance() {
        return instance;
    }
}
