using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.WebPages;
using System.Web;

namespace Portal
{
  /// <summary>
  /// Allows views or partial views to send data to the layout view. You can send information through several
  /// portals identified through a common key or through the default portal.
  /// 
  /// Example 1: Send HTML from Index to Layout through the default portal
  /// 
  /// Index.cshtml ->   @Html.PortalIn("<div>From Index to Layout</div>")  
  /// Layout.cshtml ->  @Html.PortalOut()
  /// 
  /// Example 2: Send HTML from Index to Layout through a custom portal
  /// 
  /// Index.cshtml ->   @Html.PortalIn("mykey", "<div>From Index to Layout</div>")  
  /// Layout.cshtml ->  @Html.PortalOut("mykey")
  /// 
  /// Example 3: Send a JavaScript block from partial view to layout
  /// 
  /// Partial.csthml -> @Html.PortalIn("somekey", @<text> $(function() { alert('Hello from partial view!'); }); </text>)
  /// Layout.cshtml ->  @Html.PortalOut("somekey")
  /// 
  /// Example 4: Send a unique piece of HTML from a partial to the layout (i.e. if called from multiple views is only sent once to the layout)
  /// 
  /// Partial.csthml -> @Html.PortalInUnique("uniquekey", "Show only once!")
  /// Layout.cshtml ->  @Html.PortalOut("uniquekey")
  /// 
  /// Example 5: Register CSS in the HEAD section of the layout view from a partial view. In the layout we also register main.css
  /// to prevent duplicates if another view registers main.css as well.
  /// 
  /// Partial.cshtml -> @Html.PortalInCss("~/content/some.css")
  /// Layout.cshmlt ->  <head> ... @Html.PortalInCss("~/content/main.css") @Html.PortalOutCss() ...</head>
  /// 
  /// Example 6: Register JavaScript file in the HEAD section of the layout view from a (partial) view.
  /// 
  /// View.cshtml ->   @Html.PortalInJs("~/scripts/my.js")
  /// Layout.cshtml -> @Html.PortalOutJs()  
  /// 
  /// Example 7: Register a JavaScript block in a view or partial view and output that code in the layout, ideally before the closing of the body tag
  /// 
  /// View.cshtml ->   @Html.PortalInScript(@<script type="text/javascript"> $(function () { alert('Hello!'); }) </script>)
  /// -- or --
  /// View.cshtml ->   @Html.PortalInScriptUnique(@<script type="text/javascript"> $(function () { alert('Hello!'); }) </script>)
  /// Layout.cshtml -> @Html.PortalOutScript()
  /// 
  /// 
  /// Limitations:
  /// When Razor files are rendered the order is from sub partial views to views and then to layout. For this
  /// reason you cannot have an In portal in the layout and an Out portal in a partial view; nothing gets called
  /// in the layout view until all the sub-views are rendered.
  /// 
  /// Within a (partial)view you can have an In and Out portal as long as the In portal is placed before the 
  /// Out portal; see example #5.
  /// </summary>
  public static class PortalHelper
  {
    #region Common

    const string PORTAL_KEY = "__PORTAL_KEY";
    const string PORTAL_CSS_KEY = "__PORTAL_CSS_KEY";
    const string PORTAL_JS_KEY = "__PORTAL_JS_KEY";
    const string PORTAL_SCRIPT_KEY = "__PORTAL_SCRIPT_KEY";

    private static List<string> Get(HtmlHelper helper, string key)
    {      
      var dic = helper.ViewContext.HttpContext.Items[key] as Dictionary<string, List<string>>;
      if (dic == null)
      {
        dic = new Dictionary<string, List<string>>();      
        helper.ViewContext.HttpContext.Items[key] = dic;
      }
      List<string> list;
      if (!dic.TryGetValue(key, out list))
      {
        list = new List<string>();
        dic[key] = list;        
      }
      return list;
    }

    private static void Add(HtmlHelper helper, string key, string text)
    {
      Get(helper, key).Add(text);
    }

    private static void Add(HtmlHelper helper, string text)
    {
      Add(helper, PORTAL_KEY, text);
    }

    private static void AddUnique(HtmlHelper helper, string key, string text)
    {
      var list = Get(helper, key);
      if (!list.Contains(text)) list.Add(text);
    }

    private static void AddUnique(HtmlHelper helper, string text)
    {
      AddUnique(helper, PORTAL_KEY, text);
    }

    private static void AddUniqueTop(HtmlHelper helper, string key, string text)
    {
      var list = Get(helper, key);
      if (!list.Contains(text)) list.Insert(0, text);
    }

    #endregion

    #region Miscellaneous

    /// <summary>
    /// Remove all the portal entries added up to this point for the given portal key.
    /// </summary>
    public static void Clear(this HtmlHelper helper, string key)
    {
      Get(helper, key).Clear();
    }

    /// <summary>
    /// Remove all the portal entries added up to this point for the default portal key.
    /// </summary>
    public static void Clear(this HtmlHelper helper)
    {
      Clear(helper, PORTAL_KEY);
    }

    #endregion

    #region In

    /// <summary>
    /// Add a piece of text or HTML to a specific portal key.
    /// </summary>
    public static MvcHtmlString PortalIn(this HtmlHelper helper, string key, string text)
    {
      Add(helper, key, text);
      return null;
    }

    /// <summary>
    /// Add a piece of text or HTML to the default portal key.
    /// </summary>
    public static MvcHtmlString PortalIn(this HtmlHelper helper, string text)
    {
      Add(helper, text);
      return null;
    }

    /// <summary>
    /// Add an HTML Razor template, for example @&lt;div&gt;text&lt;/div&gt;, to a specified portal key.
    /// </summary>
    public static HelperResult PortalIn(this HtmlHelper helper, string key, Func<dynamic, HelperResult> template)
    {
      Add(helper, key, template(null).ToString());
      return null;
    }

    /// <summary>
    /// Add an HTML Razor template, for example @&lt;div&gt;text&lt;/div&gt;, to the default portal key.
    /// </summary>
    public static HelperResult PortalIn(this HtmlHelper helper, Func<dynamic, HelperResult> template)
    {
      Add(helper, template(null).ToString());
      return null;
    }
    
    #endregion

    #region In Unique

    /// <summary>
    /// Add a piece of text or HTML to a specific portal key while making sure there are no duplicate entries.
    /// </summary>
    public static MvcHtmlString PortalInUnique(this HtmlHelper helper, string key, string text)
    {
      AddUnique(helper, key, text);
      return null;
    }

    /// <summary>
    /// Add a piece of text or HTML to the default portal key while making sure there are no duplicate entries.
    /// </summary>
    public static MvcHtmlString PortalInUnique(this HtmlHelper helper, string text)
    {
      AddUnique(helper, text);
      return null;
    }

    /// <summary>
    /// Add an HTML Razor template, for example @&lt;div&gt;text&lt;/div&gt;, to a specified portal key while making sure there are no duplicate entries.
    /// </summary>
    public static HelperResult PortalInUnique(this HtmlHelper helper, string key, Func<dynamic, HelperResult> template)
    {
      AddUnique(helper, key, template(null).ToString());
      return null;
    }

    /// <summary>
    /// Add an HTML Razor template, for example @&lt;div&gt;text&lt;/div&gt;, to the default portal key while making sure there are no duplicate entries.
    /// </summary>
    public static HelperResult PortalInUnique(this HtmlHelper helper, Func<dynamic, HelperResult> template)
    {
      AddUnique(helper, template(null).ToString());
      return null;
    }

    #endregion

    #region In CSS

    /// <summary>
    /// Register a CSS file with a relative path, such as ~/css/my.css
    /// </summary>
    public static MvcHtmlString PortalInCss(this HtmlHelper helper, string path)
    {
      AddUniqueTop(helper, PORTAL_CSS_KEY, VirtualPathUtility.ToAbsolute(path));
      return null;
    }

    /// <summary>
    /// Register a CSS file with an absolute path, such as http://example.com/css/my.css
    /// </summary>
    public static MvcHtmlString PortalInCssAbsolute(this HtmlHelper helper, string path)
    {
      AddUniqueTop(helper, PORTAL_CSS_KEY, path);
      return null;
    }

    #endregion

    #region In JavaScript

    /// <summary>
    /// Register a JavaScript file with a relative path, such as ~/scripts/my.js
    /// </summary>
    public static MvcHtmlString PortalInJs(this HtmlHelper helper, string path)
    {
      AddUniqueTop(helper, PORTAL_JS_KEY, VirtualPathUtility.ToAbsolute(path));
      return null;
    }

    /// <summary>
    /// Register a JavaScript file with an absolute path, such as http://example.com/scripts/my.js
    /// </summary>
    public static MvcHtmlString PortalInJsAbsolute(this HtmlHelper helper, string path)
    {
      AddUniqueTop(helper, PORTAL_JS_KEY, path);
      return null;
    }

    /// <summary>
    /// Register a block of JavaScript code, including the script block, using a template 
    /// for example: @&lt;script type="text/javascript"&gt; ... code ... &lt;/script&gt;
    /// </summary>
    public static MvcHtmlString PortalInScript(this HtmlHelper helper, Func<dynamic, HelperResult> template)
    {
      Add(helper, PORTAL_SCRIPT_KEY, template(null).ToString());
      return null;
    }

    /// <summary>
    /// Register a block of JavaScript code, including the script block, 
    /// for example: @&lt;script type="text/javascript"&gt; ... code ... &lt;/script&gt;
    /// </summary>
    public static MvcHtmlString PortalInScript(this HtmlHelper helper, string text)
    {
      Add(helper, PORTAL_SCRIPT_KEY, text);
      return null;
    }

    /// <summary>
    /// Register a block of JavaScript code, including the script block, 
    /// for example: @&lt;script type="text/javascript"&gt; ... code ... &lt;/script&gt;
    /// while making sure there are no duplicate entries.
    /// </summary>
    public static MvcHtmlString PortalInScriptUnique(this HtmlHelper helper, Func<dynamic, HelperResult> template)
    {
      AddUnique(helper, PORTAL_SCRIPT_KEY, template(null).ToString());
      return null;
    }

    #endregion

    #region Out

    /// <summary>
    /// Output the text or HTML that was added from a view using PortalIn for a given portal key.
    /// </summary>
    public static MvcHtmlString PortalOut(this HtmlHelper helper, string key)
    {
      using (var writer = new HtmlTextWriter(helper.ViewContext.Writer))
      {
        var list = Get(helper, key);
        foreach (var item in list)
        {
          writer.Write(item);
        }
      }
      return null;
    }

    /// <summary>
    /// Output the text or HTML that was added from a view using PortalIn for the default portal key.
    /// </summary>
    public static MvcHtmlString PortalOut(this HtmlHelper helper)
    {
      return PortalOut(helper, PORTAL_KEY);
    }

    /// <summary>
    /// Output CSS &lt;link ... /&gt; registrations that were added using PortalInCss 
    /// </summary>
    public static MvcHtmlString PortalOutCss(this HtmlHelper helper)
    {
      using (var writer = new HtmlTextWriter(helper.ViewContext.Writer))
      {
        var list = Get(helper, PORTAL_CSS_KEY);
        foreach (var item in list)
        {          
          writer.Write("<link href=\"");
          writer.Write(item);
          writer.Write("\" rel=\"stylesheet\" type=\"text/css\"/>");
        }
      }
      return null;
    }

    /// <summary>
    /// Output JavaScript &lt;script src="..." type="text/javascript" /&gt; registrations that were added using PortalInJs
    /// </summary>
    public static MvcHtmlString PortalOutJs(this HtmlHelper helper)
    {
      using (var writer = new HtmlTextWriter(helper.ViewContext.Writer))
      {
        var list = Get(helper, PORTAL_JS_KEY);
        foreach (var item in list)
        {
          writer.Write("<script src=\"");
          writer.Write(item);
          writer.Write("\" type=\"text/javascript\"></script>");
        }
      }
      return null;
    }

    /// <summary>
    /// Output JavaScript blocks that were added using PortalInScript
    /// </summary>
    public static MvcHtmlString PortalOutScript(this HtmlHelper helper)
    {
      using (var writer = new HtmlTextWriter(helper.ViewContext.Writer))
      {
        var list = Get(helper, PORTAL_SCRIPT_KEY);
        foreach (var item in list)
        {
          writer.Write(item);
        }
      }
      return null;
    }        

    #endregion
  }
}