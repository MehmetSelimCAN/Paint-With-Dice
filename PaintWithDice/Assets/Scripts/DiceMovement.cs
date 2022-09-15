using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceMovement : MonoBehaviour {

    [SerializeField] private float rollingSpeed = 3;
    public static bool isRolling;     //It helps us to control it so that it does not move again while moving.

    private float diceScale;      //We use this to find anchor point.

    private Transform faces;    //Gameobject that keeps all faces of dice.
    private RaycastHit hit;

    private Transform cameraTransform;
    private Vector3 cameraOffsets;

    private void Awake() {
        diceScale = transform.localScale.x;

        //These also work.
        //cubeScale = transform.localScale.y;
        //cubeScale = transform.localScale.z;

        faces = transform.GetChild(0);

        cameraTransform = Camera.main.transform;
        cameraOffsets = cameraTransform.position - transform.position;
    }

    private void Update() {
        if (isRolling) return;  //While dice is rolling, we don't want to get input from user.

        if (Input.GetKey(KeyCode.W)) PrepareProperties(Vector3.forward);
        if (Input.GetKey(KeyCode.A)) PrepareProperties(Vector3.left);
        if (Input.GetKey(KeyCode.S)) PrepareProperties(Vector3.back);
        if (Input.GetKey(KeyCode.D)) PrepareProperties(Vector3.right);
    }

    private void PrepareProperties(Vector3 movementDirection) {
        var rollingAnchor = transform.position + (Vector3.down + movementDirection) * (diceScale / 2); //If dice's scale is 1, anchor point should be 0.5f (half of scale) further from that.

        //Axis should be perpendicular to our movement direction and upward.
        //That means, if we want to move left, our movement direction is left. The axis that perpendicular to both left and up is (0, 0, 1) Vector3.forward
        //If our movement direction is right. The axis that perpendicular to both right and up is (0, 0, -1) Vector3.back
        var rollingAxis = Vector3.Cross(Vector3.up, movementDirection);
        if (!isRolling) {
            if (Physics.Raycast(transform.position + (movementDirection * diceScale), Vector3.down, out hit)) {
                if (hit.transform.tag != "Untagged") {
                    StartCoroutine(Roll(rollingAnchor, rollingAxis));
                }
            }
        }
    }

    private IEnumerator Roll(Vector3 rollingAnchor, Vector3 rollingAxis) {
        isRolling = true;   //If rolling is not over, it cannot roll again in this process.

        for (int i = 0; i < (90 / rollingSpeed); i++) {
            transform.RotateAround(rollingAnchor, rollingAxis, rollingSpeed);
            CameraMovement();
            yield return new WaitForSeconds(0.01f);
        }

        isRolling = false;  //After rolling is over, we can get input to roll again.
        CheckFaceInteractions();    //We check which face is touched to paper.
        InvisibleFaceManager.GetInstance().RefreshInvisibleFacesUI();   //After rolling is over, we refresh the invisible faces ui.
    }

    private void CheckFaceInteractions() {
        for (int i = 0; i < 6; i++) {
            faces.GetChild(i).GetComponent<FaceCheck>().CheckSurface();
        }
    }

    private void CameraMovement() {
        cameraTransform.position = transform.position + cameraOffsets;
    }
}
