using Task_Intermedia.Algorithm_Task;

var tree = new TreeNode("Root", new List<TreeNode>
            {
                new TreeNode("A", new List<TreeNode>
                {
                    new TreeNode("A1", null),
                    new TreeNode("A2", null),
                }),
                new TreeNode("B", new List<TreeNode>
                {
                    new TreeNode("B1", null),
                    new TreeNode("B2", null),
                }),
                new TreeNode("C", new List<TreeNode>
                {
                    new TreeNode("C1", null),
                    new TreeNode("C2", null),
                }),
            });

var treeTraversal = new TreeTraversal();
IEnumerable<TreeNode> leafNodes = treeTraversal.GetLeafs(tree);

foreach (var leaf in leafNodes)
{
    Console.WriteLine(leaf.payload);
}
    