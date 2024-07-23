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
        Root = new NodeTrie('\0', 0);
    }
    public int Count => count;
    private int count { get; set; }
    public void AddWord(string word)
    {
        word = word.ToUpper();
        addWord(word, 0, Root);
        count += 1;
    }
    private void addWord(string word, int pos, NodeTrie current, int deep = 0)
    {
        if (pos <= word.Length - 1 &&  current.Children.TryGetValue(word[pos], out var child))
        {
            addWord(word, pos + 1, child, child.Deep);
        }
        else
        {
            var nt = current;
            for (int i = pos; i < word.Length; i++)
            {
                nt.Children[word[i]] = new NodeTrie(word[i], ++deep);
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
        NodeTrie best = null!;
        int bestDeep = int.MinValue;
        int currentDeep = 0;
        foreach (var item in Root.Children.Values)
        {
            currentDeep = GetDeep(item);
            if (bestDeep < currentDeep)
            {
                best = item;
                bestDeep = currentDeep;
            }
        }
        string prefix = "";
        while (bestDeep > 1)
        {
            prefix += best.Value;
            best = best.Children.Values.First();
            bestDeep--;
        }
        return best == null ? prefix : prefix + best.Value;
    }
    private int GetDeep(NodeTrie current)
    {
        if (current.Children.Count == 0 || current.Children.Count > 1) return current.Deep;
        return GetDeep(current.Children.Values.First());
    }

    public IEnumerable<string> WordWithPrefix(string prefix)
    {
        var endPrefix = EndOfPrefix(prefix, Root);
        return WordWithPrefix(prefix, endPrefix);
    }
    private IEnumerable<string> WordWithPrefix(string prefix, NodeTrie current)
    {
        foreach (var item in current.Children.Values)
        {
            string newPrefix = prefix + item.Value;
            if(item.EndOfWord) yield return newPrefix;
            foreach (var word in WordWithPrefix(newPrefix, item)) yield return word;
        }
    }
    private NodeTrie EndOfPrefix(string prefix, NodeTrie current)
    {
        for (int i = 0; i < prefix.Length; i++)
        {
            if (!current.Children.TryGetValue(prefix[i], out var node))
            {
                return null!;
            }
            current = node;
        }
        return current;
    }
}