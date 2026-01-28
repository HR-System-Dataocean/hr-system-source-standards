using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.Design.WebControls;
using System.ComponentModel.Design;
using System.Web.UI.Design;
using System.ComponentModel;
using System.Web.UI;

namespace OfficeWebUI.Ribbon
{

    internal class OfficeRibbon_Designer : CompositeControlDesigner
    {
        private DesignerActionListCollection _actionLists = null;
        private OfficeRibbon _Control;
        private List<RibbonTab> _ListTabs = new List<RibbonTab>();

        public Int32 CurrentTab = 0;
        public override bool AllowResize { get { return false; } }

        public string GetDesignTimeHtml0(DesignerRegionCollection regions)
        {
            return "<div style='height:60px; padding:5px; border:1px solid #C0C0C0; font-family:tahoma; font-size:8pt;'><b>OfficeWebUI:Ribbon</b> [" + _Control.ID + "]</div>";
        }

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            _Control = (OfficeRibbon)component;

            foreach (RibbonContext lContext in _Control.Contexts)
            {
                foreach (RibbonTab lTab in lContext.Tabs)
                {
                    _ListTabs.Add(lTab);
                }
            }
        }


        protected override void OnClick(DesignerRegionMouseEventArgs e)
        {
            if (e.Region == null) return;

            String lStr = e.Region.Name.Split('_')[1];
            int regionIndex = Int32.Parse(lStr);
            CurrentTab = regionIndex;
            UpdateDesignTimeHtml();
        }

        public override String GetDesignTimeHtml(DesignerRegionCollection regions)
        {
            BuildRegions(regions);
            return BuildDesignTimeHtml();
        }

        protected virtual void BuildRegions(DesignerRegionCollection regions)
        {
            for (int i = 0; i < _ListTabs.Count; i++)
            {
                regions.Add(new DesignerRegion(this, "Tab_" + i.ToString()));
            }
            regions[CurrentTab].Highlight = true;
        }

        protected virtual string BuildDesignTimeHtml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(BuildBeginDesignTimeHtml());
            sb.Append(BuildContentDesignTimeHtml());
            return sb.ToString();
        }

        protected virtual String BuildBeginDesignTimeHtml()
        {
            // Create the table layout
            StringBuilder sb = new StringBuilder();
            sb.Append("<div style=\"width:100%\">");
            int i = 0;
            foreach (RibbonTab lTab in _ListTabs)
            {
                if (_Control.ApplicationMenuDirection == DocumentDirection.LTR) sb.Append("<div style=\"float:left; height:20px; padding:3px; font-size:8pt; font-family:tahoma;\" " + DesignerRegion.DesignerRegionAttributeName + "='" + i.ToString() + "'>" + _ListTabs[i].Text + "</div>");
                else sb.Append("<div style=\"float:right; height:20px; padding:3px; font-size:8pt; font-family:tahoma;\" " + DesignerRegion.DesignerRegionAttributeName + "='" + i.ToString() + "'>" + _ListTabs[i].Text + "</div>");
                i++;
            }
            sb.Append("<div style=\"clear:both\"></div></div>");
            return sb.ToString();
        }

        protected virtual String BuildEndDesignTimeHtml()
        {
            return ("</table>");
        }

        protected virtual String BuildContentDesignTimeHtml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div style=\"border:1px solid #C0C0C0\">");
            foreach (RibbonGroup lGroup in _ListTabs[CurrentTab].Groups)
            {
                if (_Control.ApplicationMenuDirection == DocumentDirection.LTR) sb.Append("<div style=\"float:left;\">");
                else sb.Append("<div style=\"float:right;\">");

                sb.Append("<table>");
                sb.Append("<tr><td style=\"height:80px; border-right:1px solid #C0C0C0\" valign=top>");

                foreach (GroupZone lZone in lGroup.Zones)
                {
                    if (_Control.ApplicationMenuDirection == DocumentDirection.LTR) sb.Append("<div style=\"float:left;\">");
                    else sb.Append("<div style=\"float:right;\">");

                    foreach (Control lCtr in lZone.Content)
                    {
                        sb.Append("<div style=\"font-size:8pt; font-family:tahoma; padding:2px; background:red; margin:1px;\">CTRL</div>");
                    }
                    sb.Append("</div>");
                }

                sb.Append("<div style=\"clear:both\"></div></td></tr>");
                sb.Append("<tr><td style=\"font-size:8pt; font-family:tahoma; text-align:center\">" + lGroup.Text + "</td></tr>");
                sb.Append("</table>");

                sb.Append("<div style=\"clear:both\"></div></div>");
            }
            sb.Append("<div style=\"clear:both\"></div></div>");
            return sb.ToString();
        }

        public override string GetEditableDesignerRegionContent
            (EditableDesignerRegion region)
        {
            IDesignerHost host =
                (IDesignerHost)Component.Site.GetService(typeof(IDesignerHost));
            if (host != null)
            {
            }
            return "oop" + CurrentTab.ToString();
        }

        public override void SetEditableDesignerRegionContent
             (EditableDesignerRegion region, string content)
        {
            int regionIndex = Int32.Parse(region.Name.Substring(7));

            if (content == null)
            {

            }

            IDesignerHost host =
                (IDesignerHost)Component.Site.GetService(typeof(IDesignerHost));

            if (host != null)
            {

            }
        }

        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (_actionLists == null)
                {
                    _actionLists = new DesignerActionListCollection();
                    _actionLists.AddRange(base.ActionLists);

                    // Draft for future...
                    _actionLists.Add(new ActionList(this));
                }
                return _actionLists;
            }
        }

        public void AddButton()
        {
            try
            {
                RootDesigner.AddControlToDocument(new OfficeButton(), _Control.Contexts[0].Tabs[0].Groups[0].Zones[0], ControlLocation.LastChild);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message.ToString());
            }
        }

        public class ActionList : DesignerActionList
        {
            private OfficeRibbon_Designer _parent;
            private DesignerActionItemCollection _items;

            public ActionList(OfficeRibbon_Designer parent)
                : base(parent.Component)
            {
                _parent = parent;
            }

            // Create the ActionItem collection and add one command
            public override DesignerActionItemCollection GetSortedActionItems()
            {
                if (_items == null)
                {
                    _items = new DesignerActionItemCollection();
                    _items.Add(new DesignerActionHeaderItem("Office Web UI"));
                    _items.Add(new DesignerActionMethodItem(this, "ToggleLargeText", "Test for future...", "Office Web UI"));

                }
                return _items;

            }

            private void ToggleLargeText()
            {
                OfficeRibbon ctl = (OfficeRibbon)_parent.Component;
                System.Windows.Forms.MessageBox.Show(_parent.CurrentTab.ToString());
            }
        }
    }
}
