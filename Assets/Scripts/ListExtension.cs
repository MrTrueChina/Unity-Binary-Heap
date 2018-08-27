using System.Collections.Generic;

/// <summary>
/// List的扩展方法类
/// </summary>
public static class ListExtension
{
    /// <summary>
    /// 交换两个元素，传入交换元素的下标
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="indexA"></param>
    /// <param name="indexB"></param>
    public static void Swap<T>(this List<T> list, int indexA, int indexB)
    {
        T temporary = list[indexA];
        list[indexA] = list[indexB];
        list[indexB] = temporary;
    }

    /// <summary>
    /// 返回List最后一个元素
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    public static T Last<T>(this List<T> list)
    {
        return list[list.Count - 1];
    }
}

