using System.Collections.Generic;

namespace Assets.Resources.Scripts
{
    public interface ITreeInfrastructure
    {
        public void AddTree(TreeScript tree, TreeScript fatherTree = null);

        public TreeScript GetFatherOfTree(TreeScript tree);

        public List<TreeScript> GetChildrenOfTree(TreeScript tree);
    }
}