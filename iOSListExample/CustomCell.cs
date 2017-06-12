using System;
using System.Drawing;
using CoreGraphics;
using Foundation;
using UIKit;

namespace iOSListExample
{
	public class CustomCell : UITableViewCell  
    {
		CVSource source;
		UICollectionViewFlowLayout layout;
		UICollectionView collectionView;

        private readonly int itemHeight = 50;
        private readonly int itemWidth = 50;

		UILabel titleLabel, descriptionLabel;

		public CustomCell (IntPtr p):base(p) // for the new cell reuse
		{
			SelectionStyle = UITableViewCellSelectionStyle.Blue;
			ContentView.BackgroundColor = UIColor.FromRGB (27, 16, 117);

			titleLabel = new UILabel () 
			{
				Font = UIFont.FromName("Helvetica-Bold", 25f),
				TextColor = UIColor.White,
				BackgroundColor = UIColor.Clear
			};

			ContentView.AddSubviews (new UIView[] { titleLabel, descriptionLabel });
		}

		public CustomCell (NSString cellId) : base (UITableViewCellStyle.Default, cellId)
		{
			SelectionStyle = UITableViewCellSelectionStyle.Blue;
			ContentView.BackgroundColor = UIColor.FromRGB (27, 16, 117);

			titleLabel = new UILabel () 
			{
				Font = UIFont.FromName("Helvetica-Bold", 25f),
				TextColor = UIColor.White,
				BackgroundColor = UIColor.Clear
			};

            // Adding a collection view

			layout = new UICollectionViewFlowLayout
			{
				SectionInset = new UIEdgeInsets(5, 5, 5, 5),
				MinimumInteritemSpacing = 5,
				MinimumLineSpacing = 5,
                ItemSize = new SizeF(itemWidth, itemHeight)
			};

            collectionView = new UICollectionView(ContentView.Bounds, layout);

            source = new CVSource() {
                ItemWidth = itemWidth, 
                ItemHeight = itemHeight
            };

			collectionView.RegisterClassForCell(typeof(TextCell), TextCell.CellId);
			collectionView.Source = source;

			ContentView.AddSubviews(new UIView[] {
				titleLabel,
                collectionView
			});

			titleLabel.Frame = new RectangleF(5, 4, (float)ContentView.Bounds.Width - 63, 25);
   			collectionView.Frame = new RectangleF(0, 30, 170, 200);

			ContentView.AddConstraint(
			  NSLayoutConstraint.Create(
				titleLabel, NSLayoutAttribute.Top,
				NSLayoutRelation.Equal,
				ContentView, NSLayoutAttribute.TopMargin,
				1, 3
			  )
			);

			ContentView.AddConstraint(
			  NSLayoutConstraint.Create(
				titleLabel, NSLayoutAttribute.Bottom,
				NSLayoutRelation.Equal,
                collectionView, NSLayoutAttribute.Top,
				1, 3
			  )
			);

			ContentView.AddConstraint(
			  NSLayoutConstraint.Create(
                collectionView, NSLayoutAttribute.Bottom,
				NSLayoutRelation.Equal,
                ContentView, NSLayoutAttribute.BottomMargin,
				1, 3
			  )
			);

		}

		public void UpdateCell (string title, string description)
		{
			titleLabel.Text = title;
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
		}

        // gsm

        class CVSource : UICollectionViewSource
		{
			string[] data = { "one", "two", "three", "four", "five", "six", "seven" };

			private int _ItemWidth;
			public int ItemWidth
			{
				get
				{
					return _ItemWidth;
				}
				set
				{
					_ItemWidth = value;
				}
			}

			private int _ItemHeight;
			public int ItemHeight
			{
				get
				{
					return _ItemHeight;
				}
				set
				{
					_ItemHeight = value;
				}
			}

			public override nint GetItemsCount(UICollectionView collectionView, nint section)
			{
				return data.Length;
			}

			public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
			{
				TextCell textCell = (TextCell)collectionView.DequeueReusableCell(TextCell.CellId, indexPath);

				textCell.label.Frame = new RectangleF(
				   (float)textCell.label.Frame.Left, (float)textCell.label.Frame.Top,
                    ItemWidth, ItemHeight
			    );

				textCell.Text = data[indexPath.Row];

				return textCell;
			}

			public override void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
			{
				Console.WriteLine("Row {0} selected", indexPath.Row);
			}

			public override bool ShouldSelectItem(UICollectionView collectionView, NSIndexPath indexPath)
			{
				return true;
			}
		}

		class TextCell : UICollectionViewCell
		{
			public UILabel label;

			public static readonly NSString CellId = new NSString("TextCell");

			public string Text
			{
				get
				{
					return label.Text;
				}
				set
				{
					label.Text = value;
					SetNeedsDisplay();
				}
			}

            private int _ItemWidth;
			public int ItemWidth
			{
				get
				{
					return _ItemWidth;
				}
				set
				{
					_ItemWidth = value;
				}
			}

			private int _ItemHeight;
			public int ItemHeight
			{
				get
				{
                    return _ItemHeight;
				}
				set
				{
					_ItemHeight = value;
				}
			}

			[Export("initWithFrame:")]
			TextCell(RectangleF frame) : base(frame)
			{
				label = new UILabel(ContentView.Frame)
				{
					BackgroundColor = UIColor.Red,
					TextColor = UIColor.White,
					TextAlignment = UITextAlignment.Center
				};

				ContentView.AddSubview(label);
			}
		}

	}
}

