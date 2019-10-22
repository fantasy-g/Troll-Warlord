using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneMove : MonoBehaviour {

    public Text Text;
    public Slider Slider;
    public bool FastMode = false;
    public int StepOfProgress = 2;     // 平滑进度的间隔
    [Space]
    public string AutoLoadScene = "";
    public float AutoLoadTime = 5f;
    public bool DebugLog = true;


    private IEnumerator Start() {
        if (Text)
            Text.gameObject.SetActive(false);
        if (Slider)
            Slider.gameObject.SetActive(false);
        if (AutoLoadScene != "") {
            yield return new WaitForSeconds(AutoLoadTime);
            Load(AutoLoadScene);
        }
    }

    public void Load(string sceneName) {
        if (FastMode)
            StartCoroutine(AsyncLoadScene_Fast(sceneName));
        else
            StartCoroutine(AsyncLoadScene(sceneName));

        if (Text)
            Text.gameObject.SetActive(true);
        if (Slider)
            Slider.gameObject.SetActive(true);
    }

    // 异步加载场景，进度条 Value 有过渡
    private IEnumerator AsyncLoadScene(string sceneName) {
        int progress = 0, targetProgress = 0;

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);  // Application.LoadLevelAsync()
        op.allowSceneActivation = false;

        while (op.progress < 0.9f) {
            targetProgress = (int)(op.progress * 100);
            while (progress < targetProgress) {
                progress += StepOfProgress;
                SetProcessBar(progress);
                yield return new WaitForEndOfFrame();
            }
        }

        targetProgress = 100;
        while (progress < targetProgress) {
            progress += StepOfProgress;
            progress = progress > 100 ? 100 : progress;
            SetProcessBar(progress);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
        op.allowSceneActivation = true;
    }

    // 快速加载（原速），进度条 Value 无过渡
    private IEnumerator AsyncLoadScene_Fast(string sceneName) {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;
        while (op.progress < 0.9f) {
            SetProcessBar(op.progress * 100);
            yield return new WaitForEndOfFrame();
        }
        SetProcessBar(100);
        yield return new WaitForEndOfFrame();
        op.allowSceneActivation = true;
    }

    private void SetProcessBar(float process) {
        if (DebugLog && !Text && !Slider) {
            Debug.Log("Invisible Process Bar: " + process + "%");
            return;
        }
        if (Text) {
            Text.text = process.ToString() + "%";
        }
        if (Slider) {
            Slider.value = process / 100;
        }
    }
}
