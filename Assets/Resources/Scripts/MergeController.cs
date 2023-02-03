using System.Collections.Generic;
using UnityEngine;

interface MergeableItem {
    int getSize();
    Vector3 getPosition();
    void notifyMerge();
}

public class MergeController 
{
    private static int requiredItemsForMerge = 3;
    private static Dictionary<int, int> mergeRanges = new Dictionary<int, int>(){
        {0, 100},
        {1, 100},
        {3, 100},
        {4, 100},
        {5, 100},
        {6, 100},
        {7, 100},
        {8, 100},
        {9, 100},
        {10, 100},
    };

    void CheckMerge(MergeableItem triggingItem, List<MergeableItem> items) 
    {
        List<MergeableItem> qualifyForMerge = new List<MergeableItem>();
        qualifyForMerge.Add(triggingItem);
        foreach (MergeableItem i in items) {

            if (i == triggingItem) {
                continue;
            }

            float dist = Vector3.Distance(triggingItem.getPosition(), i.getPosition());
            if (dist <= mergeRanges[triggingItem.getSize()]) {
                qualifyForMerge.Add(i);
            }

            if (qualifyForMerge.Count == requiredItemsForMerge) {
                // let all trees should be merged know that
                foreach(MergeableItem j in qualifyForMerge) {
                    j.notifyMerge();
                }
                // Vector3 midPosition = Lerp3(qualifyForMerge[0].getPosition(),
                //     qualifyForMerge[1].getPosition(),
                //     qualifyForMerge[2].getPosition(),
                //     0.5f);
                this.CreateNewItem(triggingItem.getPosition(), triggingItem.getSize() + 1);
                return;
            }

        }
    }

    private static Vector3 Lerp3(Vector3 a, Vector3 b, Vector3 c, float t) {
        return Vector3.Lerp(Vector3.Lerp(a, b, t), c, t);
    }

    private void CreateNewItem(Vector3 position, int size)
    {
        // TODO: implement - create new tree in the given location and size
    }
}
