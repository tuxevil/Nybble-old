using System.Web.UI.WebControls;


/// <summary>
/// Creates a ListSelector control to move items from ListSource to ListDestination on client side.
/// </summary>
/// <remarks>Depends on Fluent.ListTransfer component</remarks>
public partial class Controls_ListTransfer : System.Web.UI.UserControl
{

	/// <summary>
	/// Clear all items in the source and destination list
	/// </summary>
	public void Clear()
	{
		ClearSource();
		ClearDestination();
	}

	/// <summary>
	/// Clear all items in the source list
	/// </summary>
	public void ClearSource( )
	{
		lstSource.Items.Clear( );
	}

	/// <summary>
	/// Clear all items in the destination list
	/// </summary>
	public void ClearDestination( )
	{
		lstDestination.Items.Clear( );
	}

	/// <summary>
	/// Adds an item to the source list
	/// </summary>
	/// <param name="li"></param>
	public void AddItem(ListItem li)
	{
		lstSource.Items.Add(li);
	}

	/// <summary>
	/// Adds an item to the destination list
	/// </summary>
	/// <param name="li"></param>
	public void AddDestinationItem(ListItem li)
	{
		lstDestination.Items.Add(li);
	}

	/// <summary>
	/// Provides access to the DestinationItems
	/// </summary>
	public ListItemCollection DestinationItems
	{
		get { return lstDestination.Items; }
	}
}