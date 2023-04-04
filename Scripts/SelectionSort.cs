using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectionSort : ASort
{
    GameObject[] _texts;

    public SelectionSort(GameObject[] texts)
    { _texts = texts; }

    public override void Plan()
    {
        var tiles = _tiles.ToList();
        _funcs = new List<System.Func<IEnumerator>>();
        _funcs.Add(Text(_texts, 0, 0));
        _funcs.Add(() => Wait());
        var foundNextMax = false;
        for (int i=0; i<tiles.Count; i++)
        {
            var minIx = i;
            _funcs.Add(SetColor(minIx, Color.blue));
            for(int j=i+1; j<tiles.Count; j++)
            {
                if (tiles[j]._value < tiles[minIx]._value){
                    _funcs.Add(SetColor(minIx, Color.white));
                    minIx = j;
                    _funcs.Add(SetColor(minIx, Color.blue, 1.3f));
                    if (! foundNextMax){
                        foundNextMax = true;
                        _funcs.Add(Text(_texts, 1));
                    }
                }
            }
            if (minIx != i){
                _funcs.Add(Swap(i, minIx));
                SwapFunc(tiles, i, minIx);
            }
            _funcs.Add(SetColor(i, Color.green));
        }
        _funcs.Add(Text(_texts, 2));
        // Tells program to execute the next plan
        _running = false;
    }
}
