/******************************************************************************
Author: Kang Xuan

Name of Class: Dialogue

Description of Class: description

Date Created: 22/07/2021
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string name;

    [TextArea(3, 10)]
    public string[] sentences;
}
