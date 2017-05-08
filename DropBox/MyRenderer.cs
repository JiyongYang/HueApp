using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Manina.Windows.Forms
{
    class MyCustomRenderer : ImageListView.ImageListViewRenderer
    {
        // Check boxes
        private VisualStyleRenderer rCheckedNormal = null;
        private VisualStyleRenderer rUncheckedNormal = null;
        private VisualStyleRenderer rCheckedDisabled = null;
        private VisualStyleRenderer rUncheckedDisabled = null;
        // File icons
        private VisualStyleRenderer rFileIcon = null;
        // Column headers
        private VisualStyleRenderer rColumnNormal = null;
        private VisualStyleRenderer rColumnHovered = null;
        private VisualStyleRenderer rColumnSorted = null;
        private VisualStyleRenderer rColumnSortedHovered = null;
        private VisualStyleRenderer rSortAscending = null;
        private VisualStyleRenderer rSortDescending = null;
        // Items
        private VisualStyleRenderer rItemNormal = null;
        private VisualStyleRenderer rItemHovered = null;
        private VisualStyleRenderer rItemSelected = null;
        private VisualStyleRenderer rItemHoveredSelected = null;
        private VisualStyleRenderer rItemSelectedHidden = null;
        private VisualStyleRenderer rItemDisabled = null;
        // Group headers
        private VisualStyleRenderer rGroupNormal = null;
        private VisualStyleRenderer rGroupLine = null;

        /// <summary>
        /// Gets whether visual styles are supported.
        /// </summary>
        public bool VisualStylesEnabled { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this renderer can apply custom colors.
        /// </summary>
        /// <value></value>
        public override bool CanApplyColors { get { return false; } }

        public MyCustomRenderer()
        {
            VisualStylesEnabled = Application.RenderWithVisualStyles;

            // Create renderers
            if (VisualStylesEnabled)
            {
                // See http://msdn.microsoft.com/en-us/library/bb773210(VS.85).aspx
                // for part and state codes used below.

                // Check boxes
                rCheckedNormal = GetRenderer(VisualStyleElement.Button.CheckBox.CheckedNormal);
                rUncheckedNormal = GetRenderer(VisualStyleElement.Button.CheckBox.UncheckedNormal);
                rCheckedDisabled = GetRenderer(VisualStyleElement.Button.CheckBox.CheckedDisabled);
                rUncheckedDisabled = GetRenderer(VisualStyleElement.Button.CheckBox.UncheckedDisabled);
                // File icons
                rFileIcon = GetRenderer(VisualStyleElement.Button.PushButton.Normal);
                // Column headers
                rColumnNormal = GetRenderer("Header", 1, 1);
                rColumnHovered = GetRenderer("Header", 1, 2);
                rColumnSorted = GetRenderer("Header", 1, 4);
                rColumnSortedHovered = GetRenderer("Header", 1, 5);
                rSortAscending = GetRenderer(VisualStyleElement.Header.SortArrow.SortedUp);
                rSortDescending = GetRenderer(VisualStyleElement.Header.SortArrow.SortedDown);
                // Items
                rItemNormal = GetRenderer("Explorer::ListView", 1, 1);
                rItemHovered = GetRenderer("Explorer::ListView", 1, 2);
                rItemSelected = GetRenderer("Explorer::ListView", 1, 3);
                rItemHoveredSelected = GetRenderer("Explorer::ListView", 1, 6);
                rItemSelectedHidden = GetRenderer("Explorer::ListView", 1, 5);
                rItemDisabled = GetRenderer("Explorer::ListView", 1, 4);
                // Groups
                rGroupNormal = GetRenderer("Explorer::ListView", 6, 1);
                rGroupLine = GetRenderer("Explorer::ListView", 7, 1);
            }

        }

        public override ImageListViewColor[] PreferredColors
        {
            get { return new ImageListViewColor[] { GetMyTheme() }; }
        }

        private VisualStyleRenderer GetRenderer(string className, int part, int state)
        {
            VisualStyleElement e = VisualStyleElement.CreateElement(className, part, state);
            if (VisualStyleRenderer.IsElementDefined(e))
                return new VisualStyleRenderer(e);
            else
                return null;
        }

        private VisualStyleRenderer GetRenderer(VisualStyleElement e)
        {
            if (VisualStyleRenderer.IsElementDefined(e))
                return new VisualStyleRenderer(e);
            else
                return null;
        }

        public override void InitializeGraphics(Graphics g)
        {
            base.InitializeGraphics(g);
            // Change the InterpolationMode to NearestNeighbor.
            // This will effect all drawing operations.

            //g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        }

        public override int MeasureColumnHeaderHeight()
        {
            // Add 4 pixel padding to the base font height.
            return ImageListView.ColumnHeaderFont.Height + 8;
        }

        public override Size MeasureItem(View view)
        {
            Size sz = base.MeasureItem(view);
            if (VisualStylesEnabled && view != View.Details)
            {
                sz.Width += 6;
                sz.Height += 6;
            }
            return sz;
        }

        public override void DrawColumnHeader(Graphics g, ImageListView.ImageListViewColumnHeader column, ColumnState state, Rectangle bounds)
        {
            // Paint background
            if ((state & ColumnState.Hovered) != ColumnState.None)
            {
                using (Brush bHovered = new LinearGradientBrush(bounds, GetMyTheme().ColumnHeaderHoverColor1, GetMyTheme().ColumnHeaderHoverColor2, LinearGradientMode.Vertical))
                {
                    g.FillRectangle(bHovered, bounds);
                }
            }
            else
            {
                using (Brush bNormal = new LinearGradientBrush(bounds, GetMyTheme().ColumnHeaderBackColor1, GetMyTheme().ColumnHeaderBackColor2, LinearGradientMode.Vertical))
                {
                    g.FillRectangle(bNormal, bounds);
                }
            }
            using (Pen pBorder = new Pen(ImageListView.Colors.ColumnSeparatorColor))
            {
                g.DrawLine(pBorder, bounds.Left, bounds.Top, bounds.Left, bounds.Bottom);
                g.DrawLine(pBorder, bounds.Left, bounds.Bottom - 1, bounds.Right, bounds.Bottom - 1);
            }
            using (Pen pBorder = new Pen(Color.FromArgb(252, 252, 252)))
            {
                g.DrawRectangle(pBorder, bounds.Left + 1, bounds.Top, bounds.Width - 2, bounds.Height - 2);
            }

            // Draw the sort arrow
            int offset = 4;
            int width = bounds.Width - 2 * offset;
            if (ImageListView.SortOrder != SortOrder.None && ((state & ColumnState.SortColumn) != ColumnState.None))
            {
                Image img = null;
                /*if (ImageListView.SortOrder == SortOrder.Ascending)
                    img = ImageListViewResources.SortAscending;
                else if (ImageListView.SortOrder == SortOrder.Descending)
                    img = ImageListViewResources.SortDescending;*/
                if (img != null)
                {
                    g.DrawImageUnscaled(img, bounds.Right - offset - img.Width, bounds.Top + (bounds.Height - img.Height) / 2);
                    width -= img.Width + offset;
                }
            }

            // Text
            bounds.X += offset;
            bounds.Width = width;
            if (bounds.Width > 4)
            {
                using (StringFormat sf = new StringFormat())
                {
                    sf.FormatFlags = StringFormatFlags.NoWrap;
                    sf.Alignment = StringAlignment.Near;
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Trimming = StringTrimming.EllipsisCharacter;
                    using (SolidBrush bText = new SolidBrush(ImageListView.Colors.ColumnHeaderForeColor))
                    {
                        g.DrawString(column.Text, (ImageListView.ColumnHeaderFont == null ? ImageListView.Font : ImageListView.ColumnHeaderFont), bText, bounds, sf);
                    }
                }
            }
        }

        public override void DrawBackground(Graphics g, Rectangle bounds)
        {

            base.DrawBackground(g, bounds);
            // Overlay a custom background image
            //Image image = new Bitmap("C:\\Users\\xp5\\Pictures\\12.jpg");
            //Bitmap myImage = new Bitmap(image, 1000,1000);
            //g.DrawImage(myImage, 1000, 1000);

            //g.FillRectangle(brush, 0,0, 500, 500);

        }

        public override void DrawSelectionRectangle(Graphics g, Rectangle selection)
        {
            //g.FillRectangle(SystemBrushes.Highlight, selection);
            base.DrawSelectionRectangle(g, selection);
        }


        public override void DrawItem(Graphics g, ImageListViewItem item, ItemState state, Rectangle bounds)
        {
            VisualStyleRenderer rBack;

            if (!ImageListView.Enabled)
                rBack = rItemSelectedHidden;
            if (((state & ItemState.Disabled) != ItemState.None))
                rBack = rItemDisabled;
            else if (!ImageListView.Focused && ((state & ItemState.Selected) != ItemState.None))
                rBack = rItemSelected;
            else if (((state & ItemState.Selected) != ItemState.None) && ((state & ItemState.Hovered) != ItemState.None))
                rBack = rItemHoveredSelected;
            else if ((state & ItemState.Selected) != ItemState.None)
                rBack = rItemSelected;
            else if ((state & ItemState.Hovered) != ItemState.None)
                rBack = rItemHovered;
            else
                rBack = rItemNormal;

            if (VisualStylesEnabled && rBack != null)
            {
                // Do not draw the background of normal items
                if (((state & ItemState.Hovered) != ItemState.None) || ((state & ItemState.Selected) != ItemState.None))
                    rBack.DrawBackground(g, bounds, bounds);

                Size itemPadding = new Size(12, 5);

                // Draw the image
                if (ImageListView.View != View.Details)
                {
                    Image img = item.GetCachedImage(CachedImageType.Thumbnail);
                    Size szt = TextRenderer.MeasureText(item.Text, ImageListView.Font);
                    if (img != null)
                    {
                        Rectangle pos = Utility.GetSizedImageBounds(img, new Rectangle(bounds.Location + itemPadding, ImageListView.ThumbnailSize + new Size(-10, -10)));
                        // Image background
                        Rectangle imgback = pos;
                        //pos.Height -= 2;
                        //imgback.Inflate(3, 3);
                        g.FillRectangle(Brushes.White, imgback); // 기본 생성시 그림 뒤에 배경 색 지정
                        // Image border
                        /*if (img.Width > 32 && img.Height > 32)
                        {
                            using (Pen pen = new Pen(Color.FromArgb(155, 155, 155))) // (224, 224, 244)
                            {
                                g.DrawRectangle(pen, imgback.X, imgback.Y, imgback.Width - 1, imgback.Height - 1);
                            }
                        }*/
                        // Image
                        g.DrawImage(img, pos);
                    }

                    // Draw item text
                    Color foreColor = SystemColors.ControlText;
                    if ((state & ItemState.Disabled) != ItemState.None)
                        foreColor = SystemColors.GrayText;

                    Rectangle rt = new Rectangle(
                        bounds.Left + itemPadding.Width - 3, bounds.Top + 2 * itemPadding.Height + ImageListView.ThumbnailSize.Height - 7,
                        ImageListView.ThumbnailSize.Width, szt.Height * 2);

                    // TEXT 추가 가능한 부분
                    TextRenderer.DrawText(g, item.projectName + "\n" + item.projectResolution, ImageListView.Font, rt, foreColor,
                        TextFormatFlags.EndEllipsis | TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.PreserveGraphicsClipping);
                }
                else // if (ImageListView.View == View.Details)
                {
                    ;
                }

                // Focus rectangle
                if (ImageListView.Focused && ((state & ItemState.Focused) != ItemState.None))
                {
                    Rectangle focusBounds = bounds;
                    focusBounds.Inflate(-2, -2);
                    ControlPaint.DrawFocusRectangle(g, focusBounds);
                }

            }
            else
                base.DrawItem(g, item, state, bounds);

        }

        /*public override void DrawColumnHeader(Graphics g, ImageListView.ImageListViewColumnHeader column, ColumnState state, Rectangle bounds)
        {
            base.DrawColumnHeader(g, column, state, bounds);
            // Outline the hovered column
            if ((state & ColumnState.Hovered) == ColumnState.Hovered)
                g.DrawRectangle(Pens.Yellow, bounds);
        }*/

        public override void DrawColumnExtender(Graphics g, Rectangle bounds)
        {
            // Do not draw an extender
            using (Brush bNormal = new LinearGradientBrush(bounds, GetMyTheme().ColumnHeaderBackColor1, GetMyTheme().ColumnHeaderBackColor2, LinearGradientMode.Vertical))
            {
                g.FillRectangle(bNormal, bounds);
            }
            using (Pen pBorder = new Pen(ImageListView.Colors.ColumnSeparatorColor))
            {
                g.DrawLine(pBorder, bounds.Left, bounds.Top, bounds.Left, bounds.Bottom);
                g.DrawLine(pBorder, bounds.Left, bounds.Bottom - 1, bounds.Right, bounds.Bottom - 1);
            }
            using (Pen pBorder = new Pen(Color.FromArgb(252, 252, 252)))
            {
                g.DrawRectangle(pBorder, bounds.Left + 1, bounds.Top, bounds.Width - 2, bounds.Height - 2);
            }
        }

        public override void DrawGalleryImage(Graphics g, ImageListViewItem item, Image image, Rectangle bounds)
        {
            base.DrawGalleryImage(g, item, image, bounds);
            g.DrawRectangle(Pens.Black, bounds);
        }

        public override void DrawPane(Graphics g, ImageListViewItem item, Image image, Rectangle bounds)
        {
            ;
        }

        public override void DrawInsertionCaret(Graphics g, Rectangle bounds)
        {
            // Draw a red caret
            g.FillRectangle(Brushes.Red, bounds);
        }

        public override void OnLayout(LayoutEventArgs e)
        {
            // Add a 10 pixel margin to the 
            // left of the item display area
            //e.ItemAreaBounds.X += 10;
            //e.ItemAreaBounds.Width -= 10;
        }

        public override void Dispose()
        {
            base.Dispose();

            // Dispose local resources
            //myFont.Dispose();
            //myBrush.Dispose();
        }

        public override void DrawCheckBox(Graphics g, ImageListViewItem item, Rectangle bounds)
        {
            VisualStyleRenderer renderer;
            if (item.Enabled)
            {
                if (item.Checked)
                    renderer = rCheckedNormal;
                else
                    renderer = rUncheckedNormal;
            }
            else
            {
                if (item.Checked)
                    renderer = rCheckedDisabled;
                else
                    renderer = rUncheckedDisabled;
            }

            if (VisualStylesEnabled && renderer != null)
                renderer.DrawBackground(g, bounds, bounds);
            else
                base.DrawCheckBox(g, item, bounds);
        }

        private static ImageListViewColor GetMyTheme()
        {
            ImageListViewColor c = new ImageListViewColor();

            // control
            c.ControlBackColor = Color.White;
            c.DisabledBackColor = Color.FromArgb(220, 220, 220);

            // item
            c.BackColor = Color.White;
            c.ForeColor = Color.FromArgb(60, 60, 60);
            c.BorderColor = Color.FromArgb(187, 190, 183);

            c.UnFocusedColor1 = Color.FromArgb(235, 235, 235);
            c.UnFocusedColor2 = Color.FromArgb(217, 217, 217);
            c.UnFocusedBorderColor = Color.FromArgb(168, 169, 161);
            c.UnFocusedForeColor = Color.FromArgb(40, 40, 40);

            c.HoverColor1 = Color.Transparent;
            c.HoverColor2 = Color.Transparent;
            c.HoverBorderColor = Color.Transparent;

            c.SelectedColor1 = Color.FromArgb(222, 222, 222); // 주황색(244, 125, 77) ==> (209, 209, 209)
            c.SelectedColor2 = Color.FromArgb(209, 209, 209);  // 주황색235, 110, 60) ==> (209, 209, 209)
            c.SelectedBorderColor = Color.FromArgb(209, 209, 209); // 주황색(240, 119, 70) ==> (209, 209, 209)
            c.SelectedForeColor = Color.White;

            c.DisabledColor1 = Color.FromArgb(217, 217, 217);
            c.DisabledColor2 = Color.FromArgb(197, 197, 197);
            c.DisabledBorderColor = Color.FromArgb(128, 128, 128);
            c.DisabledForeColor = Color.FromArgb(128, 128, 128);

            c.InsertionCaretColor = Color.FromArgb(209, 209, 209); // 주황색(240, 119, 70) ==> (209, 209, 209)

            // thumbnails & pane
            c.ImageInnerBorderColor = Color.Transparent;
            c.ImageOuterBorderColor = Color.White;

            // details view
            c.CellForeColor = Color.FromArgb(60, 60, 60);
            c.ColumnHeaderBackColor1 = Color.FromArgb(247, 247, 247);
            c.ColumnHeaderBackColor2 = Color.FromArgb(235, 235, 235);
            c.ColumnHeaderHoverColor1 = Color.White;
            c.ColumnHeaderHoverColor2 = Color.FromArgb(245, 245, 245);
            c.ColumnHeaderForeColor = Color.FromArgb(60, 60, 60);
            c.ColumnSelectColor = Color.FromArgb(34, 128, 128, 128);
            c.ColumnSeparatorColor = Color.FromArgb(106, 128, 128, 128);
            //c.mAlternateBackColor = Color.FromArgb(234, 234, 234);
            //c.mAlternateCellForeColor = Color.FromArgb(40, 40, 40);

            // image pane
            c.PaneBackColor = Color.White;
            c.PaneSeparatorColor = Color.FromArgb(216, 216, 216);
            c.PaneLabelColor = Color.FromArgb(156, 156, 156);

            // selection rectangle (마우스 드래그 시 나타나는 네모 색깔)
            c.SelectionRectangleColor1 = Color.FromArgb(128, SystemColors.Highlight);      // (64, 240, 116, 68)
            c.SelectionRectangleColor2 = Color.FromArgb(128, SystemColors.Highlight);      // (64, 240, 116, 68)
            c.SelectionRectangleBorderColor = SystemColors.Highlight;     // (240, 119, 70)

            return c;
        }
    }
}