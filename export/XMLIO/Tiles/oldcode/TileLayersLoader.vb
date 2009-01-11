
Imports System.Xml.Linq
Imports CommonObjects

Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Content
Imports Microsoft.Xna.Framework.Graphics


Public Class TileLayersLoader
    Implements ILoader

    Public docTileLayers As XDocument
    Public mTileLayersList As TileLayerList
    Protected mDetailedTileList As DetailedTileList

    Public Sub New(ByVal theFile As String, ByVal theDetailedTileList As DetailedTileList)
        
        mDetailedTileList = theDetailedTileList
        mTileLayersList = New TileLayerList()
        Load(theFile)
        Populate()

    End Sub

    Public Sub Load(ByVal theFile As String) Implements ILoader.Load
        If theFile = "" Then
            theFile = CurDir() & "/Tiles/TileLayers.xml"
        Else
            theFile = CurDir() & "/Tiles/" & theFile & ".xml"
        End If
        docTileLayers = XDocument.Load(theFile)
    End Sub

    Public Sub Populate() Implements ILoader.Populate
        Dim theTileLayer As TileLayer

        Dim mTileLayouts = From tl In docTileLayers...<tileLayer> _
                          Select mID = CInt(tl.<id>.Value), _
                          mRows = CInt(tl.<rows>.Value), _
                          mColumns = CInt(tl.<columns>.Value), _
                          mLayoutArray = tl.<layoutArray>, _
                          mDims = CInt(tl.<tileDims>.Value)


        Dim totRows As Integer
        Dim totColumns As Integer
        Dim id As Integer
        Dim y As Integer
        Dim x As Integer
        Dim dims As Integer



        'For Each tl In mTileLayouts
        '    Dim Layout As Integer(,)
        '    '' TODO Add row and column checking for errors
        '    totRows = tl.mRows
        '    totColumns = tl.mColumns
        '    id = tl.mID
        '    dims = tl.mDims

        '    Dim mLayout = From a In tl.mLayoutArray...<row> _
        '                  Select mRow = a.Value

        '    ' Dim mLayout = From a In tl.mLayoutArray _
        '    '               Select mRow = a...<row>.Value
        '    x = -1
        '    y = -1

        '    Dim aRow() As Integer
        '    'go through each row and create an int array out of the string
        '    For Each a In mLayout
        '        Dim columns As String
        '        y = y + 1
        '        columns = a
        '        'gets the row as an int array
        '        aRow = GetIntArray(columns)
        '        'checks to see if the array is wide enough
        '        'later this will test to see if row length is correct
        '        If aRow.GetLength(0) > x Then
        '            x = aRow.GetLength(0) - 1
        '        End If
        '        'resizes the layout array whilst saving existing values
        '        ReDim Preserve Layout(x, y)
        '        'loops through the int array adding the values into the layout array
        '        For index As Integer = 0 To x
        '            Layout(index, y) = aRow(index)
        '        Next


        '    Next

        '    theTileLayer = New TileLayer(id, dims, Layout, mDetailedTileList)
        '    mTileLayersList.Add(theTileLayer)


        'Next


    End Sub

    ''' <summary>
    ''' Takes a comma seperated string and returns an int array of values
    ''' </summary>
    ''' <param name="s"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function GetIntArray(ByVal s As String) As Integer()
        Dim ret(0) As Integer
        Dim count As Int16 = 1
        Do While s.Contains(",") = True
            ReDim Preserve ret(count - 1)
            ret(count - 1) = GetNextValue(s)
            count = count + 1
        Loop
        ReDim Preserve ret(count - 1)
        ret(count - 1) = GetNextValue(s)
        Return ret
    End Function

    ''' <summary>
    ''' this takes a reference to a string, returns the next int value and
    ''' removes that value from the from of the string (along with " " and ,
    ''' </summary>
    ''' <param name="s"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function GetNextValue(ByRef s As String) As Integer
        Dim retVal As Integer
        Dim index As Integer
        If s.Contains(",") Then
            index = s.IndexOf(",")
            Dim subString As String = s.Substring(0, index + 1)
            subString = CleanLeft(subString)
            subString = CleanRight(subString)

            retVal = CInt(subString)
            s = CleanRight(s)
            s = deleteTillComma(s)
            s = CleanLeft(s)
        Else
            s = CleanLeft(s)
            s = CleanRight(s)

            retVal = CInt(s)
        End If

        Return retVal
    End Function

    Protected Function deleteTillComma(ByVal s As String) As String
        s = s.Remove(0, 1)
        If Not s.StartsWith(",") Then
            s = deleteTillComma(s)
        End If
        Return s
    End Function

    Protected Function CleanLeft(ByVal s As String) As String
        If (s.StartsWith(" ") = True) Or (s.StartsWith(",") = True) Then
            Return CleanLeft(s.Remove(0, 1))
        Else
            Return s
        End If
    End Function

    Protected Function CleanRight(ByVal s As String) As String
        If (s.EndsWith(" ") = True) Or (s.EndsWith(",") = True) Then
            s = s.Remove(s.Count - 1)
            CleanRight(s)
        End If
        Return s
    End Function



End Class
