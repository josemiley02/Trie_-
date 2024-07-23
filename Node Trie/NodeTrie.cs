namespace Trie.Node;
public class NodeTrie
{
    public NodeTrie(char vlaue, int deep)
    {
        Value = vlaue;
        EndOfWord = false;
        Children = [];
        Deep = deep;
    }

    public char Value { get; private set; }
    public bool EndOfWord { get; set; }
    public int Deep { get; private set; }
    public Dictionary<char, NodeTrie> Children { get; private set; }

    public NodeTrie this[char key] => Children[key];
}