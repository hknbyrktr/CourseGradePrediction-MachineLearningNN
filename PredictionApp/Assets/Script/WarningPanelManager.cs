using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WarningPanelManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI warningText;

    public Transform ganoTrnsfrm;
    public Transform lessonTrnsfrm;
    public GameObject result;

    public Transform panel;
    public GameObject yesBtn;

    [HideInInspector] public List<float> valuesList = new List<float>();


    [SerializeField] public Color ganoResultColor;
    [SerializeField] public Color lessonResultColor;

    /// <summary>
    /// 'Cikis Yap' i actirmak icin "true", Sonuclari gostermek icin "false".
    /// </summary>
    /// <param name="status"></param>
    public void OpenWarninPanel(bool status)
    {
        if (status)
        {
            title.text = "~Çıkış Yap~";
            warningText.text = "Çıkış yapmak istediğinize eminmisiniz?";

            yesBtn.SetActive(true);

            Vector2 size = new Vector2(panel.GetComponent<RectTransform>().sizeDelta.x, 315);
            panel.GetComponent<RectTransform>().sizeDelta = size;
        }
        else
        {
            title.text = "~Sonuçlar~";
            warningText.text = "";

            if (PredictionManager.Instance.whichModel == 0)
            {
                SetPanel_1();
            }
            else
            {
                SetPanel_2();
            }
        }

        this.transform.GetComponent<Canvas>().sortingOrder = 2;

    }


    void SetPanel_1()
    {
        string[] departments = { "Bilgisayar Mühendisliği GANO: ", "Hukuk GANO: ", "Gazetecilik GANO: " };

        for (int i = 0; i < valuesList.Count; i++)
        {
            Transform newResult = Instantiate(result, ganoTrnsfrm).transform;

            newResult.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = valuesList[i].ToString();
            newResult.GetChild(0).GetComponent<TextMeshProUGUI>().text = departments[i];

            newResult.GetComponent<Image>().color = ganoResultColor;
            newResult.GetChild(1).GetComponent<Image>().color = ganoResultColor;
        }


        Vector2 size = new Vector2(panel.GetComponent<RectTransform>().sizeDelta.x, 480);
        panel.GetComponent<RectTransform>().sizeDelta = size;

        yesBtn.SetActive(false);

    }


    void SetPanel_2()
    {

        string[] CE_Lessons = { "Bilgisayar Mühendisliğine Giriş: ", "Programlama Tekniklerine Giriş: ", "Yapısal Programlama: ", "Elektirik Devreleri: ", "Diferansiyel Denklemler: ", "Nesne Yönelimli Programlama: ",
            "Mantık Devreleri: ", "Mikroişlemciler: ", "Web Teknolojileri: ", "Makine Öğrenmesi: "};

        string[] L_Lessons = { "Anayasa Hukuku: ", "Borçlar Hukuku Genel Hükümler: ", "Türk Hukuk Tarihi: ", "Eşya Hukuku: ", "Miras Hukuku: ", "Milletlerarası Özel Hukuk: ", "Tüketici Hukuku: ", "İdare Hukuku: ",
            "İktisada Giriş: ", "İnsan Hakları Hukuku:", };

        string[] J_Lessons = { "Temel Fotoğrafçılık: ", "Temel Gazetecilik: ", "Sosyoloji: ", "Basın Fotoğrafçılığı Uygulamaları: ", "Görsel İşitsel Haberciliğe Giriş: ", "Medya Ahlakı ve Hukuku: ",
            "Türk Basın Tarihi:", "Eleştirel Medya Kuramları: ", "TV Haberciliği: ", "İnternet Haberciliği Uygulamaları: ", };

        lessonTrnsfrm.gameObject.SetActive(true);
        Transform content = lessonTrnsfrm.GetChild(0).GetChild(0);

        for (int i = 0; i < valuesList.Count; i++)
        {
            Transform newResult = Instantiate(result, content).transform;

            newResult.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = valuesList[i].ToString();

            int x = PredictionManager.Instance.whichModel;
            if (x == 1)
                newResult.GetChild(0).GetComponent<TextMeshProUGUI>().text = CE_Lessons[i];
            else if (x == 2)
                newResult.GetChild(0).GetComponent<TextMeshProUGUI>().text = L_Lessons[i];
            else if (x == 3)
                newResult.GetChild(0).GetComponent<TextMeshProUGUI>().text = J_Lessons[i];

            newResult.GetComponent<Image>().color = lessonResultColor;
            newResult.GetChild(1).GetComponent<Image>().color = lessonResultColor;


        }


        Vector2 size = new Vector2(panel.GetComponent<RectTransform>().sizeDelta.x, 740);
        panel.GetComponent<RectTransform>().sizeDelta = size;

        yesBtn.SetActive(false);

    }



    public void CloseWarningPanel()
    {
        this.transform.GetComponent<Canvas>().sortingOrder = 0;

        for (int i = 0; i < ganoTrnsfrm.childCount; i++)
        {
            Destroy(ganoTrnsfrm.GetChild(i).gameObject);
        }

        if (lessonTrnsfrm.gameObject.activeSelf)
        {
            Transform content = lessonTrnsfrm.GetChild(0).GetChild(0);

            for (int i = 0; i < content.childCount; i++)
            {
                Destroy(content.GetChild(i).gameObject);
            }

            lessonTrnsfrm.gameObject.SetActive(false);
        }
        valuesList.Clear();

    }

    public bool ActiveSelf()
    {
        if (this.transform.GetComponent<Canvas>().sortingOrder == 2)
            return true;
        else
            return false;
    }
}
