using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TreeGUI : MonoBehaviour
{
	public GUIStyle scrollViewStyle;
	public GUIStyle treeHandleStyle;
	public GUIStyle textStyle;
	public GUIStyle resizeStyle;

	public GUILayoutOption[] options = new GUILayoutOption[] { GUILayout.ExpandWidth (false) };
	public bool isVisible = false;
	public Rect position;
	public GUISkin theSkin;
	private Vector2 mScrollPosition = Vector2.zero;

	private bool mbDragging = false, mbResizing = false;
	private Vector2 mLastMousePosition;
	private int mIdResizeButton = 0; 
	private TreeNode mTree;
	
	public GUIContent resizeContent = new GUIContent("/", "resize");
	
	public class TreeNode
	{
		bool mbOpen;
		List<TreeNode> mlstChildren = new List<TreeNode>();
		string msText;
		
		public TreeNode(string sText)
		{
			msText = sText;
		}
		
		public List<TreeNode> Children
		{
			get { return mlstChildren; }
		}
		
		public void AddChild(TreeNode node)
		{
			mlstChildren.Add(node);
		}
		
		public string Text 
		{ 
			get { return msText;}
			set { msText = value;}
		}
		public bool IsOpen
		{
			get { return mbOpen;}
			set { mbOpen = value;}
		}
		public void SetOpenRecursively(bool val)
		{
			IsOpen = val;
			foreach (TreeNode node in mlstChildren)
				node.SetOpenRecursively(val);
		}
	}

	public TreeGUI()
	{
	}
	
	// Use this for initialization
	void Start ()
	{
	}


	void Awake ()
	{
		mTree = GenerateTree(5, 5, "Root");
		Debug.Log("Tree generated.");
	}
	
	TreeNode GenerateTree(int depth, int breadth, string sText)
	{
		TreeNode nodeThis = new TreeNode(sText);
		
		if (depth > 0)
		{
			for (int k=0; k < breadth; k++)
			{
				nodeThis.AddChild(GenerateTree(depth-1, breadth, "Node " + depth + ":" + k));
			}
		}
		return nodeThis;
	}


	void CheckExpandAll(Event evt, TreeNode node)
	{
		if (evt.button == 0 &&  evt.clickCount == 2)
		{
			node.SetOpenRecursively(!node.IsOpen);
		}
	}
	
	private void GUIRenderTreeNode(TreeNode node)
	{
		GUILayout.BeginHorizontal ();
		if (GUILayout.Button (node.IsOpen ? " - " : " + ", treeHandleStyle)) 
		{
			node.IsOpen = !node.IsOpen;
		}
		if (node.IsOpen) 
		{
			GUILayout.BeginVertical ();
			if (GUILayout.Button (node.Text, textStyle))
				CheckExpandAll(Event.current, node);
			
			foreach (TreeNode nodeChild in node.Children)
			{
				GUIRenderTreeNode(nodeChild);
			}
			GUILayout.EndVertical ();
		} 
		else if (GUILayout.Button (node.Text, textStyle))
		{
			CheckExpandAll(Event.current, node);
		}
			
		GUILayout.EndHorizontal ();
	}
	
	private void ShowTreeWindow(int idWindow)
	{
		if (theSkin != null)
			GUI.skin = theSkin;
		else
			theSkin = GUI.skin;
		
		GUILayoutOption[] options = { GUILayout.Width (position.width-20), GUILayout.Height (position.height-55) };
		mScrollPosition = GUILayout.BeginScrollView (mScrollPosition, false, false, 
		                                            theSkin.horizontalScrollbar, theSkin.verticalScrollbar, 
		                                            scrollViewStyle, options);
		GUIRenderTreeNode(mTree);
		GUILayout.EndScrollView ();
		GUILayout.BeginHorizontal();
		GUILayout.Space(position.width - 40);
		GUILayout.Label(resizeContent, resizeStyle);
		if (Event.current.type == EventType.MouseDown && !mbDragging) 
		{
			mbDragging = true;
			mLastMousePosition = Event.current.mousePosition;
			mIdResizeButton = GUIUtility.GetControlID(resizeContent, FocusType.Passive);
			
			mbResizing = Vector2.Distance(new Vector2(position.width, position.height), Event.current.mousePosition) < 20;
		}
		else if (Event.current.type == EventType.MouseUp)
		{
			mbDragging = false;
			mbResizing = false;
		}
		else if (mbDragging && Event.current.type == EventType.MouseDrag) {
			Vector2 delt = Event.current.mousePosition - mLastMousePosition;
			
			if (mbResizing)
			{
				position.width += delt.x;
				position.height += delt.y;
				mLastMousePosition = Event.current.mousePosition;
			}
			else
			{
				position.x += delt.x;
				position.y += delt.y;
			}
		}
		
		GUILayout.EndHorizontal();
		GUI.DragWindow();
		
	}
	// Update is called once per frame
	void OnGUI ()
	{
		if (isVisible) {
			this.position = GUILayout.Window(0, position, ShowTreeWindow, "My Tree");

		}
	}
	
}
