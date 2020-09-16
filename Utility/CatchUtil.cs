using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Utility
{
    public class EltInfo
    {
        public string str;//显示值
        public string attr;//属性
        public string tag;//标签类型
        public int idx;//控件索引
       
        /// <summary>
        /// 查询模式 模糊查找 精确查找
        /// </summary>
        public string smode;//查询模式

        public void Clear()
        {
            str = "";
            attr = "";
            tag = "";
            idx = -1;
        }
    }
    public enum EltAttr : int
    {
        NO = -1,
        ID = 0,
        NAME = 1,
        OUT_TEXT = 2,
        CLASS_NAME = 3,
        OUT_HTML = 4,
        VALUE = 5,
        HREF = 6,
        SRC = 7,
        TITLE = 8
    }
    public class CatchUtil
    {
        public EltInfo getEltInfo(HtmlElementCollection dom, HtmlElement elt)
        {
            EltInfo efo = new EltInfo();
            EltAttr ea = getEltAttr(efo, elt);
            efo.idx = getEltIndex(dom, elt, ea);
            return efo;
        }
        /// <summary>
        /// 获取元素信息，包含元素索引
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="elt"></param>
        /// <returns></returns>
        public EltInfo getEltInfo(HtmlDocument doc, HtmlElement elt)
        {
            EltInfo efo = new EltInfo();
            EltAttr ea = getEltAttr(efo, elt);
            efo.idx = getEltIndex(doc, elt);
            return efo;
        }
        /// <summary>
        /// 获取元素索引
        /// </summary>
        /// <param name="doc">html doc</param>
        /// <param name="elt">元素</param>
        /// <returns></returns>
        public int getEltIndex(HtmlDocument doc, HtmlElement elt)
        {
            HtmlElementCollection es = doc.GetElementsByTagName(elt.TagName);
            int idx = 0;
            bool isfind = false;
            foreach (HtmlElement et in es)
            {
                idx++;
                if (et == elt)
                {
                    isfind = true;
                    break;
                }
            }
            return idx;
        }
        public int getEltIndex(HtmlElementCollection dom, HtmlElement elt, EltAttr eltAttr)
        {
            int len = dom.Count;
            HtmlElement d = null;
            int idx = 0;

            for (int i = 0; i < dom.Count; i++)
            {
                d = dom[i];
                if (d == elt)
                {
                    return idx;
                }
                else
                {
                    bool isok = false;
                    switch (eltAttr)
                    {
                        case EltAttr.ID:
                            if (d.Id == elt.Id)
                            {
                                idx++;
                                isok = true;
                            }
                            break;
                        case EltAttr.NAME:
                            if (d.Name == elt.Name)
                            {
                                idx++;
                                isok = true;
                            }
                            break;
                        case EltAttr.OUT_TEXT:
                            if (d.OuterText == elt.OuterText)
                            {
                                idx++;
                                isok = true;
                            }
                            break;
                        case EltAttr.CLASS_NAME:
                            if (d.GetAttribute("className") == elt.GetAttribute("className"))
                            {
                                idx++;
                                isok = true;
                            }
                            break;
                        case EltAttr.OUT_HTML:
                            if (d.OuterHtml == elt.OuterHtml)
                            {
                                idx++;
                                isok = true;
                            }
                            break;
                        case EltAttr.VALUE:
                            if (d.GetAttribute("value") == elt.GetAttribute("value"))
                            {
                                idx++;
                                isok = true;
                            }
                            break;
                        case EltAttr.HREF:
                            if (d.GetAttribute("href") == elt.GetAttribute("href"))
                            {
                                idx++;
                                isok = true;
                            }
                            break;
                        case EltAttr.TITLE:
                            if (d.GetAttribute("title") == elt.GetAttribute("title"))
                            {
                                idx++;
                                isok = true;
                            }
                            break;
                    }
                    if (!isok && d.Children.Count > 0)
                    {
                        idx += getEltIndex(d.Children, elt, eltAttr);
                    }
                }
            }
            return idx;
        }
        public EltAttr getEltAttr(EltInfo efo, HtmlElement elt)
        {
            efo.tag = elt.TagName;
            if (!string.IsNullOrEmpty(elt.Id))
            {
                efo.str = elt.Id;
                efo.attr = "ID";
                return EltAttr.ID;
            }
            if (!string.IsNullOrEmpty(elt.Name))
            {
                efo.str = elt.Name;
                efo.attr = "Name";
                return EltAttr.NAME;
            }
            if (!string.IsNullOrEmpty(elt.OuterText))
            {
                efo.str = elt.OuterText;
                efo.attr = "OuterText";
                return EltAttr.OUT_TEXT;
            }
            if (!string.IsNullOrEmpty(elt.GetAttribute("className")))
            {
                efo.str = elt.GetAttribute("className");
                efo.attr = "ClassName";
                return EltAttr.CLASS_NAME;
            }
            if (!string.IsNullOrEmpty(elt.OuterHtml))
            {
                efo.str = elt.OuterHtml;
                efo.attr = "OuterHtml";
                return EltAttr.OUT_HTML;
            }
            if (!string.IsNullOrEmpty(elt.GetAttribute("value")))
            {
                efo.str = elt.GetAttribute("value");
                efo.attr = "Value";
                return EltAttr.VALUE;
            }
            if (!string.IsNullOrEmpty(elt.GetAttribute("href")))
            {
                efo.str = elt.GetAttribute("href");
                efo.attr = "Href";
                return EltAttr.HREF;
            }
            if (!string.IsNullOrEmpty(elt.GetAttribute("src")))
            {
                efo.str = elt.GetAttribute("src");
                efo.attr = "Src";
                return EltAttr.SRC;
            }
            if (!string.IsNullOrEmpty(elt.GetAttribute("title")))
            {
                efo.str = elt.GetAttribute("title");
                efo.attr = "Title";
                return EltAttr.TITLE;
            }
            return EltAttr.NO;
        }

        public HtmlElement GetElementByEltInfo(HtmlDocument doc, EltInfo elt)
        {
            HtmlElement element = null;
            if (elt.attr.Equals("ID"))
            {
                element = doc.GetElementById(elt.str);
            }
            else
            {
                HtmlElementCollection es = doc.GetElementsByTagName(elt.tag);
                element = es[elt.idx - 1];
            }
            return element;
        }
    }
}
