Imports WcfServiceLibrary1

Public Class Service1
    Implements IService1

    Public Function GetData(ByVal value As Integer) As String Implements IService1.GetData
        Return String.Format("You entered: {0}", value)
    End Function

    Public Function GetWochentag() As String Implements IService1.GetWochentag
        Return Date.Now.ToString("dddd")
    End Function

    Public Function GetSchnitzel() As Schnitzel Implements IService1.GetSchnitzel
        Return New Schnitzel()

    End Function
End Class
