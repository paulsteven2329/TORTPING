Imports System.Net.NetworkInformation
Imports System.Diagnostics
Imports System.Management

Public Class tool
    Dim r As Timer = New Timer()
    Dim w As Integer = 300
    Dim h As Integer = 300
    Dim HAND As Integer = 150
    Dim u As Integer
    Dim cx As Integer, cy As Integer
    Dim x As Integer, y As Integer
    Dim tx As Integer, ty As Integer, lim As Integer = 20
    Dim bmp As Bitmap
    Dim p As Pen
    Dim g As Graphics

    'date and time
    Dim currentTime As DateTime = DateTime.Now

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form2.Show()
        Me.Close()

    End Sub

    Private t As Timer = New Timer()

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub

    Private Sub tool_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        radar()
        Label1.Text = My.Computer.Name
        Label2.Text = My.Computer.Info.OSFullName
        Label3.Text = My.Computer.Info.OSPlatform
        Label4.Text = My.Computer.Info.OSVersion
        Label5.Text = SystemInformation.UserName
        Label6.Text = ("MONITOR HEIGHT") & SystemInformation.PrimaryMonitorSize.Height
        Label7.Text = ("MONITOR WIDTH") & SystemInformation.PrimaryMonitorSize.Width
        Label8.Text = ("PHYSICAL MEMORY") & My.Computer.Info.AvailablePhysicalMemory
        ipnet()

        ' Retrieve and display processor information
        Dim processorQuery As New SelectQuery("Win32_Processor")
        Dim processorSearcher As New ManagementObjectSearcher(processorQuery)
        For Each processor As ManagementObject In processorSearcher.Get()
            Label11.Text &= "Processor: " & processor("Name").ToString() & vbCrLf
            Label11.Text &= "Manufacturer: " & processor("Manufacturer").ToString() & vbCrLf
            Label11.Text &= "Architecture: " & processor("Architecture").ToString() & vbCrLf
            Label11.Text &= "Current Clock Speed: " & processor("CurrentClockSpeed").ToString() & " MHz" & vbCrLf
            Label11.Text &= "Number of Cores: " & processor("NumberOfCores").ToString() & vbCrLf
            Label11.Text &= "----------------------------------------" & vbCrLf
        Next

        ' Retrieve and display memory information
        Dim memoryQuery As New SelectQuery("Win32_PhysicalMemory")
        Dim memorySearcher As New ManagementObjectSearcher(memoryQuery)
        For Each memory As ManagementObject In memorySearcher.Get()
            Label11.Text &= "Memory Capacity: " & CLng(memory("Capacity")) / (1024 * 1024) & " MB" & vbCrLf
            Label11.Text &= "Speed: " & memory("Speed").ToString() & " MHz" & vbCrLf
            Label11.Text &= "Manufacturer: " & memory("Manufacturer").ToString() & vbCrLf
            Label11.Text &= "----------------------------------------" & vbCrLf
        Next

        ' Retrieve and display disk drive information
        Dim driveQuery As New SelectQuery("Win32_DiskDrive")
        Dim driveSearcher As New ManagementObjectSearcher(driveQuery)
        For Each drive As ManagementObject In driveSearcher.Get()
            Label11.Text &= "Drive: " & drive("Caption").ToString() & vbCrLf
            Label11.Text &= "Capacity: " & CLng(drive("Size")) / (1024 * 1024 * 1024) & " GB" & vbCrLf
            Label11.Text &= "Interface Type: " & drive("InterfaceType").ToString() & vbCrLf
            Label11.Text &= "Manufacturer: " & drive("Manufacturer").ToString() & vbCrLf
            Label11.Text &= "----------------------------------------" & vbCrLf
        Next

        ' Display the time and date in a matrix-style format
        Label12.Text = String.Format("{0:00}:{1:00}:{2:00}", currentTime.Hour, currentTime.Minute, currentTime.Second)
        Label13.Text = String.Format("{0:00}/{1:00}/{2:0000}", currentTime.Day, currentTime.Month, currentTime.Year)

    End Sub

    Private Sub radar()
        bmp = New Bitmap(w + 1, h + 1)
        Me.BackColor = Color.Black
        cx = w / 2
        cy = h / 2
        u = 0
        r.Interval = 5
        AddHandler r.Tick, AddressOf radar_Tick
        r.Start()
    End Sub

    Private Sub radar_Tick(sender As Object, e As EventArgs)
        p = New Pen(Color.DarkGreen, 1.0F)
        g = Graphics.FromImage(bmp)
        Dim tu As Integer = (u - lim) Mod 360

        If u >= 0 AndAlso u <= 180 Then
            x = cx + CInt(HAND * Math.Sin(Math.PI * u / 180))
            y = cy - CInt(HAND * Math.Cos(Math.PI * u / 180))
        Else
            x = cx - CInt(HAND * -Math.Sin(Math.PI * u / 180))
            y = cy - CInt(HAND * Math.Cos(Math.PI * u / 180))
        End If

        If tu >= 0 AndAlso tu <= 180 Then
            tx = cx + CInt(HAND * Math.Sin(Math.PI * tu / 180))
            ty = cy - CInt(HAND * Math.Cos(Math.PI * tu / 180))
        Else
            tx = cx - CInt(HAND * -Math.Sin(Math.PI * tu / 180))
            ty = cy - CInt(HAND * Math.Cos(Math.PI * tu / 180))
        End If

        g.DrawEllipse(p, 0, 0, w, h)
        g.DrawEllipse(p, 80, 80, w - 160, h - 160)
        g.DrawLine(p, New Point(cx, 0), New Point(cx, h))
        g.DrawLine(p, New Point(0, cy), New Point(w, cy))
        g.DrawLine(New Pen(Color.Black, 1.0F), New Point(cx, cy), New Point(tx, ty))
        g.DrawLine(p, New Point(cx, cy), New Point(x, y))
        PictureBox1.Image = bmp
        p.Dispose()
        g.Dispose()

        u += 1
        If u = 360 Then
            u = 0
        End If
    End Sub

    Private Sub ipnet()


        Dim host As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName())
            Dim ipAddress As String = ""
            Dim macAddress As String = ""

            ' Get the first IPv4 address for this machine
            For Each ip As System.Net.IPAddress In host.AddressList
                If ip.AddressFamily = Net.Sockets.AddressFamily.InterNetwork Then
                    ipAddress = ip.ToString()
                    Exit For
                End If
            Next

            ' Get the MAC address of the first network interface
            Dim networkInterfaces() As NetworkInterface = NetworkInterface.GetAllNetworkInterfaces()
            Dim adapter As NetworkInterface = networkInterfaces(0)
            Dim macBytes As Byte() = adapter.GetPhysicalAddress().GetAddressBytes()
            For i As Integer = 0 To macBytes.Length - 1
                macAddress += macBytes(i).ToString("X2")
                If i <> macBytes.Length - 1 Then
                    macAddress += "-"
                End If
            Next

        Label9.Text = ("IP Address: " & ipAddress)
        Label10.Text = ("MAC Address: " & macAddress)

        Console.ReadLine()
        End Sub




End Class
