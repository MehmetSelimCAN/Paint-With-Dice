using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    private static LevelManager instance;
    private Transform mapPapers;

    private void Awake() {
        instance = this;

        mapPapers = GameObject.Find("Map").transform.Find("MapPapers");
    }

    public void CheckPaperCorrectness(Transform paper) {
        int paperIndex = paper.GetSiblingIndex();
        Transform mapPaper = mapPapers.GetChild(paperIndex);

        //If the dice number on the map paper matches the current papers dice number
        if (paper.GetComponentInChildren<SpriteRenderer>().sprite == mapPaper.GetComponentInChildren<SpriteRenderer>().sprite) {
            mapPaper.GetComponent<MeshRenderer>().material.color = Color.green;
        } else {
            mapPaper.GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

    public void CheckLevelCompleted() {
        for (int i = 0; i < mapPapers.childCount; i++) {
            if (mapPapers.GetChild(i).GetComponent<MeshRenderer>().material.color == Color.red) {
                //If there is any red paper on the map, it will return.
                return;
            }
        }
        //If there is no red paper on the map, that means all the paper is green and can go to next level.
        NextLevel();
    }

    public void NextLevel() {
        //This playerprefs is for the level papers on the menu. When player returns to the menu, completed levels will turn green.
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);  //After level 1 is completed, PlayerPrefs Level1 is going to be 1.

        //There is 6 level in the game right now. If player finish the last level, it will return to the menu.
        if (SceneManager.GetActiveScene().buildIndex == 6) {
            StartCoroutine(WaitForFade(0));
        }
        else {
            StartCoroutine(WaitForFade(SceneManager.GetActiveScene().buildIndex + 1));
        }

    }

    private IEnumerator WaitForFade(int sceneIndex) {
        FadeManager.GetInstance().FadeIn();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneIndex);
    }

    public static LevelManager GetInstance() {
        return instance;
    }
}
