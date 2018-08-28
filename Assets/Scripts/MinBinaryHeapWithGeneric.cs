using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 二叉堆节点，保存了数值和映射的目标
/// </summary>
/// <typeparam name="T"></typeparam>
public struct Node<T>
{
    public T obj;
    public float value;
}

/// <summary>
/// 添加泛型，可以通过节点的 obj 获取到存入的对象
/// </summary>
/// <typeparam name="T"></typeparam>
public class MinBinaryHeapWithGeneric<T>
{
    List<Node<T>> _nodes = new List<Node<T>>();


    //存入
    public void SetNode(Node<T> newNode)
    {
        _nodes.Add(newNode);

        BottomToTop();
    }
    public void SetNode(T obj, float value)
    {
        SetNode(new Node<T> { obj = obj, value = value });
    }
    
    void BottomToTop()
    {
        int currentIndex = _nodes.Count - 1;                        //正在进行调整的元素的下标，最开始是最末尾

        while (currentIndex != 0 && _nodes[currentIndex].value < _nodes[GetParentIndex(currentIndex)].value)  //现在正在调整的元素不是根元素，并且值比父节点小   父节点下标 = (当前节点下标 - 1) / 2，不分左右
        {
            int parentIndex = GetParentIndex(currentIndex);         //计算并存储父节点的下标

            _nodes.Swap(currentIndex, parentIndex);                 //交换两个节点，用的是扩展方法

            currentIndex = parentIndex;                             //将检测元素下标改为父节点下标
        }
    }
    int GetParentIndex(int currentIndex)
    {
        return (currentIndex - 1) / 2;
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
        if (_nodes.Count == 0) return;

        ReplaceHeadWithBottom();                    //用末尾位置代替顶部值
        TopToBottom();                              //从顶部向下调整堆
    }
    void ReplaceHeadWithBottom()
    {
        _nodes[0] = _nodes.Last();
        _nodes.RemoveAt(_nodes.Count - 1);
    }
    void TopToBottom()
    {
        int currentIndex = 0;   //记录正在调整的元素的下标

        while (true)            //写判断太长太乱，用死循环 + 跳出
        {
            int smallerChildIndex = FindSmallerChind(currentIndex);     //获取比较小的那个子节点的下标

            if (smallerChildIndex > 0 && _nodes[smallerChildIndex].value < _nodes[currentIndex].value)    //如果有子节点，并且比较大的子节点的值比当前节点小
            {
                _nodes.Swap(currentIndex, smallerChildIndex);           //交换当前节点和比较小的子节点

                currentIndex = smallerChildIndex;                       //将调整的节点设为比较小的子节点的下标
            }
            else
            {
                break;          //没有子节点或者较大的子节点的值比当前节点的值小，则说明调整完成，跳出循环
            }
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
    int GetLeftChildIndex(int parentIndex)
    {
        return parentIndex * 2 + 1;
    }
    bool HaveRightChildNode(int parentIndex)
    {
        return GetRightChildIndex(parentIndex) < _nodes.Count;
    }
    int GetRightChildIndex(int parentIndex)
    {
        return parentIndex * 2 + 2;
    }
}
