using UnityEngine.UI;
using DG.Tweening;
using UnityEngine;

public class AppManager : Singleton<AppManager>
{

    [SerializeField] Canvas infoCanvas;
    [SerializeField] WarningPanelManager warningPanelManager;


    [Tooltip("0-FirstSelections / 1- DepartmentSelections / 2- LessonSelections / 3- PredictionPanel")]
    public RectTransform[] Panels;

    [HideInInspector] public int currentPanelIndeks = 0;
    [HideInInspector] public int prevPanelIndeks = 0;


    [HideInInspector] public bool animationActive = false;

    public Transform selectionPanel;

    public int departmentNo;
    public int lessonNo;



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (infoCanvas.sortingOrder == 2)
            {
                infoCanvas.sortingOrder = 0;
                return;
            }
            else if (warningPanelManager.ActiveSelf())
            {
                warningPanelManager.CloseWarningPanel();
                return;
            }

            RightShift();
        }

    }



    public void LeftShift(int x)
    {
        if (!animationActive)
        {
            animationActive = true;

            RectTransform currentPanel = Panels[currentPanelIndeks];
            prevPanelIndeks = currentPanelIndeks;
            currentPanelIndeks += x;
            RectTransform nextPanel = Panels[currentPanelIndeks];

            currentPanel.GetComponent<CanvasGroup>().DOFade(0, 1f);

            currentPanel.DOAnchorPosX(-850, 1f).OnComplete(() =>
            {
                currentPanel.gameObject.SetActive(false);
                animationActive = false;
            });



            nextPanel.gameObject.SetActive(true);

            nextPanel.GetComponent<CanvasGroup>().DOFade(1, 1f);


        }
    }



    public void RightShift()
    {
        if (!animationActive)
        {
            if (currentPanelIndeks == 0)
            {
                warningPanelManager.OpenWarninPanel(true);
                return;
            }

            int x = 1;
            if (currentPanelIndeks == 2)                                                              // PredictionPanel duzgunce sirada olmadigi icin ekstra kod yazmak yerine bu sekilde uyarlandi.
            {
                if (prevPanelIndeks == 0)
                    x = 2;
                else
                    x = 1;

                selectionPanel.GetComponent<CanvasGroup>().DOFade(1, 1f);
            }


            RectTransform currentPanel = Panels[currentPanelIndeks];
            currentPanelIndeks -= x;
            RectTransform prevPanel = Panels[currentPanelIndeks];

            animationActive = true;

            currentPanel.GetComponent<CanvasGroup>().DOFade(0, 1f).OnComplete(() =>
            {
                currentPanel.gameObject.SetActive(false);
                animationActive = false;                                                               // Animasyon varken farkli bir animasyon olusmasin.
            });


            prevPanel.gameObject.SetActive(true);

            prevPanel.GetComponent<CanvasGroup>().DOFade(1, 1f);
            prevPanel.DOAnchorPosX(0, 1.5f);


        }
    }



    public void InfoCanvasOrder()
    {
        if (infoCanvas.sortingOrder == 0)
            infoCanvas.sortingOrder = 2;
        else
            infoCanvas.sortingOrder = 0;

    }

}
