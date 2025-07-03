using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DepartmentSelectionsMngr : MonoBehaviour
{


    public void DepartmentSelections(int departmentNo)
    {
        if (!AppManager.Instance.animationActive)
        {
            AppManager.Instance.LeftShift(1);

            AppManager.Instance.departmentNo = departmentNo;

            AppManager.Instance.selectionPanel.GetComponent<CanvasGroup>().DOFade(0, 1f);

            PredictionManager.Instance.whichModel = departmentNo;
        }

        //SetLessonSelectionsPanel(departmentNo);
    }
    /*
    void SetLessonSelectionsPanel(int department)
    {

        string[] CE_Lessons = { "Bilgisayar Mühendisliğine Giriş", "Programlama Tekniklerine Giriş", "Yapısal Programlama", "Elektirik Devreleri", "Diferansiyel Denklemler", "Nesne Yönelimli Programlama",
            "Mantık Devreleri", "Mikroişlemciler", "Web Teknolojileri", "Makine Öğrenmesi"};

        string[] L_Lessons = { "Anayasa Hukuku", "Borçlar Hukuku Genel Hükümler", "Türk Hukuk Tarihi", "Eşya Hukuku", "Miras Hukuku", "Milletlerarası Özel Hukuk", "Tüketici Hukuku", "İdare Hukuku",
            "İktisada Giriş", "İnsan Hakları Hukuku", };

        string[] J_Lessons = { "Temel Fotoğrafçılık", "Temel Gazetecilik", "Sosyoloji", "Basın Fotoğrafçılığı Uygulamaları", "Görsel İşitsel Haberciliğe Giriş", "Medya Ahlakı ve Hukuku", "Türk Basın Tarihi",
            "Eleştirel Medya Kuramları", "TV Haberciliği", "İnternet Haberciliği Uygulamaları", };


        for (int i = 0; i < lessonContent.childCount; i++)
        {
            switch (department)
            {
                case 1:
                    {
                        lessonContent.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = CE_Lessons[i];
                        lessonContent.GetChild(i).GetComponent<Image>().color = new Color32(186, 103, 170, 255);

                        PredictionManager.Instance.whichModel = 1;

                        break;
                    }
                case 2:
                    {
                        lessonContent.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = L_Lessons[i];
                        lessonContent.GetChild(i).GetComponent<Image>().color = new Color32(186, 118, 103, 255);

                        PredictionManager.Instance.whichModel = 2;

                        break;
                    }
                case 3:
                    {
                        lessonContent.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = J_Lessons[i];
                        lessonContent.GetChild(i).GetComponent<Image>().color = new Color32(103, 135, 186, 255);
                        
                        PredictionManager.Instance.whichModel = 3;

                        break;
                    }
            }

        }

    }
    */

}
