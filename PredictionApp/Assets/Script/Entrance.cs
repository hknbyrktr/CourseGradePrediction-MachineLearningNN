using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.VisualScripting;

public class Entrance : MonoBehaviour
{

    [SerializeField] CanvasGroup fadeScreen;
    [SerializeField] Slider slider;


    void Start()
    {
        fadeScreen.alpha = 1;
        fadeScreen.DOFade(0, 1f);

        StartCoroutine(LoadTheSlider());
    }

    IEnumerator LoadTheSlider()
    {

        yield return new WaitForSeconds(1);

        while (slider.value < .35)                                                                    // Cok hizli bir sekilde yuklenmemesi icin ufak bi oyun.
            slider.value += Time.deltaTime * .5f;

        yield return new WaitForSeconds(.8f);


        AsyncOperation operation = SceneManager.LoadSceneAsync("MainScene");

        float progress = .35f;                                                                        // zaten yukarida .35 e kadar elle yukledigimiz icin tekrar 0' a dusmesin diye.
        while (!operation.isDone)
        {

            if ((operation.progress / .9f) > .35f)
                progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;

            yield return null;
        }
        slider.value = 1f;





    }






}
