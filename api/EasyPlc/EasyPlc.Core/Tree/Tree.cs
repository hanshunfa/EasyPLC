/*=============================================================================================
* 
*      *******    *******         **    **
*      **         **              **    **
*      **         **              **    **
*      *******    *******   **    ********
*           **    **              **    **
*           **    **              **    **
*      *******    **              **    **
*
* 创建者：韩顺发
* CLR版本：4.0.30319.42000
* 电子邮箱：shunfa.han@kstopa.com.cn
* 创建时间：2023/11/22 15:29:40
* 版本：v1.0.0
* 描述：
*
* ==============================================================================================
* 修改人：
* 修改时间：
* 修改说明：
* 版本：
* 
===============================================================================================*/

namespace EasyPlc.Core;

/// <summary>
/// 
/// </summary>
public class ResourceNode
{
    public string Title { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string ValueType { get; set; }
    public int ValueLength { get; set; }
    public string Category { get; set; }
    public dynamic Value { get; set; }
}

public class TreeNode<T>
{
    public T Data { get; set; }
    public List<TreeNode<T>> Children { get; set; }

}
public class Tree<T>
{
    public TreeNode<T> Root { get; set; }
}

/*

//
public class BinaryTreeNode<T> : TreeNode<T>
{
    public BinaryTreeNode()
    {
        Children = new List<TreeNode<T>>() { null, null };
    }
    public BinaryTreeNode<T> Parent { get; set; }

    public BinaryTreeNode<T> Left
    {
        get
        {
            return (BinaryTreeNode<T>)Children[0];
        }
        set
        {
            Children[0] = value;
        }
    }
    public BinaryTreeNode<T> Right
    {
        get
        {
            return (BinaryTreeNode<T>)Children[1];
        }
        set
        {
            Children[1] = value;
        }
    }
    public int GetHeight()
    {
        int height = 1;
        BinaryTreeNode<T> current = this;
        while (current.Parent != null)
        {
            height++;
            current = current.Parent;
        }
        return height;
    }
}
/// <summary>
/// 二叉树遍历类型
/// </summary>
public enum TranversalEnum
{
    PREORDER,
    INORDER,
    POSTORDER,
}

public class BinaryTree<T>
{
    public BinaryTreeNode<T> Root { get; set; }
    /// <summary>
    /// 二叉树所有节点数量
    /// </summary>
    public int Count { get; set; }

    private void TranversePreOrder(BinaryTreeNode<T> node, List<BinaryTreeNode<T>> result)
    {
        if (node != null)
        {
            result.Add(node);//前序遍历
            TranversePreOrder(node.Left, result);
            TranversePreOrder(node.Right, result);
        }
    }
    private void TranverseInOrder(BinaryTreeNode<T> node, List<BinaryTreeNode<T>> result)
    {
        if (node != null)
        {
            TranverseInOrder(node.Left, result);
            result.Add(node);
            TranverseInOrder(node.Right, result);
        }
    }
    private void TranversePostOrder(BinaryTreeNode<T> node, List<BinaryTreeNode<T>> result)
    {
        if (node != null)
        {
            TranversePostOrder(node.Left, result);
            TranversePostOrder(node.Right, result);
            result.Add(node);
        }
    }
    /// <summary>
    /// 遍历
    /// </summary>
    /// <param name="mode"></param>
    /// <returns></returns>
    public List<BinaryTreeNode<T>> Tranverse(TranversalEnum mode)
    {
        List<BinaryTreeNode<T>> nodes = new List<BinaryTreeNode<T>>();
        switch (mode)
        {
            case TranversalEnum.PREORDER:
                TranversePreOrder(Root, nodes);
                break;
            case TranversalEnum.INORDER:
                TranverseInOrder(Root, nodes);
                break;
            case TranversalEnum.POSTORDER:
                TranversePostOrder(Root, nodes);
                break;
        }
        return nodes;
    }
    public int GetHeight()
    {
        int height = 0;
        foreach (var node in Tranverse(TranversalEnum.PREORDER))
        {
            height = Math.Max(height, node.GetHeight());
        }
        return height;
    }

}

public class BinarySearchTree<T> : BinaryTree<T> where T : IComparable
{
    public bool Contains(T data)
    {
        BinaryTreeNode<T> node = Root;
        while (node != null)
        {
            int result = data.CompareTo(node.Data);
            if (result == 0)
            {
                return true;
            }
            else if (result < 0)
            {
                node = node.Left;
            }
            else
            {
                node = node.Right;
            }
        }
        return false;
    }
    private BinaryTreeNode<T> GetParentForNewNode(T data)
    {
        BinaryTreeNode<T> current = Root;
        BinaryTreeNode<T> parent = null;
        while (current != null)
        {
            parent = current;
            int result = data.CompareTo(current.Data);
            if (result == 0)
            {
                throw new ArgumentException($"The node {data} already exists");
            }
            else if (result < 0)
            {
                current = current.Left;
            }
            else
            {
                current = current.Right;
            }
        }

        return parent;
    }
    public void Add(T data)
    {
        BinaryTreeNode<T> parent = GetParentForNewNode(data);
        BinaryTreeNode<T> node = new BinaryTreeNode<T>() { Data = data, Parent = parent };
        if (parent == null)
        {
            Root = node;
        }
        else if (data.CompareTo(parent.Data) < 0)
        {
            parent.Left = node;
        }
        else
        {
            parent.Right = node;
        }
        Count++;
    }
    public void Remove(T data)
    {

    }
    /// <summary>
    /// 从以根节点未node的树中，移除含有data的节点
    /// </summary>
    /// <param name="node"></param>
    /// <param name="data"></param>
    /// <exception cref="ArgumentException"></exception>
    private void Remove(BinaryTreeNode<T> node, T data)
    {
        if (node == null)
        {
            throw new ArgumentException($"The node {data} does not exist");
        }
        else if (data.CompareTo(node.Data) < 0)
        {
            Remove(node.Left, data);
        }
        else if (data.CompareTo(node.Data) > 0)
        {
            Remove(node.Right, data);
        }
        else //移除的节点找到了
        {
            if (node.Left == null && node.Right == null)
            {
                //是叶节点，将该节点替换为null
                ReplaceInParent(node, null);
                Count--;
            }
            else if (node.Right == null)
            {
                //右节点为空,用该节点的左节点替换该节点
                ReplaceInParent(node, node.Left);
                Count--;
            }
            else if (node.Left == null)
            {
                //左节点为空，用改节点的右节点替换该节点
                ReplaceInParent(node, node.Right);
                Count--;

            }
            else
            {
                //左右节点都不为空,找到以该节点的右节点为根节点的子树的最小节点，
                //将上述最小节点的值替换到该节点的值，然后再移除最小节点上的数据。
                BinaryTreeNode<T> succor = FindMininumInSubTree(node.Right);
                //succor的左节点一定是null，后续Remove方法，一定进入左节点是空的分支。
                node.Data = succor.Data;
                Remove(succor, succor.Data);//
            }
        }
    }
    /// <summary>
    /// 替换节点，删除node，用newNode来替换node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="newNode"></param>
    private void ReplaceInParent(BinaryTreeNode<T> node, BinaryTreeNode<T> newNode)
    {
        //首先变更node.Parent中含有的对node的信息
        if (node.Parent != null)
        {
            if (node.Parent.Left == node)
            {
                node.Parent.Left = newNode;
            }
            else
            {
                node.Parent.Right = newNode;
            }
        }
        else
        {
            Root = newNode;
        }
        //再变更newNode中有关Parent的信息
        if (newNode != null)
        {
            newNode.Parent = node.Parent;
        }
    }
    /// <summary>
    /// 找到以node为根节点的子树的最小节点
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    private BinaryTreeNode<T> FindMininumInSubTree(BinaryTreeNode<T> node)
    {
        while (node.Left != null)
        {
            node = node.Left;
        }
        return node;
    }

}


*/