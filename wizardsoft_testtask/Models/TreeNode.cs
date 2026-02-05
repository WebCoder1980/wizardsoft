namespace wizardsoft_testtask.Models
{
    public class TreeNode
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public long? ParentId { get; set; }
        public TreeNode? Parent { get; set; }
        public ICollection<TreeNode> Children { get; set; } = new List<TreeNode>();
    }
}
