namespace DiceThrows
{
    partial class MainForm
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
            this.btn_close = new System.Windows.Forms.Button();
            this.btn_create_dices = new System.Windows.Forms.Button();
            this.btn_roll_dices = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_black_dice_ace_counter = new System.Windows.Forms.Label();
            this.lbl_white_dice_ace_counter = new System.Windows.Forms.Label();
            this.btn_cancel_roll = new System.Windows.Forms.Button();
            this.btn_stop_roll = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbl_black_dice_throws_counter = new System.Windows.Forms.Label();
            this.lbl_white_dice_throws_counter = new System.Windows.Forms.Label();
            this.btn_inject_rollResults = new System.Windows.Forms.Button();
            this.btn_store_rollResults = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(411, 443);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(75, 23);
            this.btn_close.TabIndex = 0;
            this.btn_close.Text = "Close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // btn_create_dices
            // 
            this.btn_create_dices.Location = new System.Drawing.Point(40, 40);
            this.btn_create_dices.Name = "btn_create_dices";
            this.btn_create_dices.Size = new System.Drawing.Size(75, 23);
            this.btn_create_dices.TabIndex = 1;
            this.btn_create_dices.Text = "Create dices";
            this.btn_create_dices.UseVisualStyleBackColor = true;
            this.btn_create_dices.Click += new System.EventHandler(this.btn_create_dices_Click);
            // 
            // btn_roll_dices
            // 
            this.btn_roll_dices.Location = new System.Drawing.Point(151, 40);
            this.btn_roll_dices.Name = "btn_roll_dices";
            this.btn_roll_dices.Size = new System.Drawing.Size(75, 23);
            this.btn_roll_dices.TabIndex = 2;
            this.btn_roll_dices.Text = "Roll dices";
            this.btn_roll_dices.UseVisualStyleBackColor = true;
            this.btn_roll_dices.Click += new System.EventHandler(this.btn_roll_dices_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(37, 174);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Black dice";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(240, 174);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "White dice";
            // 
            // lbl_black_dice_ace_counter
            // 
            this.lbl_black_dice_ace_counter.AutoSize = true;
            this.lbl_black_dice_ace_counter.Font = new System.Drawing.Font("Agency FB", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_black_dice_ace_counter.Location = new System.Drawing.Point(144, 192);
            this.lbl_black_dice_ace_counter.Name = "lbl_black_dice_ace_counter";
            this.lbl_black_dice_ace_counter.Size = new System.Drawing.Size(34, 42);
            this.lbl_black_dice_ace_counter.TabIndex = 5;
            this.lbl_black_dice_ace_counter.Text = "0";
            // 
            // lbl_white_dice_ace_counter
            // 
            this.lbl_white_dice_ace_counter.AutoSize = true;
            this.lbl_white_dice_ace_counter.Font = new System.Drawing.Font("Agency FB", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_white_dice_ace_counter.Location = new System.Drawing.Point(344, 192);
            this.lbl_white_dice_ace_counter.Name = "lbl_white_dice_ace_counter";
            this.lbl_white_dice_ace_counter.Size = new System.Drawing.Size(34, 42);
            this.lbl_white_dice_ace_counter.TabIndex = 6;
            this.lbl_white_dice_ace_counter.Text = "0";
            // 
            // btn_cancel_roll
            // 
            this.btn_cancel_roll.Location = new System.Drawing.Point(368, 40);
            this.btn_cancel_roll.Name = "btn_cancel_roll";
            this.btn_cancel_roll.Size = new System.Drawing.Size(75, 40);
            this.btn_cancel_roll.TabIndex = 7;
            this.btn_cancel_roll.Text = "Cancel Roll and rollback";
            this.btn_cancel_roll.UseVisualStyleBackColor = true;
            this.btn_cancel_roll.Click += new System.EventHandler(this.btn_cancel_roll_Click);
            // 
            // btn_stop_roll
            // 
            this.btn_stop_roll.Location = new System.Drawing.Point(264, 40);
            this.btn_stop_roll.Name = "btn_stop_roll";
            this.btn_stop_roll.Size = new System.Drawing.Size(75, 40);
            this.btn_stop_roll.TabIndex = 8;
            this.btn_stop_roll.Text = "Stop Roll and commit";
            this.btn_stop_roll.UseVisualStyleBackColor = true;
            this.btn_stop_roll.Click += new System.EventHandler(this.btn_stop_roll_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(37, 207);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "Aces #";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(37, 257);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "Throws #";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(240, 257);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 16);
            this.label5.TabIndex = 12;
            this.label5.Text = "Throws #";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(240, 207);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 16);
            this.label6.TabIndex = 11;
            this.label6.Text = "Aces #";
            // 
            // lbl_black_dice_throws_counter
            // 
            this.lbl_black_dice_throws_counter.AutoSize = true;
            this.lbl_black_dice_throws_counter.Font = new System.Drawing.Font("Agency FB", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_black_dice_throws_counter.Location = new System.Drawing.Point(144, 239);
            this.lbl_black_dice_throws_counter.Name = "lbl_black_dice_throws_counter";
            this.lbl_black_dice_throws_counter.Size = new System.Drawing.Size(34, 42);
            this.lbl_black_dice_throws_counter.TabIndex = 13;
            this.lbl_black_dice_throws_counter.Text = "0";
            // 
            // lbl_white_dice_throws_counter
            // 
            this.lbl_white_dice_throws_counter.AutoSize = true;
            this.lbl_white_dice_throws_counter.Font = new System.Drawing.Font("Agency FB", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_white_dice_throws_counter.Location = new System.Drawing.Point(344, 239);
            this.lbl_white_dice_throws_counter.Name = "lbl_white_dice_throws_counter";
            this.lbl_white_dice_throws_counter.Size = new System.Drawing.Size(34, 42);
            this.lbl_white_dice_throws_counter.TabIndex = 14;
            this.lbl_white_dice_throws_counter.Text = "0";
            // 
            // btn_inject_rollResults
            // 
            this.btn_inject_rollResults.Location = new System.Drawing.Point(40, 88);
            this.btn_inject_rollResults.Name = "btn_inject_rollResults";
            this.btn_inject_rollResults.Size = new System.Drawing.Size(122, 23);
            this.btn_inject_rollResults.TabIndex = 15;
            this.btn_inject_rollResults.Text = "Inject roll results to DB";
            this.btn_inject_rollResults.UseVisualStyleBackColor = true;
            this.btn_inject_rollResults.Click += new System.EventHandler(this.btn_inject_rollResults_Click);
            // 
            // btn_store_rollResults
            // 
            this.btn_store_rollResults.Location = new System.Drawing.Point(40, 127);
            this.btn_store_rollResults.Name = "btn_store_rollResults";
            this.btn_store_rollResults.Size = new System.Drawing.Size(122, 23);
            this.btn_store_rollResults.TabIndex = 16;
            this.btn_store_rollResults.Text = "Store roll results to file";
            this.btn_store_rollResults.UseVisualStyleBackColor = true;
            this.btn_store_rollResults.Click += new System.EventHandler(this.btn_store_rollResults_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 478);
            this.Controls.Add(this.btn_store_rollResults);
            this.Controls.Add(this.btn_inject_rollResults);
            this.Controls.Add(this.lbl_white_dice_throws_counter);
            this.Controls.Add(this.lbl_black_dice_throws_counter);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_stop_roll);
            this.Controls.Add(this.btn_cancel_roll);
            this.Controls.Add(this.lbl_white_dice_ace_counter);
            this.Controls.Add(this.lbl_black_dice_ace_counter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_roll_dices);
            this.Controls.Add(this.btn_create_dices);
            this.Controls.Add(this.btn_close);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Button btn_create_dices;
        private System.Windows.Forms.Button btn_roll_dices;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_black_dice_ace_counter;
        private System.Windows.Forms.Label lbl_white_dice_ace_counter;
        private System.Windows.Forms.Button btn_cancel_roll;
        private System.Windows.Forms.Button btn_stop_roll;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbl_black_dice_throws_counter;
        private System.Windows.Forms.Label lbl_white_dice_throws_counter;
        private System.Windows.Forms.Button btn_inject_rollResults;
        private System.Windows.Forms.Button btn_store_rollResults;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

