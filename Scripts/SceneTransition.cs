using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DKCommon
{
    public class SceneTransition : MonoBehaviour
    {
        private static string lastSceneName;
        public void LoadScene(string sceneName)
        {
            lastSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(sceneName);
        }

        public void LoadCurrentSceneAgain()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        public void BackToLastScene()
        {
            if (string.IsNullOrEmpty(lastSceneName))
            {
                LoadCurrentSceneAgain();
            }
            else
            {
                LoadScene(lastSceneName);
            }
        }
    }
}
