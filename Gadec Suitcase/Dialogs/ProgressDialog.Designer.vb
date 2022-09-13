<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ProgressDialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ProgressDialog))
        Me.OutputProgressBar = New System.Windows.Forms.ProgressBar()
        Me.OutputTextBox = New System.Windows.Forms.TextBox()
        Me.ltClose = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'OutputProgressBar
        '
        Me.OutputProgressBar.Location = New System.Drawing.Point(12, 12)
        Me.OutputProgressBar.Name = "OutputProgressBar"
        Me.OutputProgressBar.Size = New System.Drawing.Size(626, 10)
        Me.OutputProgressBar.TabIndex = 1
        '
        'OutputTextBox
        '
        Me.OutputTextBox.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OutputTextBox.Location = New System.Drawing.Point(12, 28)
        Me.OutputTextBox.Multiline = True
        Me.OutputTextBox.Name = "OutputTextBox"
        Me.OutputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.OutputTextBox.Size = New System.Drawing.Size(626, 311)
        Me.OutputTextBox.TabIndex = 2
        Me.OutputTextBox.WordWrap = False
        '
        'ltClose
        '
        Me.ltClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ltClose.Location = New System.Drawing.Point(563, 360)
        Me.ltClose.Name = "ltClose"
        Me.ltClose.Size = New System.Drawing.Size(75, 23)
        Me.ltClose.TabIndex = 15
        Me.ltClose.Text = "XXX"
        Me.ltClose.UseVisualStyleBackColor = True
        '
        'ProgressDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.ltClose
        Me.ClientSize = New System.Drawing.Size(650, 351)
        Me.Controls.Add(Me.ltClose)
        Me.Controls.Add(Me.OutputTextBox)
        Me.Controls.Add(Me.OutputProgressBar)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ProgressDialog"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "XXX"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OutputProgressBar As System.Windows.Forms.ProgressBar
    Friend WithEvents OutputTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ltClose As Button
End Class
