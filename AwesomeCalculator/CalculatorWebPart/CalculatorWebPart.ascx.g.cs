﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AwesomeCalculator.CalculatorWebPart {
    using System.Web.UI.WebControls.Expressions;
    using System.Web.UI.HtmlControls;
    using System.Collections;
    using System.Text;
    using System.Web.UI;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using Microsoft.SharePoint.WebPartPages;
    using System.Web.SessionState;
    using System.Configuration;
    using Microsoft.SharePoint;
    using System.Web;
    using System.Web.DynamicData;
    using System.Web.Caching;
    using System.Web.Profile;
    using System.ComponentModel.DataAnnotations;
    using System.Web.UI.WebControls;
    using System.Web.Security;
    using System;
    using Microsoft.SharePoint.Utilities;
    using System.Text.RegularExpressions;
    using System.Collections.Specialized;
    using System.Web.UI.WebControls.WebParts;
    using Microsoft.SharePoint.WebControls;
    using System.CodeDom.Compiler;
    
    
    public partial class CalculatorWebPart {
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.Button btnTest;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebPartCodeGenerator", "12.0.0.0")]
        public static implicit operator global::System.Web.UI.TemplateControl(CalculatorWebPart target) 
        {
            return target == null ? null : target.TemplateControl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.Button @__BuildControlbtnTest() {
            global::System.Web.UI.WebControls.Button @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Button();
            this.btnTest = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "btnTest";
            @__ctrl.Text = "Run Test";
            @__ctrl.Click -= new System.EventHandler(this.btnTest_Click);
            @__ctrl.Click += new System.EventHandler(this.btnTest_Click);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private void @__BuildControlTree(global::AwesomeCalculator.CalculatorWebPart.CalculatorWebPart @__ctrl) {
            global::System.Web.UI.WebControls.Button @__ctrl1;
            @__ctrl1 = this.@__BuildControlbtnTest();
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(@__ctrl1);
            @__ctrl.SetRenderMethodDelegate(new System.Web.UI.RenderMethod(this.@__Render__control1));
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private void @__Render__control1(System.Web.UI.HtmlTextWriter @__w, System.Web.UI.Control parameterContainer) {
            @__w.Write("\r\n\r\n<link rel=\"stylesheet\" href=\"");
                     @__w.Write( SPContext.Current.Site.RootWeb.Url + "/SiteAssets/Css/awesome-calculator.css" );

            @__w.Write("\" />\r\n\r\n<div class=\"container\">\r\n    <div class=\"overflow\">\r\n        <div class=\"" +
                    "fl\">\r\n            <div class=\"field\">\r\n                <label for=\"number1\">Numb" +
                    "er 1:</label>\r\n                <input type=\"text\" id=\"number1\" placeholder=\"numb" +
                    "er 1\" />\r\n            </div>\r\n            <div class=\"field\">\r\n                <" +
                    "label for=\"number2\">Number 2:</label>\r\n                <input type=\"text\" id=\"nu" +
                    "mber2\" placeholder=\"number 2\" />\r\n            </div>\r\n        </div>\r\n        <d" +
                    "iv class=\"fl\">\r\n            <div class=\"field\">\r\n                <input type=\"ra" +
                    "dio\" name=\"operation\" id=\"sum\" value=\"sum\" checked=\"checked\" />\r\n               " +
                    " <label for=\"sum\" style=\"min-width: 50px;\">Addition</label>\r\n            </div>\r" +
                    "\n            <div class=\"field\">\r\n                <input type=\"radio\" name=\"oper" +
                    "ation\" id=\"sub\" value=\"sub\" />\r\n                <label for=\"sub\" style=\"min-widt" +
                    "h: 50px;\">Subtraction</label>\r\n            </div>\r\n        </div>\r\n    </div>\r\n " +
                    "   <div class=\"field\">\r\n        <a class=\"link-btn fl\" id=\"btnSaveItem\">OK</a>\r\n" +
                    "        <a class=\"link-btn fl\" id=\"btnTotal\">Total</a>\r\n        <span class=\"sma" +
                    "ll fl\" id=\"txtExRate\"></span>\r\n        <a class=\"small fl\" style=\"visibility:hid" +
                    "den\" id=\"quotesSource\">(source)</a>\r\n        <span class=\"fl\" id=\"txtTotal\"></sp" +
                    "an>\r\n    </div>\r\n    <div class=\"field\">\r\n        <table>\r\n            <tr>\r\n   " +
                    "             <th>Number 1</th>\r\n                <th>Number 2</th>\r\n             " +
                    "   <th>Result</th>\r\n            </tr>\r\n            <tr>\r\n                <td id=" +
                    "\"number1Result\"></td>\r\n                <td id=\"number2Result\"></td>\r\n           " +
                    "     <td id=\"result\"></td>\r\n            </tr>\r\n        </table>\r\n    </div>\r\n</d" +
                    "iv>\r\n<script type=\"text/javascript\" src=\"");
                            @__w.Write( SPContext.Current.Site.RootWeb.Url + "/SiteAssets/Scripts/jquery-3.3.1.min.js" );

            @__w.Write("\"></script>\r\n<script type=\"text/javascript\" src=\"");
                            @__w.Write( SPContext.Current.Site.RootWeb.Url + "/SiteAssets/Scripts/awesome-calculator.js" );

            @__w.Write("\"></script>\r\n\r\n");
            parameterContainer.Controls[0].RenderControl(@__w);
        }
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private void InitializeControl() {
            this.@__BuildControlTree(this);
            this.Load += new global::System.EventHandler(this.Page_Load);
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected virtual object Eval(string expression) {
            return global::System.Web.UI.DataBinder.Eval(this.Page.GetDataItem(), expression);
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected virtual string Eval(string expression, string format) {
            return global::System.Web.UI.DataBinder.Eval(this.Page.GetDataItem(), expression, format);
        }
    }
}
