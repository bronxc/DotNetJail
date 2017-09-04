<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.tbLog = New System.Windows.Forms.TextBox()
        Me.imJail = New System.Windows.Forms.PictureBox()
        Me.lblDrop = New System.Windows.Forms.Label()
        CType(Me.imJail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tbLog
        '
        Me.tbLog.BackColor = System.Drawing.Color.White
        Me.tbLog.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbLog.Location = New System.Drawing.Point(12, 236)
        Me.tbLog.Multiline = True
        Me.tbLog.Name = "tbLog"
        Me.tbLog.ReadOnly = True
        Me.tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.tbLog.Size = New System.Drawing.Size(612, 274)
        Me.tbLog.TabIndex = 0
        Me.tbLog.WordWrap = False
        '
        'imJail
        '
        Me.imJail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.imJail.Image = CType(resources.GetObject("imJail.Image"), System.Drawing.Image)
        Me.imJail.Location = New System.Drawing.Point(120, 50)
        Me.imJail.Name = "imJail"
        Me.imJail.Size = New System.Drawing.Size(386, 180)
        Me.imJail.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imJail.TabIndex = 2
        Me.imJail.TabStop = False
        '
        'lblDrop
        '
        Me.lblDrop.AutoSize = True
        Me.lblDrop.Font = New System.Drawing.Font("Constantia", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDrop.Location = New System.Drawing.Point(243, 21)
        Me.lblDrop.Name = "lblDrop"
        Me.lblDrop.Size = New System.Drawing.Size(141, 26)
        Me.lblDrop.TabIndex = 1
        Me.lblDrop.Text = "Drop in Here"
        '
        'frmMain
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(637, 519)
        Me.Controls.Add(Me.tbLog)
        Me.Controls.Add(Me.lblDrop)
        Me.Controls.Add(Me.imJail)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Jail Dropper"
        CType(Me.imJail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tbLog As System.Windows.Forms.TextBox
    Friend WithEvents lblDrop As System.Windows.Forms.Label
    Friend WithEvents imJail As System.Windows.Forms.PictureBox

End Class
