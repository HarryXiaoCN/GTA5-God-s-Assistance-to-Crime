<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.断开本地网络按钮 = New System.Windows.Forms.Button()
        Me.关闭游戏进程按钮 = New System.Windows.Forms.Button()
        Me.建立本地服务器按钮 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.主机IP文本 = New System.Windows.Forms.TextBox()
        Me.执行断网操作选项 = New System.Windows.Forms.CheckBox()
        Me.执行杀进程操作选项 = New System.Windows.Forms.CheckBox()
        Me.联网同步执行操作按钮 = New System.Windows.Forms.Button()
        Me.消息文本 = New System.Windows.Forms.TextBox()
        Me.联机状态提示 = New System.Windows.Forms.Label()
        Me.连接按钮 = New System.Windows.Forms.Button()
        Me.联机状态更新时钟 = New System.Windows.Forms.Timer(Me.components)
        Me.命令行 = New System.Windows.Forms.TextBox()
        Me.无敌时钟 = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        '断开本地网络按钮
        '
        Me.断开本地网络按钮.Location = New System.Drawing.Point(12, 12)
        Me.断开本地网络按钮.Name = "断开本地网络按钮"
        Me.断开本地网络按钮.Size = New System.Drawing.Size(176, 34)
        Me.断开本地网络按钮.TabIndex = 0
        Me.断开本地网络按钮.Text = "断开本地网络"
        Me.断开本地网络按钮.UseVisualStyleBackColor = True
        '
        '关闭游戏进程按钮
        '
        Me.关闭游戏进程按钮.Location = New System.Drawing.Point(194, 12)
        Me.关闭游戏进程按钮.Name = "关闭游戏进程按钮"
        Me.关闭游戏进程按钮.Size = New System.Drawing.Size(165, 34)
        Me.关闭游戏进程按钮.TabIndex = 1
        Me.关闭游戏进程按钮.Text = "关闭游戏进程"
        Me.关闭游戏进程按钮.UseVisualStyleBackColor = True
        '
        '建立本地服务器按钮
        '
        Me.建立本地服务器按钮.Location = New System.Drawing.Point(12, 52)
        Me.建立本地服务器按钮.Name = "建立本地服务器按钮"
        Me.建立本地服务器按钮.Size = New System.Drawing.Size(347, 34)
        Me.建立本地服务器按钮.TabIndex = 2
        Me.建立本地服务器按钮.Text = "建立本地服务器"
        Me.建立本地服务器按钮.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 99)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 12)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "主机IP："
        '
        '主机IP文本
        '
        Me.主机IP文本.Location = New System.Drawing.Point(74, 96)
        Me.主机IP文本.Name = "主机IP文本"
        Me.主机IP文本.Size = New System.Drawing.Size(225, 21)
        Me.主机IP文本.TabIndex = 4
        Me.主机IP文本.Text = "127.0.0.1:2333"
        '
        '执行断网操作选项
        '
        Me.执行断网操作选项.AutoSize = True
        Me.执行断网操作选项.Checked = True
        Me.执行断网操作选项.CheckState = System.Windows.Forms.CheckState.Checked
        Me.执行断网操作选项.Location = New System.Drawing.Point(17, 123)
        Me.执行断网操作选项.Name = "执行断网操作选项"
        Me.执行断网操作选项.Size = New System.Drawing.Size(96, 16)
        Me.执行断网操作选项.TabIndex = 5
        Me.执行断网操作选项.Text = "执行断网操作"
        Me.执行断网操作选项.UseVisualStyleBackColor = True
        '
        '执行杀进程操作选项
        '
        Me.执行杀进程操作选项.AutoSize = True
        Me.执行杀进程操作选项.Checked = True
        Me.执行杀进程操作选项.CheckState = System.Windows.Forms.CheckState.Checked
        Me.执行杀进程操作选项.Location = New System.Drawing.Point(119, 123)
        Me.执行杀进程操作选项.Name = "执行杀进程操作选项"
        Me.执行杀进程操作选项.Size = New System.Drawing.Size(108, 16)
        Me.执行杀进程操作选项.TabIndex = 6
        Me.执行杀进程操作选项.Text = "执行杀进程操作"
        Me.执行杀进程操作选项.UseVisualStyleBackColor = True
        '
        '联网同步执行操作按钮
        '
        Me.联网同步执行操作按钮.BackColor = System.Drawing.Color.LightCoral
        Me.联网同步执行操作按钮.Font = New System.Drawing.Font("宋体", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.联网同步执行操作按钮.Location = New System.Drawing.Point(12, 145)
        Me.联网同步执行操作按钮.Name = "联网同步执行操作按钮"
        Me.联网同步执行操作按钮.Size = New System.Drawing.Size(344, 68)
        Me.联网同步执行操作按钮.TabIndex = 7
        Me.联网同步执行操作按钮.Text = "联网同步执行操作"
        Me.联网同步执行操作按钮.UseVisualStyleBackColor = False
        '
        '消息文本
        '
        Me.消息文本.Location = New System.Drawing.Point(13, 223)
        Me.消息文本.Multiline = True
        Me.消息文本.Name = "消息文本"
        Me.消息文本.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.消息文本.Size = New System.Drawing.Size(345, 201)
        Me.消息文本.TabIndex = 8
        Me.消息文本.Text = "Ctrl+F1 触发联网同步执行操作" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "F1 加血" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "F2 换防弹衣" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "F3 金刚不坏身" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "F4 神隐" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.消息文本.WordWrap = False
        '
        '联机状态提示
        '
        Me.联机状态提示.AutoSize = True
        Me.联机状态提示.Location = New System.Drawing.Point(10, 456)
        Me.联机状态提示.Name = "联机状态提示"
        Me.联机状态提示.Size = New System.Drawing.Size(0, 12)
        Me.联机状态提示.TabIndex = 9
        '
        '连接按钮
        '
        Me.连接按钮.Location = New System.Drawing.Point(308, 95)
        Me.连接按钮.Name = "连接按钮"
        Me.连接按钮.Size = New System.Drawing.Size(51, 22)
        Me.连接按钮.TabIndex = 10
        Me.连接按钮.Text = "连接"
        Me.连接按钮.UseVisualStyleBackColor = True
        '
        '联机状态更新时钟
        '
        Me.联机状态更新时钟.Interval = 1000
        '
        '命令行
        '
        Me.命令行.Location = New System.Drawing.Point(12, 433)
        Me.命令行.MaxLength = 20
        Me.命令行.Multiline = True
        Me.命令行.Name = "命令行"
        Me.命令行.Size = New System.Drawing.Size(346, 21)
        Me.命令行.TabIndex = 11
        '
        '无敌时钟
        '
        Me.无敌时钟.Interval = 1000
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(371, 467)
        Me.Controls.Add(Me.命令行)
        Me.Controls.Add(Me.连接按钮)
        Me.Controls.Add(Me.联机状态提示)
        Me.Controls.Add(Me.断开本地网络按钮)
        Me.Controls.Add(Me.消息文本)
        Me.Controls.Add(Me.联网同步执行操作按钮)
        Me.Controls.Add(Me.执行杀进程操作选项)
        Me.Controls.Add(Me.执行断网操作选项)
        Me.Controls.Add(Me.主机IP文本)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.建立本地服务器按钮)
        Me.Controls.Add(Me.关闭游戏进程按钮)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.Text = "犯罪之神辅助[By:HarryXiaoCN]"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents 断开本地网络按钮 As Button
    Friend WithEvents 关闭游戏进程按钮 As Button
    Friend WithEvents 建立本地服务器按钮 As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents 主机IP文本 As TextBox
    Friend WithEvents 联网同步执行操作按钮 As Button
    Friend WithEvents 消息文本 As TextBox
    Friend WithEvents 联机状态提示 As Label
    Friend WithEvents 连接按钮 As Button
    Friend WithEvents 执行断网操作选项 As CheckBox
    Friend WithEvents 执行杀进程操作选项 As CheckBox
    Friend WithEvents 联机状态更新时钟 As Timer
    Friend WithEvents 命令行 As TextBox
    Friend WithEvents 无敌时钟 As Timer
End Class
