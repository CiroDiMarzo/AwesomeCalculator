<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CalculatorWebPart.ascx.cs" Inherits="AwesomeCalculator.CalculatorWebPart.CalculatorWebPart" %>

<style>
    div.field {
        margin: 10px 0px 20px 0px;
    }
    div.field button {
        margin-right:10px;
    }
    div.field label {
        margin-right:20px
    }
    div.field input {
        margin-right: 10px;
    }
    div.field a {
        cursor: pointer;
        background-color: white;
        padding: 7px;
        font-size: smaller;
        border: 1px solid darkgrey;
    }
</style>

<div class="field">
    <label for="number1">Number 1:</label>
    <input type="text" id="number1" placeholder="number 1" />
</div>
<div class="field">
    <label for="number2">Number 2:</label>
    <input type="text" id="number2" placeholder="number 2" />
</div>
<div class="field">
    <label for="sum" style="min-width: 50px;">Addition</label>
    <input type="radio" name="operation" id="sum" value="sum" checked="checked" />
    <label for="sub" style="min-width: 50px;">Subtraction</label>
    <input type="radio" name="operation" id="sub" value="sub" />
</div>
<div class="field">
    <a id="btnSaveItem" onclick="save()">Save Item</a>
    <a id="btnTotal" onclick="readTotal()">Total</a>
</div>
<div class="field">
    <table>
        <tr>
            <th>Number 1</th>
            <th>Number 2</th>
            <th>Result</th>
        </tr>
        <tr>
            <td id="number1Result"></td>
            <td id="number2Result"></td>
            <td id="result"></td>
        </tr>
    </table>
</div>
<script type="text/javascript" src="<%= SPContext.Current.Site.RootWeb.Url + "/SiteAssets/Scripts/jquery-3.3.1.min.js" %>"></script>
<script type="text/javascript" src="<%= SPContext.Current.Site.RootWeb.Url + "/SiteAssets/Scripts/awesome-calculator.js" %>"></script>

<asp:Button runat="server" ID="btnTest" OnClick="btnTest_Click" Text="Run Test" />