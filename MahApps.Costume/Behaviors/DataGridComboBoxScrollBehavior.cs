using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace MahAppsDemo.Behaviors
{
    /// <summary>
    /// Used for ComboBox with DataGridComboBoxStyle style to scroll to the selected row in the dropdown DataGrid.
    /// Example:
    ///  <ComboBox Style="{StaticResource DataGridComboBoxStyle}"  >
    ///     <i:Interaction.Behaviors>
    ///         <beh:DataGridComboBoxScrollBehavior />
    ///     </i:Interaction.Behaviors>
    ///  </ComboBox>
    /// </summary>
    /// <seealso cref="System.Windows.Interactivity.Behavior{System.Windows.Controls.ComboBox}" />
    public class DataGridComboBoxScrollBehavior : Behavior<ComboBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.DropDownOpened += OnDropDownOpened;
            AssociatedObject.SelectionChanged += OnSelectionChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.DropDownOpened -= OnDropDownOpened;
            AssociatedObject.SelectionChanged -= OnSelectionChanged;
        }

        void OnDropDownOpened(object sender, System.EventArgs e)
        {
            ComboBox cbo = sender as ComboBox;
            if (cbo != null)
            {
                var dg = cbo.Template.FindName("PART_PopupDataGrid", cbo) as DataGrid;
                if ((dg != null) && (dg.SelectedItem != null))
                {
                    dg.UpdateLayout();
                    dg.ScrollIntoView(dg.SelectedItem);
                }
            }
        }

        private void OnSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ComboBox cbo = sender as ComboBox;
            if (cbo != null)
            {
                cbo.IsDropDownOpen = false;
            }
        }
    }
}

