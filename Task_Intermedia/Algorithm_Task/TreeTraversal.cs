using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Intermedia.Algorithm_Task
{
    public record TreeNode(string payload, IEnumerable<TreeNode> Children);

    public class TreeTraversal
    {
        public IEnumerable<TreeNode> GetLeafs(TreeNode root)
        {
            if (root.Children == null || !root.Children.Any())
            {
                // If the current node has no children, it is a leaf node.
                return new List<TreeNode> { root };
            }

            List<TreeNode> leafNodes = new List<TreeNode>();

            foreach (var child in root.Children)
            {
                // Recursively traverse the child nodes and add their leaf nodes to the result.
                leafNodes.AddRange(GetLeafs(child));
            }

            return leafNodes;
        }
    }
}
