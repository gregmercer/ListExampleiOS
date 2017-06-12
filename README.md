# ListExampleiOS
ListExampleiOS - Shows different lists using UITableView

Note: Selecting "Custom List" from the Home Page displays - an example of how to add a collection view into each table row. The collection view has a wrapping set of TextCells. See CustomCell.cs for the code behind this.

Two key lines in getting this to work are found here:

```
    public partial class ListCustomViewController : UIViewController
    {
        ...
        
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            ...

            table.RowHeight = UITableView.AutomaticDimension;
            table.EstimatedRowHeight = new nfloat(15.0);
```
