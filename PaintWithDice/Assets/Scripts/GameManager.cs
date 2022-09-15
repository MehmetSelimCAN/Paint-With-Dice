using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private void Update() {
        //Restart
        if (Input.GetKeyDown(KeyCode.R)) {
            StartCoroutine(WaitForFade(SceneManager.GetActiveScene().buildIndex));
        }
        //Back to Menu
        else if (Input.GetKeyDown(KeyCode.Escape)) {
            StartCoroutine(WaitForFade(0));
        }
    }

    private IEnumerator WaitForFade(int sceneIndex) {
        FadeManager.GetInstance().FadeIn();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneIndex);
    }
}
