using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InsertionSort : ASort
{
    GameObject[] _texts;

    public InsertionSort(GameObject[] texts)
    { _texts = texts;}

    public override void Plan()
    {
        var tiles = _tiles.ToList();
        _funcs = new List<System.Func<IEnumerator>>();
        var orange = new Color(1, 114/255f, 0, 0.7f);
        _funcs.Add(Text(_texts, 0, 0));
        _funcs.Add(() => Wait());
        _funcs.Add(SetColor(0, Color.red));
        for(int i=1; i<tiles.Count; i++)
        {
            _funcs.Add(SetColor(i, orange));
            if (i == 3)
                _funcs.Add(Text(_texts, 1));
            var temp = i;
            for(int j=i-1; j>=0; j--)
            {
                if (tiles[j]._value > tiles[temp]._value)
                {
                    _funcs.Add(Swap(temp, j));
                    SwapFunc(tiles, temp, j);
                    temp = j;
                }
                else break;
            }
            _funcs.Add(SetColor(temp, Color.red));
        }
        _funcs.Add(Text(_texts, 2));
        // Tells program to execute the next plan
        _running = false;
    }
}
