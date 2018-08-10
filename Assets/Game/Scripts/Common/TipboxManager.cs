using System.Collections.Generic;
using UnityEngine;

public class TipboxManager
{
    private readonly List<Tipbox> list = new List<Tipbox>();
    private readonly List<Tipbox> needRemove = new List<Tipbox>();

    private TipboxManager()
    {
        list.Clear();
        needRemove.Clear();
    }

    public void ShowMessage(string msg)
    {
        GameObject obj = Object.Instantiate(Game.Config.TipboxPrefab, Vector3.zero, Quaternion.identity);
        Tipbox tipbox = obj.GetComponent<Tipbox>();
        list.Add(tipbox);
        tipbox.SetUp(msg);
    }

    public void RemoveTipbox(Tipbox tipbox)
    {
        needRemove.Clear();
        foreach (var item in list)
        {
            if (tipbox == item)
            {
                needRemove.Add(item);
            }
            else
            {
                if (item == null || item.gameObject == null)
                {
                    needRemove.Add(item);
                }
            }
        }

        foreach (var item in needRemove)
        {
            list.Remove(item);
        }
    }
}