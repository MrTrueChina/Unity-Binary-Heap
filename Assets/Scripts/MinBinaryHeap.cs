using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 二叉堆节点，保存了数值和映射的目标
/// </summary>
/// <typeparam name="T"></typeparam>
public struct MinBinaryHeapNode<T>
{
    public T obj;
    public float value;
}

/// <summary>
/// 添加泛型，可以通过节点的 obj 获取到存入的对象
/// </summary>
/// <typeparam name="T"></typeparam>
public class MinBinaryHeap<T>
{
    List<MinBinaryHeapNode<T>> _nodes = new List<MinBinaryHeapNode<T>>();


    //存入
    public void SetNode(MinBinaryHeapNode<T> newNode)
    {
        _nodes.Add(newNode);

        BottomToTop(_nodes.Count - 1);
    }
    public void SetNode(T obj, float value)
    {
        SetNode(new MinBinaryHeapNode<T> { obj = obj, value = value });
    }
    


    //读取
    public T GetTopNodeObject()
    {
        if (_nodes.Count == 0) return default(T);       //defult(T)：根据T的类型返回默认值

        return _nodes[0].obj;
    }


    //移除
    public void RemoveTop()
    {
        RemoveAt(0);
    }
    public void RemoveFirstThroughValue(float value)
    {
        int removeIndex = FindFirstIndexThroughValue(value);

        RemoveAt(removeIndex);
    }
    public void RemoveFirstThroughObj(T obj)
    {
        int removeIndex = FindFirstIndexThroughObj(obj);

        RemoveAt(removeIndex);
    }
    public void RemoveNode(MinBinaryHeapNode<T> node)
    {
        for (int i = 0; i < _nodes.Count; i++)
            if (_nodes[i].Equals(node))
            {
                RemoveAt(i);
                return;
            }
    }
    public void RemoveAt(int removeIndex)
    {
        if (removeIndex < 0 || removeIndex >= _nodes.Count) return;

        int lastLeftIndex = GetLastLeftChildIndex(removeIndex);


        _nodes[removeIndex] = _nodes[lastLeftIndex];
        TopToBottom(removeIndex);


        _nodes[lastLeftIndex] = _nodes.Last();
        BottomToTop(lastLeftIndex);
        _nodes.RemoveAt(_nodes.Count - 1);
    }
    int FindFirstIndexThroughValue(float value)
    {
        for (int i = 0; i < _nodes.Count; i++)
            if (_nodes[i].value == value)
                return i;
        return -1;
    }
    int FindFirstIndexThroughObj(T obj)
    {
        for (int i = 0; i < _nodes.Count; i++)
            if (_nodes[i].obj.Equals(obj))
                return i;
        return -1;
    }
    int GetLastLeftChildIndex(int parentIndex)
    {
        int nextLeftChildIndex = parentIndex;

        while (nextLeftChildIndex < _nodes.Count)
            nextLeftChildIndex = GetLeftChildIndex(nextLeftChildIndex);     //循环找下一个左子节点直到超出列表

        return GetParentIndex(nextLeftChildIndex);                          //返回这个下标的父节点下标，就是最远的左子节点
    }

    void TopToBottom(int startIndex)
    {
        int currentIndex = startIndex;  //记录正在调整的元素的下标

        while (true)                    //写判断太长太乱，用死循环 + 跳出
        {
            int smallerChildIndex = FindSmallerChind(currentIndex);     //获取比较小的那个子节点的下标

            if (smallerChildIndex > 0 && _nodes[smallerChildIndex].value < _nodes[currentIndex].value)    //如果有子节点，并且比较小的子节点的值比当前节点小
            {
                _nodes.Swap(currentIndex, smallerChildIndex);           //交换当前节点和比较小的子节点

                currentIndex = smallerChildIndex;                       //将调整的节点设为比较小的子节点的下标
            }
            else
            {
                break;                  //没有子节点或者较小的子节点的值比当前节点的值小，则说明调整完成，跳出循环
            }
        }
    }
    void BottomToTop(int startIndex)
    {
        int currentIndex = startIndex;                              //正在进行调整的元素的下标

        while (currentIndex != 0 && _nodes[currentIndex].value < _nodes[GetParentIndex(currentIndex)].value)  //现在正在调整的元素不是根元素，并且值比父节点小   父节点下标 = (当前节点下标 - 1) / 2，不分左右
        {
            int parentIndex = GetParentIndex(currentIndex);         //计算并存储父节点的下标
            
            _nodes.Swap(currentIndex, parentIndex);                 //交换两个节点，用的是扩展方法
            
            currentIndex = parentIndex;                             //将检测元素下标改为父节点下标
        }
    }

    int FindSmallerChind(int parentIndex)
    {
        if (!HaveLeftChildNode(parentIndex)) return -1;     //二叉堆是完全二叉树，如果没有左子节点就不会有右子节点，找不到左子节点的时候返回负数表示不存在

        if (!HaveRightChildNode(parentIndex)) return GetLeftChildIndex(parentIndex);

        int leftChildIndex = GetLeftChildIndex(parentIndex);
        int rightChildIndex = GetRightChildIndex(parentIndex);

        return _nodes[leftChildIndex].value < _nodes[rightChildIndex].value ? leftChildIndex : rightChildIndex;
    }
    bool HaveLeftChildNode(int parentIndex)
    {
        return GetLeftChildIndex(parentIndex) < _nodes.Count;   //如果左子节点的下标小于节点List里的元素总数，则说明存在左子节点
    }
    bool HaveRightChildNode(int parentIndex)
    {
        return GetRightChildIndex(parentIndex) < _nodes.Count;
    }

    int GetParentIndex(int currentIndex)
    {
        return (currentIndex - 1) / 2;
    }
    int GetLeftChildIndex(int parentIndex)
    {
        return parentIndex * 2 + 1;
    }
    int GetRightChildIndex(int parentIndex)
    {
        return parentIndex * 2 + 2;
    }


    //查询
    public bool ContainsValue(float value)
    {
        foreach (MinBinaryHeapNode<T> node in _nodes)
            if (node.value == value)
                return true;
        return false;
    }

    public T FindFirstThroughValue(float value)
    {
        foreach (MinBinaryHeapNode<T> node in _nodes)
            if (node.value == value)
                return node.obj;
        return default(T);
    }

    public bool isEmpty
    {
        get { return _nodes == null || _nodes.Count == 0; }
    }

    public int Count
    {
        get { return _nodes.Count; }
    }
    
    public List<MinBinaryHeapNode<T>> GetNodes()
    {
        return _nodes;
    }
}

public static partial class ListExtension       //partical：加了这个关键字的类可以分开写在多个地方，比如这里给List增加Swap扩展方法，因为有partical所以能在别的.cs文件里添加其他方法而他们又共同属于同一个ListExtension类
{
    public static void Swap<T>(this List<T> list, int aIndex, int bIndex)
    {
        if (aIndex < 0 || aIndex >= list.Count || bIndex < 0 || bIndex >= list.Count) return;

        T temporary = list[aIndex];
        list[aIndex] = list[bIndex];
        list[bIndex] = temporary;
    }
}
