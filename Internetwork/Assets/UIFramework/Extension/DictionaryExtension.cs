using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 对Dictory的扩展
/// </summary>
public static class DictionaryExtension  {

    /// <summary>
    /// 尝试根据key得到value，得到了的话直接返回value，没有得到直接返回null
    /// this Dictionary<Tkey,Tvalue> dict 这个字典表示我们要获取值的字典
    /// </summary>
    public static Tvalue TryGet<Tkey, Tvalue>(this Dictionary<Tkey, Tvalue> dict, Tkey key)
    {
        Tvalue value;
        dict.TryGetValue(key, out value);
        return value;
    }
    /// <summary>
    /// 尝试根据 key 得到 value，得到了直接返回 value，没有得到返回 null 或用户指定的默认值。
    /// </summary>
    /// <typeparam name="TKey">字典的键类型</typeparam>
    /// <typeparam name="TValue">字典的值类型</typeparam>
    /// <param name="dict">需要扩展的字典</param>
    /// <param name="key">要查找的键</param>
    /// <param name="defaultValue">键不存在时的默认返回值，默认为 default(TValue)</param>
    /// <returns>键对应的值，或默认值</returns>
    public static Tvalue TryGet<Tkey, Tvalue>(this Dictionary<Tkey, Tvalue> dict, Tkey key, Tvalue defaultValue)
    {
       return dict.TryGetValue(key, out var value)?value : defaultValue;
    }

}
