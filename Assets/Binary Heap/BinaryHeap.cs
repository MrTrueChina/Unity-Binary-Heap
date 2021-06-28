using System.Collections.Generic;
using System.Linq;
using System;

namespace MtC.Tools.BinaryHeap
{
    /// <summary>
    /// 二叉堆节点
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct BinaryHeapNode<T>
    {
        /// <summary>
        /// 这个节点对应的对象
        /// </summary>
        public T obj;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="obj"></param>
        public BinaryHeapNode(T obj)
        {
            this.obj = obj;
        }
    }

    /// <summary>
    /// 添加泛型，可以通过节点的 obj 获取到存入的对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BinaryHeap<T>
    {
        /// <summary>
        /// 所有节点
        /// </summary>
        private List<BinaryHeapNode<T>> nodes = new List<BinaryHeapNode<T>>();

        /// <summary>
        /// 比较两个节点，返回负数表示 节点a 更接近堆顶，返回正数表示 节点b 更接近堆顶，返回 0 表示两个节点比较上相同
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public abstract int Comparison(BinaryHeapNode<T> a, BinaryHeapNode<T> b);

        /// <summary>
        /// 获取堆顶节点的对象
        /// </summary>
        /// <returns></returns>
        public T GetTopNodeObject()
        {
            // 堆是空的，返回默认值，用默认值是因为有些对象不允许为空
            if (nodes.Count == 0)
            {
                return default(T);
            }

            // 二叉堆的结构决定了第一个节点就是堆顶
            return nodes[0].obj;
        }

        /// <summary>
        /// 移除第一个指定的对象的节点
        /// </summary>
        /// <param name="obj"></param>
        public void RemoveFirstThroughObj(T obj)
        {
            // 找到第一个是指定对象的节点
            int removeIndex = nodes.FindIndex(node => Equals(node.obj, obj));

            // 删除掉
            RemoveAt(removeIndex);
        }

        /// <summary>
        /// 移除指定索引的节点
        /// </summary>
        /// <param name="removeIndex"></param>
        private void RemoveAt(int removeIndex)
        {
            // 获取从要移除的索引开始向左下方向的最后一个节点，因为二叉堆的结构，每一层是从左向右添加的，左下方向最后一个就是最深层的子节点
            int lastLeftIndex = GetLastLeftChildIndex(removeIndex);

            // 把左下方最后一个节点覆盖到被移除的节点的位置，之后从移除节点位置向下更新
            nodes[removeIndex] = nodes[lastLeftIndex];
            TopToBottom(removeIndex);

            // 到这里，要移除的节点已经被移除，但四叉树里出现了两个相同的节点，就是最左下节点
            // 根据二叉堆的原理，这次上到下更新不会改变最左下节点，因此可以确定两个节点中一个必然在最左下位置

            // 用整个堆的最后一个节点覆盖掉最左下位置，之后从最左下位置下到上更新
            nodes[lastLeftIndex] = nodes.Last();
            BottomToTop(lastLeftIndex);

            // 到这里，两个相同的最左下节点中的一个被覆盖掉。但对应的，出现了两个相同的末尾节点，其中一个在堆的末尾

            // 移除多出来的最后一个节点
            nodes.RemoveAt(nodes.Count - 1);
        }

        /// <summary>
        /// 获取指定节点向左下方向的最后一个子节点
        /// </summary>
        /// <param name="parentIndex"></param>
        /// <returns></returns>
        private int GetLastLeftChildIndex(int parentIndex)
        {
            // 从指定节点作为起点
            int nextLeftChildIndex = parentIndex;

            //循环找下一个左子节点直到超出列表
            while (nextLeftChildIndex < nodes.Count)
            {
                nextLeftChildIndex = GetLeftChildIndex(nextLeftChildIndex);
            }

            //返回这个索引的父节点索引，就是最远的左子节点
            return GetParentIndex(nextLeftChildIndex);
        }

        /// <summary>
        /// 以一个节点为起点，从上向下调整节点位置
        /// </summary>
        /// <param name="startIndex"></param>
        private void TopToBottom(int startIndex)
        {
            //记录正在调整的元素的索引
            int currentIndex = startIndex;

            //判断比较复杂，用死循环 + 跳出
            while (true)
            {
                //获取比较小的那个子节点的索引
                int smallerChildIndex = FindSmallerChind(currentIndex);

                //如果有子节点，并且比较小的子节点比当前节点更应该接近堆顶
                if (smallerChildIndex > 0 && Comparison(nodes[smallerChildIndex], nodes[currentIndex]) < 0)
                {
                    //交换当前节点和比较小的子节点
                    nodes.Swap(currentIndex, smallerChildIndex);

                    //将正在调整的节点设为比较小的子节点的索引
                    currentIndex = smallerChildIndex;
                }
                else
                {
                    //没有子节点或者较小的子节点比当前节点更应该接近堆底，则说明调整完成，跳出循环
                    break;
                }
            }
        }

        /// <summary>
        /// 以指定节点为起点，从下向上调整节点位置
        /// </summary>
        /// <param name="startIndex"></param>
        private void BottomToTop(int startIndex)
        {
            //正在进行调整的元素的索引
            int currentIndex = startIndex;

            //现在正在调整的元素不是根元素，并且比父节更应该接近堆顶
            while (currentIndex != 0 && Comparison(nodes[currentIndex], nodes[GetParentIndex(currentIndex)]) < 0)
            {
                //先保存父节点的索引
                int parentIndex = GetParentIndex(currentIndex);

                //交换两个节点
                nodes.Swap(currentIndex, parentIndex);

                //将检测元素索引改为父节点索引
                currentIndex = parentIndex;
            }
        }

        /// <summary>
        /// 查找子节点中更应该接近堆顶的子节点的索引
        /// </summary>
        /// <param name="parentIndex"></param>
        /// <returns></returns>
        private int FindSmallerChind(int parentIndex)
        {
            //二叉堆是完全二叉树，如果没有左子节点就不会有右子节点，找不到左子节点的时候返回负数表示不存在
            if (!HaveLeftChildNode(parentIndex))
            {
                return -1;
            }

            // 没有右子节点，则做子节点就是更应该接近堆顶的那个子节点
            if (!HaveRightChildNode(parentIndex))
            {
                return GetLeftChildIndex(parentIndex);
            }

            // 获取左右子节点的索引
            int leftChildIndex = GetLeftChildIndex(parentIndex);
            int rightChildIndex = GetRightChildIndex(parentIndex);

            // 返回更应该接近堆顶的那个，一样的话返回哪个都行
            return Comparison(nodes[leftChildIndex], nodes[rightChildIndex]) < 0 ? leftChildIndex : rightChildIndex;
        }

        /// <summary>
        /// 判断一个节点是否有左子节点
        /// </summary>
        /// <param name="parentIndex"></param>
        /// <returns></returns>
        private bool HaveLeftChildNode(int parentIndex)
        {
            return GetLeftChildIndex(parentIndex) < nodes.Count;   //如果左子节点的索引小于节点List里的元素总数，则说明存在左子节点
        }

        /// <summary>
        /// 判断一个节点是否有右子节点
        /// </summary>
        /// <param name="parentIndex"></param>
        /// <returns></returns>
        private bool HaveRightChildNode(int parentIndex)
        {
            return GetRightChildIndex(parentIndex) < nodes.Count;
        }

        /// <summary>
        /// 获取父节点索引
        /// </summary>
        /// <param name="currentIndex"></param>
        /// <returns></returns>
        private int GetParentIndex(int currentIndex)
        {
            return (currentIndex - 1) / 2;
        }

        /// <summary>
        /// 获取左子节点索引
        /// </summary>
        /// <param name="parentIndex"></param>
        /// <returns></returns>
        private int GetLeftChildIndex(int parentIndex)
        {
            return parentIndex * 2 + 1;
        }

        /// <summary>
        /// 获取右子节点索引
        /// </summary>
        /// <param name="parentIndex"></param>
        /// <returns></returns>
        private int GetRightChildIndex(int parentIndex)
        {
            return parentIndex * 2 + 2;
        }

        /// <summary>
        /// 这个二叉堆是不是空的
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return Count == 0;
            }
        }

        /// <summary>
        /// 这个二叉堆的总结点数量
        /// </summary>
        public int Count
        {
            get
            {
                return nodes.Count;
            }
        }
    }

    //partical：加了这个关键字的类可以分开写在多个地方，比如这里给List增加Swap扩展方法，因为有partical所以能在别的.cs文件里添加其他方法而他们又共同属于同一个ListExtension类
    public static partial class ListExtension
    {
        /// <summary>
        /// 交换两个元素的位置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="aIndex"></param>
        /// <param name="bIndex"></param>
        public static void Swap<T>(this List<T> list, int aIndex, int bIndex)
        {
            if (aIndex < 0 || aIndex >= list.Count || bIndex < 0 || bIndex >= list.Count) return;

            T temporary = list[aIndex];
            list[aIndex] = list[bIndex];
            list[bIndex] = temporary;
        }
    }
}
