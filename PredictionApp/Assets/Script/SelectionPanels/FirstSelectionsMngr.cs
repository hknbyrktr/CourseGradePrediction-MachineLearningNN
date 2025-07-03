using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstSelectionsMngr : MonoBehaviour
{


    public void ScorePrediction()
    {
        AppManager.Instance.LeftShift(1);

    }

    public void DepartmentPrediction()
    {
        AppManager.Instance.LeftShift(2);

        AppManager.Instance.selectionPanel.GetComponent<CanvasGroup>().DOFade(0, 1f);

        PredictionManager.Instance.whichModel = 0;
    }


}
