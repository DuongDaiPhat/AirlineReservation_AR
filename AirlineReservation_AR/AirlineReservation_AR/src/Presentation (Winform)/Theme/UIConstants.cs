using System.Drawing;

namespace AirlineReservation_AR.src.Presentation__Winform_.Theme
{
    /// <summary>
    /// Centralized UI Constants for Admin Dashboard
    /// Professional Dark Theme - Airline Industry Standard
    /// </summary>
    public static class UIConstants
    {
        #region Primary Colors
        
        /// <summary>Navy Blue - Sidebar/Header background</summary>
        public static readonly Color SidebarBg = Color.FromArgb(13, 27, 42);           // #0D1B2A
        
        /// <summary>Royal Blue - Active button/Primary accent</summary>
        public static readonly Color PrimaryActive = Color.FromArgb(46, 80, 144);      // #2E5090
        
        /// <summary>Sky Blue - Hover state/Secondary accent</summary>
        public static readonly Color PrimaryHover = Color.FromArgb(74, 144, 217);      // #4A90D9
        
        /// <summary>Light Blue - Links and highlights</summary>
        public static readonly Color PrimaryLight = Color.FromArgb(100, 181, 246);     // #64B5F6
        
        #endregion

        #region Neutral Colors
        
        /// <summary>Deep Dark - Card backgrounds on dark theme</summary>
        public static readonly Color DarkPanel = Color.FromArgb(27, 38, 59);           // #1B263B
        
        /// <summary>Light Gray - Main content area background</summary>
        public static readonly Color ContentBg = Color.FromArgb(248, 249, 250);        // #F8F9FA
        
        /// <summary>White - Card/Form background</summary>
        public static readonly Color CardBg = Color.White;                              // #FFFFFF
        
        /// <summary>Border color for cards and inputs</summary>
        public static readonly Color Border = Color.FromArgb(222, 226, 230);           // #DEE2E6
        
        /// <summary>Divider lines</summary>
        public static readonly Color Divider = Color.FromArgb(233, 236, 239);          // #E9ECEF
        
        #endregion

        #region Text Colors
        
        /// <summary>Primary text - Dark</summary>
        public static readonly Color TextPrimary = Color.FromArgb(33, 37, 41);         // #212529
        
        /// <summary>Secondary text - Muted</summary>
        public static readonly Color TextSecondary = Color.FromArgb(108, 117, 125);    // #6C757D
        
        /// <summary>White text - On dark backgrounds</summary>
        public static readonly Color TextLight = Color.White;
        
        /// <summary>Disabled text</summary>
        public static readonly Color TextDisabled = Color.FromArgb(173, 181, 189);     // #ADB5BD
        
        #endregion

        #region Semantic Colors
        
        /// <summary>Success - Green</summary>
        public static readonly Color Success = Color.FromArgb(40, 167, 69);            // #28A745
        
        /// <summary>Success Light - Background</summary>
        public static readonly Color SuccessLight = Color.FromArgb(212, 237, 218);     // #D4EDDA
        
        /// <summary>Danger - Red</summary>
        public static readonly Color Danger = Color.FromArgb(220, 53, 69);             // #DC3545
        
        /// <summary>Danger Light - Background</summary>
        public static readonly Color DangerLight = Color.FromArgb(248, 215, 218);      // #F8D7DA
        
        /// <summary>Warning - Amber</summary>
        public static readonly Color Warning = Color.FromArgb(255, 193, 7);            // #FFC107
        
        /// <summary>Warning Light - Background</summary>
        public static readonly Color WarningLight = Color.FromArgb(255, 243, 205);     // #FFF3CD
        
        /// <summary>Info - Cyan</summary>
        public static readonly Color Info = Color.FromArgb(23, 162, 184);              // #17A2B8
        
        /// <summary>Info Light - Background</summary>
        public static readonly Color InfoLight = Color.FromArgb(209, 236, 241);        // #D1ECF1
        
        #endregion

        #region Layout Constants
        
        /// <summary>Sidebar width when expanded</summary>
        public const int SidebarExpandedWidth = 220;
        
        /// <summary>Sidebar width when collapsed</summary>
        public const int SidebarCollapsedWidth = 60;
        
        /// <summary>Header height</summary>
        public const int HeaderHeight = 60;
        
        /// <summary>Menu button height</summary>
        public const int MenuButtonHeight = 50;
        
        /// <summary>Standard padding</summary>
        public const int PaddingStandard = 16;
        
        /// <summary>Small padding</summary>
        public const int PaddingSmall = 8;
        
        /// <summary>Large padding</summary>
        public const int PaddingLarge = 24;
        
        #endregion

        #region Border Radius
        
        /// <summary>Card border radius</summary>
        public const int RadiusCard = 8;
        
        /// <summary>Button border radius</summary>
        public const int RadiusButton = 6;
        
        /// <summary>Input border radius</summary>
        public const int RadiusInput = 4;
        
        /// <summary>Large radius (avatars, pills)</summary>
        public const int RadiusLarge = 20;
        
        #endregion

        #region Typography
        
        /// <summary>Page title font</summary>
        public static readonly Font FontTitle = new Font("Segoe UI", 20F, FontStyle.Bold);
        
        /// <summary>Section header font</summary>
        public static readonly Font FontHeader = new Font("Segoe UI", 14F, FontStyle.Bold);
        
        /// <summary>Sidebar menu font</summary>
        public static readonly Font FontMenu = new Font("Segoe UI", 11F, FontStyle.Regular);
        
        /// <summary>Body text font</summary>
        public static readonly Font FontBody = new Font("Segoe UI", 10F, FontStyle.Regular);
        
        /// <summary>Button text font</summary>
        public static readonly Font FontButton = new Font("Segoe UI", 10F, FontStyle.Bold);
        
        /// <summary>Small/caption font</summary>
        public static readonly Font FontSmall = new Font("Segoe UI", 9F, FontStyle.Regular);
        
        /// <summary>DataGridView font</summary>
        public static readonly Font FontGrid = new Font("Segoe UI", 9F, FontStyle.Regular);
        
        #endregion

        #region Animation (Minimal for WinForms)
        
        /// <summary>Sidebar animation interval (ms)</summary>
        public const int AnimationInterval = 15;
        
        /// <summary>Sidebar animation step (px)</summary>
        public const int AnimationStep = 8;
        
        #endregion
    }
}
