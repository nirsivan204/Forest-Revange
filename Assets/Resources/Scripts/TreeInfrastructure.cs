using System.Collections.Generic;
using UnityEngine;

namespace Assets.Resources.Scripts
{
    public class TreeInfrastructure : MonoBehaviour, ITreeInfrastructure
    {
        public void AddTree(TreeScript tree, TreeScript fatherTree = null)
        {
            
        }

        public TreeScript GetFatherOfTree(TreeScript tree)
        {
            // return tree.
            return null;
        }

        public List<TreeScript> GetChildrenOfTree(TreeScript tree)
        {
            
            return null;
        }
    }
}