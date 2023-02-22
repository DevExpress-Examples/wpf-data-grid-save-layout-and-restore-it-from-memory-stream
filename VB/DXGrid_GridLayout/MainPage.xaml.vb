Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Controls
Imports System.IO

Namespace DXGrid_GridLayout

    Public Partial Class MainPage
        Inherits UserControl

        Public Shared ReadOnly IsLayoutSavedProperty As DependencyProperty = DependencyProperty.Register("IsLayoutSaved", GetType(Boolean), GetType(MainPage), Nothing)

        Public Property IsLayoutSaved As Boolean
            Get
                Return CBool(GetValue(IsLayoutSavedProperty))
            End Get

            Set(ByVal value As Boolean)
                SetValue(IsLayoutSavedProperty, value)
            End Set
        End Property

        Private layoutStream As MemoryStream

        Public Sub New()
            DataContext = Me
            Me.InitializeComponent()
            IsLayoutSaved = False
            Me.grid.ItemsSource = IssueList.GetData()
        End Sub

        Private Sub SaveButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            layoutStream = New MemoryStream()
            Me.grid.SaveLayoutToStream(layoutStream)
            IsLayoutSaved = True
        End Sub

        Private Sub LoadButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            layoutStream.Position = 0
            Me.grid.RestoreLayoutFromStream(layoutStream)
        End Sub

        Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me.grid.Columns.Add(New DevExpress.Xpf.Grid.GridColumn() With {.FieldName = "IsPrivate"})
        End Sub
    End Class

    Public Class IssueList

        Shared Public Function GetData() As List(Of IssueDataObject)
            Dim data As List(Of IssueDataObject) = New List(Of IssueDataObject)()
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
