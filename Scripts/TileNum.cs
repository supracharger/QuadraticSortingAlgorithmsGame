using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TileNum 
{
    public GameObject _tile;
    public int _value {get; private set;}
    
    public TileNum(int value, int index)
    {
        _value = value;
        var obj = Engine._tile;
        _tile = GameObject.Instantiate(obj, obj.transform.parent.transform);
        _tile.GetComponentInChildren<TMP_Text>().text = value.ToString();
        _tile.transform.position += Vector3.right * (index * 210);
        _tile.SetActive(true);
    }

    public void SetColor(Color color)
    { _tile.GetComponent<RawImage>().color = color;}

    public override string ToString()
    { return _value.ToString();}
}
