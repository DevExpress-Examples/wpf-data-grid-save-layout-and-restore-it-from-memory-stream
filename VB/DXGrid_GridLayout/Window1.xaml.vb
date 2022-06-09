Imports System.Collections.ObjectModel
Imports System.IO
Imports System.Windows

Namespace DXGrid_GridLayout

    Public Partial Class Window1
        Inherits Window

        Private layoutStream As MemoryStream

        Public Sub New()
            Me.InitializeComponent()
            Me.grid.ItemsSource = IssueList.GetData()
        End Sub

        Private Sub OnSaveLayout(ByVal sender As Object, ByVal e As RoutedEventArgs)
            layoutStream = New MemoryStream()
            Me.grid.SaveLayoutToStream(layoutStream)
        End Sub

        Private Sub OnRestoreLayout(ByVal sender As Object, ByVal e As RoutedEventArgs)
            layoutStream.Position = 0
            Me.grid.RestoreLayoutFromStream(layoutStream)
        End Sub

        Private Sub OnAddNewColumn(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me.grid.Columns.Add(New DevExpress.Xpf.Grid.GridColumn() With {.FieldName = "IsPrivate"})
        End Sub
    End Class

    Public Class IssueList

        Shared Public Function GetData() As ObservableCollection(Of IssueDataObject)
            Dim data As ObservableCollection(Of IssueDataObject) = New ObservableCollection(Of IssueDataObject)()
            data.Add(New IssueDataObject() With {.IssueName = "Transaction History", .IssueType = "Bug", .IsPrivate = True})
            data.Add(New IssueDataObject() With {.IssueName = "Ledger: Inconsistency", .IssueType = "Bug", .IsPrivate = False})
            data.Add(New IssueDataObject() With {.IssueName = "Data Import", .IssueType = "Request", .IsPrivate = False})
            data.Add(New IssueDataObject() With {.IssueName = "Data Archiving", .IssueType = "Request", .IsPrivate = True})
            Return data
        End Function
    End Class

    Public Class IssueDataObject

        Public Property IssueName As String

        Public Property IssueType As String

        Public Property IsPrivate As Boolean
    End Class
End Namespace
