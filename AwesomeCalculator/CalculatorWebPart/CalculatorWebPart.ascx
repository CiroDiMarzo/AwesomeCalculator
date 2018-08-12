<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CalculatorWebPart.ascx.cs" Inherits="AwesomeCalculator.CalculatorWebPart.CalculatorWebPart" %>

<label for="number1">Number 1:</label>
<input type="text" id="number1" placeholder="number 1" />
<label for="number2">Number 2:</label>
<input type="text" id="number2" placeholder="number 2" />

<button id="btnSaveItem" value="Save item" onclick="onSaveItemClick()">Save Item</button>
<button id="btnTotal" value="Total" onclick="onTotalClick()">Total</button>

<script type="text/javascript" src="<%= SPContext.Current.Site.RootWeb.Url + "/SiteAssets/Scripts/jquery-3.3.1.min.js" %>"></script>
<script type="text/javascript" src="<%= SPContext.Current.Site.RootWeb.Url + "/SiteAssets/Scripts/awesome-calculator.js" %>"></script>