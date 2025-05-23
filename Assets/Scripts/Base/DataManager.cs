using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.XR;
using UnityEngine;

public class DataManager : MonoBehaviour
{

    public static DataManager init;
    void Awake()
    {
        if (!init)
        {
            init = this;
        }
    }


    public string GetString(string dataType)
    {
        string str = PlayerPrefs.GetString(dataType);
        return str;
    }


    public void SetString(string dataType, string value)
    {
        PlayerPrefs.SetString(dataType, value);
        PlayerPrefs.Save();
    }


    public int GetInt(string intType)
    {
        int integer = PlayerPrefs.GetInt(intType);
        return integer;
    }


    public void SetInt(string dataType, int value)
    {
        PlayerPrefs.SetInt(dataType, value);
        PlayerPrefs.Save();
    }

    public bool GetBoolean(string dataType)
    {
        int boolState = PlayerPrefs.GetInt(dataType);
        if (boolState == 1)
        {
            return true;
        }
        return false;
    }

    public void SetBoolean(string dataType, bool isData)
    {
        if (isData)
        {
            PlayerPrefs.SetInt(dataType, 1);
        }
        else
        {
            PlayerPrefs.SetInt(dataType, 0);
        }
        PlayerPrefs.Save();
    }


    public void SetColor(string dataType, Color col)
    {
        float[] colorData = new float[] { col.r, col.g, col.b, col.a };

    }

    public float[] GetFloatArray(string dataType, int value)
    {
        float[] floatDataArray = new float[value];
        if (!PlayerPrefs.HasKey(dataType))
        {
            return null;
        }

        for (int i = 0; i < value; i++)
            floatDataArray[i] = PlayerPrefs.GetFloat(i + dataType);

        return floatDataArray;
    }

    public void SetFloatArray(string dataType, float[] arrValue)
    {
        PlayerPrefs.SetFloat(dataType, 0);

        for (int i = 0; i < arrValue.Length; i++)
        {
            PlayerPrefs.SetFloat(i + dataType, arrValue[i]);
            PlayerPrefs.Save();
        }
    }





}
