using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace GameManager.Initializer
{
    public class LoadingManager : MonoBehaviour
    {
        [SerializeField] private Image progressBar;

        private void Start()
        {
            StartCoroutine(LoadGameScene());
        }

        private IEnumerator LoadGameScene()
        {
            var asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(2);
            if (asyncLoad == null) yield break;
            asyncLoad.allowSceneActivation = false;

            while (!asyncLoad.isDone)
            {
                progressBar.fillAmount = asyncLoad.progress;

                if (asyncLoad.progress >= 0.9f)
                {
                    progressBar.fillAmount = 1f;
                    yield return new WaitForSeconds(1f);
                    asyncLoad.allowSceneActivation = true;
                }

                yield return null;
            }
        }
    }
}
