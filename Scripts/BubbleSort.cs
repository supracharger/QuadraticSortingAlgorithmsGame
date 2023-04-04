using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BubbleSort : ASort
{
    GameObject[] _texts;

    public BubbleSort(GameObject[] texts)
    { _texts = texts; }

    public override void Plan()
    {
        var tiles = _tiles.ToList();
        _funcs = new List<Func<IEnumerator>>();
        _funcs.Add(Text(_texts, 0, 0f));
        _funcs.Add(() => Wait());
        for(int i=0; i<tiles.Count; i++)
        {
            _funcs.Add(SetColor(0, Color.blue));
            int j = 1;
            for(j=1; j<tiles.Count-i; j++)
            {
                _funcs.Add(SetColor(j, Color.blue));
                if (tiles[j]._value < tiles[j-1]._value)
                {
                    _funcs.Add(Swap(j-1, j));
                    SwapFunc(tiles, j-1, j);
                }
                _funcs.Add(SetColor(j-1, Color.white));
            }
            _funcs.Add(SetColor(j-1, Color.yellow));
            if (i == 0)
                _funcs.Add(Text(_texts, 1));
        }
        _funcs.Add(Text(_texts, 2));
        // Tells program to execute the plan
        _running = false;
    }
}
