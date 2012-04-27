Portal
======

Portal MVC is a ASP.NET MVC Razor view helper that allows you to send blocks of HTML or JavaScript from a view or partial view to the layout view. 

This is helpful when you want to include various bits of JavaScript, CSS, or HTML from views or partial views into parent views or the layout view.

With Portal, from a view or partial view, you add something into the portal using @Html.Portal*In*XXX, where XXX is one of the available methods. You then call the matching method @Html.Portal*Out*XXX.

The table below lists the methods available and a description of what they do.

<table>
    <thead>
        <tr>
            <th>In</th>
            <th>Out</th>
            <th>Description</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>@Html.PortalIn(string text)</td>
            <td>@Html.PortalOut()</td>
            <td>Send any text (HTML, JavaScript, CSS, etc) through the default portal.</td>
        </tr>
        <tr>
            <td>@Html.PortalIn(string key, string text)</td>
            <td>@Html.PortalOut(string key)</td>
            <td>Send any text (HTML, JavaScript, CSS, etc) through a custom portal identified by a key. The *out* portal must use the same key.</td>
        </tr>
    </tbody>
</table>