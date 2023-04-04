using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public abstract class ASort : IDisposable
{
    protected List<TileNum> _tiles;
    protected List<Func<IEnumerator>> _funcs;
    int _funcI;
    protected bool _running = true;

    public abstract void Plan();

    public Func<IEnumerator> NextExecute()
    {
        if (_running || _funcs == null || _funcI >= _funcs.Count) 
            return null;
        _running = true;
        return _funcs[_funcI++];
    }

    public ASort(){}

    public bool Init(GameObject inputObj, GameObject erMsgObj){
        var erMsg = erMsgObj.GetComponentInChildren<TMP_Text>();
        erMsg.text = "";
        erMsgObj.SetActive(false);
        try{
            List<int> nums = null;
            try {
                nums = inputObj.GetComponent<TMP_InputField>()
                    .text.Trim().Split(new string[]{", "}, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse).ToList();
            } catch {
                throw new Exception("There are only Integer values allowed & please separte them with a comma ', '.");
            }
            if (nums == null || nums.Count < 2)
                throw new Exception("Please add more numbers to the list.");
            if (nums.Any(v => v >= 100))
                throw new Exception("All values in list need to be smaller than <100.");
            if (nums.Count > 9)
                throw new Exception("There are too many numbers to sort. The length needs to be smaller <9.");
           // Create the tiles
            _tiles = nums.Select((n, i) => new TileNum(n, i)).ToList();
            return true;
        }
        catch (Exception e){
            erMsg.text = e.Message;
            erMsgObj.SetActive(true);
        }
        return false;
    }

    // Wraping Func to make sure ints 'indexs' are not overwritten
    protected Func<IEnumerator> SetColor(int index, Color color, float wait = 0.5f){
        return () => SetColor(index, color, wait, true);
    }

    IEnumerator SetColor(int index, Color color, float wait, bool unused)
    {
        _tiles[index].SetColor(color);
        yield return new WaitForSeconds(wait);
        // Tells program to execute the next plan
        _running = false;
    }

    // Wraping Func to make sure ints 'indexs' are not overwritten
    protected Func<IEnumerator> Swap(int ix1, int ix2)
    {
        return () => Swap(ix1, ix2, true);
    }

    IEnumerator Swap(int ix1, int ix2, bool unused)
    {
        float wait = 0.1f;
        Transform right = null, left = null;
        // Find the right & left tile
        if (_tiles[ix1]._tile.transform.position.x > _tiles[ix2]._tile.transform.position.x)
        {
            right = _tiles[ix1]._tile.transform;
            left = _tiles[ix2]._tile.transform;
        }
        else
        {
            right = _tiles[ix2]._tile.transform;
            left = _tiles[ix1]._tile.transform;
        }
        var rightSt = right.position;
        var leftSt = left.position;
        var rightShift = rightSt + (Vector3.up * 230);
        var leftShift = leftSt + (Vector3.down * 230);
        // Move up & down
        var length = 15;
        for (int i = 1; i <= length; i++)
        {
            right.position = (i * rightShift + (length-i) * rightSt) / length;
            left.position = (i * leftShift + (length-i) * leftSt) / length;
            yield return new WaitForSeconds(wait);
        }
        // Swap positions
        length = 20;
        for (int i=1; i<=length; i++)
        {
            right.position = (i * leftSt + (length-i) * rightShift) / length;
            left.position = (i * rightSt + (length-i) * leftShift) / length;
            yield return new WaitForSeconds(wait);
        }
        // Swap in the list
        SwapFunc(_tiles, ix1, ix2);
        // Tells program to execute the next plan
        _running = false;
    } 

    // Wraping Func to make sure ints 'indexs' are not overwritten
    protected Func<IEnumerator> Text(GameObject[] texts, int index, float wait = 4f)
    {
        return () => Text(texts, index, wait, true);
    }

    IEnumerator Text(GameObject[] texts, int index, float wait, bool unused){
        texts.Select(v => { v.SetActive(false); return 0; }).ToArray();
        texts[index].SetActive(true);
        yield return new WaitForSeconds(wait);
        // Tells program to execute the next plan
        _running = false;
    }

    protected IEnumerator Wait(float wait = 2f){
        yield return new WaitForSeconds(wait);
        // Tells program to execute the next plan
        _running = false;
    }

    protected static void SwapFunc<T>(List<T> items, int ix1, int ix2)
    {
        T temp = items[ix1];
        items[ix1] = items[ix2];
        items[ix2] = temp;
    }

    public void Dispose(){
        if (_tiles == null) return;
        _tiles.ForEach(t => GameObject.Destroy(t._tile));
        _tiles = null;
    }
}
