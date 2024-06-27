using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hearts_", menuName = "Hearts")]
public class HeartSO : ScriptableObject
{
    public GameObject fullHeart;
    public GameObject emptyHeart;
    public GameObject quarterHeart;
    public GameObject halfHeart;
    public GameObject threeQuarterHeart;
}
