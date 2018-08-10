using UnityEngine;

public static class TransformExtension
{
    public static void DestroyChildren(this Transform trans)
    {
        foreach (Transform child in trans)
        {
            Object.Destroy(child.gameObject);
        }
    }

    public static Transform AddChildFromPrefab(this Transform trans, Transform prefab, string name = null)
    {
        Transform childTrans = Object.Instantiate(prefab);
        childTrans.SetParent(trans, false);
        if (name != null)
        {
            childTrans.gameObject.name = name;
        }
        return childTrans;
    }

    public static void SetLocalX(this Transform trans, float x)
    {
        var pos = trans.localPosition;
        pos.x = x;
        trans.localPosition = pos;
    }

    public static void SetLocalY(this Transform trans, float y)
    {
        var pos = trans.localPosition;
        pos.y = y;
        trans.localPosition = pos;
    }

    public static void SetLocalZ(this Transform trans, float z)
    {
        var pos = trans.localPosition;
        pos.z = z;
        trans.localPosition = pos;
    }

    public static void SetLocalXOffset(this Transform trans, float offset)
    {
        var pos = trans.localPosition;
        pos.x += offset;
        trans.localPosition = pos;
    }

    public static void SetLocalYOffset(this Transform trans, float offset)
    {
        var pos = trans.localPosition;
        pos.y += offset;
        trans.localPosition = pos;
    }

    public static void SetLocalZOffset(this Transform trans, float offset)
    {
        var pos = trans.localPosition;
        pos.z += offset;
        trans.localPosition = pos;
    }

    public static void SetWorldYOffset(this Transform trans, float offset)
    {
        var pos = trans.position;
        pos.y += offset;
        trans.position = pos;
    }
}