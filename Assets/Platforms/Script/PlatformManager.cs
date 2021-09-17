using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatformManager : MonoBehaviour
{
    static public PlatformManager Instance;

    [SerializeField] private Text txtVer = null;
    [SerializeField] private Text txtState = null;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        txtVer.text = string.Format("Ver.{0}", Application.version);
        txtState.text = string.Empty;
    }

    public void SetState(string msg, bool isClear = false)
    {
        if(isClear)
        {
            txtState.text = msg;
        }
        else
        {
            txtState.text += string.Format("\n{0}", msg);
        }

        txtState.gameObject.SetActive(false);
        txtState.gameObject.SetActive(true);
    }
}
