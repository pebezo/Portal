Portal
======

Portal MVC is a **ASP.NET MVC Razor** view helper that allows you to send blocks of HTML or JavaScript from a view or partial view to the layout view. 

This is helpful when you want to include various bits of JavaScript, CSS, or HTML from views or partial views into parent views or the layout view.

With Portal, from a view or partial view, you add something into the portal using @Html.PortalInXXX, where XXX is one of the available methods. You then call the matching method @Html.PortalOutXXX.

The table below lists the methods available and a description of what they do.

<table id="portal-doc">
    <thead>
        <tr>
            <th>In</th>
            <th>Out</th>
            <th>Description</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>@Html.PortalIn(text)</td>
            <td>@Html.PortalOut()</td>
            <td>Send any text (HTML, JavaScript, CSS, etc) through the default portal.</td>
        </tr>
        <tr>
            <td>@Html.PortalIn(key, text)</td>
            <td>@Html.PortalOut(key)</td>
            <td>Send any text (HTML, JavaScript, CSS, etc) through a custom portal identified by a key. The out portal must use the same key.</td>
        </tr>
        <tr>
            <td>@Html.PortalIn(key, template)</td>
            <td>@Html.PortalOut(key)</td>
            <td>Send an HTML template through a custom portal identified by a key. For example: 
                <br />
                @Html.PortalIn("somekey", @<text> $(function() { alert('Hi'); }); </text>)
                <br />
                The out portal must use the same key. 
            </td>
        </tr>
        <tr>
            <td>@Html.PortalInUnique(key, text)</td>
            <td>@Html.PortalOut(key)</td>
            <td>Same as PortalIn(key, text) except that if you try to add the same text twice only the first addition is actually added.</td>
        </tr>
        <tr>
            <td>@Html.PortalInCss(path)</td>
            <td>@Html.PortalOutCss()</td>
            <td>Registers a path to a CSS file, for example:
                <br />
                @Html.PortalInCss("~/content/some.css")
                <br />
                and then outputs a link tag. The output in our example would be: 
                <br />
                &lt;link href="/content/some.css" rel="stylesheet" type="text/css" /&gt;
				Duplicate paths are removed. In the layout file you should call PortalInCss for all the CSS files that may be added
             </td>
        </tr>
		<tr>
            <td>@Html.PortalInJs(path)</td>
            <td>@Html.PortalOutJs()</td>
            <td>Registers a path to a JS file, for example:
                <br />
                @Html.PortalInJs("~/scripts/my.js")
                <br />
                and then outputs a script tag. The output in our example would be: 
                <br />
                &lt;script src="/scripts/my.js" type="text/javascript" &gt;&lt;/script&gt;
				Duplicate paths are removed.
             </td>
        </tr>
    </tbody>
</table>