using System;
using System.Collections.Generic;
using TMPro;
using Unity.Barracuda;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PredictionManager : Singleton<PredictionManager>
{
    public WarningPanelManager warningPanelManager;
    
    [Space(10)]
    [Header("Gano Models")]
    [SerializeField] public NNModel BM_Gano_Model;
    [SerializeField] public NNModel H_Gano_Model;
    [SerializeField] public NNModel G_Gano_Model;

    [Space(10)]
    [Header("Lesson Models")]
    [SerializeField] public NNModel BM_Lesson_Model;
    [SerializeField] public NNModel H_Lesson_Model;
    [SerializeField] public NNModel G_Lesson_Model;

    [Tooltip("0- gano_Model / 1- BM_Model / 2- H_Model / 3- G_Model")]
    [HideInInspector] public int whichModel;

    IWorker worker;


    [HideInInspector] public int age;
    [HideInInspector] public int momJob;
    [HideInInspector] public int dadJob;
    [HideInInspector] public int favLesson1;
    [HideInInspector] public int favLesson2;
    [HideInInspector] public int privateLesson;
    [HideInInspector] public int activity;

    [HideInInspector] public float studyHours;
    [HideInInspector] public float bookCount;
    [HideInInspector] public float literatureScore;
    [HideInInspector] public float mathScore;
    [HideInInspector] public float physicalScore;
    [HideInInspector] public float chemicalScore;
    [HideInInspector] public float biologyScore;
    [HideInInspector] public float historyScore;
    [HideInInspector] public float geographyScore;
    [HideInInspector] public float religiousScore;
    [HideInInspector] public float physicalEducationScore;
    [HideInInspector] public float artScore;
    [HideInInspector] public float musicScore;
    [HideInInspector] public float englishScore;


    // Gano modellerinde ve lesson modellerinde inputlar degismedigi icin dogal olarak olceklerde aynı
    float[] Bm_Input_Mean = new float[] { 21.3727f, 3.3788f, 3.4848f, 3.4182f, 8.9818f, 0.6061f, 48.5539f, 17.0545f, 3.4303f, 70.5818f, 73.5485f, 61.0818f, 64.9879f, 66.3697f, 61.9939f, 68.3212f, 60.0000f, 71.3394f, 69.9667f, 68.8545f, 71.5818f };
    float[] Bm_Input_Scale = new float[] { 3.3697f, 1.4561f, 1.5279f, 1.2960f, 2.0060f, 0.4886f, 28.8314f, 12.5337f, 1.5714f, 21.7168f, 21.8360f, 23.1164f, 22.5754f, 23.7972f, 22.6274f, 17.9222f, 23.3869f, 20.7342f, 21.0577f, 23.2376f, 16.9237f };

    float[] H_Input_Mean = new float[] { 21.3077f, 3.4396f, 3.4890f, 2.2720f, 9.3022f, 0.2665f, 42.8610f, 16.9698f, 3.4011f, 67.9863f, 70.3571f, 49.5137f, 53.5467f, 53.6236f, 52.8681f, 65.4753f, 50.3874f, 60.7170f, 63.3709f, 63.2912f, 70.4451f };
    float[] H_Input_Scale = new float[] { 3.5245f, 1.4503f, 1.4908f, 1.2382f, 2.2106f, 0.4421f, 25.4106f, 12.3057f, 1.5041f, 21.2598f, 22.0202f, 21.1171f, 22.5848f, 21.4082f, 22.8645f, 17.3283f, 22.1564f, 19.5966f, 19.8355f, 23.1193f, 16.9076f };

    float[] G_Input_Mean = new float[] { 21.5327f, 3.3203f, 3.3529f, 4.0261f, 9.2059f, 0.5784f, 44.2771f, 16.8922f, 3.3758f, 73.4608f, 70.7320f, 56.0163f, 60.5425f, 58.5196f, 64.3431f, 70.5784f, 62.2320f, 68.7190f, 67.6340f, 68.9608f, 71.1176f };
    float[] G_Input_Scale = new float[] { 3.6381f, 1.5131f, 1.5231f, 2.3858f, 2.0643f, 0.4938f, 26.7021f, 11.7158f, 1.5122f, 21.0549f, 22.0787f, 22.9276f, 22.9207f, 24.0956f, 25.4759f, 17.9993f, 24.0128f, 20.7697f, 20.2350f, 22.9198f, 17.0177f };


    float[] Bm_Lesson_Output_Mean = new float[] { 68.3364f, 69.2894f, 70.5591f, 66.9379f, 69.3167f, 68.9864f, 68.7652f, 71.2773f, 69.8894f, 67.1348f };
    float[] Bm_Lesson_Output_Scale = new float[] { 28.7328f, 30.2576f, 29.8265f, 30.3873f, 29.7369f, 29.6173f, 30.1045f, 28.8371f, 29.8441f, 30.1709f };

    float[] H_Lesson_Output_Mean = new float[] { 65.0124f, 67.0220f, 66.6951f, 70.2170f, 65.8791f, 69.8297f, 67.5124f, 65.9286f, 68.4602f, 68.4560f };
    float[] H_Lesson_Output_Scale = new float[] { 31.1522f, 29.5454f, 30.5410f, 30.5846f, 29.9665f, 29.0864f, 30.6689f, 30.0178f, 30.2895f, 30.6391f };

    float[] G_Lesson_Output_Mean = new float[] { 67.3840f, 69.8791f, 67.4542f, 64.4412f, 66.6422f, 68.3056f, 67.9232f, 65.5703f, 66.5016f, 67.7042f };
    float[] G_Lesson_Output_Scale = new float[] { 30.9512f, 31.5364f, 31.0652f, 31.3715f, 30.5970f, 31.1515f, 31.6167f, 32.7258f, 31.3795f, 29.5577f };




    private void Start()
    {
        warningPanelManager = FindObjectOfType<WarningPanelManager>();
    }

    public void PretictionBtn()
    {
        float[][] means = { Bm_Input_Mean, H_Input_Mean, G_Input_Mean };
        float[][] scales = { Bm_Input_Scale, H_Input_Scale, G_Input_Scale };

        float[] inputData = new float[] { age, momJob, dadJob, favLesson1, favLesson2, privateLesson, studyHours, bookCount, activity, literatureScore, mathScore, physicalScore,
                                          chemicalScore, biologyScore, historyScore, geographyScore, religiousScore, physicalEducationScore, artScore, musicScore, englishScore };

        if (whichModel == 0)
        {
            GanoPredict(inputData, means, scales);
        }
        else
        {

            int i = whichModel - 1;
            float[] standardizedInput = StandardizeInput(inputData, means[i], scales[i]);

            if (whichModel == 1)
                LessonPredict(standardizedInput, BM_Lesson_Model, i);
            else if (whichModel == 2)
                LessonPredict(standardizedInput, H_Lesson_Model, i);
            else if (whichModel == 3)
                LessonPredict(standardizedInput, G_Lesson_Model, i);

        }


    }

    void GanoPredict(float[] inputData, float[][] means, float[][] scales)
    {
        Model[] models = {
        ModelLoader.Load(BM_Gano_Model),
        ModelLoader.Load(H_Gano_Model),
        ModelLoader.Load(G_Gano_Model)
        };

        string[] labels = { "BM", "H", "G" };

        for (int i = 0; i < models.Length; i++)
        {
            float[] standardizedInput = StandardizeInput(inputData, means[i], scales[i]);
            Tensor inputTensor = new Tensor(1, standardizedInput.Length, standardizedInput);
            worker = WorkerFactory.CreateWorker(WorkerFactory.Type.Auto, models[i]);
            worker.Execute(inputTensor);
            Tensor output = worker.PeekOutput();

            output[0] = (float)Math.Round(output[0],2);

            warningPanelManager.valuesList.Add(output[0]);

            Debug.Log($"{labels[i]} Çıktısı: {output[0]}");

            inputTensor.Dispose();
            output.Dispose();
            worker.Dispose();
        }

        warningPanelManager.OpenWarninPanel(false);

    }


    void LessonPredict(float[] standardizedInput, NNModel model, int i)
    {
        float[][] means = { Bm_Lesson_Output_Mean, H_Lesson_Output_Mean, G_Lesson_Output_Mean };
        float[][] scales = { Bm_Lesson_Output_Scale, H_Lesson_Output_Scale, G_Lesson_Output_Scale };

        Model runtimeModel = ModelLoader.Load(model);
        worker = WorkerFactory.CreateWorker(WorkerFactory.Type.Auto, runtimeModel);


        Tensor inputTensor = new Tensor(1, standardizedInput.Length, standardizedInput);                              // (batch, features)
        worker.Execute(inputTensor);
        Tensor output = worker.PeekOutput();

        float[] standardizedOutput = StandardizeOutput(output, means[i], scales[i]);

        Debug.Log("Model Output:");
        for (int j = 0; j < standardizedOutput.Length; j++)
        {
            Debug.Log($"Output {j}: {standardizedOutput[j]}");

            warningPanelManager.valuesList.Add(standardizedOutput[j]);
        }

        warningPanelManager.OpenWarninPanel(false);

        inputTensor.Dispose();
        output.Dispose();
        worker.Dispose();
    }


    float[] StandardizeInput(float[] input, float[] mean, float[] scale)
    {
        float[] result = new float[input.Length];
        for (int i = 0; i < input.Length; i++)
            result[i] = (input[i] - mean[i]) / scale[i];
        return result;
    }

    float[] StandardizeOutput(Tensor output, float[] mean, float[] scale)
    {
        float[] result = new float[output.length];

        for (int i = 0; i < output.length; i++)
        {
            float value = output[0, i];
            result[i] = (value * scale[i]) + mean[i];
            result[i] = (float)Math.Round(result[i], 2);
        }

        return result;
    }



    void OnDestroy()
    {
        worker?.Dispose();
    }
}
