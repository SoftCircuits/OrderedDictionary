// Copyright (c) 2020 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftCircuits.Collections;
using System.Collections.Generic;
using System.Linq;

namespace OrderedDictionary.Tests
{
    [TestClass]
    public class Tests
    {
        private readonly List<(string, string)> TestData;

        public Tests()
        {
            // Initialize test data
            TestData = new List<(string, string)>();
            for (char c = 'a'; c <= ('z' - 3); c++)
            {
                TestData.Add((c.ToString(), new string(new char[]
                {
                    c,
                    (char)(c + 1),
                    (char)(c + 2),
                    (char)(c + 3),
                })));
            }
        }

        [TestMethod]
        public void TestAdd()
        {
            // Index initializers
            OrderedDictionary<int, string> dictionary = new OrderedDictionary<int, string>
            {
                [101] = "101",
                [102] = "102",
                [103] = "103",
                [104] = "104",
                [105] = "105",
            };
            Assert.AreEqual(5, dictionary.Count);
            Assert.AreEqual("101", dictionary[101]);
            Assert.AreEqual("102", dictionary[102]);
            Assert.AreEqual("103", dictionary[103]);
            Assert.AreEqual("104", dictionary[104]);
            Assert.AreEqual("105", dictionary[105]);

            // Add
            dictionary.Add(201, "201");
            dictionary.Add(202, "202");
            dictionary.Add(203, "203");
            dictionary.Add(204, "204");
            dictionary.Add(205, "205");
            Assert.AreEqual(10, dictionary.Count);
            Assert.AreEqual("201", dictionary[201]);
            Assert.AreEqual("202", dictionary[202]);
            Assert.AreEqual("203", dictionary[203]);
            Assert.AreEqual("204", dictionary[204]);
            Assert.AreEqual("205", dictionary[205]);

            // Add KeyValuePair<>
            dictionary.Add(new KeyValuePair<int, string>(301, "301"));
            dictionary.Add(new KeyValuePair<int, string>(302, "302"));
            dictionary.Add(new KeyValuePair<int, string>(303, "303"));
            dictionary.Add(new KeyValuePair<int, string>(304, "304"));
            dictionary.Add(new KeyValuePair<int, string>(305, "305"));
            Assert.AreEqual(15, dictionary.Count);
            Assert.AreEqual("301", dictionary[301]);
            Assert.AreEqual("302", dictionary[302]);
            Assert.AreEqual("303", dictionary[303]);
            Assert.AreEqual("304", dictionary[304]);
            Assert.AreEqual("305", dictionary[305]);

            // Add range
            List<KeyValuePair<int, string>> temp = new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(401, "401"),
                new KeyValuePair<int, string>(402, "402"),
                new KeyValuePair<int, string>(403, "403"),
                new KeyValuePair<int, string>(404, "404"),
                new KeyValuePair<int, string>(405, "405"),
            };
            dictionary.AddRange(temp);
            Assert.AreEqual(20, dictionary.Count);
            Assert.AreEqual("401", dictionary[401]);
            Assert.AreEqual("402", dictionary[402]);
            Assert.AreEqual("403", dictionary[403]);
            Assert.AreEqual("404", dictionary[404]);
            Assert.AreEqual("405", dictionary[405]);

            // Add dictionary
            OrderedDictionary<int, string> temp2 = new OrderedDictionary<int, string>
            {
                [501] = "501",
                [502] = "502",
                [503] = "503",
                [504] = "504",
                [505] = "505",
            };
            dictionary.AddRange(temp2);
            Assert.AreEqual(25, dictionary.Count);
            Assert.AreEqual("501", dictionary[501]);
            Assert.AreEqual("502", dictionary[502]);
            Assert.AreEqual("503", dictionary[503]);
            Assert.AreEqual("504", dictionary[504]);
            Assert.AreEqual("505", dictionary[505]);

            Assert.IsTrue(dictionary.ContainsKey(101));
            Assert.IsTrue(dictionary.ContainsKey(102));
            Assert.IsTrue(dictionary.ContainsKey(103));
            Assert.IsTrue(dictionary.ContainsKey(104));
            Assert.IsTrue(dictionary.ContainsKey(105));
            Assert.IsTrue(dictionary.ContainsKey(201));
            Assert.IsTrue(dictionary.ContainsKey(202));
            Assert.IsTrue(dictionary.ContainsKey(203));
            Assert.IsTrue(dictionary.ContainsKey(204));
            Assert.IsTrue(dictionary.ContainsKey(205));
            Assert.IsTrue(dictionary.ContainsKey(301));
            Assert.IsTrue(dictionary.ContainsKey(302));
            Assert.IsTrue(dictionary.ContainsKey(303));
            Assert.IsTrue(dictionary.ContainsKey(304));
            Assert.IsTrue(dictionary.ContainsKey(305));
            Assert.IsTrue(dictionary.ContainsKey(401));
            Assert.IsTrue(dictionary.ContainsKey(402));
            Assert.IsTrue(dictionary.ContainsKey(403));
            Assert.IsTrue(dictionary.ContainsKey(404));
            Assert.IsTrue(dictionary.ContainsKey(405));
            Assert.IsTrue(dictionary.ContainsKey(501));
            Assert.IsTrue(dictionary.ContainsKey(502));
            Assert.IsTrue(dictionary.ContainsKey(503));
            Assert.IsTrue(dictionary.ContainsKey(504));
            Assert.IsTrue(dictionary.ContainsKey(101));
            Assert.IsTrue(dictionary.ContainsKey(102));
            Assert.IsTrue(dictionary.ContainsKey(103));
            Assert.IsTrue(dictionary.ContainsKey(104));
            Assert.IsTrue(dictionary.ContainsKey(105));

            Assert.IsFalse(dictionary.ContainsKey(1));
            Assert.IsFalse(dictionary.ContainsKey(10));
            Assert.IsFalse(dictionary.ContainsKey(100));
            Assert.IsFalse(dictionary.ContainsKey(500));
            Assert.IsFalse(dictionary.ContainsKey(1000));
        }

        [TestMethod]
        public void TestInsert()
        {
            var dictionary = TestData.ToOrderedDictionary(x => x.Item1, x => x.Item2);

            dictionary.Insert(0, "0", "00");
            dictionary.Insert(10, "10", "1010");
            dictionary.Insert(11, "11", "1111");
            dictionary.Insert(12, "12", "1212");
            dictionary.Insert(13, "13", "1313");

            Assert.AreEqual(TestData.Count + 5, dictionary.Count);

            Assert.AreEqual("00", dictionary["0"]);
            Assert.AreEqual("00", dictionary.ByIndex[0]);
            Assert.AreEqual("1010", dictionary["10"]);
            Assert.AreEqual("1010", dictionary.ByIndex[10]);
            Assert.AreEqual("1111", dictionary["11"]);
            Assert.AreEqual("1111", dictionary.ByIndex[11]);
            Assert.AreEqual("1212", dictionary["12"]);
            Assert.AreEqual("1212", dictionary.ByIndex[12]);
            Assert.AreEqual("1313", dictionary["13"]);
            Assert.AreEqual("1313", dictionary.ByIndex[13]);

            // Verify original items still intact
            foreach ((string key, string value) in TestData)
            {
                Assert.IsTrue(dictionary.TryGetValue(key, out string result));
                Assert.AreEqual(value, result);
            }
        }

        [TestMethod]
        public void TestAccess()
        {
            var dictionary = TestData.ToOrderedDictionary(x => x.Item1, x => x.Item2);

            Assert.AreEqual(TestData.Count, dictionary.Count);
            Assert.AreEqual(0, dictionary.IndexOf("a"));
            Assert.AreEqual(1, dictionary.IndexOf("b"));
            Assert.AreEqual(2, dictionary.IndexOf("c"));
            Assert.AreEqual(22, dictionary.IndexOf("w"));
            Assert.AreEqual(-1, dictionary.IndexOf("z"));

            Assert.AreEqual("abcd", dictionary["a"]);
            Assert.AreEqual("bcde", dictionary["b"]);
            Assert.AreEqual("cdef", dictionary["c"]);
            Assert.AreEqual("wxyz", dictionary["w"]);

            foreach ((string key, string value) in TestData)
            {
                Assert.AreEqual(value, dictionary[key]);
                Assert.IsTrue(dictionary.TryGetValue(key, out string result));
                Assert.AreEqual(value, result);
            }
            for (int i = 0; i < TestData.Count; i++)
                Assert.AreEqual(TestData[i].Item2, dictionary.ByIndex[i]);

            Assert.IsFalse(dictionary.TryGetValue("x", out string _));
            Assert.IsFalse(dictionary.TryGetValue("y", out string _));
            Assert.IsFalse(dictionary.TryGetValue("z", out string _));

            // Write test
            dictionary["a"] = "Test1";
            Assert.AreEqual("Test1", dictionary["a"]);
            dictionary.ByIndex[4] = "Test2";
            Assert.AreEqual("Test2", dictionary.ByIndex[4]);
        }

        [TestMethod]
        public void TestRemove()
        {
            var dictionary = TestData.ToOrderedDictionary(x => x.Item1, x => x.Item2);
            string result;

            // Remove
            Assert.IsTrue(dictionary.Remove("a"));
            Assert.IsTrue(dictionary.Remove("b"));
            Assert.IsTrue(dictionary.Remove("c"));
            Assert.IsFalse(dictionary.Remove("a"));
            Assert.IsFalse(dictionary.Remove("b"));
            Assert.IsFalse(dictionary.Remove("c"));
            Assert.AreEqual(TestData.Count - 3, dictionary.Count);

            List<string> deletedKeys = new List<string>
            {
                "a",
                "b",
                "c"
            };
            foreach ((string key, string value) in TestData)
            {
                if (deletedKeys.Contains(key))
                    Assert.IsFalse(dictionary.TryGetValue(key, out result));
                else
                {
                    Assert.IsTrue(dictionary.TryGetValue(key, out result));
                    Assert.AreEqual(value, result);
                }
            }

            // RemoveAt
            Assert.AreEqual("defg", dictionary.ByIndex[0]);
            dictionary.RemoveAt(0);   // "d"
            Assert.AreEqual("tuvw", dictionary.ByIndex[15]);
            dictionary.RemoveAt(15);   // "t"
            Assert.AreEqual("uvwx", dictionary.ByIndex[15]);
            dictionary.RemoveAt(15);   // "u"
            Assert.AreEqual(TestData.Count - 6, dictionary.Count);

            deletedKeys.AddRange(new[] { "d", "t", "u" });
            foreach ((string key, string value) in TestData)
            {
                if (deletedKeys.Contains(key))
                    Assert.IsFalse(dictionary.TryGetValue(key, out result));
                else
                {
                    Assert.IsTrue(dictionary.TryGetValue(key, out result));
                    Assert.AreEqual(value, result);
                }
            }

            // Clear
            dictionary.Clear();
            Assert.AreEqual(0, dictionary.Count);
        }

        [TestMethod]
        public void TestKeysAndValues()
        {
            var dictionary = TestData.ToOrderedDictionary(x => x.Item1, x => x.Item2);

            List<string> keys = dictionary.Keys.ToList();
            List<string> values = dictionary.Values.ToList();
            for (int i = 0; i < TestData.Count; i++)
            {
                Assert.AreEqual(TestData[i].Item1, keys[i]);
                Assert.AreEqual(TestData[i].Item2, values[i]);
            }
        }

        [TestMethod]
        public void TestEnumerator()
        {
            var dictionary = TestData.ToOrderedDictionary(x => x.Item1, x => x.Item2);

            int i = 0;
            foreach (var x in dictionary)
                Assert.AreEqual(TestData[i++].Item2, x);
        }
    }
}
