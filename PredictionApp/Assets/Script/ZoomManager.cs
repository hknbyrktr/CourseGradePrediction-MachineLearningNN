using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine;

public class ZoomManager : MonoBehaviour
{
    float zoomValue = 1f;

    bool ThereIsImage = false;

    Transform newImage;

    GameObject selectedGameObject;

    [SerializeField] Canvas InfoCanvas;

    private void Update()
    {
        if (InfoCanvas.sortingOrder == 2)
        {
            if (EventSystem.current.currentSelectedGameObject != null)
                zoomValue += Input.GetAxis("Mouse ScrollWheel");


            if (Input.touchCount == 2)
            {

                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                Vector2 touchZeroPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePos = touchOne.position - touchOne.deltaPosition;

                float prevMagnitude = (touchZeroPos - touchOnePos).magnitude;                    //  Magnitude  x ve y eksenin de 2 boyutlu bir buyukluk old icin
                                                                                                 //  karelerinin kokunu alıyo yani iki parmak arasi mesafeyi.
                float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

                float difference = (currentMagnitude - prevMagnitude) * 0.001f;

                zoomValue += difference;

            }

            if (zoomValue < 1) zoomValue = Mathf.Lerp(zoomValue, 1f, 1);
            if (zoomValue > 1.8) zoomValue = Mathf.Lerp(zoomValue, 1.8f, 1);


            if (zoomValue > 1 && !ThereIsImage)
                CreateImage();
            

#if UNITY_EDITOR
            else if (selectedGameObject != EventSystem.current.currentSelectedGameObject && ThereIsImage)
            {
                Destroy(newImage.gameObject);

                zoomValue = 0;
                ThereIsImage = false;
            }

#elif UNITY_ANDROID
            
            else if (Input.touchCount < 2 && ThereIsImage)
            {
                Destroy(newImage.gameObject);

                zoomValue = 0;
                ThereIsImage = false;
            }
            
#endif

            if (ThereIsImage)
                Zoom(zoomValue);
        
        }
    }


    void CreateImage()
    {
        selectedGameObject = EventSystem.current.currentSelectedGameObject;

        newImage = Instantiate(EventSystem.current.currentSelectedGameObject, GameObject.FindWithTag("Panel").transform).transform;

        newImage.position = EventSystem.current.currentSelectedGameObject.transform.position;
        Destroy(newImage.GetComponent<ZoomManager>());                                                                         // Yenisinde ZoomManager olursa ortalik karisir.

        ThereIsImage = true;
    }


    void Zoom(float zoomDegeri)
    {
        newImage.transform.GetComponent<RectTransform>().DOScale(zoomDegeri, 0f).SetEase(Ease.OutBack);
    }

}
