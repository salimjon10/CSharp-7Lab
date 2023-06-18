using System;
using System.Collections;
using System.Collections.Generic;

public class BinaryTree<T> : IEnumerable<T>
{
    private BinaryTreeNode<T> root;
    private BinaryTreeNode<T> current;

    public void Add(T value)     
    {
        if (root == null)
        {
            root = new BinaryTreeNode<T>(value);
        }
        else
        {
            AddTo(root, value);
        }
    }

    private void AddTo(BinaryTreeNode<T> node, T value)
    {
        if (Comparer<T>.Default.Compare(value, node.Value) < 0)
        {
            if (node.Left == null)
            {
                node.Left = new BinaryTreeNode<T>(value);
            }
            else
            {
                AddTo(node.Left, value);
            }
        }
        else
        {
            if (node.Right == null)
            {
                node.Right = new BinaryTreeNode<T>(value);
            }
            else
            {
                AddTo(node.Right, value);
            }
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        current = null;
        return InOrderTraversal().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private IEnumerable<T> InOrderTraversal()
    {
        if (root != null)
        {
            Stack<BinaryTreeNode<T>> stack = new Stack<BinaryTreeNode<T>>();
            BinaryTreeNode<T> current = root;

            while (stack.Count > 0 || current != null)
            {
                if (current != null)
                {
                    stack.Push(current);
                    current = current.Left;
                }
                else
                {
                    current = stack.Pop();
                    yield return current.Value;
                    current = current.Right;
                }
            }
        }
    }

    private IEnumerable<T> ReverseInOrderTraversal()
    {
        if (root != null)
        {
            Stack<BinaryTreeNode<T>> stack = new Stack<BinaryTreeNode<T>>();
            BinaryTreeNode<T> current = root;

            while (stack.Count > 0 || current != null)
            {
                if (current != null)
                {
                    stack.Push(current);
                    current = current.Right;
                }
                else
                {
                    current = stack.Pop();
                    yield return current.Value;
                    current = current.Left;
                }
            }
        }
    }

    public T Current()
    {
        return current != null ? current.Value : default(T);
    }

     public bool Next()
    {
        if (current == null)
        {
            current = FindFirstNode(root);
            return current != null;
        }
        else
        {
            if (current.Right != null)
            {
                current = FindFirstNode(current.Right);
                return current != null;
            }
            else
            {
                BinaryTreeNode<T> parent = FindParentWithLeftChild(root, current);
                current = parent;
                return current != null;
            }
        }
    }

    public bool Previous()
    {
        if (current == null)
        {
            current = FindLastNode(root);
            return current != null;
        }
        else
        {
            if (current.Left != null)
            {
                current = FindLastNode(current.Left);
                return current != null;
            }
            else
            {
                BinaryTreeNode<T> parent = FindParentWithRightChild(root, current);
                current = parent;
                return current != null;
            }
        }
    }

    // Метод для поиска первого узла, начиная с заданного узла, для которого отсутствует левый дочерний элемент
    private BinaryTreeNode<T> FindFirstNode(BinaryTreeNode<T> node)
    {
        while (node.Left != null)
        {
            node = node.Left;
        }
        return node;
    }

    // Метод для поиска последнего узла, начиная с заданного узла, для которого отсутствует правый дочерний элемент
    private BinaryTreeNode<T> FindLastNode(BinaryTreeNode<T> node)
    {
        while (node.Right != null)
        {
            node = node.Right;
        }
        return node;
    }

    // Метод для поиска родительского узла с левым дочерним элементом
    private BinaryTreeNode<T> FindParentWithLeftChild(BinaryTreeNode<T> root, BinaryTreeNode<T> child)
    {
        BinaryTreeNode<T> parent = null;
        while (root != null && root != child)
        {
            if (Comparer<T>.Default.Compare(child.Value, root.Value) < 0)
            {
                parent = root;
                root = root.Left;
            }
            else
            {
                root = root.Right;
            }
        }
        return parent;
    }

    // Метод для поиска родительского узла с правым дочерним элементом
    private BinaryTreeNode<T> FindParentWithRightChild(BinaryTreeNode<T> root, BinaryTreeNode<T> child)
    {
        BinaryTreeNode<T> parent = null;
        while (root != null && root != child)
        {
            if (Comparer<T>.Default.Compare(child.Value, root.Value) > 0)
            {
                parent = root;
                root = root.Right;
            }
            else
            {
                root = root.Left;
            }
        }
        return parent;
    }

    private class BinaryTreeNode<TNode>
    {
        public TNode Value { get; }
        public BinaryTreeNode<TNode> Left { get; set; }
        public BinaryTreeNode<TNode> Right { get; set; }

        public BinaryTreeNode(TNode value)
        {
            Value = value;
        }
     }
}
