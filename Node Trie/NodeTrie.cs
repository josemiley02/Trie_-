namespace Trie.Node
{
    public class NodeTrie
    {
        public NodeTrie(char vlaue)
        {
            Value = vlaue;
            EndOfWord = false;
            Children = [];
        }

        public char Value { get; private set; }
        public bool EndOfWord { get; set; }
        public Dictionary<char, NodeTrie> Children { get; private set;}
        
        public NodeTrie this[char key] => Children[key];
    }
}