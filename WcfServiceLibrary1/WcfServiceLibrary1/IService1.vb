<ServiceContract()>
Public Interface IService1

    <OperationContract()>
    Function GetData(ByVal value As Integer) As String

    <OperationContract>
    Function GetWochentag() As String

    <OperationContract>
    Function GetSchnitzel() As Schnitzel

End Interface

<DataContract>
Public Class Schnitzel

    <DataMember()>
    Public Property HatSoße As Boolean

    Public Property Geheim As String

End Class

