using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLevelManager : MonoBehaviour {

    private static MenuLevelManager instance;

    private Transform levelPapers;

    private Transform dice;
    private Vector3 skipTutorialPosition;

    private Transform cameraTransform;
    private Vector3 cameraOffsets;

    private void Awake() {
        instance = this;

        levelPapers = GameObject.Find("LevelPapers").transform;

        for (int i = 1; i < 7; i++) {
            if (PlayerPrefs.GetInt("Level" + i) == 1) { //e.g. If level 3 is completed, second index of level papers should turn green.
                levelPapers.GetChild(i - 1).GetComponent<MeshRenderer>().material.color = Color.green;
            }
        }

        dice = GameObject.Find("Dice").transform;

        cameraTransform = Camera.main.transform;
        cameraOffsets = cameraTransform.position - dice.position;

        skipTutorialPosition = new Vector3(15, 0.63f, -1);

        //If the player has played any level even once, dice's spawn position changes.
        if (PlayerPrefs.GetInt("GoToLevelOnce") == 1) {
            dice.position = skipTutorialPosition;
            cameraTransform.position = skipTutorialPosition + cameraOffsets;
        }
    }

    public void GoToLevel(string levelSceneName) {
        if (!PlayerPrefs.HasKey("GoToLevelOnce")) {
            PlayerPrefs.SetInt("GoToLevelOnce", 1);
        }
        StartCoroutine(WaitForFade(levelSceneName));
    }

    private IEnumerator WaitForFade(string levelSceneName) {
        FadeManager.GetInstance().FadeIn();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(levelSceneName);
    }

    public static MenuLevelManager GetInstance() {
        return instance;
    }
}
