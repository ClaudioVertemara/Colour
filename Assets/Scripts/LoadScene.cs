using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Project: Colour
 * Code Author: Claudio Vertemara
*/

public class LoadScene : MonoBehaviour
{
	// Use this for initialization
	void Start() {
		StartCoroutine(LoadAsynchronously("Museum"));
	}

	IEnumerator LoadAsynchronously(string sceneName) {
		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

		while (!operation.isDone) {
			yield return null;
		}
	}
}
