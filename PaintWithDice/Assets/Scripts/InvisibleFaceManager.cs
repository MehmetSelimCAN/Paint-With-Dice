using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleFaceManager : MonoBehaviour {

    private static InvisibleFaceManager instance;

    private Transform invisibleFacesUI;
    private Transform dice;

    private void Awake() {
        instance = this;

        invisibleFacesUI = GameObject.Find("InvisibleFacesUI").transform;
        invisibleFacesUI.gameObject.SetActive(false);

        dice = GameObject.Find("Dice").transform;
    }

    private void Start() {
        RefreshInvisibleFacesUI();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (invisibleFacesUI.gameObject.activeInHierarchy) {
                HideInvisibleFaces();
            } else {
                ShowInvisibleFaces();
            }
        }
    }

    private void ShowInvisibleFaces() {
        invisibleFacesUI.gameObject.SetActive(true);
    }

    private void HideInvisibleFaces() {
        invisibleFacesUI.gameObject.SetActive(false);
    }

    public void RefreshInvisibleFacesUI() {
        Transform leftFaceSign = invisibleFacesUI.Find("Left");
        Transform rightFaceSign = invisibleFacesUI.Find("Right");
        Transform bottomFaceSign = invisibleFacesUI.Find("Bottom");

        //First, close all UI sprites
        for (int i = 0; i < 6; i++) {
            leftFaceSign.Find("Faces").GetChild(i).gameObject.SetActive(false);
            rightFaceSign.Find("Faces").GetChild(i).gameObject.SetActive(false);
            bottomFaceSign.Find("Faces").GetChild(i).gameObject.SetActive(false);
        }

        Vector3 diceLocalXDirection = dice.TransformDirection(Vector3.right);
        Vector3 diceLocalYDirection = dice.TransformDirection(Vector3.up);
        Vector3 diceLocalZDirection = dice.TransformDirection(Vector3.forward);

        //Sometimes dice's rotation stucks at 90.0001. That causes incorrect result for this statement. (diceLocalXDirection == Vector3.left)
        //Because of that, I used Vector3.Distance.
        #region Left Hidden Face UI
        if (Vector3.Distance(diceLocalXDirection, Vector3.left) < 0.1f) {
            leftFaceSign.Find("Faces").transform.Find("YellowFace").gameObject.SetActive(true);
        }
        else if (Vector3.Distance(diceLocalXDirection, Vector3.right) < 0.1f) {
            leftFaceSign.Find("Faces").transform.Find("RedFace").gameObject.SetActive(true);
        }
        else {
            if (Vector3.Distance(diceLocalYDirection, Vector3.left) < 0.1f) {
                leftFaceSign.Find("Faces").transform.Find("GreenFace").gameObject.SetActive(true);
            }
            else if (Vector3.Distance(diceLocalYDirection, Vector3.right) < 0.1f) {
                leftFaceSign.Find("Faces").transform.Find("PurpleFace").gameObject.SetActive(true);
            }
            else {
                if (Vector3.Distance(diceLocalZDirection, Vector3.left) < 0.1f) {
                    leftFaceSign.Find("Faces").transform.Find("BlueFace").gameObject.SetActive(true);
                }
                else if (Vector3.Distance(diceLocalZDirection, Vector3.right) < 0.1f) {
                    leftFaceSign.Find("Faces").transform.Find("AquaFace").gameObject.SetActive(true);
                }
            }
        }
        #endregion
        #region Right Hidden Face UI
        if (Vector3.Distance(diceLocalXDirection, Vector3.forward) < 0.1f) {
            rightFaceSign.Find("Faces").transform.Find("YellowFace").gameObject.SetActive(true);
        }
        else if (Vector3.Distance(diceLocalXDirection, Vector3.back) < 0.1f) {
            rightFaceSign.Find("Faces").transform.Find("RedFace").gameObject.SetActive(true);
        }
        else {
            if (Vector3.Distance(diceLocalYDirection, Vector3.forward) < 0.1f) {
                rightFaceSign.Find("Faces").transform.Find("GreenFace").gameObject.SetActive(true);
            }
            else if (Vector3.Distance(diceLocalYDirection, Vector3.back) < 0.1f) {
                rightFaceSign.Find("Faces").transform.Find("PurpleFace").gameObject.SetActive(true);
            }
            else {
                if (Vector3.Distance(diceLocalZDirection, Vector3.forward) < 0.1f) {
                    rightFaceSign.Find("Faces").transform.Find("BlueFace").gameObject.SetActive(true);
                }
                else if (Vector3.Distance(diceLocalZDirection, Vector3.back) < 0.1f) {
                    rightFaceSign.Find("Faces").transform.Find("AquaFace").gameObject.SetActive(true);
                }
            }
        }
        #endregion
        #region Bottom Hidden Face UI
        if (Vector3.Distance(diceLocalXDirection, Vector3.down) < 0.1f) {
            bottomFaceSign.Find("Faces").transform.Find("YellowFace").gameObject.SetActive(true);
        }
        else if (Vector3.Distance(diceLocalXDirection, Vector3.up) < 0.1f) {
            bottomFaceSign.Find("Faces").transform.Find("RedFace").gameObject.SetActive(true);
        }
        else {
            if (Vector3.Distance(diceLocalYDirection, Vector3.down) < 0.1f) {
                bottomFaceSign.Find("Faces").transform.Find("GreenFace").gameObject.SetActive(true);
            }
            else if (Vector3.Distance(diceLocalYDirection, Vector3.up) < 0.1f) {
                bottomFaceSign.Find("Faces").transform.Find("PurpleFace").gameObject.SetActive(true);
            }
            else {
                if (Vector3.Distance(diceLocalZDirection, Vector3.down) < 0.1f) {
                    bottomFaceSign.Find("Faces").transform.Find("BlueFace").gameObject.SetActive(true);
                }
                else if (Vector3.Distance(diceLocalZDirection, Vector3.up) < 0.1f) {
                    bottomFaceSign.Find("Faces").transform.Find("AquaFace").gameObject.SetActive(true);
                }
            }
        }
        #endregion
    }

    public static InvisibleFaceManager GetInstance() {
        return instance;
    }
}
