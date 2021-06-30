using System.Collections.Generic;
using System.Linq;
using System;

namespace MtC.Tools.BinaryHeap
{
    /// <summary>
    /// 二叉堆基类，需要注意这个基类不能根据存入的对象变化自动更新，需要手动调用更新方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BinaryHeap<T>
    {
        /// <summary>
        /// 节点对象
        /// </summary>
        protected class BinaryHeapNode
        {
            /// <summary>
            /// 这个节点对应的对象
            /// </summary>
            public T obj;

            /// <summary>
            /// 排序值，这个值越小越接近堆顶
            /// </summary>
            public float sort;

            /// <summary>
            /// 构造
            /// </summary>
            /// <param name="obj"></param>
            public BinaryHeapNode(T obj)
            {
                this.obj = obj;
            }

            /// <summary>
            /// 检测当前节点是否比指定节点更远离堆顶
            /// </summary>
            /// <param name="node"></param>
            /// <returns></returns>
            public bool LowerThan(BinaryHeapNode node)
            {
                return sort > node.sort;
            }

            /// <summary>
            /// 检测当前节点是否比指定节点更接近堆顶
            /// </summary>
            /// <param name="node"></param>
            /// <returns></returns>
            public bool HigherThan(BinaryHeapNode node)
            {
                return sort < node.sort;
            }
        }

        /// <summary>
        /// 所有节点
        /// </summary>
        protected List<BinaryHeapNode> nodes = new List<BinaryHeapNode>();

        /// <summary>
        /// 计算一个对象的排序值，标准是：越接近堆顶的对象排序值越低
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected abstract float CalculateSort(T obj);

        /// <summary>
        /// 添加对象到堆中
        /// </summary>
        /// <param name="obj"></param>
        public void Add(T obj)
        {
            // 创建节点
            BinaryHeapNode node = new BinaryHeapNode(obj);

            // 设置节点的排序值
            node.sort = CalculateSort(node.obj);

            // 加入到节点列表最后面，四叉树的结构导致越靠后的节点在越深的层，这样可以保证存到最底层
            nodes.Add(node);

            // 以新节点为起点，自下向上调整结构
            BottomToTop(nodes.Count - 1);
        }

        /// <summary>
        /// 获取堆顶对象
        /// </summary>
        /// <returns></returns>
        public T GetTop()
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
        /// 移除第一个指定的对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>如果删除成功则返回 true，否则返回 false</returns>
        public bool Remove(T obj)
        {
            // 找到第一个是指定对象的节点
            int removeIndex = nodes.FindIndex(node => Equals(node.obj, obj));

            // 没找到，返回删除失败
            if(removeIndex < 0)
            {
                return false;
            }

            // 删除掉并返回删除成功
            RemoveAt(removeIndex);
            return true;
        }

        /// <summary>
        /// 移除所有符合条件的对象
        /// </summary>
        /// <param name="predicate"></param>
        public void RemoveAll(Predicate<T> predicate)
        {
            // 找出所有需要删除的节点
            List<BinaryHeapNode> needRemoveNodes = nodes.Where(node => predicate(node.obj)).ToList();

            // 使用根据索引删除的方式依次删除这些节点
            needRemoveNodes.ForEach(node =>
            {
                RemoveAt(nodes.IndexOf(node));
            });

            // 这里使用根据节点删除而不是根据对象删除是因为相同的对象可以多次存入堆占用多个节点
            // 虽然一个节点只能删除一次，根据对象删多次实际上不会导致漏删
            // 但这会产生一个 “疑似有的节点删多次有的节点没删掉” 的理解困难
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
                if (smallerChildIndex > 0 && nodes[smallerChildIndex].HigherThan(nodes[currentIndex]))
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
            while (currentIndex != 0 && nodes[currentIndex].HigherThan(nodes[GetParentIndex(currentIndex)]))
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
            return nodes[leftChildIndex].HigherThan(nodes[rightChildIndex]) ? leftChildIndex : rightChildIndex;
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
        /// 检测堆里是否有指定的对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Contains(T obj)
        {
            return Any(nodeObj => Equals(nodeObj, obj));
        }

        /// <summary>
        /// 检测堆里是否有至少一个对象符合标准
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public bool Any(Predicate<T> predicate)
        {
            return nodes.Any(node => predicate(node.obj));
        }

        /// <summary>
        /// 检测堆里是否所有对象都符合标准
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public bool All(Predicate<T> predicate)
        {
            return nodes.All(node => predicate(node.obj));
        }

        /// <summary>
        /// 获取对象列表，需要注意由于二叉堆的特性，只有第一个对象是最符合标准的，后续对象是乱序
        /// </summary>
        /// <returns></returns>
        public List<T> GetList()
        {
            return nodes.Select(node => node.obj).ToList();
        }

        /// <summary>
        /// 更新堆中所有对象的排序
        /// </summary>
        public void UpdateAll()
        {
            Update(obj => true);
        }

        /// <summary>
        /// 更新堆中所有符合标准的对象的排序，不提供根据对象更新第一个的功能，假设一个对象存入了两次，只更新一个比全都不更新错的更严重
        /// </summary>
        /// <param name="predicate"></param>
        public void Update(Predicate<T> predicate)
        {
            // 筛选出需要更新的节点
            List<BinaryHeapNode> needUpdateNodes = nodes.Where(node => predicate(node.obj)).ToList();

            // 以按照索引的方式依次更新这些节点
            needUpdateNodes.ForEach(node =>
            {
                UpdateAt(nodes.IndexOf(node));
            });

            // 更新可能导致节点顺序调整，这种情况下不能使用顺序更新或根据对象更新，只能使用根据节点更新以保证每个需要更新的节点都会更新到
        }

        /// <summary>
        /// 更新指定的索引的节点
        /// </summary>
        /// <param name="updateNodeIndex"></param>
        private void UpdateAt(int updateNodeIndex)
        {
            // 获取到节点
            BinaryHeapNode node = nodes[updateNodeIndex];

            // 计算出这个节点现在应该有的排序值
            float currentSort = CalculateSort(node.obj);

            // 排序值比记录的值小，需要向上更新
            if(currentSort < node.sort)
            {
                BottomToTop(updateNodeIndex);
            }

            // 排序值比记录的值大，需要向下更新
            if(currentSort > node.sort)
            {
                TopToBottom(updateNodeIndex);
            }
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
