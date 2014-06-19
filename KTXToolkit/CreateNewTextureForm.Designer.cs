namespace KTXToolkit
{
    partial class CreateNewTextureForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBoxTextureType = new System.Windows.Forms.GroupBox();
            this.radioButtonTextureCubeMapArray = new System.Windows.Forms.RadioButton();
            this.radioButtonTexture2DArray = new System.Windows.Forms.RadioButton();
            this.radioButtonTexture1DArray = new System.Windows.Forms.RadioButton();
            this.radioButtonTextureCubeMap = new System.Windows.Forms.RadioButton();
            this.radioButtonTexture3D = new System.Windows.Forms.RadioButton();
            this.radioButtonTexture2D = new System.Windows.Forms.RadioButton();
            this.radioButtontexture1D = new System.Windows.Forms.RadioButton();
            this.groupBoxDimensions = new System.Windows.Forms.GroupBox();
            this.labelMipMapLevels = new System.Windows.Forms.Label();
            this.labelArrayLayers = new System.Windows.Forms.Label();
            this.labelDepth = new System.Windows.Forms.Label();
            this.labelHeight = new System.Windows.Forms.Label();
            this.labelWidth = new System.Windows.Forms.Label();
            this.groupBoxTextureFormat = new System.Windows.Forms.GroupBox();
            this.comboBoxDataType = new System.Windows.Forms.ComboBox();
            this.labelDataType = new System.Windows.Forms.Label();
            this.comboBoxDataFormat = new System.Windows.Forms.ComboBox();
            this.labelDataFormat = new System.Windows.Forms.Label();
            this.comboBoxInternalFormat = new System.Windows.Forms.ComboBox();
            this.labelInternalFormat = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonCalculateMipLength = new System.Windows.Forms.Button();
            this.textBoxMipMaps = new KTXToolkit.NumericTextBox();
            this.textBoxArrayLayers = new KTXToolkit.NumericTextBox();
            this.textBoxDepth = new KTXToolkit.NumericTextBox();
            this.textBoxHeight = new KTXToolkit.NumericTextBox();
            this.textBoxWidth = new KTXToolkit.NumericTextBox();
            this.groupBoxTextureType.SuspendLayout();
            this.groupBoxDimensions.SuspendLayout();
            this.groupBoxTextureFormat.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxTextureType
            // 
            this.groupBoxTextureType.Controls.Add(this.radioButtonTextureCubeMapArray);
            this.groupBoxTextureType.Controls.Add(this.radioButtonTexture2DArray);
            this.groupBoxTextureType.Controls.Add(this.radioButtonTexture1DArray);
            this.groupBoxTextureType.Controls.Add(this.radioButtonTextureCubeMap);
            this.groupBoxTextureType.Controls.Add(this.radioButtonTexture3D);
            this.groupBoxTextureType.Controls.Add(this.radioButtonTexture2D);
            this.groupBoxTextureType.Controls.Add(this.radioButtontexture1D);
            this.groupBoxTextureType.Location = new System.Drawing.Point(13, 13);
            this.groupBoxTextureType.Name = "groupBoxTextureType";
            this.groupBoxTextureType.Size = new System.Drawing.Size(291, 121);
            this.groupBoxTextureType.TabIndex = 0;
            this.groupBoxTextureType.TabStop = false;
            this.groupBoxTextureType.Text = "Texture Type";
            // 
            // radioButtonTextureCubeMapArray
            // 
            this.radioButtonTextureCubeMapArray.AutoSize = true;
            this.radioButtonTextureCubeMapArray.Location = new System.Drawing.Point(134, 92);
            this.radioButtonTextureCubeMapArray.Name = "radioButtonTextureCubeMapArray";
            this.radioButtonTextureCubeMapArray.Size = new System.Drawing.Size(140, 17);
            this.radioButtonTextureCubeMapArray.TabIndex = 6;
            this.radioButtonTextureCubeMapArray.TabStop = true;
            this.radioButtonTextureCubeMapArray.Text = "Texture Cube Map Array";
            this.radioButtonTextureCubeMapArray.UseVisualStyleBackColor = true;
            this.radioButtonTextureCubeMapArray.CheckedChanged += new System.EventHandler(this.radioButtonTextureCubeMapArray_CheckedChanged);
            // 
            // radioButtonTexture2DArray
            // 
            this.radioButtonTexture2DArray.AutoSize = true;
            this.radioButtonTexture2DArray.Location = new System.Drawing.Point(134, 44);
            this.radioButtonTexture2DArray.Name = "radioButtonTexture2DArray";
            this.radioButtonTexture2DArray.Size = new System.Drawing.Size(105, 17);
            this.radioButtonTexture2DArray.TabIndex = 5;
            this.radioButtonTexture2DArray.TabStop = true;
            this.radioButtonTexture2DArray.Text = "Texture 2D Array";
            this.radioButtonTexture2DArray.UseVisualStyleBackColor = true;
            this.radioButtonTexture2DArray.CheckedChanged += new System.EventHandler(this.radioButtonTexture2DArray_CheckedChanged);
            // 
            // radioButtonTexture1DArray
            // 
            this.radioButtonTexture1DArray.AutoSize = true;
            this.radioButtonTexture1DArray.Location = new System.Drawing.Point(134, 20);
            this.radioButtonTexture1DArray.Name = "radioButtonTexture1DArray";
            this.radioButtonTexture1DArray.Size = new System.Drawing.Size(105, 17);
            this.radioButtonTexture1DArray.TabIndex = 4;
            this.radioButtonTexture1DArray.TabStop = true;
            this.radioButtonTexture1DArray.Text = "Texture 1D Array";
            this.radioButtonTexture1DArray.UseVisualStyleBackColor = true;
            this.radioButtonTexture1DArray.CheckedChanged += new System.EventHandler(this.radioButtonTexture1DArray_CheckedChanged);
            // 
            // radioButtonTextureCubeMap
            // 
            this.radioButtonTextureCubeMap.AutoSize = true;
            this.radioButtonTextureCubeMap.Location = new System.Drawing.Point(7, 92);
            this.radioButtonTextureCubeMap.Name = "radioButtonTextureCubeMap";
            this.radioButtonTextureCubeMap.Size = new System.Drawing.Size(113, 17);
            this.radioButtonTextureCubeMap.TabIndex = 3;
            this.radioButtonTextureCubeMap.TabStop = true;
            this.radioButtonTextureCubeMap.Text = "Texture Cube Map";
            this.radioButtonTextureCubeMap.UseVisualStyleBackColor = true;
            this.radioButtonTextureCubeMap.CheckedChanged += new System.EventHandler(this.radioButtonTextureCubeMap_CheckedChanged);
            // 
            // radioButtonTexture3D
            // 
            this.radioButtonTexture3D.AutoSize = true;
            this.radioButtonTexture3D.Location = new System.Drawing.Point(7, 68);
            this.radioButtonTexture3D.Name = "radioButtonTexture3D";
            this.radioButtonTexture3D.Size = new System.Drawing.Size(78, 17);
            this.radioButtonTexture3D.TabIndex = 2;
            this.radioButtonTexture3D.TabStop = true;
            this.radioButtonTexture3D.Text = "Texture 3D";
            this.radioButtonTexture3D.UseVisualStyleBackColor = true;
            this.radioButtonTexture3D.CheckedChanged += new System.EventHandler(this.radioButtonTexture3D_CheckedChanged);
            // 
            // radioButtonTexture2D
            // 
            this.radioButtonTexture2D.AutoSize = true;
            this.radioButtonTexture2D.Location = new System.Drawing.Point(7, 44);
            this.radioButtonTexture2D.Name = "radioButtonTexture2D";
            this.radioButtonTexture2D.Size = new System.Drawing.Size(78, 17);
            this.radioButtonTexture2D.TabIndex = 1;
            this.radioButtonTexture2D.TabStop = true;
            this.radioButtonTexture2D.Text = "Texture 2D";
            this.radioButtonTexture2D.UseVisualStyleBackColor = true;
            this.radioButtonTexture2D.CheckedChanged += new System.EventHandler(this.radioButtonTexture2D_CheckedChanged);
            // 
            // radioButtontexture1D
            // 
            this.radioButtontexture1D.AutoSize = true;
            this.radioButtontexture1D.Location = new System.Drawing.Point(7, 20);
            this.radioButtontexture1D.Name = "radioButtontexture1D";
            this.radioButtontexture1D.Size = new System.Drawing.Size(78, 17);
            this.radioButtontexture1D.TabIndex = 0;
            this.radioButtontexture1D.TabStop = true;
            this.radioButtontexture1D.Text = "Texture 1D";
            this.radioButtontexture1D.UseVisualStyleBackColor = true;
            this.radioButtontexture1D.CheckedChanged += new System.EventHandler(this.radioButtontexture1D_CheckedChanged);
            // 
            // groupBoxDimensions
            // 
            this.groupBoxDimensions.Controls.Add(this.buttonCalculateMipLength);
            this.groupBoxDimensions.Controls.Add(this.labelMipMapLevels);
            this.groupBoxDimensions.Controls.Add(this.textBoxMipMaps);
            this.groupBoxDimensions.Controls.Add(this.labelArrayLayers);
            this.groupBoxDimensions.Controls.Add(this.textBoxArrayLayers);
            this.groupBoxDimensions.Controls.Add(this.textBoxDepth);
            this.groupBoxDimensions.Controls.Add(this.textBoxHeight);
            this.groupBoxDimensions.Controls.Add(this.textBoxWidth);
            this.groupBoxDimensions.Controls.Add(this.labelDepth);
            this.groupBoxDimensions.Controls.Add(this.labelHeight);
            this.groupBoxDimensions.Controls.Add(this.labelWidth);
            this.groupBoxDimensions.Location = new System.Drawing.Point(13, 141);
            this.groupBoxDimensions.Name = "groupBoxDimensions";
            this.groupBoxDimensions.Size = new System.Drawing.Size(290, 106);
            this.groupBoxDimensions.TabIndex = 1;
            this.groupBoxDimensions.TabStop = false;
            this.groupBoxDimensions.Text = "Dimensions";
            // 
            // labelMipMapLevels
            // 
            this.labelMipMapLevels.AutoSize = true;
            this.labelMipMapLevels.Location = new System.Drawing.Point(126, 53);
            this.labelMipMapLevels.Name = "labelMipMapLevels";
            this.labelMipMapLevels.Size = new System.Drawing.Size(82, 13);
            this.labelMipMapLevels.TabIndex = 9;
            this.labelMipMapLevels.Text = "MipMap Levels:";
            // 
            // labelArrayLayers
            // 
            this.labelArrayLayers.AutoSize = true;
            this.labelArrayLayers.Location = new System.Drawing.Point(126, 26);
            this.labelArrayLayers.Name = "labelArrayLayers";
            this.labelArrayLayers.Size = new System.Drawing.Size(68, 13);
            this.labelArrayLayers.TabIndex = 7;
            this.labelArrayLayers.Text = "Array Layers:";
            // 
            // labelDepth
            // 
            this.labelDepth.AutoSize = true;
            this.labelDepth.Location = new System.Drawing.Point(6, 79);
            this.labelDepth.Name = "labelDepth";
            this.labelDepth.Size = new System.Drawing.Size(39, 13);
            this.labelDepth.TabIndex = 2;
            this.labelDepth.Text = "Depth:";
            // 
            // labelHeight
            // 
            this.labelHeight.AutoSize = true;
            this.labelHeight.Location = new System.Drawing.Point(6, 53);
            this.labelHeight.Name = "labelHeight";
            this.labelHeight.Size = new System.Drawing.Size(41, 13);
            this.labelHeight.TabIndex = 1;
            this.labelHeight.Text = "Height:";
            // 
            // labelWidth
            // 
            this.labelWidth.AutoSize = true;
            this.labelWidth.Location = new System.Drawing.Point(6, 26);
            this.labelWidth.Name = "labelWidth";
            this.labelWidth.Size = new System.Drawing.Size(38, 13);
            this.labelWidth.TabIndex = 0;
            this.labelWidth.Text = "Width:";
            // 
            // groupBoxTextureFormat
            // 
            this.groupBoxTextureFormat.Controls.Add(this.comboBoxDataType);
            this.groupBoxTextureFormat.Controls.Add(this.labelDataType);
            this.groupBoxTextureFormat.Controls.Add(this.comboBoxDataFormat);
            this.groupBoxTextureFormat.Controls.Add(this.labelDataFormat);
            this.groupBoxTextureFormat.Controls.Add(this.comboBoxInternalFormat);
            this.groupBoxTextureFormat.Controls.Add(this.labelInternalFormat);
            this.groupBoxTextureFormat.Location = new System.Drawing.Point(13, 254);
            this.groupBoxTextureFormat.Name = "groupBoxTextureFormat";
            this.groupBoxTextureFormat.Size = new System.Drawing.Size(290, 152);
            this.groupBoxTextureFormat.TabIndex = 2;
            this.groupBoxTextureFormat.TabStop = false;
            this.groupBoxTextureFormat.Text = "Format";
            // 
            // comboBoxDataType
            // 
            this.comboBoxDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDataType.FormattingEnabled = true;
            this.comboBoxDataType.Location = new System.Drawing.Point(7, 121);
            this.comboBoxDataType.Name = "comboBoxDataType";
            this.comboBoxDataType.Size = new System.Drawing.Size(275, 21);
            this.comboBoxDataType.TabIndex = 6;
            // 
            // labelDataType
            // 
            this.labelDataType.AutoSize = true;
            this.labelDataType.Location = new System.Drawing.Point(7, 105);
            this.labelDataType.Name = "labelDataType";
            this.labelDataType.Size = new System.Drawing.Size(57, 13);
            this.labelDataType.TabIndex = 5;
            this.labelDataType.Text = "Data Type";
            // 
            // comboBoxDataFormat
            // 
            this.comboBoxDataFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDataFormat.FormattingEnabled = true;
            this.comboBoxDataFormat.Location = new System.Drawing.Point(7, 77);
            this.comboBoxDataFormat.Name = "comboBoxDataFormat";
            this.comboBoxDataFormat.Size = new System.Drawing.Size(275, 21);
            this.comboBoxDataFormat.TabIndex = 4;
            // 
            // labelDataFormat
            // 
            this.labelDataFormat.AutoSize = true;
            this.labelDataFormat.Location = new System.Drawing.Point(7, 60);
            this.labelDataFormat.Name = "labelDataFormat";
            this.labelDataFormat.Size = new System.Drawing.Size(65, 13);
            this.labelDataFormat.TabIndex = 3;
            this.labelDataFormat.Text = "Data Format";
            // 
            // comboBoxInternalFormat
            // 
            this.comboBoxInternalFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxInternalFormat.FormattingEnabled = true;
            this.comboBoxInternalFormat.Location = new System.Drawing.Point(7, 32);
            this.comboBoxInternalFormat.Name = "comboBoxInternalFormat";
            this.comboBoxInternalFormat.Size = new System.Drawing.Size(275, 21);
            this.comboBoxInternalFormat.TabIndex = 2;
            // 
            // labelInternalFormat
            // 
            this.labelInternalFormat.AutoSize = true;
            this.labelInternalFormat.Location = new System.Drawing.Point(4, 16);
            this.labelInternalFormat.Name = "labelInternalFormat";
            this.labelInternalFormat.Size = new System.Drawing.Size(77, 13);
            this.labelInternalFormat.TabIndex = 1;
            this.labelInternalFormat.Text = "Internal Format";
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(12, 412);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 3;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(93, 412);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonCalculateMipLength
            // 
            this.buttonCalculateMipLength.Location = new System.Drawing.Point(129, 74);
            this.buttonCalculateMipLength.Name = "buttonCalculateMipLength";
            this.buttonCalculateMipLength.Size = new System.Drawing.Size(139, 23);
            this.buttonCalculateMipLength.TabIndex = 10;
            this.buttonCalculateMipLength.Text = "Calculate MipMip Levels";
            this.buttonCalculateMipLength.UseVisualStyleBackColor = true;
            this.buttonCalculateMipLength.Click += new System.EventHandler(this.buttonCalculateMipLength_Click);
            // 
            // textBoxMipMaps
            // 
            this.textBoxMipMaps.AllowSpace = false;
            this.textBoxMipMaps.Location = new System.Drawing.Point(214, 53);
            this.textBoxMipMaps.Name = "textBoxMipMaps";
            this.textBoxMipMaps.Size = new System.Drawing.Size(54, 20);
            this.textBoxMipMaps.TabIndex = 8;
            this.textBoxMipMaps.Text = "1";
            // 
            // textBoxArrayLayers
            // 
            this.textBoxArrayLayers.AllowSpace = false;
            this.textBoxArrayLayers.Location = new System.Drawing.Point(214, 23);
            this.textBoxArrayLayers.Name = "textBoxArrayLayers";
            this.textBoxArrayLayers.Size = new System.Drawing.Size(54, 20);
            this.textBoxArrayLayers.TabIndex = 6;
            this.textBoxArrayLayers.Text = "8";
            // 
            // textBoxDepth
            // 
            this.textBoxDepth.AllowSpace = false;
            this.textBoxDepth.Location = new System.Drawing.Point(66, 72);
            this.textBoxDepth.Name = "textBoxDepth";
            this.textBoxDepth.Size = new System.Drawing.Size(54, 20);
            this.textBoxDepth.TabIndex = 5;
            this.textBoxDepth.Text = "256";
            // 
            // textBoxHeight
            // 
            this.textBoxHeight.AllowSpace = false;
            this.textBoxHeight.Location = new System.Drawing.Point(66, 46);
            this.textBoxHeight.Name = "textBoxHeight";
            this.textBoxHeight.Size = new System.Drawing.Size(54, 20);
            this.textBoxHeight.TabIndex = 4;
            this.textBoxHeight.Text = "256";
            // 
            // textBoxWidth
            // 
            this.textBoxWidth.AllowSpace = false;
            this.textBoxWidth.Location = new System.Drawing.Point(66, 19);
            this.textBoxWidth.Name = "textBoxWidth";
            this.textBoxWidth.Size = new System.Drawing.Size(54, 20);
            this.textBoxWidth.TabIndex = 3;
            this.textBoxWidth.Text = "256";
            // 
            // CreateNewTextureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(315, 442);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.groupBoxTextureFormat);
            this.Controls.Add(this.groupBoxDimensions);
            this.Controls.Add(this.groupBoxTextureType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CreateNewTextureForm";
            this.Text = "Create New Texture Form";
            this.groupBoxTextureType.ResumeLayout(false);
            this.groupBoxTextureType.PerformLayout();
            this.groupBoxDimensions.ResumeLayout(false);
            this.groupBoxDimensions.PerformLayout();
            this.groupBoxTextureFormat.ResumeLayout(false);
            this.groupBoxTextureFormat.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxTextureType;
        private System.Windows.Forms.RadioButton radioButtontexture1D;
        private System.Windows.Forms.RadioButton radioButtonTexture2D;
        private System.Windows.Forms.RadioButton radioButtonTextureCubeMap;
        private System.Windows.Forms.RadioButton radioButtonTexture3D;
        private System.Windows.Forms.RadioButton radioButtonTexture2DArray;
        private System.Windows.Forms.RadioButton radioButtonTexture1DArray;
        private System.Windows.Forms.RadioButton radioButtonTextureCubeMapArray;
        private System.Windows.Forms.GroupBox groupBoxDimensions;
        private System.Windows.Forms.Label labelDepth;
        private System.Windows.Forms.Label labelHeight;
        private System.Windows.Forms.Label labelWidth;
        private System.Windows.Forms.Label labelMipMapLevels;
        private System.Windows.Forms.Label labelArrayLayers;
        private System.Windows.Forms.GroupBox groupBoxTextureFormat;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private NumericTextBox textBoxMipMaps;
        private NumericTextBox textBoxArrayLayers;
        private NumericTextBox textBoxDepth;
        private NumericTextBox textBoxHeight;
        private NumericTextBox textBoxWidth;
        private System.Windows.Forms.ComboBox comboBoxInternalFormat;
        private System.Windows.Forms.Label labelInternalFormat;
        private System.Windows.Forms.ComboBox comboBoxDataFormat;
        private System.Windows.Forms.Label labelDataFormat;
        private System.Windows.Forms.ComboBox comboBoxDataType;
        private System.Windows.Forms.Label labelDataType;
        private System.Windows.Forms.Button buttonCalculateMipLength;
    }
}