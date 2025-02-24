using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private GameObject quarterView;
    [SerializeField] private GameObject tpsView;

    private bool isTPSMode = false;


    private void Update()
    {
        if (isTPSMode)
        {
            quarterView.SetActive(false);
            tpsView.SetActive(true);
        }
        else
        {
            quarterView.SetActive(true);
            tpsView.SetActive(false);
        }
    }

    /// <summary>
    /// TPS ��带 Ȱ��ȭ or ��Ȱ��ȭ�Ѵ�
    /// </summary>
    public void SetTPSMode(bool tpsMode)
    {
        isTPSMode = tpsMode;
    }
}
