using Trie.ITrie;
using Trie.Node;
namespace Trie;

public class Trie : ITrie.ITrie
{
    public NodeTrie this[char value]
    {
        get
        {
            try
            {
                return Root[value];
            }
            catch (KeyNotFoundException)
            {
                return null!;
            }
        }
    }

    public NodeTrie Root { get; }

    public Trie()
    {
        Root = new NodeTrie('^');
    }
    public int Count => count;
    private int count { get; set; }
    public void AddWord(string word)
    {
        addWord(word, 0, Root);
        count += 1;
    }
    private void addWord(string word, int pos, NodeTrie current)
    {
        if (current.Children.TryGetValue(word[pos], out var child))
        {
            addWord(word, pos + 1, child);
        }
        else
        {
            var nt = current;
            for (int i = pos; i < word.Length; i++)
            {
                nt.Children[word[i]] = new NodeTrie(word[i]);
                nt = nt[word[i]];
            }
            nt.EndOfWord = true;
        }
    }

    public void Clear()
    {
        Root.Children.Clear();
        count = 0;
    }

    public bool ContainsWord(string word)
    {
        return containsWord(word, 0, Root);
    }
    private bool containsWord(string word, int pos, NodeTrie current)
    {
        if (!current.Children.TryGetValue(word[pos], out var child))
        {
            return false;
        }
        return containsWord(word, pos + 1, child);
    }

    public string GreatestCommonPrefix()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<string> WordWithPrefix(string prefix)
    {
        throw new NotImplementedException();
    }
}