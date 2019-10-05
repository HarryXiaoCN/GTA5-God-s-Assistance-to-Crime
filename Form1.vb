Imports System.ComponentModel
Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports System.Text
Imports System.Runtime.InteropServices

Public Class Form1
    Private serverSK As Socket, serverSK_S As Socket, sCSK(3) As 终端机
    Private clientSK As Socket, clientSK_S As Socket
    Private serverSKThread As Thread
    Private clientSKThread As Thread
    Private GTA5基址 As Long, 存活终端数量 As Long, 存活终端位ID(3) As Boolean, 单发送锁 As Boolean
    Private 同步操作讯息 As String, 线程信息(3) As String, 线程首ASC(3) As Integer, 数据接收线程存活(3) As Boolean, 发送锁(3) As Boolean

    Public Declare Auto Function RegisterHotKey Lib "user32.dll" Alias "RegisterHotKey" (ByVal hwnd As IntPtr, ByVal id As Integer, ByVal fsModifiers As Integer, ByVal vk As Integer) As Boolean
    Public Declare Auto Function UnRegisterHotKey Lib "user32.dll" Alias "UnregisterHotKey" (ByVal hwnd As IntPtr, ByVal id As Integer) As Boolean
    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = 786 Then
            Select Case m.WParam.ToInt32
                Case 1
                    Try
                        联网同步执行操作按钮_Click(Nothing, Nothing)
                    Catch ex As Exception
                        错误消息("联网同步执行操作失败,原因：" & ex.Message)
                    End Try
                Case 2
                    加血术()
                Case 3
                    硬甲术()
                Case 4
                    无敌时钟.Enabled = Not 无敌时钟.Enabled
                    加血术()
                    硬甲术()
                    操作消息("金刚不坏身：" & 无敌时钟.Enabled)
                Case 5
                    神隐术()
            End Select
        End If
        MyBase.WndProc(m)
    End Sub
    Private Sub 神隐术(Optional hp As Integer = 0)
        Try
            Dim r As 内存地址 = 获得警星地址()
            Dim bytesWrite As Integer
            Dim b() As Byte = BitConverter.GetBytes(hp)
            Dim c As UInteger = b.Length
            WriteProcessMemory(r.句柄, r.地址, b, c, bytesWrite)
        Catch ex As Exception
            错误消息("神隐失败,原因：" & ex.Message)
        End Try
    End Sub
    Private Sub 金刚不坏身()
        加血术(99999)
        硬甲术(99999)
    End Sub
    Private Sub 硬甲术(Optional hp As Single = 50.0F)
        Try
            Dim r As 内存地址 = 获得防弹衣地址()
            Dim bytesWrite As Integer
            Dim b() As Byte = BitConverter.GetBytes(hp)
            Dim c As UInteger = b.Length
            WriteProcessMemory(r.句柄, r.地址, b, c, bytesWrite)
        Catch ex As Exception
            错误消息("加防弹衣失败,原因：" & ex.Message)
        End Try
    End Sub
    Private Sub 加血术(Optional hp As Single = 500.0F)
        Try
            Dim r As 内存地址 = 获得生命地址()
            Dim bytesWrite As Integer
            Dim b() As Byte = BitConverter.GetBytes(hp)
            Dim c As UInteger = b.Length
            WriteProcessMemory(r.句柄, r.地址, b, c, bytesWrite)
        Catch ex As Exception
            错误消息("加血失败,原因：" & ex.Message)
        End Try
    End Sub
    Private Sub 断开本地网络按钮_Click(sender As Object, e As EventArgs) Handles 断开本地网络按钮.Click
        If 断开本地网络按钮.Text = "断开本地网络" Then
            Shell("cmd /c ipconfig /release")
            断开本地网络按钮.Text = "恢复本地网络"
        Else
            Shell("cmd /c ipconfig /renew")
            断开本地网络按钮.Text = "断开本地网络"
        End If
    End Sub

    Private Sub 关闭游戏进程按钮_Click(sender As Object, e As EventArgs) Handles 关闭游戏进程按钮.Click
        Shell("cmd /c ipconfig /release")
        Shell("cmd /c taskkill /F /IM GTA5.exe /T")
        Shell("cmd /c taskkill /F /IM PlayGTAV.exe /T")
        Shell("cmd /c taskkill /F /IM Launcher.exe /T")
        Shell("cmd /c taskkill /F /IM SocialClubHelper.exe /T")
    End Sub

    Private Sub 建立本地服务器按钮_Click(sender As Object, e As EventArgs) Handles 建立本地服务器按钮.Click
        Dim sT() As String, ip As String, port As Long, port2 As Long = 1
        If 主机IP文本.Text <> "" Then
            If 建立本地服务器按钮.Text = "建立本地服务器" Then
                If InStr(主机IP文本.Text, ":") > 0 Then
                    Try
                        sT = Split(主机IP文本.Text, ":")
                        ip = sT(0)
                        port = Val(sT(1))
                        If UBound(sT) > 1 Then
                            port2 = Val(sT(2))
                        End If
                    Catch ex As Exception
                        错误消息("解析主机地址失败,原因：" & ex.Message)
                        Exit Sub
                    End Try
                Else
                    ip = 主机IP文本.Text
                    port = "2333"
                End If
                建立本地服务器按钮.Text = "关闭本地服务器"
                Try
                    serverSK = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                    serverSK.Bind(New IPEndPoint(IPAddress.Parse(ip), port))
                    serverSK.Listen(3)
                    serverSK_S = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                    serverSK_S.Bind(New IPEndPoint(IPAddress.Parse(ip), port + port2))
                    serverSK_S.Listen(3)
                    serverSKThread = New Thread(AddressOf 服务器子套接字)
                    serverSKThread.Start()
                Catch ex As Exception
                    错误消息("建立本地服务器失败,原因：" & ex.Message)
                    建立本地服务器按钮.Text = "建立本地服务器"
                Finally
                    操作消息("本地服务器建立完毕，开放端口：" & port)
                    联机状态更新时钟.Enabled = True
                    连接按钮.Enabled = False
                End Try
            Else
                建立本地服务器按钮.Text = "建立本地服务器"
                Try
                    serverSK.Close()
                    serverSKThread.Abort()
                Catch ex As Exception

                End Try
                操作消息("本地服务器已关闭。")
                联机状态更新时钟.Enabled = False
                连接按钮.Enabled = True
            End If
        End If
    End Sub
    Private Function 获得存活终端位ID() As Long
        Dim i As Long
        For i = 1 To 3
            If 存活终端位ID(i) = False Then
                Return i
            End If
        Next
        Return 0
    End Function
    Private Sub 服务器子套接字()
        While (True)
            If 存活终端数量 < 3 Then
                存活终端数量 += 1
                Dim 存活id As Integer = 获得存活终端位ID()
                sCSK(存活id) = New 终端机 With {
                    .s = serverSK.Accept(),
                    .g = serverSK_S.Accept(),
                    .id = 存活id
                }
                存活终端位ID(sCSK(存活id).id) = True
                线程首ASC(sCSK(存活id).id) = 0
                线程信息(sCSK(存活id).id) = ""
                发送锁(sCSK(存活id).id) = False
                Dim skT = New Thread(AddressOf 服务器子套接字监听)
                skT.Start(sCSK(存活id))
            End If
        End While
    End Sub
    Private Function 获得日秒() As Long
        Dim t As TimeSpan
        t = Now().TimeOfDay
        Return t.Hours * 3600 + t.Minutes * 60 + t.Seconds
    End Function
    Private Sub 服务器子套接字监听(c As 终端机)
        Dim 延迟底 As Single, 延迟包已发送 As Boolean
        线程消息("子线程[" & c.id & "]已启动。")
        Dim 消息线程 As New Thread(AddressOf 服务器子套接字监听委托)
        数据接收线程存活(c.id) = True
        消息线程.Start(c)
        While (建立本地服务器按钮.Text = "关闭本地服务器" And 线程首ASC(c.id) <> 63 And 数据接收线程存活(c.id) = True)
            Try
                If 延迟包已发送 = False Then
                    延迟底 = 获得日秒()
                    Dim dtStartTime As DateTime = Now
                    Dim sendStr As String = "b" & dtStartTime & Chr(1)
                    While (发送锁(c.id))
                        Thread.Sleep(5)
                    End While
                    发送锁(c.id) = True
                    c.s.Send(Encoding.UTF8.GetBytes(sendStr))
                    发送锁(c.id) = False
                    延迟包已发送 = True
                Else
                    If 获得日秒() - 延迟底 >= 3 Then
                        延迟包已发送 = False
                    End If
                End If
            Catch ex As Exception
                错误消息("[" & c.id & "]:" & "服务器子套接字监听发生错误,原因：" & ex.Message)
                Exit While
            End Try

        End While
        存活终端数量 -= 1
        存活终端位ID(c.id) = False
        Try
            c.s.Close()
            c.g.Close()
            消息线程.Abort()
        Catch ex As Exception

        End Try
        线程消息("子线程[" & c.id & "]已关闭。")
    End Sub
    Private Sub 服务器子套接字监听委托(c As 终端机)
        Dim firstChr As String, sT() As String, 包时间 As DateTime
        Dim 冗余内容 As String = ""
        Try
            Dim sendStr As String = "n" & c.id & Chr(1)
            While (发送锁(c.id))
                Thread.Sleep(5)
            End While
            发送锁(c.id) = True
            c.s.Send(Encoding.UTF8.GetBytes(sendStr))
            发送锁(c.id) = False
        Catch ex As Exception
            错误消息("[" & c.id & "]:" & "发送终端ID失败,原因：" & ex.Message)
        End Try
        While (建立本地服务器按钮.Text = "关闭本地服务器" And 线程首ASC(c.id) <> 63)
            Try
                Dim bytes(1024) As Byte
                c.g.Receive(bytes)
                线程信息(c.id) = Replace(Encoding.UTF8.GetString(bytes), vbNullChar, "")
                sT = Split(线程信息(c.id), Chr(1))
                For i = 0 To UBound(sT) - 1
                    If i = 0 Then sT(0) = 冗余内容 & sT(0)
                    线程首ASC(c.id) = Asc(sT(i))
                    firstChr = Mid(sT(i), 1, 1)
                    Select Case firstChr
                        Case "i"
                            Dim sendStr As String = "[" & c.id & "]:" & Mid(sT(i), 2, sT(i).Length)
                            网络消息(sendStr)
                            sendStr = "i" & sendStr & Chr(1)
                            消息分发(sendStr)
                        Case "d"
                            同步操作转发()
                            同步操作()
                        Case "b"
                            包时间 = DateTime.Parse(Mid(sT(i), 2, sT(i).Length))
                            Dim dtEndTime As DateTime = Now
                            Dim a1 As TimeSpan = dtEndTime - 包时间
                            c.延迟 = a1.TotalMilliseconds \ 2
                            Dim sendStr As String = "y" & 获得终端延迟() & Chr(1)
                            While (发送锁(c.id))
                                Thread.Sleep(5)
                            End While
                            发送锁(c.id) = True
                            c.s.Send(Encoding.UTF8.GetBytes(sendStr))
                            发送锁(c.id) = False
                            '线程消息("子线程[" & c.id & "]延迟：" & sendStr)
                    End Select
                Next
                冗余内容 = sT(UBound(sT))
            Catch ex As Exception
                错误消息("[" & c.id & "]:" & "服务器子套接字监听委托发生错误,原因：" & ex.Message)
                Exit While
            End Try
        End While
        Try
            c.s.Close()
            c.g.Close()
        Catch ex As Exception

        End Try
        数据接收线程存活(c.id) = False
    End Sub
    Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Try
            serverSK.Close()
            serverSK_S.Close()
            serverSKThread.Abort()
        Catch ex As Exception

        End Try
        Try
            clientSK.Close()
            clientSK_S.Close()
            clientSKThread.Abort()
        Catch ex As Exception

        End Try
        UnRegisterHotKey(Handle, 1)
        UnRegisterHotKey(Handle, 2)
        UnRegisterHotKey(Handle, 3)
        UnRegisterHotKey(Handle, 4)
        UnRegisterHotKey(Handle, 5)
        End
    End Sub
    Private Function IP有效性验证(ipStr As String) As String
        If UCase(ipStr) = LCase(ipStr) Then
            Return True
        End If
        Return False
    End Function
    Private Sub 连接按钮_Click(sender As Object, e As EventArgs) Handles 连接按钮.Click
        Dim sT() As String, ip As String, port As Long, port2 As Long = 1
        If 连接按钮.Text = "连接" Then
            连接按钮.Text = "断开"
            If InStr(主机IP文本.Text, ":") > 0 Then
                Try
                    sT = Split(主机IP文本.Text, ":")
                    If IP有效性验证(sT(0)) Then
                        ip = sT(0)
                    Else
                        Dim ipHost As IPHostEntry = Dns.GetHostEntry(sT(0))
                        ip = ipHost.AddressList(0).ToString
                    End If
                    port = Val(sT(1))
                    If UBound(sT) > 1 Then
                        port2 = Val(sT(2))
                    End If
                Catch ex As Exception
                    错误消息("解析主机地址失败,原因：" & ex.Message)
                    Exit Sub
                End Try
            Else
                ip = 主机IP文本.Text
                port = "2333"
            End If
            Try
                clientSK = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                clientSK.Connect(New IPEndPoint(IPAddress.Parse(ip), port))
                clientSK_S = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                clientSK_S.Connect(New IPEndPoint(IPAddress.Parse(ip), port + port2))
                clientSKThread = New Thread(AddressOf 连接远程主机)
                clientSKThread.Start()
            Catch ex As Exception
                错误消息("连接主机失败,原因：" & ex.Message)
                连接按钮.Text = "连接"
            Finally
                操作消息("已连接远程主机。")
            End Try
        Else
            连接按钮.Text = "连接"
            Try
                clientSK.Close()
                clientSK_S.Close()
                clientSKThread.Abort()
            Catch ex As Exception

            Finally
                操作消息("已断开远程主机。")
            End Try
        End If
    End Sub

    Private Sub 连接远程主机()
        Dim 消息 As String, sT() As String, firstChr As String, infoASC As Integer, 冗余内容 As String = "", sendStr As String
        线程消息("子线程已启动。")
        单发送锁 = False
        While (连接按钮.Text = "断开" And infoASC <> 63)
            Try
                Dim bytes(1024) As Byte
                clientSK.Receive(bytes)
                消息 = Replace(Encoding.UTF8.GetString(bytes), vbNullChar, "")
                sT = Split(消息, Chr(1))
                For i = 0 To UBound(sT) - 1
                    If i = 0 Then sT(0) = 冗余内容 & sT(0)
                    infoASC = Asc(sT(i))
                    firstChr = Mid(sT(i), 1, 1)
                    Select Case firstChr
                        Case "i"
                            网络消息(Mid(sT(i), 2, sT(i).Length))
                        Case "d"
                            同步操作()
                        Case "b"
                            sendStr = sT(i) & Chr(1)
                            While (单发送锁)
                                Thread.Sleep(5)
                            End While
                            单发送锁 = True
                            clientSK_S.Send(Encoding.UTF8.GetBytes(sendStr))
                            单发送锁 = False
                        Case "y"
                            联机状态提示.Text = Mid(sT(i), 2, sT(i).Length)
                        Case "n"
                            网络消息("您的终端ID为：[" & Mid(sT(i), 2, 1) & "]")
                    End Select
                Next
                冗余内容 = sT(UBound(sT))
            Catch ex As Exception
                错误消息("与远程主机连接发生错误,原因：" & ex.Message)
                Exit While
            End Try
        End While
        clientSK.Close()
        clientSK_S.Close()
        线程消息("子线程已关闭。")
        连接按钮.Text = "连接"
    End Sub

    Private Sub 错误消息(s As String)
        Try
            消息文本.SelectionStart = Len(消息文本.Text)
            消息文本.SelectedText = Now() & " [错误] " & s & vbCrLf
        Catch ex As Exception
            Debug.Print("错误消息错误，原因：" & ex.Message)
        End Try
    End Sub
    Private Sub 网络消息(s As String)
        Try
            消息文本.SelectionStart = Len(消息文本.Text)
            消息文本.SelectedText = Now() & " [消息] " & s & vbCrLf
        Catch ex As Exception
            Debug.Print("网络消息错误，原因：" & ex.Message)
        End Try
    End Sub
    Private Sub 操作消息(s As String)
        Try
            消息文本.SelectionStart = Len(消息文本.Text)
            消息文本.SelectedText = Now() & " [操作] " & s & vbCrLf
        Catch ex As Exception
            Debug.Print("操作消息错误，原因：" & ex.Message)
        End Try
    End Sub
    Private Sub 线程消息(s As String)
        Try
            消息文本.SelectionStart = Len(消息文本.Text)
            消息文本.SelectedText = Now() & " [线程] " & s & vbCrLf
        Catch ex As Exception
            Debug.Print("线程消息错误，原因：" & ex.Message)
        End Try
    End Sub
    Private Sub 联网同步执行操作按钮_Click(sender As Object, e As EventArgs) Handles 联网同步执行操作按钮.Click
        If 建立本地服务器按钮.Text = "关闭本地服务器" Then
            同步操作转发()
            同步操作()
        ElseIf 连接按钮.Text = "断开" Then
            Try
                While (单发送锁)
                    Thread.Sleep(5)
                End While
                单发送锁 = True
                clientSK_S.Send(Encoding.UTF8.GetBytes(同步操作讯息))
                单发送锁 = False
            Catch ex As Exception
                错误消息("同步操作发送给主机失败，原因：" & ex.Message)
            End Try
            同步操作()
        End If
    End Sub
    Private Sub 消息分发(消息 As String)
        Dim i As Long
        For i = 1 To UBound(sCSK)
            Try
                If 存活终端位ID(i) Then
                    While (发送锁(i))
                        Thread.Sleep(5)
                    End While
                    发送锁(i) = True
                    sCSK(i).s.Send(Encoding.UTF8.GetBytes(消息))
                    发送锁(i) = False
                End If
            Catch ex As Exception
                错误消息("消息分发给[" & sCSK(i).id & "失败，原因：" & ex.Message)
            End Try
        Next
    End Sub

    Private Sub 无敌时钟_Tick(sender As Object, e As EventArgs) Handles 无敌时钟.Tick
        金刚不坏身()
    End Sub

    Private Sub 同步操作转发()
        Dim i As Long
        For i = 1 To UBound(sCSK)
            Try
                If 存活终端位ID(i) Then
                    While (发送锁(i))
                        Thread.Sleep(5)
                    End While
                    发送锁(i) = True
                    sCSK(i).s.Send(Encoding.UTF8.GetBytes(同步操作讯息))
                    发送锁(i) = False
                End If
            Catch ex As Exception
                错误消息("同步操作发送给[" & sCSK(i).id & "失败，原因：" & ex.Message)
            End Try
        Next
    End Sub
    Private Sub 同步操作()
        If 执行断网操作选项.Checked Then
            断开本地网络按钮_Click(Nothing, Nothing)
        End If
        If 执行杀进程操作选项.Checked Then
            关闭游戏进程按钮_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub 联机状态更新时钟_Tick(sender As Object, e As EventArgs) Handles 联机状态更新时钟.Tick
        联机状态提示.Text = 获得终端延迟()
    End Sub
    Private Function 获得终端延迟() As String
        Dim i As Long, visStr As String = ""
        For i = 1 To UBound(sCSK)
            If 存活终端位ID(i) Then
                visStr = visStr & "[" & i & "]" & sCSK(i).延迟 & "-"
            End If
        Next
        If visStr.Length > 0 Then
            visStr = Mid(visStr, 1, visStr.Length - 1)
        End If
        Return visStr
    End Function
    Private Function 获得防弹衣地址() As 内存地址
        Dim pid As Integer, r As New 内存地址
        pid = Val(ProcessPidOnly("GTA5"))
        If pid > 0 Then
            r.句柄 = getHandle(pid)
            Try
                r.地址 = readAddress(r.句柄, GTA5基址 + &H1F3B2B0)
                r.地址 += &H8
                r.地址 = readAddress(r.句柄, r.地址)
                r.地址 += &H18
                r.地址 = readAddress(r.句柄, r.地址)
                r.地址 = readAddress(r.句柄, r.地址)
                r.地址 += &H6C8
            Catch ex As Exception
                GTA5基址 = 0
            Finally
                r.有效 = True
            End Try
        End If
        Return r
    End Function
    Private Function 获得警星地址() As 内存地址
        Dim pid As Integer, r As New 内存地址
        pid = Val(ProcessPidOnly("GTA5"))
        If pid > 0 Then
            r.句柄 = getHandle(pid)
            Try
                r.地址 = readAddress(r.句柄, GTA5基址 + &H252E438)
                r.地址 += &H5D8
            Catch ex As Exception
                GTA5基址 = 0
            Finally
                r.有效 = True
            End Try
        End If
        Return r
    End Function
    Private Function 获得生命地址() As 内存地址
        Dim pid As Integer, r As New 内存地址
        pid = Val(ProcessPidOnly("GTA5"))
        If pid > 0 Then
            r.句柄 = getHandle(pid)
            Try
                r.地址 = readAddress(r.句柄, GTA5基址 + &H1C48E80)
                r.地址 += &H280
            Catch ex As Exception
                GTA5基址 = 0
            Finally
                r.有效 = True
            End Try
        End If
        Return r
    End Function
    Public Function ProcessPidOnly(ByVal ProcessName As String) As String
        Dim myProcess As Process() = Process.GetProcessesByName(ProcessName)
        Dim mp() As Process
        mp = Process.GetProcesses()
        Dim pid As String = ""
        If myProcess.Length - 1 = 0 Then
            pid = myProcess(0).Id
        Else
            For i As Short = 0 To myProcess.Length - 1
                pid = pid & myProcess(i).Id & ";"
            Next
        End If
        If GTA5基址 = 0 Then
            For Each i In mp
                If i.ProcessName = ProcessName Then
                    GTA5基址 = i.Modules(0).BaseAddress.ToInt64
                    Exit For
                End If
            Next
        End If
        Return pid
    End Function

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        同步操作讯息 = "d" & Chr(1)
        Control.CheckForIllegalCrossThreadCalls = False
        RegisterHotKey(Handle, 1, 2, Keys.F1)
        RegisterHotKey(Handle, 2, 0, Keys.F1)
        RegisterHotKey(Handle, 3, 0, Keys.F2)
        RegisterHotKey(Handle, 4, 0, Keys.F3)
        RegisterHotKey(Handle, 5, 0, Keys.F4)
    End Sub
    Private Sub 命令行_KeyDown(sender As Object, e As KeyEventArgs) Handles 命令行.KeyDown
        If e.KeyCode = Keys.Enter Then
            If 连接按钮.Text = "断开" Then
                Try
                    Dim sendStr As String = "i" & Replace(命令行.Text, vbCrLf, "") & Chr(1)
                    While (单发送锁)
                        Thread.Sleep(5)
                    End While
                    单发送锁 = True
                    clientSK_S.Send(Encoding.UTF8.GetBytes(sendStr))
                    单发送锁 = False
                Catch ex As Exception
                    错误消息("发送给主机消息失败，原因：" & ex.Message)
                End Try
            ElseIf 建立本地服务器按钮.Text = "关闭本地服务器" Then
                Dim sendStr As String = "[0]:" & Replace(命令行.Text, vbCrLf, "")
                网络消息(sendStr)
                sendStr = "i" & sendStr & Chr(1)
                消息分发(sendStr)
            End If
            命令行.Text = ""
        End If
    End Sub
    Private Sub 命令行_KeyPress(sender As Object, e As KeyPressEventArgs) Handles 命令行.KeyPress
        If e.KeyChar = vbCrLf Then
            e.KeyChar = ""
        End If
    End Sub
End Class
Public Class 终端机
    Public s As Socket, g As Socket
    Public id As Integer, 延迟 As Integer
End Class
Public Class 内存地址
    Public 有效 As Boolean
    Public 句柄 As IntPtr
    Public 地址 As IntPtr
End Class
Module ModMem
    Public Declare Function OpenProcess Lib "kernel32" (ByVal dwDesiredAccess As Integer, ByVal bInheritHandle As Integer, ByVal dwProcessId As Integer) As Integer
    <DllImport("kernel32.dll", SetLastError:=True)>
    Public Function ReadProcessMemory(
      ByVal hProcess As IntPtr,
      ByVal lpBaseAddress As IntPtr,
      <Out()> ByVal lpBuffer As Byte(),
      ByVal dwSize As Integer,
      ByRef lpNumberOfBytesRead As Integer) As Boolean
    End Function
    <DllImport("kernel32.dll", SetLastError:=True)>
    Public Function WriteProcessMemory(
        ByVal hProcess As IntPtr,
        ByVal lpBaseAddress As IntPtr,
        <MarshalAs(UnmanagedType.AsAny)> ByVal lpBuffer As Object,
        ByVal nSize As System.UInt32,
        <Out()> ByRef lpNumberOfBytesWritten As Int32) As Boolean
    End Function
    <DllImport("kernel32.dll", SetLastError:=True)>
    Public Function WriteProcessMemory(
        ByVal hProcess As IntPtr,
        ByVal lpBaseAddress As IntPtr,
        ByVal lpBuffer As Byte(),
        ByVal nSize As System.UInt32,
        <Out()> ByRef lpNumberOfBytesWritten As Int32) As Boolean
    End Function
    Public Declare Function CloseHandle Lib "kernel32" (ByVal hObject As Integer) As Long
    Public Const PROCESS_VM_READ As Long = &H10
    Public Const PROCESS_ALL_ACCESS = &H1F0FFF
    Public Function readAddress(ByVal handle As Integer, ByVal address As Long) As Long
        Dim h As Integer
        Dim buffer(11) As Byte
        If handle > 0 Then
            h = ReadProcessMemory(handle, address, buffer, buffer.Length, 0&)
            Return BitConverter.ToInt64(buffer, 0)
        End If
        Return 0
    End Function
    Public Function readSingle(ByVal handle As Integer, ByVal tmpAddr As Long) As Single
        Dim h As Integer
        Dim buffer(4) As Byte
        Dim bytesRead As Integer
        Dim strHex As String = "", strTmp As String = "", ret As Single
        If handle > 0 Then
            h = ReadProcessMemory(handle, tmpAddr, buffer, 4, bytesRead)
            ret = BitConverter.ToSingle(buffer, 0)
            Return ret
        End If
        Return 0
    End Function
    Public Function getHandle(ByVal PID As Integer) As Integer
        Dim ph As Integer
        ph = OpenProcess(PROCESS_ALL_ACCESS, 0, PID)
        If ph > 0 Then Return ph
        Return -1
    End Function
End Module
