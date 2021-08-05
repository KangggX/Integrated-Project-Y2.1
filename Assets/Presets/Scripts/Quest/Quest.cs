/******************************************************************************
Author: Kang Xuan
Name of Class: Quest
Description of Class: Used to create an array of quests.
Date Created: 31/07/2021
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    [TextArea(3, 10)]
    public string[] questList;
}
