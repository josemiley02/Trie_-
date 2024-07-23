using Trie.Node;
namespace Trie.ITrie;

public interface ITrie
{
    NodeTrie Root { get; }
    void AddWord(string word);
    string GreatestCommonPrefix();
    IEnumerable<string> WordWithPrefix(string prefix);
    bool ContainWord(string word);
    void Clear();
    int Count { get; }
    public NodeTrie this[char value]
    {
        get;
    }
}