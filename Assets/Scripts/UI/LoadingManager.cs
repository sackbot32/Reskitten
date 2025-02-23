using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System.Collections;
public class LoadingManager : MonoBehaviour
{
    public static LoadingManager instance;
    public List<Image> loadingScreenImages = new List<Image>();
    public Transform loadingRotation;

    public float transitionDuration;

    private Coroutine spinCoroutine;
    private void Awake()
    {
        if(LoadingManager.instance == null)
        {
            LoadingManager.instance = this;
            ShowLoadScreen(false, 0);
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    public void ShowLoadScreen(bool show,float transitionDurationCalled = 0.2f)
    {
        if (show)
        {
            foreach (Image image in loadingScreenImages)
            {
                Color current = image.color;
                image.DOColor(new Color(current.r,current.g,current.b,1f), transitionDurationCalled).OnComplete(() =>
                {
                    spinCoroutine = StartCoroutine(SpinLogo());
                });
            }
        }
        else
        {
            foreach (Image image in loadingScreenImages)
            {
                Color current = image.color;
                image.DOColor(new Color(current.r, current.g, current.b, 0f), transitionDurationCalled);
                if(spinCoroutine != null)
                {
                    StopCoroutine(spinCoroutine);
                    spinCoroutine = null;
                }
            }
        }
    }

    public IEnumerator SceneChangeWithLoadScreen(int index)
    {
        ShowLoadScreen(true, transitionDuration);
        //yield return new WaitForSeconds(transitionDuration + transitionDuration/2f);
        yield return new WaitForSeconds(transitionDuration);
        AsyncOperation sceneLoad = SceneManager.LoadSceneAsync(index);
        sceneLoad.allowSceneActivation = false;
        while (sceneLoad.progress < 0.9f)
        {
            yield return null;
        }
        ShowLoadScreen(false, transitionDuration);

        sceneLoad.allowSceneActivation = true;
    }

    public IEnumerator SceneChangeWithLoadScreen(string sceneName)
    {
        ShowLoadScreen(true, transitionDuration);
        AsyncOperation sceneLoad = SceneManager.LoadSceneAsync(sceneName);
        sceneLoad.allowSceneActivation = false;
        while (sceneLoad.progress < 0.9f)
        {
            yield return null;
        }
        ShowLoadScreen(false, transitionDuration);

        sceneLoad.allowSceneActivation = true;
    }

    IEnumerator SpinLogo()
    {
        while (true)
        {
            loadingRotation.transform.rotation = Quaternion.Euler(0, loadingRotation.transform.rotation.eulerAngles.y - 2f, 0);
            yield return null;
        }
    }
}
