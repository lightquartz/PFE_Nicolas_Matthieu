using UnityEditor;

namespace eDriven
{
    public static class eDrivenMenu
    {
// ReSharper disable InconsistentNaming
        public const string HOMEPAGE = "http://edrivengui.com/";
        public const string TWITTER = "https://twitter.com/DankoKozar";
        public const string AUTHORS_HOMEPAGE = "http://dankokozar.com";
        public const string API = "http://edriven.dankokozar.com/api/1-0/";
        public const string MANUAL = "http://edriven.dankokozar.com/manual/eDriven_Manual_1-0.pdf";
        public const string GITHUB = "https://github.com/dkozar/eDriven";
        public const string VIDEO = "http://www.youtube.com/playlist?list=PL7EE340828F962941";
        public const string DEMO = "http://edrivenunity.com/";
        public const string FORUM_CORE = "http://forum.unity3d.com/threads/76835-eDriven-framework-for-Unity-released";
        public const string FORUM_GUI = "http://forum.unity3d.com/threads/142733-eDriven.Gui";
        public const string FORUM_ASSET_STORE = "http://forum.unity3d.com/threads/143093-eDriven.Gui-GUI-for-programmers";
        public const string FORUM_QA = "http://forum.unity3d.com/threads/148424-eDriven-Q-amp-A";
// ReSharper restore InconsistentNaming

        /// <summary>
        /// API
        /// </summary>
        //[MenuItem("Help/eDriven/About")]
        //public static void About()
        //{
        //    EditorSettings.ShowAbout = true;
        //}

        /// <summary>
        /// API
        /// </summary>
        [MenuItem("Help/eDriven/eDriven.Gui Homepage")]
        public static void LounchHomepage()
        {
            Help.BrowseURL(HOMEPAGE);
        }

        /// <summary>
        /// Manual
        /// </summary>
        [MenuItem("Help/eDriven/Documentation/Manual")]
        public static void LounchManual()
        {
            Help.BrowseURL(MANUAL);
        }

        /// <summary>
        /// API
        /// </summary>
        [MenuItem("Help/eDriven/Documentation/API")]
        public static void LounchApi()
        {
            Help.BrowseURL(API);
        }

        /// <summary>
        /// Core source @ Github
        /// </summary>
        [MenuItem("Help/eDriven/Documentation/eDriven.Core Source")]
        public static void LounchGithub()
        {
            Help.BrowseURL(GITHUB);
        }

        /// <summary>
        /// Video channel
        /// </summary>
        [MenuItem("Help/eDriven/Documentation/Video Channel")]
        public static void LounchVideo()
        {
            Help.BrowseURL(VIDEO);
        }

        /// <summary>
        /// Video channel
        /// </summary>
        [MenuItem("Help/eDriven/Documentation/Demo Site")]
        public static void LounchDemo()
        {
            Help.BrowseURL(DEMO);
        }
        
        /// <summary>
        /// Q&A thread
        /// </summary>
        [MenuItem("Help/eDriven/Forum/eDriven.Core Thread")]
        public static void LounchForumCore()
        {
            Help.BrowseURL(FORUM_CORE);
        }

        /// <summary>
        /// Q&A thread
        /// </summary>
        [MenuItem("Help/eDriven/Forum/eDriven.Gui Thread")]
        public static void LounchForumGui()
        {
            Help.BrowseURL(FORUM_GUI);
        }

        /// <summary>
        /// Q&A thread
        /// </summary>
        [MenuItem("Help/eDriven/Forum/eDriven.Gui on Assets and Asset Store")]
        public static void LounchForumAssetStore()
        {
            Help.BrowseURL(FORUM_ASSET_STORE);
        }

        /// <summary>
        /// Q&A thread
        /// </summary>
        [MenuItem("Help/eDriven/Forum/eDriven Questions and Answers")]
        public static void LounchForumQA()
        {
            Help.BrowseURL(FORUM_QA);
        }

        /// <summary>
        /// Author's homepage
        /// </summary>
        [MenuItem("Help/eDriven/Contact/Author @Twitter")]
        public static void LounchTwitter()
        {
            Help.BrowseURL(TWITTER);
        }

        /// <summary>
        /// Author's homepage
        /// </summary>
        [MenuItem("Help/eDriven/Contact/Author's homepage")]
        public static void LounchAuthorsHomepage()
        {
            Help.BrowseURL(AUTHORS_HOMEPAGE);
        }
    }
}