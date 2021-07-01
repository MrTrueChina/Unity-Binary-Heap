using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

namespace MtC.Tools.BinaryHeap
{
    public class FloatMinBinaryHeap : BinaryHeap<float>
    {
        protected override float CalculateSort(float obj)
        {
            return obj;
        }

        /// <summary>
        /// 以字符串形式获取这个二叉堆的结构
        /// </summary>
        /// <returns></returns>
        public string Print()
        {
            /*
             * 1
             * 一个元素以内：高度1，宽度1，空格0
             * 
             *  1
             * 1 1
             * 三个元素以内，高2宽1，空格 1
             * 
             *    1
             *  1   1
             * 1 1 1 1
             * 7元素，高3，宽7，空格 3/1 3 1
             * 
             *        1
             *    1       1
             *  1   1   1   1
             * 1 1 1 1 1 1 1 1
             * 15元素，高4，宽15，空格 7/3 7 3/1 3 3 3 1
             * 
             * 宽 = 底层元素数 * 2 - 1
             * 元素数 = 高 * 高 - 1
             * 高 = 元素数 以2为底求对数 + 1
             */

            // 计算高度，高度 = 元素数 以2为底 的对数 + 1，向下取整
            int maxHeight = (int)Mathf.Log(Count, 2) + 1;

            // 最后一行宽度 = 2 的 (高度 - 1) 次方
            int lastLineWeight = (int)Mathf.Pow(2, maxHeight - 1);

            StringBuilder stringBuilder = new StringBuilder();

            for (int height = 1; height <= maxHeight; height++)
            {
                // 每一行第一个元素前面的空格 = 2的(总层数-当前层数)次方 - 1
                int toFirestElementLength = (int)Mathf.Pow(2, maxHeight - height) - 1;

                // 之后每个元素之间的空格 = 前面的空格 * 2 + 1 = 2的(总层数-当前层数 + 1)次方 - 1
                int elementToElementLength = (int)Mathf.Pow(2, maxHeight - height + 1) - 1;

                // 计算出这个层级的元素在列表中的索引范围
                int startIndex = (int)Mathf.Pow(2, height - 1) - 1;
                int endIndex = Mathf.Min(nodes.Count, (int)Mathf.Pow(2, height) - 1);

                // 添加第一个元素之前的空格
                for (int i = 0; i < toFirestElementLength; i++)
                {
                    stringBuilder.Append(" ");
                }

                for (int i = startIndex; i < endIndex; i++)
                {
                    // 添加元素
                    stringBuilder.Append(nodes[i].obj.ToString());
                    //stringBuilder.Append(i);

                    // 最后一个元素之前的需要添加元素之间的空格
                    if (i < endIndex - 1)
                    {
                        for (int j = 0; j < elementToElementLength; j++)
                        {
                            stringBuilder.Append(" ");
                        }
                    }
                }

                // 添加最后一个元素之后的空格
                for (int i = 0; i < toFirestElementLength; i++)
                {
                    stringBuilder.Append(" ");
                }

                // 换行
                stringBuilder.Append("\n");
            }

            return stringBuilder.ToString();
        }
    }
}
