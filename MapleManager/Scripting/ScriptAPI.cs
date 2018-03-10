﻿using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MapleManager.WzTools.FileSystem;
using MapleManager.WzTools.Objects;
using Int8 = System.SByte;
using UInt8 = System.Byte;

namespace MapleManager
{
    public abstract class ScriptAPI : IEnumerable<ScriptNode>
    {
        public abstract ScriptNode GetNode(string path);

        public Int8 GetInt8(string path) => GetNode(path).ToInt8();
        public Int16 GetInt16(string path) => GetNode(path).ToInt16();
        public Int32 GetInt32(string path) => GetNode(path).ToInt32();
        public Int64 GetInt64(string path) => GetNode(path).ToInt64();

        public UInt8 GetUInt8(string path) => GetNode(path).ToUInt8();
        public UInt16 GetUInt16(string path) => GetNode(path).ToUInt16();
        public UInt32 GetUInt32(string path) => GetNode(path).ToUInt32();
        public UInt64 GetUInt64(string path) => GetNode(path).ToUInt64();

        public string GetString(string path) => GetNode(path).ToString();

        public abstract IEnumerator<ScriptNode> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class ScriptNode : ScriptAPI
    {
        private object _obj;
        public string Name { get; private set; }
        private ScriptNode _parent;

        public ScriptNode(object obj, ScriptNode parent)
        {
            _obj = obj;
            _parent = parent;
        }

        private ScriptNode LoadTreeNodeInfo(TreeNode node, string key)
        {
            var tnValue = node.Tag;
            object obj = null;
            switch (tnValue)
            {
                case null:
                    obj = node;
                    break;
                case FSFile file:
                    obj = file.Object;
                    break;
                case FSDirectory dir:
                    obj = node;
                    break;
            }

            return new ScriptNode(obj, this) { Name = key };

        }

        public ScriptNode this[string key]
        {
            get
            {
                switch (_obj)
                {
                    case PcomObject po:
                        var x = po.Get(key);
                        if (x == null) return null;
                        return new ScriptNode(x, this) { Name = key };
                    case TreeNode tn:
                        if (tn.Nodes.ContainsKey(key))
                        {
                            return LoadTreeNodeInfo(tn.Nodes[key], key);
                        }
                        return null;
                    case TreeView tv:
                        if (tv.Nodes.ContainsKey(key))
                        {
                            return LoadTreeNodeInfo(tv.Nodes[key], key);
                        }
                        return null;
                }

                throw new Exception("??? Don't know how to handle this type: " + _obj);
            }
        }

        public override ScriptNode GetNode(string path)
        {
            var nodes = path.Split('/');

            ScriptNode ret = this;
            foreach (var node in nodes)
            {
                if (node == "..") ret = ret._parent;
                else if (node == ".") continue;
                else ret = ret[node];
            }

            return ret;
        }

        public Int8 ToInt8()
        {
            switch (_obj)
            {
                case Int8 __sb: return (Int8)__sb;
                case Int16 _ss: return (Int8)_ss;
                case Int32 _si: return (Int8)_si;
                case Int64 _sl: return (Int8)_sl;
                case UInt8 _ub: return (Int8)_ub;
                case UInt16 us: return (Int8)us;
                case UInt32 ui: return (Int8)ui;
                case UInt64 ul: return (Int8)ul;
                case String __s: return Int8.Parse(__s);
            }

            throw new Exception($"Not sure how to convert '{_obj}' into an Int8");
        }

        public Int16 ToInt16()
        {
            switch (_obj)
            {
                case Int8 __sb: return (Int16)__sb;
                case Int16 _ss: return (Int16)_ss;
                case Int32 _si: return (Int16)_si;
                case Int64 _sl: return (Int16)_sl;
                case UInt8 _ub: return (Int16)_ub;
                case UInt16 us: return (Int16)us;
                case UInt32 ui: return (Int16)ui;
                case UInt64 ul: return (Int16)ul;
                case String __s: return Int16.Parse(__s);
            }

            throw new Exception($"Not sure how to convert '{_obj}' into an Int16");
        }

        public Int32 ToInt32()
        {
            switch (_obj)
            {
                case Int8 __sb: return (Int32)__sb;
                case Int16 _ss: return (Int32)_ss;
                case Int32 _si: return (Int32)_si;
                case Int64 _sl: return (Int32)_sl;
                case UInt8 _ub: return (Int32)_ub;
                case UInt16 us: return (Int32)us;
                case UInt32 ui: return (Int32)ui;
                case UInt64 ul: return (Int32)ul;
                case String __s: return Int32.Parse(__s);
            }

            throw new Exception($"Not sure how to convert '{_obj}' into an Int32");
        }

        public Int64 ToInt64()
        {
            switch (_obj)
            {
                case Int8 __sb: return (Int64)__sb;
                case Int16 _ss: return (Int64)_ss;
                case Int32 _si: return (Int64)_si;
                case Int64 _sl: return (Int64)_sl;
                case UInt8 _ub: return (Int64)_ub;
                case UInt16 us: return (Int64)us;
                case UInt32 ui: return (Int64)ui;
                case UInt64 ul: return (Int64)ul;
                case String __s: return Int64.Parse(__s);
            }

            throw new Exception($"Not sure how to convert '{_obj}' into an Int64");
        }

        public UInt8 ToUInt8()
        {
            switch (_obj)
            {
                case Int8 __sb: return (UInt8)__sb;
                case Int16 _ss: return (UInt8)_ss;
                case Int32 _si: return (UInt8)_si;
                case Int64 _sl: return (UInt8)_sl;
                case UInt8 _ub: return (UInt8)_ub;
                case UInt16 us: return (UInt8)us;
                case UInt32 ui: return (UInt8)ui;
                case UInt64 ul: return (UInt8)ul;
                case String __s: return UInt8.Parse(__s);
            }

            throw new Exception($"Not sure how to convert '{_obj}' into an UInt8");
        }

        public UInt16 ToUInt16()
        {
            switch (_obj)
            {
                case Int8 __sb: return (UInt16)__sb;
                case Int16 _ss: return (UInt16)_ss;
                case Int32 _si: return (UInt16)_si;
                case Int64 _sl: return (UInt16)_sl;
                case UInt8 _ub: return (UInt16)_ub;
                case UInt16 us: return (UInt16)us;
                case UInt32 ui: return (UInt16)ui;
                case UInt64 ul: return (UInt16)ul;
                case String __s: return UInt16.Parse(__s);
            }

            throw new Exception($"Not sure how to convert '{_obj}' into an UInt16");
        }

        public UInt32 ToUInt32()
        {
            switch (_obj)
            {
                case Int8 __sb: return (UInt32)__sb;
                case Int16 _ss: return (UInt32)_ss;
                case Int32 _si: return (UInt32)_si;
                case Int64 _sl: return (UInt32)_sl;
                case UInt8 _ub: return (UInt32)_ub;
                case UInt16 us: return (UInt32)us;
                case UInt32 ui: return (UInt32)ui;
                case UInt64 ul: return (UInt32)ul;
                case String __s: return UInt32.Parse(__s);
            }

            throw new Exception($"Not sure how to convert '{_obj}' into an UInt32");
        }

        public UInt64 ToUInt64()
        {
            switch (_obj)
            {
                case Int8 __sb: return (UInt64)__sb;
                case Int16 _ss: return (UInt64)_ss;
                case Int32 _si: return (UInt64)_si;
                case Int64 _sl: return (UInt64)_sl;
                case UInt8 _ub: return (UInt64)_ub;
                case UInt16 us: return (UInt64)us;
                case UInt32 ui: return (UInt64)ui;
                case UInt64 ul: return (UInt64)ul;
                case String __s: return UInt64.Parse(__s);
            }

            throw new Exception($"Not sure how to convert '{_obj}' into an UInt64");
        }

        public override string ToString()
        {
            return _obj.ToString();
        }

        private IEnumerable<ScriptNode> _enumerable = null;

        public bool HasMembers
        {
            get
            {
                if (_enumerable != null) return _enumerable.Any();
                else
                {
                    // Try to do it manually
                    switch (_obj)
                    {
                        case WzProperty prop: return prop.HasMembers;
                        case TreeNode tn: return tn.Nodes.Count > 0;
                        case TreeView tv: return tv.Nodes.Count > 0;
                    }
                    return false;
                }
            }
        }
        private IEnumerable<ScriptNode> Members => _enumerable ?? (_enumerable = _getEnumerable());

        private IEnumerable<ScriptNode> _getEnumerable()
        {
            switch (_obj)
            {
                case WzProperty prop:
                    return prop.Select(x => new ScriptNode(x.Value, this) { Name = x.Key });
                case TreeNode tn:
                    {
                        var nodes = new List<TreeNode>();
                        foreach (TreeNode v in tn.Nodes)
                        {
                            nodes.Add(v);
                        }
                        return nodes.Select(x => LoadTreeNodeInfo(x, x.Name));
                    }
                case TreeView tv:
                    {
                        var nodes = new List<TreeNode>();
                        foreach (TreeNode v in tv.Nodes)
                        {
                            nodes.Add(v);
                        }
                        return nodes.Select(x => LoadTreeNodeInfo(x, x.Name));
                    }
            }

            return Enumerable.Empty<ScriptNode>();
        }

        public override IEnumerator<ScriptNode> GetEnumerator() => Members.GetEnumerator();
    }
}