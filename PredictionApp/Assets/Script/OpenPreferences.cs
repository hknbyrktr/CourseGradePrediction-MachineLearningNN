using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using TMPro;

public class OpenPreferences : MonoBehaviour
{

    [Tooltip("0- Age / 1- MomJob / 2- DadJab / 3- FavLesson1 / 4- FavLesson2 / 5- PrivateLesson / 6- Activity")]
    [SerializeField] public int choice;

    List<Transform> childList = new();

    [SerializeField] bool isOption = false;

    TextMeshProUGUI infoText;
    public string text;

    public RectTransform PredictionPanel;

    bool isThisOpen;

    void Start()
    {
        if (isOption)
            AddChild();

        infoText = GameObject.FindWithTag("infoText").GetComponent<TextMeshProUGUI>();
    }


    private void LateUpdate()
    {
        if (isOption)
        {
            GameObject selected = EventSystem.current.currentSelectedGameObject;

            if (selected == null)
            {
                infoText.text = "";

                if (isThisOpen)
                    CloseThisOption();

                return;
            }

            bool isInList = childList.Any(t => selected.transform.IsChildOf(t));                       // Eger secilen nesne listedeki herhangi bir objenin cocuguysa secenekleri kapatmamasi icin.

            if (!isInList && isThisOpen)
            {
                CloseThisOption();
            }

        }
    }



    public void SelectThisOption(int value)
    {
        this.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;

        switch (choice)
        {
            case 0: { PredictionManager.Instance.age = value; break; }
            case 1: { PredictionManager.Instance.momJob = value; break; }
            case 2: { PredictionManager.Instance.dadJob = value; break; }
            case 3: { PredictionManager.Instance.favLesson1 = value; break; }
            case 4: { PredictionManager.Instance.favLesson2 = value; break; }
            case 5: { PredictionManager.Instance.privateLesson = value; break; }
            case 6: { PredictionManager.Instance.activity = value; break; }

        }

        CloseThisOption();
    }


    public void WriteInfo(int whichInfo)
    {
        string[] text = {
            "Lütfen haftalık toplam ders çalışma saatinizi girin.",
            "Lütfen yıllık okuduğunuz toplam kitap sayısını girin.",
            "Lütfen lisedeki 4 yıllık Edebiyat dersi ortalamanızı girin.",
            "Lütfen lisedeki 4 yıllık Matematik dersi ortalamanızı girin.",
            "Lütfen lisedeki 4 yıllık Fizik dersi ortalamanızı girin.",
            "Lütfen lisedeki 4 yıllık Kimya dersi ortalamanızı girin.",
            "Lütfen lisedeki 4 yıllık Biyoloji dersi ortalamanızı girin.",
            "Lütfen lisedeki 4 yıllık Tarih dersi ortalamanızı girin.",
            "Lütfen lisedeki 4 yıllık Coğrafya dersi ortalamanızı girin.",
            "Lütfen lisedeki 4 yıllık Din Kültürü dersi ortalamanızı girin.",
            "Lütfen lisedeki 4 yıllık Beden Eğitimi dersi ortalamanızı girin.",
            "Lütfen lisedeki 4 yıllık Resim dersi ortalamanızı girin.",
            "Lütfen lisedeki 4 yıllık Müzik dersi ortalamanızı girin.",
            "Lütfen lisedeki 4 yıllık İngilizce dersi ortalamanızı girin."
        };

        infoText.text = text[whichInfo];
    }

    public void SendInput(int whichInput)
    {

        if (this.transform.GetComponent<TMP_InputField>().text == "")
            return;

        float value = float.Parse(this.transform.GetComponent<TMP_InputField>().text);

        switch (whichInput)
        {
            case 0: PredictionManager.Instance.studyHours = value;  break;
            case 1: PredictionManager.Instance.bookCount = value;  break;
            case 2: PredictionManager.Instance.literatureScore = value;  break;
            case 3: PredictionManager.Instance.mathScore = value;  break;
            case 4: PredictionManager.Instance.physicalScore = value;  break;
            case 5: PredictionManager.Instance.chemicalScore = value;  break;
            case 6: PredictionManager.Instance.biologyScore = value;  break;
            case 7: PredictionManager.Instance.historyScore = value;  break;
            case 8: PredictionManager.Instance.geographyScore = value;  break;
            case 9: PredictionManager.Instance.religiousScore = value;  break;
            case 10: PredictionManager.Instance.physicalEducationScore = value;  break;
            case 11: PredictionManager.Instance.artScore = value;  break;
            case 12: PredictionManager.Instance.musicScore = value;  break;
            case 13: PredictionManager.Instance.englishScore = value;  break;
        }

    }



    public void OpenThisOption(float y)
    {
        if (!isThisOpen)
        {
            isThisOpen = true;

            print(this.transform.GetChild(1).gameObject.activeSelf);

            this.transform.GetChild(1).gameObject.SetActive(true);
            print(this.transform.GetChild(1).gameObject.activeSelf);

            float x = this.transform.GetChild(1).GetComponent<RectTransform>().sizeDelta.x;
            this.transform.GetChild(1).GetComponent<RectTransform>().DOSizeDelta(new Vector2(x, y), 0.2f).SetEase(Ease.Flash);

            infoText.text = text;
        }
    }



    void CloseThisOption()
    {
        isThisOpen = false;

        float x = this.transform.GetChild(1).GetComponent<RectTransform>().sizeDelta.x;

        this.transform.GetChild(1).GetComponent<RectTransform>().DOSizeDelta(new Vector2(x, 0), .2f).SetEase(Ease.Flash).OnComplete(() =>
        this.transform.GetChild(1).gameObject.SetActive(false)
        );

    }


    void AddChild()
    {
        childList.Add(this.transform);

        Transform content = this.transform.GetChild(1).GetChild(0).GetChild(0);

        for (int i = 0; i < content.childCount; i++)
        {
            childList.Add(content.GetChild(i));
        }

    }

    public void PanelUp()
    {
        PredictionPanel.DOAnchorPosY(160, .6f);
    }
    public void PanelDown()
    {
        PredictionPanel.DOAnchorPosY(40, .6f);
    }
}
