using System.Collections.Generic;
using System.Linq;

/*
 * 二叉堆，二叉树的变体，常见的二叉堆是最大堆和最小堆，这个例子是最小堆
 * 
 * 最小堆的规则是“父节点的值大于等于两个子节点的值”，这个规则可以让堆顶元素的值永远是堆里最小的
 * 
 * 要做到这个功能的关键在于“每次插入新节点都在堆底，之后通过一次次的大小检测和对换将这个节点上移到正确的位置（比所有子节点都小但比父节点大）”
 * 
 * 然而“每次插入都在堆底”这个要求在一般的二叉树里很不好实现，于是二叉堆使用List来解决这个问题
 * 
 * 
 * 
 * List里每个元素在堆里的位置是这样的，可以看到只要在List的尾部插入元素就是插入到堆底
 * 
 *               0
 *       1               2
 *   3       4       5       6
 * 7   8   9   10  11  12  13  14
 * 
 * 可以看出父子级和下标的关系：
 * 左子节点 = 下标 * 2 + 1
 * 右子节点 = 下标 * 2 + 2
 * 父节点 = (下标 - 1) / 2       这个式子里全是int，int运算自动抛弃小数，因此不需要强转
 */

/// <summary>
/// 最简单的最小堆，存入float排序，仅做演示用，没有实用价值
/// </summary>
public class MinBinaryHeapDemo
{
    List<float> _nodes = new List<float>();


    //存入
    public void SetValue(float newNode)
    {
        _nodes.Add(newNode);

        BottomToTop();
    }
    /// <summary>
    /// 从底到顶调整堆
    /// </summary>
    void BottomToTop()
    {
        int currentIndex = _nodes.Count - 1;                        //正在进行调整的元素的下标，最开始是最末尾

        while (currentIndex != 0 && _nodes[currentIndex] < _nodes[GetParentIndex(currentIndex)])  //现在正在调整的元素不是根元素，并且值比父节点小   父节点下标 = (当前节点下标 - 1) / 2，不分左右
        {
            int parentIndex = GetParentIndex(currentIndex);         //计算并存储父节点的下标

            _nodes.Swap(currentIndex, parentIndex);                 //交换两个节点，用的是扩展方法

            currentIndex = parentIndex;                             //将检测元素下标改为父节点下标
        }
    }
    /// <summary>
    /// 获取一个节点的父节点的下标
    /// </summary>
    /// <param name="currentIndex"></param>
    /// <returns></returns>
    int GetParentIndex(int currentIndex)
    {
        return (currentIndex - 1) / 2;
    }


    //读取
    public float GetTop()
    {
        if (_nodes.Count == 0) return 0;

        return _nodes[0];
    }


    //移除
    /// <summary>
    /// 移除顶部值
    /// </summary>
    public void RemoveTop()
    {
        if (_nodes.Count == 0) return;

        ReplaceheWithBottom();                      //用末尾位置代替顶部值
        TopToBottom();                              //从顶部向下调整堆
    }

    /// <summary>
    /// 用末尾值代替顶部值
    /// </summary>
    void ReplaceheWithBottom()
    {
        _nodes[0] = _nodes.Last();
        _nodes.RemoveAt(_nodes.Count - 1);
    }

    /// <summary>
    /// 从顶到底调整堆
    /// </summary>
    void TopToBottom()
    {
        int currentIndex = 0;   //记录正在调整的元素的下标

        while (true)            //写判断太长太乱，用死循环 + 跳出
        {
            int smallerChildIndex = FindSmallerChind(currentIndex);     //获取比较小的那个子节点的下标

            if (smallerChildIndex > 0 && _nodes[smallerChildIndex] < _nodes[currentIndex])    //如果有子节点，并且比较大的子节点的值比当前节点小
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

    /// <summary>
    /// 传入下标，返回子节点中小的那一个的下标，如果没有子节点则返回负数
    /// </summary>
    /// <param name="parentIndex"></param>
    /// <returns></returns>
    int FindSmallerChind(int parentIndex)
    {
        if (!HaveLeftChildNode(parentIndex)) return -1;     //二叉堆是完全二叉树，如果没有左子节点就不会有右子节点，找不到左子节点的时候返回负数表示不存在

        if (!HaveRightChildNode(parentIndex)) return GetLeftChildIndex(parentIndex);

        int leftChildIndex = GetLeftChildIndex(parentIndex);
        int rightChildIndex = GetRightChildIndex(parentIndex);

        return _nodes[leftChildIndex] < _nodes[rightChildIndex] ? leftChildIndex : rightChildIndex;
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
    

    //测试用
    /// <summary>
    /// 用string形式返回堆
    /// </summary>
    /// <returns></returns>
    public string Print()
    {
        string heapString = "";

        int length = 1;         //单层长度，默认是顶层的1

        for (int i = 0, j = 1; i < _nodes.Count; i++, j++)
        {
            heapString += _nodes[i].ToString();
            heapString += "  ";

            if (j >= length)    //检测是否写完了一行
            {
                heapString += "\n";     //如果写完一行了则进行换行
                length *= 2;            //获取下一层长度（二叉堆是完全二叉树，每一行都是加倍的）
                j = 0;                  //将当前长度返回0
            }
        }

        return heapString;
    }
}
